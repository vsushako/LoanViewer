using System.Collections.Generic;
using System.Web.Configuration;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using LoansViewer.Api;
using LoansViewer.DAO;
using LoansViewer.Domain;

namespace LoansViewer
{
    public class Bootstrapper
    {
        private static IContainer _container;

        public static T Resolve<T>()
        {

            return _container.Resolve<T>();
        }

        public static void Register()
        {
            var builder = new ContainerBuilder();

            var connectionString = WebConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            builder
                .RegisterGeneric(typeof(MongoDbDao<>))
                .As(typeof(IMongoDao<>))
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<BpmHttpProvider>()
                .As<IHttpProvider>();

            builder.RegisterType<BpmRequest>()
                .As<IApiRequest>()
                .WithParameter("url", WebConfigurationManager.AppSettings.Get("BpmLoanUrl"));

            builder.RegisterType<AuthentificationService>()
                .As<IAuthentificationService>()
                .WithParameters(new[] {
                    new NamedParameter("url", WebConfigurationManager.AppSettings.Get("BpmAuthUrl")),
                    new NamedParameter("userName", WebConfigurationManager.AppSettings.Get("BpmUserName")),
                    new NamedParameter("userPassword", WebConfigurationManager.AppSettings.Get("BpmUserPassword"))
                });

            builder.RegisterType<Loan>()
                .As<ILoan>();

            _container = builder.Build();
        }
    }
}