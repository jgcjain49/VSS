<%@ Page Title="" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="OrderTracking.aspx.cs" Inherits="Admin_CommTrex.OrderTracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading pt">
        <h3>Order Tracking</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Order Tracking</li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <div class="form-group">
        <div class="form-horizontal adminex-form">
            <section class="panel">

                <div class="panel-body">
                    <header class="panel-heading">Order Tracking Filters</header>
                    <div class="form-group">
                        
                            <label class="col-sm-2 control-label">Order Number </label>
                           <div class="col-sm-4 control-label"><input type="text" class="text form-control" id="txtorderNumber" runat="server" /></div>
                            <div class="col-sm-4 control-label"><button class="btn gbtn" type="button" runat="server" onserverclick="btnSearch_ServerClick" id="btnSearch" title="Search" style="width: 100px;">
                                    Search&nbsp;&nbsp;&nbsp;<i class="fa fa-search"></i></button></div>
                        </div>

 
                        <%--<tr>
                            <td style="padding-right: 15px;">
                                <label class="control-label">
                                    Order Date (From)
                                </label>
                            </td>
                            <td style="padding-right: 30px;">
                                <input type="date" id="ordPlcDt" runat="server" onchange="ordPlcDtSelected(event);" />
                            </td>
                            <td style="padding-right: 15px;">
                                <label class="control-label">
                                    Order Date (Till)
                                </label>
                            </td>
                            <td style="padding-right: 30px;">
                                <input type="date" id="ordCnclDt" runat="server" disabled="disabled"/>
                            </td>
                            <td>
                                <button class="btn btn-success" type="button" runat="server" onserverclick="btnReset_ServerClick" id="btnReset" title="Reset" style="width: 100px;">
                                    Clear&nbsp;&nbsp;&nbsp;<i class="fa fa-undo"></i>
                                </button>
                            </td>
                        </tr>--%>
                        <%--<tr style="margin-top: 15px;">
                            <td style="padding-right: 15px;">
                                <label class="control-label">
                                    Status
                                </label>
                            </td>
                            <td style="padding-right: 30px;">
                                <asp:DropDownList runat="server" ID="ddlStatus" Style="height: 34px;">
                                    <asp:ListItem Text="--Please Select --" />
                                    <asp:ListItem Text="Success" />
                                    <asp:ListItem Text="Failure" />
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                </div>

                <div class="alert toss" runat="server" id="updateActionDiv" style="display: block;"></div>


                 <div class="form-group">
                   <div class="form-horizontal adminex-form">
                     <section class="panel">
                      <div class="panel-body" style="padding:0px 40px;">

                        <div class="form-group">
                           <label class="col-sm-2 control-label">Courier Company </label>
                           <div class="col-sm-10 control-label">
                                <asp:dropdownlist id="ddlCourierCompany" runat="server" class="form-control">
                                 <asp:ListItem Text="--Please Select--"/>
                                 <asp:ListItem Text="DHL"/>
                                 <asp:ListItem Text="FEDEX"/>
                                 <asp:ListItem Text="TNT"/>
                             </asp:dropdownlist> </div>
                        </div>


                        <div class="form-group">
                            <label class="col-sm-2 control-label">Status 1</label>
                            <div class="col-sm-4"><input type="text" runat="server" name="txtStatus1" id="txtStatus1" value=" " class="form-control" /></div>
                            <label class="col-sm-2 control-label">Status 2</label>
                            <div class="col-sm-4"><input type="text" runat="server" name="txtStatus2" id="txtStatus2" value=" " class="form-control" /></div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Status 3</label>
                            <div class="col-sm-4"><input type="text" runat="server" name="txtStatus3" id="txtStatus3" value=" " class="form-control"/></div>
                            <label class="col-sm-2 control-label">Status 4</label>
                            <div class="col-sm-4"><input type="text" runat="server" name="txtStatus4" id="txtStatus4" value=" " class="form-control"/></div>
                        </div>

                        <div class="col-sm-12" style="text-align:center;">
                            <button class="btn gbtn" type="button" runat="server" onserverclick="btnSave_ServerClick" id="btnSave" title="Save" >
                                Save&nbsp;&nbsp;&nbsp;<i class="fa fa-save"></i></button>
                        </div>


                    </div>
                         </section>
                       </div>
                </div>

                <!-- confirmation for Accept-->
                <%--<div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="confirmAcceptOrder" class="modal fade">
                        <div class="modal-dialog" style="width: fit-content;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">Proceed Confirmation</h4>
                                </div>
                                <div class="modal-body">

                                    <div role="form">
                                        <div class="form-group">
                                            <h3>Do you really want to proceed?</h3>
                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="panel-body" style="text-align: center">
                                                    <button class="btn btn-success" type="button" onserverclick="btnCnfrmYes_ServerClick"
                                                        runat="server" id="btnCnfrmYes">
                                                        Yes <i class="fa fa-check-circle"></i>
                                                    </button>
                                                    &nbsp;&nbsp;&nbsp;<!-- for space between btns -->
                                                    <button class="btn btn-danger" type="button" data-dismiss="modal"
                                                        id="btnCncl">
                                                        Cancel <i class="fa fa-times-circle"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                <!-- // END confirmation for Accept -->
            </section>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
</asp:Content>
