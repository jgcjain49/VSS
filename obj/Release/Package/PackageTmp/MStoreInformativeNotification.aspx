<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/AdminEx.Master"  MaintainScrollPositionOnPostback="true" CodeBehind="MStoreInformativeNotification.aspx.cs" Inherits="Admin_CommTrex.MStoreInformativeNotification" %>

<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    
    <!-- For grid stylings as dynamic table-->
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />

    <!-- For checkbox stylings -->
    <link href="AdminExContent/js/iCheck/skins/flat/flat.css" rel="stylesheet" />
    <link href="AdminExContent/js/iCheck/skins/flat/blue.css" rel="stylesheet" />

    <link href="AdminExContent/js/iCheck/skins/minimal/minimal.css" rel="stylesheet" />
    <link href="AdminExContent/js/iCheck/skins/minimal/blue.css" rel="stylesheet" />
    
    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />

    <style>
        /*Small css for this page only*/
        /*.prerview-img-thumb,.fileupload-preview {
            cursor:pointer;
        }
        
        .modal .modal-dialog { width: 800px; }*/
    </style>

</asp:Content>
<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>New Notifications</h3>
        <ul class="breadcrumb">
                <li>
                    <a href="#"> Announcement </a>
                </li>
                <li class="active"> Notifications </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <%--<form runat="server">--%>
            <asp:HiddenField ID="activeTab" EnableViewState ="true" runat="server" value="addproducts"/>
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading custom-tab ">
                    <ul class="nav nav-tabs">
                        <li class="active" id="tab_addproducts">
                            <a href="#addNotify" data-toggle="tab">Send Notifications</a>
                        </li>
                        <li class="" id="tab_modifyproducts">
                            <a href="#modifyNotify" data-toggle="tab">View Notifications</a>
                        </li>
                    </ul>
                </header>


              <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane active" id="addNotify">
        <!-- adds notification panel -->
    <div class="row">
        <div class="col-sm-12">
            <section class="panel">
                <header class="panel-heading">
                    Notification from selected information
                            <span class="tools pull-right">
                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                <span class="collapsible-server-hidden">
                                    <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="o" />
                                </span>
                            </span>
                </header>
                <div class="panel-body">
                    <div class="adv-table" style="visibility: hidden">
                        <asp:GridView ID="grdInformation" runat="server" AutoGenerateColumns="false"
                            RowStyle-CssClass="gradeA"
                            data-sorting="2,1" data-add-printing-function="true"
                            class="dynamic-table-grid display table table-bordered table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="center iCheck-Radio-Grid-Button">
                                    <ItemTemplate>
                                        <div class="flat-blue">
                                            <div class="radio">
                                                <asp:CheckBox ID="cbSelectInformation" runat="server" />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Name" FooterText="Name" DataField="IM_vCharInfoName_En" />
                                <asp:BoundField HeaderText="Address" FooterText="Address" DataField="IM_vCharInfoAdd_En" />
                                <asp:BoundField HeaderText="Category" FooterText="" DataField="CM_vCharName_En" />
                                <asp:BoundField HeaderText="Sub Category" FooterText="Sub Category" DataField="SCM_vCharName_En" />
                                <asp:BoundField DataField="IM_bIntInfoId" HeaderStyle-CssClass="col-hidden-to-clnt" />
                                <asp:BoundField DataField="IIG_vCharImagePath" HeaderStyle-CssClass="col-hidden-to-clnt" />
                                <asp:BoundField DataField="IM_bIntCatId" HeaderStyle-CssClass="col-hidden-to-clnt" />
                                <asp:BoundField DataField="IM_bIntSubCatId" HeaderStyle-CssClass="col-hidden-to-clnt" />
                                <asp:BoundField DataField="IM_iNtInfoType" HeaderStyle-CssClass="col-hidden-to-clnt" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <!--//adv table -->
                </div>
                <!--//panel-body -->
            </section>
            <!--//panel -->
        </div>
        <!--//col-sm-12 -->
    </div> <!--//row -->
    <div class="row">
        <div class="col-sm-12">
            <section class="panel">
                <header class="panel-heading">
                    Notification details
                            <span class="tools pull-right">
                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                <span class="collapsible-server-hidden">
                                    <asp:HiddenField runat="server" ID="HiddenField2" EnableViewState="true" Value="o" />
                                </span>
                            </span>
                </header>
                <div class="panel-body">
                    <div class="form-horizontal adminex-form">
                        <%--<div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">Notification Message </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtMessage" name="txtMessage" runat="server" CssClass="form-control tooltips" data-trigger="hover" 
                                    data-toggle="tooltip" title="" placeholder="Message to be shown in notification panel" data-original-title="Message to be shown in notification panel only for display in panel"></asp:TextBox>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">Notification Title </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtNotificationTitle" name="txtNotificationTitle" runat="server" CssClass="form-control tooltips" data-trigger="hover" 
                                    data-toggle="tooltip" title="" placeholder="Title of notification" data-original-title="Title of notification shown in app screen"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">Notification Text </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtNotificationText" name="txtNotificationText" runat="server" CssClass="form-control tooltips" data-trigger="hover" 
                                    TextMode="MultiLine" Columns="5" Rows="3"
                                    data-toggle="tooltip" title="" placeholder="Actual text of notification" data-original-title="Actual text of notification shown in app screen"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            
                                <label class="control-label col-sm-2">Notification Image</label>
                                <div class="col-sm-10">
                                    <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Notification Main Image">
                                        <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 300px; height: 250px;">
                                            <asp:Image ID="MainImage" runat="server" AlternateText="No Image" />
                                        </div>
                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 300px; max-height: 250px; line-height: 20px;"></div>
                                        <div>
                                            <span class="btn btn-default btn-file">
                                                <span class="fileupload-new"><i class="fa fa-paperclip"></i> Select image</span>
                                                <span class="fileupload-exists"><i class="fa fa-undo"></i> Change</span>
                                                <asp:HiddenField ID="txtNotificationImagePath" runat="server"></asp:HiddenField>
                                                <asp:FileUpload ID="FileNotificationImage" runat="server" class="default" />
                                            </span>
                                            <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                        </div>
                                    </div>
                                    <!-- //file upload-->
                                </div>
                                <!-- //col-md-9-->
                        </div><!-- //form-group -->
                    </div>
                </div>
                <!--//panel-body -->
            </section>
            <!--//panel -->
        </div> <!--//col-sm-12 -->
            </div> <!--//row -->
    
