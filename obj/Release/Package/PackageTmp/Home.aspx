<%@ Page Title="Comm Trex Admin" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VTalk_WebApp.Home" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
</asp:Content>
<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading pt">
        <h3>Comm Trex Home</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Home</a>
            </li>
            <li class="active">Comm Trex </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading" style="display: none">
                Company Details
                        <span class="tools pull-right">
                            <a href="javascript:;" class="fa fa-chevron-up"></a>
                            <span class="collapsible-server-hidden">
                                <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="o" />
                            </span>
                        </span>
            </header>

            <div class="panel-body">
                <div class="form-horizontal adminex-form" style="display: none">
                    <div class="form-group">
                        <label class="col-sm-2 col-sm-2 control-label">Company Name </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtCompanyName" name="txtCompanyName" runat="server" AutoPostBack="false" Visible="true"
                                CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" MaxLength="80" data-original-title="Company Name"></asp:TextBox>
                        </div>
                    </div>

                    <%--<div class="form-group">
                        <label class="col-sm-2 col-sm-2 control-label">Company Address</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtAddress" name="txtAddress" runat="server" AutoPostBack="false" Visible="true"
                                CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" MaxLength="80" data-original-title="Address"></asp:TextBox>
                        </div>
                    </div>--%>

                    <div class="form-group">
                        <label class="col-sm-2 col-sm-2 control-label">Select Support Center</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="cmbTaluka" runat="server" Font-Bold="True" AutoPostBack="True" Style="display: none"
                                CssClass="form-control m-bot15">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <%--   <div class="row">
                    <div class="col-lg-12">
                        <section class="panel">
                            <div class="panel-body" style="text-align: center">
                                <button class="btn btn-success" type="button"
                                    onserverclick="btnLogin_ServerClick" runat="server" id="btnLogin">
                                    PROCEED FURTHER
                                    <i class="fa fa-arrow-right"></i>
                                </button>
                                <button class="btn btn-info" type="button"
                                    onserverclick="btnCancel_ServerClick" runat="server" id="btnCancel">
                                    CANCEL <i class="fa fa-times"></i>
                                </button>
                            </div>
                            <div class="panel-body">
                                <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                    Select Support Center to Continue..
                                </div>
                            </div>
                        </section>
                        <!-- //panel -->
                    </div>
                    <!-- //Grid 12 -->
                </div>--%>

                <div class="row">
                    <div class="col-lg-12" style="text-align: center">
                        <section class="panel">
                            <%--<asp:Chart ID="Chart1" runat="server" Width="1000px" Height="400px" >
                                <Series>
                                    <asp:Series Name="Series1" XValueMember="1" YValueMembers="2" IsValueShownAsLabel="true">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX Title="Dates" LineColor="Gray">
                                            <MajorGrid Enabled="false" />
                                           
                                        </AxisX>
                                        <AxisY Title="Commission in Rupees " LineColor="Gray">
                                            <MajorGrid LineColor="LightGray" />
                                        </AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>--%>
                        </section>
                    </div>
                </div>


            </div>
        </section>
        <%--//Section--%>
    </div>
    <%--//panel body--%>
</asp:Content>
<asp:Content ID="contChild_PageLevelScripts" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    <!-- No Scripts -->
    <script src="js/pagesjs/Home.aspx.js"></script>
</asp:Content>
