<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LoansViewer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManager ID="sm" EnablePageMethods="true" runat="server"/>--%>

    <style>
        .form-inline textarea.form-control {
            max-width: 100%;
            width: 100%;
            height: 100%;
        }

        .center-block {
            width: 700px;
            max-width: 100%;
            margin-top: 50px;

        }

        body {
            height: 100vh;
        }

        .body_form {
            height: 100%;
        }

        .container.body-content {
            height: calc(100% - 50px);
        }

        .form-inline {
            margin-top: 15px;
        }

        .h-100 {
            height: 100%;
        }

        .h-50 {
            height: 50%;
        }

        .h-80 {
            height: 80%;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#getLoans").click(function () {
                var phone = $("#tel").val();
                if (!/^\+639[0-9]{9}$/.test(phone)) {
                    alert("Wrong number");
                    return;
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "<%= ResolveUrl("Default.aspx/GetLoans") %>",
                    data: JSON.stringify({ "phone": phone }),
                    datatype: "json",
                    success: function (result) {
                        $("#loans").text(JSON.stringify(JSON.parse(result.d), null, 2));
                    },
                    error: function (xmlhttprequest, textstatus, errorthrown) {
                        alert(" conection to the server failed ");
                        console.log("error: " + errorthrown);
                    }
                });
            });
        })
    </script>
    <div class="center-block h-80">
        <div class="form-inline">
            <div class="form-group">
                <label for="tel">Phone number</label>
                <input type="tel" class="form-control" id="tel" aria-describedby="emailHelp" placeholder="+639000000000">
            </div>
            <button type="button" id="getLoans" class="btn btn-primary mb-2">Get loans</button>
        </div>

        <div class="form-inline h-50">
            <label for="loans">Loans</label>
            <textarea class="form-control h-100" id="loans"></textarea>
        </div>
    </div>
</asp:Content>
