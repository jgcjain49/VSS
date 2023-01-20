<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" CodeBehind="Default.aspx.cs" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <link href="css/style.css" rel='stylesheet' type='text/css' />
    <link href="css/default.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="AdminExContent/fonts/css/font-awesome.min.css" /> 
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <!-- Jquery Library -->
    <script src="js/websitejs/jquery.balloon.js" type="text/javascript"></script>
    <!-- Tooltip library -->
    <script src="js/websitejs/utilityfunctions.js" type="text/javascript"></script>
    <!-- Utility functions library -->
    <script src="js/pagesjs/default.js" type="text/javascript"></script>
    <!-- Javascript for this page. -->
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!--webfonts-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800' rel='stylesheet' type='text.css' />
    <!--//webfonts-->
    <title>Comm Trex Login</title>
    <script type="text/javascript">
        function noBack() { window.history.forward() }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
        //this js for do not copy past password txtbox
        $(function () {
            $('.nocopy').bind('copy', function (e) {
                e.preventDefault();
            });
            $('.nopaste').bind('paste', function (e) {
                e.preventDefault();
            });
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("#txtPassword").attr("type", type);
            });
        });
    </script>

</head>
<body>
    <div class="main">
        <form id="loginpage" runat="server">
            <%--<h1><span>CommTrex</span>    <label> Login </label> </h1>--%>
            <h1><span>
                <img style="width: 291px;" src="images/Logo_login.png" /><br />
                Admin Login</span></h1>
            <div class="inset">
                <p>
                    <label for="KEY">COMPANY KEY</label>

                    <asp:TextBox runat="server" ID="txtKey" placeholder="XXXX-XXXX" CssClass="inputs" MaxLength="9">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="validator" ID="requiredKey" ControlToValidate="txtKey" runat="server" ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                </p>
                <p>
                    <label for="email">LOGIN ID</label>
                    <asp:TextBox runat="server" ID="txtId" placeholder="Login ID" CssClass="inputs"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="validator" ID="requiredId" ControlToValidate="txtId" runat="server" ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                </p>
                <p>
                    <label for="password">PASSWORD</label>
                    <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" CssClass="inputs" MaxLength="36" TextMode="Password" AutoComplete="New-Password"
                        onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false"></asp:TextBox>
                    <span id="toggle_pwd" class="fa fa-eye field_icon"></span>
                    <asp:RequiredFieldValidator CssClass="validator" ID="requiredPass" ControlToValidate="txtPassword" runat="server" ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                </p>
                <p>
                    <label for="ddlRoleList">ROLE</label>
                    <asp:DropDownList ID="ddlRoleList" class="drpinputs custom-select" runat="server">
                        <asp:ListItem>ADMIN</asp:ListItem>
                        <%--<asp:ListItem>MD</asp:ListItem>
                        <asp:ListItem>Distributor</asp:ListItem>--%>
                    </asp:DropDownList>
                </p>

                <p>
                    <asp:CheckBox runat="server" name="remember" ID="remember" />
                    <label for="remember">Remember me on this computer</label>
                </p>
                <p class="error" runat="server" style="display: none;" id="error">
                    <asp:Label runat="server" ID="lblError" Text="Invalid login values."></asp:Label>
                </p>
            </div>

            <%--<p class="p-container">
                <span><a href="#">Forgot password ?</a></span>
                <span style="float:right"><a href="#">Register Now</a></span>
            </p>--%>
            <div style="padding-bottom: 10px">
                <p class="p-container" style="text-align: center;">
                    <asp:Button CssClass="btn" runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click" />
                    <br />
                </p>
            </div>

            <%--//Added to display sharelocation--%>
            <div class="bottom-location-popup" style="display: none">
                <table>
                    <tr style="background-color: rgba(255,255,255,0.8);">
                        <td colspan="2" class="header lbl-caption">Your Location<span class="close-button">X</span></td>
                    </tr>
                    <tr>
                        <td class="lbl-caption">IP Address</td>
                        <td class="lbl-value"><span id="ip-addr"></span></td>
                    </tr>
                    <tr>
                        <td class="lbl-caption">Host</td>
                        <td class="lbl-value"><span id="host"></span></td>
                    </tr>
                    <%--<tr>
                        <td class="lbl-caption">Domain</td>
                        <td class="lbl-value"><span id="domain"></span></td>
                    </tr>--%>
                    <tr>
                        <td class="lbl-caption">Address</td>
                        <td class="lbl-value"><span id="address"><%--Mumbai, Maharashtra, India--%></span></td>
                    </tr>
                    <%--<tr>
                        <td class="lbl-caption">Lat,Long</td>
                        <td class="lbl-value"><span id="geo-loc"></span></td>
                    </tr>
                    <tr>
                        <td class="lbl-caption">Browser</td>
                        <td class="lbl-value"><span id="user-agent"></span></td>
                    </tr>--%>
                </table>
            </div>

            <%--// Added to keep location trace--%>
            <asp:HiddenField runat="server" ID="locationJson" Value="" />

        </form>
    </div>
    <!-----start-copyright---->

    <!-----//end-copyright---->
</body>
</html>
