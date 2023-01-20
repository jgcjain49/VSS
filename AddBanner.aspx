<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="AddBanner.aspx.cs" Inherits="Admin_CommTrex.AddBanner" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CntAdminEx_Header" runat="Server">
    <div class="page-heading pt">
        <h3>Add/Modify/Delete Banners</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Banners </a>
            </li>
            <li class="active">Add Banner </li>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CntAdminEx_Body" runat="Server">

    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addproducts">
                        <a href="#addNotify" data-toggle="tab">Add Banner</a>
                    </li>
                    <li class="" id="tab_modifyproducts">
                        <a href="#modifyNotify" data-toggle="tab">Modify/delete Banner</a>
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
                                        Banner details
                            <span class="tools pull-right">
                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                <span class="collapsible-server-hidden">
                                    <asp:HiddenField runat="server" ID="HiddenField2" EnableViewState="true" Value="o" />
                                </span>
                            </span>
                                    </header>
                                    <div class="panel-body">
                                        <div class="form-horizontal adminex-form">


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Area Name</label>
                                                <div class="col-sm-10">

                                                    <asp:TextBox ID="TextBox1" name="txtNotificationText" Enabled="false" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                        Columns="5" Rows="3"
                                                        data-toggle="tooltip" title="" placeholder="Actual Description about Banner" data-original-title="Actual Description about Banner"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Description about Banner</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtDescriptiomText" name="txtNotificationText" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                        TextMode="MultiLine" Columns="5" Rows="3"
                                                        data-toggle="tooltip" title="" placeholder="Actual Description about Banner" data-original-title="Actual Description about Banner"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Image Path </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtImageText" name="txtImagePath" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Display an Image Path or Icon Name" data-original-title="Display an Image Path or Icon Name" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Select an Image </label>
                                                <div class="col-sm-10">
                                                    <button class="btn gbtns" type="button"
                                                        data-toggle="modal" data-target="#modCatImages" btn-action-image="Class"
                                                        runat="server" id="btnSelectICon">
                                                        <i class="fa fa-folder-open"></i> Browse Images For Banner
                                                    </button>
                                                </div>
                                            </div>
                                            <!--//panel-body -->
                                </section>
                                <!--//panel -->
                            </div>
                            <!--//col-sm-12 -->
                        </div>
                        <!--//row -->



                        <div class="row">
                            <div class="col-lg-12">
                                <section class="panel">
                                    <div class="panel-body" style="text-align: center">
                                        <button class="pnl-opener btn gbtn" type="button"
                                            btn-action="New"
                                            data-open-on="Save" data-open-panels="pnlFreeCategoryMaster"
                                            onserverclick="btnSave_Click"
                                            runat="server" id="btnSave">
                                            New <i class="fa fa-plus-square"></i>
                                        </button>
                                        <button class="btn gbtn1" type="button"
                                            runat="server" id="btnClear"
                                            onserverclick="btnClear_ServerClick">
                                            Clear <i class="fa fa-refresh"></i>
                                        </button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="alert toss" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to add Banner.
                                        </div>
                                    </div>
                                </section>
                                <!-- //panel -->
                            </div>
                            <!-- //Grid 12 -->
                        </div>
                        <!--//row buttons -->
                    </div>
                    <%--End of Add Notifications--%>
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modCatImages" class="modal fade">
                        <div class="modal-dialog" style="width: 280px;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblimgnm" runat="server" Text="Select Category Images"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div role="form">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                                        <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 75px; height: 75px;">
                                                            <asp:Image ID="MainImage" runat="server" AlternateText="No Image" />
                                                        </div>
                                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 75px; max-height: 75px; line-height: 20px;"></div>
                                                        <div>
                                                            <span class="btn btn-default btn-file">
                                                                <span class="fileupload-new"><i class="fa fa-picture-o"></i>Select image</span>
                                                                <span class="fileupload-exists"><i class="fa fa-undo"></i>Change</span>
                                                                <asp:HiddenField ID="txtImgPathMain" runat="server"></asp:HiddenField>
                                                                <asp:FileUpload ID="FileMainImage" runat="server" class="default" />
                                                            </span>
                                                            <a href="#" class="btn gbtns fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                                        </div>
                                                    </div>
                                                    <!-- //file upload-->
                                                </div>
                                                <!-- //col-md-9-->
                                            </div>
                                            <!-- //row -->
                                        </div>
                                        <!-- //form-group -->
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <button class="btn gbtn" type="button"
                                                        runat="server" id="btnSaveFilePath" onserverclick="btnSaveFilePath_ServerClick">
                                                        <i class="fa fa-floppy-o"></i> Save 
                                                    </button>
                                                    <button class="btn gbtn1" type="button"
                                                        runat="server" id="btnSaveFileCancel">
                                                        <i class="fa fa-times"></i> Cancel 
                                                    </button>
                                                </div>
                                            </div>
                                            <%--//row--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modSendingMessage" class="modal fade" style="width: 100%;">
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
                            <div class="alert toss" runat="server" id="updateActionDiv">
                                Banner Report
                            </div>
                        </div>
                        <div class="adv-table nice-scroll-grid">
                            <asp:GridView ID="grdProducts" runat="server"
                                EnableModelValidation="True" AutoGenerateColumns="False"
                                OnRowEditing="GridView1_RowEditing"
                                OnRowUpdating="GridView1_RowUpdating1"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                RowStyle-CssClass="gradeA"
                                class="dynamic-table-grid display table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTalID" runat="server" Text='<%# Eval("B_bIntId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">

                                        <ItemTemplate>
                                            <asp:Label ID="lblTalDes" runat="server" Text='<%# Eval("B_nVarcharDesc") %>'></asp:Label>
                                        </ItemTemplate>


                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTalDes" runat="server" Text='<%# Bind("B_nVarcharDesc") %>'>
                                            </asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IMAGE" HeaderStyle-CssClass="nosort">

                                        <ItemTemplate>
                                            <asp:HiddenField ID="imgPath" Value='<%# Eval("B_NvarCharImagePath") %>' runat="server" />
                                            <asp:HiddenField ID="imgOriginalPath" Value='<%# Eval("B_NvarCharImagePath") %>' runat="server" />
                                            <asp:Image ID="ImgCat" runat="server" AlternateText=" " Height="50" Width="50" ImageUrl='<%# Eval("B_NvarCharImagePath") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Selection" ControlStyle-CssClass="column-full-width">
                                        <ItemTemplate>
                                            <div class="btn-group action-buttons-group">

                                                <button class="btn gbtn" type="button"
                                                    data-toggle="modal" data-target="#modCatImages"
                                                    runat="server" id="btnImage">
                                                    Browse CIg <i class="fa fa-eye-slash"></i>
                                                </button>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />

                                    <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                        <ItemTemplate>
                                            <a id="A1" class="delete" runat="server"
                                                data-cat-id='<%# Eval("B_bIntId") %>' data-cat-path='<%# Eval("B_NvarCharImagePath") %>'
                                                data-cat-name='<%# Eval("B_NvarCharImagePath") %>' href="javascript:;">Delete</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <!--//adv table -->
                        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteConfirm" class="modal fade">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                        <h4 class="modal-title">Delete the banner.</h4>
                                    </div>
                                    <div class="modal-body">

                                        <div role="form">
                                            <div class="form-group">
                                                <h3>Are you sure you want to delete banner with following details?</h3>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2 col-sm-2 control-label">ID </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDelCatID" name="txtDelCatID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <asp:HiddenField ID="txtDelCatIDHidden" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Id -->
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2 col-sm-2 control-label">Banner Path </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDelCatName" name="txtDelCatName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <asp:HiddenField ID="txtDelCatPath" runat="server"></asp:HiddenField>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Name -->

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="panel-body" style="text-align: center">
                                                        <button class="btn gbtn" type="button"
                                                            runat="server" id="btnDeleteProduct" onserverclick="btnDeleteProduct_ServerClick">
                                                            <i class="fa fa-trash"></i>Delete banner
                                                        </button>
                                                        <button class="btn gbtn1" type="button"
                                                            id="btnCancelDeleteProduct">
                                                            Cancel <i class="fa fa-times-circle"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Action Buttons -->

                                        </div>
                                        <!-- //form -->
                                    </div>
                                    <!-- //modal body -->
                                </div>
                                <!-- //modal content -->
                            </div>
                            <!-- //modal dialog -->
                        </div>
                        <!-- //dialog -->

                    </div>
                    <%--End of View Notifications--%>
                </div>
                <%-- end of tab content--%>
            </div>
            <%-- end of panel--%>
        </section>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="Server">
    <!--file upload-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-fileupload.min.js"></script>
    <!--tags input-->
    <script src="AdminExContent/js/jquery-tags-input/jquery.tagsinput.js"></script>
    <script src="AdminExContent/js/tagsinput-init.js"></script>
    <!--bootstrap input mask-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-inputmask/bootstrap-inputmask.min.js"></script>
    <!--dynamic table-->
    <script type="text/javascript" src="AdminExContent/js/advanced-datatable/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="AdminExContent/js/data-tables/DT_bootstrap.js"></script>
    <script src="js/pagesjs/AddBanner.js"></script>
</asp:Content>


