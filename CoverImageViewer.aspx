<%@ Page Title="" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="CoverImageViewer.aspx.cs" Inherits="Admin_CommTrex.CoverImageViewer" %>
<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />

    <style>
        /*Small css for this page only*/
        .prerview-img-thumb,.fileupload-preview {
            cursor:pointer;
        }
        
        .modal .modal-dialog { width: 800px; }
    </style>

</asp:Content>
<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>Modify product cover images and thumbnail</h3>
        <ul class="breadcrumb">
            <li>Master</li>
            <li><a href="ProductMaster.aspx">Product Master </a></li>
            <li class="active">Cover Image Gallery </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <%--<form runat="server">--%>

        <div class="row">
            <div class="col-lg-12">
                <section class="panel">
                    <header class="panel-heading">
                        Product basic details
                        <span class="tools pull-right">
                            <a href="javascript:;" class="fa fa-chevron-down"></a>
                            <span class="collapsible-server-hidden">
                                <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="o" />
                            </span>
                        </span>
                    </header><!-- Panel header -->
                    <div class="panel-body">
                        <div class="form">
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2 col-sm-2 control-label">ID </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtProdId" name="txtDelProdId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div><!--//ID-->
                            </div> <!--//form-group -->
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2 col-sm-2 control-label">NAME </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtProdName" name="txtDelProdName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div> <!-- Name -->
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2 col-sm-2 control-label">TYPE </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtProdType" name="txtDelProdType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div> <!-- Type -->
                        </div><!--//Form-->
                    </div><!--//panel-body-->
                </section>
            </div><!--//col-lg-12-->
        </div><!--//row -->

        <div class="row">
            <div class="col-lg-12">
                <section class="panel">
                    <header class="panel-heading">
                        Product cover image & thumbs selection
                    <span class="tools pull-right">
                        <a href="javascript:;" class="fa fa-chevron-down"></a>
                        <span class="collapsible-server-hidden">
                            <asp:HiddenField runat="server" ID="HiddenField2" EnableViewState="true" Value="o" />
                        </span>
                    </span>
                    </header>
                    <!-- //Panel heading-->
                    <div class="panel-body">
                        <div class="form">
                            <div class="form-group">
                                <div class ="row">
                                    <label class="control-label col-md-3">Main Image</label>
                                    <div class="col-md-9">
                                        <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                            <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 300px; height: 250px;">
                                                <asp:Image ID="MainImage" runat="server" AlternateText="No Image" />
                                            </div>
                                            <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 300px; max-height: 250px; line-height: 20px;"></div>
                                            <div>
                                                <span class="btn btn-default btn-file">
                                                    <span class="fileupload-new"><i class="fa fa-paperclip"></i> Select image</span>
                                                    <span class="fileupload-exists"><i class="fa fa-undo"></i> Change</span>
                                                    <asp:HiddenField ID="txtImgPathMain" runat="server"></asp:HiddenField>
                                                    <asp:FileUpload ID="FileMainImage" runat="server" class="default" />
                                                </span>
                                                <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                            </div>
                                        </div><!-- //file upload-->
                                    </div><!-- //col-md-9-->
                                </div> <!-- //row -->
                            </div><!-- //form-group -->
                            <div class="form-group">
                                <div class ="row">
                                    <label class="control-label col-md-3">Thumb (256x256)</label>
                                    <div class="col-md-9">
                                        <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Thumbnail 256X256">
                                            <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 256px; height: 256px;">
                                                <asp:Image ID="Img256x256" runat="server" ImageUrl="http://www.placehold.it/256x256/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                            </div>
                                            <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 256px; max-height: 256px; line-height: 20px;"></div>
                                            <div>
                                                <span class="btn btn-default btn-file">
                                                    <span class="fileupload-new"><i class="fa fa-paperclip"></i> Select image</span>
                                                    <span class="fileupload-exists"><i class="fa fa-undo"></i> Change</span>
                                                    <asp:HiddenField ID="txtThumbPath256" runat="server"></asp:HiddenField>
                                                    <asp:FileUpload ID="FileThumb256" runat="server" class="default" />
                                                </span>
                                                <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                            </div>
                                        </div><!-- //file upload-->
                                    </div><!-- //col-md-9-->
                                </div> <!-- //row -->
                            </div><!-- //form-group -->
                            <div class="form-group">
                                <div class ="row">
                                    <label class="control-label col-md-3">Thumb (128x128)</label>
                                    <div class="col-md-9">
                                        <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Thumbnail 128X128">
                                            <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 128px; height: 128px;">
                                                <asp:Image ID="Img128x128" runat="server" ImageUrl="http://www.placehold.it/128x128/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                            </div>
                                            <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 128px; max-height: 128px; line-height: 20px;"></div>
                                            <div>
                                                <span class="btn btn-default btn-file">
                                                    <span class="fileupload-new"><i class="fa fa-paperclip"></i> Select image</span>
                                                    <span class="fileupload-exists"><i class="fa fa-undo"></i> Change</span>
                                                    <asp:HiddenField ID="txtThumbPath128" runat="server"></asp:HiddenField>
                                                    <asp:FileUpload ID="FileThumb128" runat="server" class="default" />
                                                </span>
                                                <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                            </div>
                                        </div><!-- //file upload-->
                                    </div><!-- //col-md-9-->
                                </div> <!-- //row -->
                            </div><!-- //form-group -->
                            <div class="form-group">
                                <div class ="row">
                                    <label class="control-label col-md-3">Thumb (64x64)</label>
                                    <div class="col-md-9">
                                        <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Thumbnail 64X64">
                                            <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 64px; height: 64px;">
                                                <asp:Image ID="Img64x64" runat="server" ImageUrl="http://www.placehold.it/64x64/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                            </div>
                                            <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 64px; max-height: 64px; line-height: 20px;"></div>
                                            <div>
                                                <span class="btn btn-default btn-file">
                                                    <span class="fileupload-new"><i class="fa fa-paperclip"></i> Select image</span>
                                                    <span class="fileupload-exists"><i class="fa fa-undo"></i> Change</span>
                                                    <asp:HiddenField ID="txtThumbPath64" runat="server"></asp:HiddenField>
                                                    <asp:FileUpload ID="FileThumb64" runat="server" class="default" />
                                                </span>
                                                <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                            </div>
                                        </div><!-- //file upload-->
                                    </div><!-- //col-md-9-->
                                </div> <!-- //row -->
                            </div><!-- //form-group -->
                            <div class="form-group">
                                <div class ="row">
                                    <label class="control-label col-md-3">Thumb (32x32)</label>
                                    <div class="col-md-9">
                                        <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Thumbnail 32X32">
                                            <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 32px; height: 32px;">
                                                <asp:Image ID="Img32x32" runat="server" ImageUrl="http://www.placehold.it/32x32/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                            </div>
                                            <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 32px; max-height: 32px; line-height: 20px;"></div>
                                            <div>
                                                <span class="btn btn-default btn-file">
                                                    <span class="fileupload-new"><i class="fa fa-paperclip"></i> Select image</span>
                                                    <span class="fileupload-exists"><i class="fa fa-undo"></i> Change</span>
                                                    <asp:HiddenField ID="txtThumbPath32" runat="server"></asp:HiddenField>
                                                    <asp:FileUpload ID="FileThumb32" runat="server" class="default" />
                                                </span>
                                                <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                            </div>
                                        </div><!-- //file upload-->
                                    </div><!-- //col-md-9-->
                                </div> <!-- //row -->
                            </div><!-- //form-group -->
                            <div class="form-group">
                                <div class ="row">
                                    <label class="control-label col-md-3">Thumb (16x16)</label>
                                    <div class="col-md-9">
                                        <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Thumbnail 16X16">
                                            <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 16px; height: 16px;">
                                                <asp:Image ID="Img16x16" runat="server" ImageUrl="http://www.placehold.it/16x16/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                            </div>
                                            <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 16px; max-height: 16px; line-height: 20px;"></div>
                                            <div>
                                                <span class="btn btn-default btn-file">
                                                    <span class="fileupload-new"><i class="fa fa-paperclip"></i> Select image</span>
                                                    <span class="fileupload-exists"><i class="fa fa-undo"></i> Change</span>
                                                    <asp:HiddenField ID="txtThumbPath16" runat="server"></asp:HiddenField>
                                                    <asp:FileUpload ID="FileThumb16" runat="server" class="default" />
                                                </span>
                                                <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i> Remove</a>
                                            </div>
                                        </div><!-- //file upload-->
                                    </div><!-- //col-md-9-->
                                </div> <!-- //row -->
                            </div><!-- //form-group -->
                        </div><!-- //form -->
                    </div><!-- //Panel body-->
                </section><!-- //Panel-->
            </div><!-- col-lg-12-->
        </div><!-- //Main Row-->

        <div class="row">
            <div class="col-lg-12">
                <section class="panel">
                    <div class="panel-body" style="text-align: center">
                        <button class="btn btn-success" type="button"
                            btn-action="New"
                            runat="server" id="btnSave" onserverclick="btnSave_ServerClick">
                            <i class="fa fa-save"></i> Save
                        </button>
                        <button class="btn btn-info" type="button"
                            id="btnClear">
                            Clear <i class="fa fa-refresh"></i>
                        </button>
                    </div>
                    <div class="panel-body">
                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                            Press save to modify thumbs.<br />
                            If any thumbs if not selected it's automatically created by server.
                        </div>
                    </div>
                </section><%-- //panel --%>
            </div><%-- //Grid 12 --%>
        </div><%--//row buttons --%>

        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modImageFull" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                        <h4 class="modal-title"><span id="ImageName">Image Viewer</span></h4>
                    </div>
                    <div class="modal-body">
                        <div role="form">
                            <div class="form-group" style="text-align:center;">
                                <img id="dlgImgPreview" src="http://www.placehold.it/350x350/EFEFEF/AAAAAA&amp;text=no+image" alt="No image" style="max-height:1024px;max-width:768px;"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
                            <!-- //Thumbnail view modal -->

   <%-- </form>--%>
</asp:Content>
<asp:Content ID="contChild_PageLevelScripts" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    
    <!--file upload-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-fileupload.min.js"></script>
    <!--tags input-->
    <script src="AdminExContent/js/jquery-tags-input/jquery.tagsinput.js"></script>
    <script src="AdminExContent/js/tagsinput-init.js"></script>
    <!--bootstrap input mask-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-inputmask/bootstrap-inputmask.min.js"></script>

    <!-- Page scripts -->
    <script src="js/pagesjs/CoverImageViewer.js"></script>

</asp:Content>