<%--     <div class="row">
        <div class="col-lg-6">
            <section class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-6" style="width: 20%;">
                            <button class="btn btn-success" type="button" data-toggle="modal" data-target="#modSendingMessage"
                                runat="server" id="btnSendNotification" onserverclick="btnSendNotification_ServerClick">
                                Send Now <i class="fa fa-arrow-circle-right"></i>
                            </button>
                        </div>
                        <div class="col-xs-6" style="width: 80%;">
                            <div class="alert alert-info" style="padding: 8px;" runat="server" id="progInfo">
                                No action taken yet.
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>--%>

                            <div class="row">
                                <div class="col-lg-12">
                                    <section class="panel">
                                        <div class="panel-body" style="text-align:center">
                                           <button class="btn btn-success" type="button" data-toggle="modal" data-target="#modSendingMessage"
                                               runat="server" id="btnSendNotification" onserverclick="btnSendNotification_ServerClick">
                                               Send Now <i class="fa fa-arrow-circle-right"></i>
                                           </button>
                                        </div>
                                        <div class="panel-body">
                                            <div class="alert alert-info" style="padding: 8px;" runat="server" id="progInfo">
                                                 No action taken yet.
                                            </div>
                                        </div>
                                    </section> <!-- //panel -->
                                </div><!-- //Grid 12 -->
                            </div><!--//row buttons -->
                    </div> 
        <%--End of Add Notifications--%>

                        
       <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modSendingMessage" class="modal fade" style="width:100%;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Notifying selected user(s)</h4>
                    </div>
                    <div class="modal-body">
                        Please wait for few minutes...
                                        <br />
                        <div style="text-align: center">
                            <img src="images/loading-blue.gif" alt="Please wait" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--View Notifications--%>
                 <div class="tab-pane" id="modifyNotify">

                                 <!-- Notifications gridview -->
                  <div class="form-group">
                       <div class="alert alert-info" runat="server" id="updateActionDiv">
                             Notifications Report
                       </div>
                  </div>
                  <div class="adv-table nice-scroll-grid">
                        <asp:GridView ID="grdViewNotifications" runat="server" AutoGenerateColumns="false"
                            RowStyle-CssClass="gradeA"
                            class="dynamic-table-grid display table table-bordered table-striped">
                            <Columns>
                                <asp:BoundField HeaderText="ID"  DataField="NM_bIntId" />
                                <asp:BoundField HeaderText="INFORMATION"  DataField="IM_vCharInfoName_En" />
                                <asp:BoundField HeaderText="TITLE"  DataField="NM_nVarrNotiTitle_Reg" />
                                <asp:BoundField HeaderText="DESCRIPTION"  DataField="NM_nVarNotiText_Reg" />
                                <asp:TemplateField HeaderText="IMAGE" HeaderStyle-CssClass="nosort">
                                      <ItemTemplate>
                                         <asp:Image ID="MyImage" Runat="Server" AlternateText=" " ImageUrl='<%# Eval("NM_vCharImagePath") %>' Height="50" Width="50" visible="true"/>
                                       </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="TOTAL COUNT"   DataField="NM_iNtTotalUsers"  />
                                <asp:BoundField HeaderText="SEND COUNT"   DataField="NM_iNtTotalSentCnt"  />
                                <asp:BoundField HeaderText="FAILED COUNT"   DataField="NM_iNtTotalFailedCnt"  />
                                <asp:BoundField HeaderText="NOTIFICATION DATE"   DataField="NM_dtSendDate"  />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <!--//adv table -->

                     </div>
                        <%--End of View Notifications--%>
                </div> <%-- end of tab content--%>
            </div><%-- end of panel--%>
       </section>
  </div>



    <%--</form>--%>
</asp:Content>
<asp:Content ID="contChild_PageLevelScripts" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    
    <!--icheck -->
    <script src="AdminExContent/js/iCheck/jquery.icheck.js"></script>
    <script src="AdminExContent/js/icheck-init.js"></script>

    <!--dynamic table-->
    <script type="text/javascript" src="AdminExContent/js/advanced-datatable/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="AdminExContent/js/data-tables/DT_bootstrap.js"></script>

    <!--file upload-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-fileupload.min.js"></script>
    <!--tags input-->
    <script src="AdminExContent/js/jquery-tags-input/jquery.tagsinput.js"></script>
    <script src="AdminExContent/js/tagsinput-init.js"></script>
    <!--bootstrap input mask-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-inputmask/bootstrap-inputmask.min.js"></script>

    <!--dynamic table initialization -->
    <script src="js/pagesjs/MStoreInformativeNotification.js"></script>

</asp:Content>

