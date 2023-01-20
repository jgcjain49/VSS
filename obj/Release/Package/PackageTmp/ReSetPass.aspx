<%@ Page Title="" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="ReSetPass.aspx.cs" Inherits="Admin_CommTrex.ReSetPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading pt">
        <h3>ReSet Password</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Home</a>
            </li>
            <li class="active">ReSet PassWord </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    

     <div class="tab-pane" id="modifyUser">
        <div class="form-group">
            <div class="alert toss" runat="server" id="updateActionDiv">
                Enter credentials to CHANGE the password.
            </div>
            <section class="panel">
            <div class="panel-body">
                <div class="form-horizontal adminex-form" style="text-align: center">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Current Password
                            <span style="color: red">*</span>
                        </label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtCurrPass" name="txtCurrPass" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                data-toggle="tooltip" title="" TextMode="Password" placeholder="Enter your Current Password" data-original-title="Enter your Current Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            New Password
                            <span style="color: red">*</span>
                        </label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtEnterPass" name="txtEnterPass" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                data-toggle="tooltip" title="" TextMode="Password" placeholder="Enter New Password" data-original-title="Enter New Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            confirm New Password 
                            <span style="color: red">*</span>
                        </label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtEnterCnfPass" name="txtEnterCnfPass" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                data-toggle="tooltip" title="" placeholder="Enter Password to confirm" data-original-title="Enter Password to confirm"></asp:TextBox>
                        </div>
                    </div>
                    <%--onserverclick="btnFetchUserReportClick"--%>
                    <div class="panel-body" style="text-align: center">
                        <button class="btn gbtn" type="button"
                            runat="server" id="btnPassChnge" onserverclick="btnPassChangeClick">
                            PROCEED FURTHER
                                    <i class="fa fa-arrow-right"></i>
                        </button>
                    </div>
                </div>
            </div>
                </section> 
        </div>
    </div>
        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
</asp:Content>
