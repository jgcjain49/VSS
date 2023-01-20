<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="LabQualityReport.aspx.cs" Inherits="Admin_CommTrex.LabQualityReport" %>

<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />
    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />

    <link href="js/iCheck/skins/flat/grey.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/red.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/green.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/blue.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/yellow.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/purple.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>Add/Modify/Delete Lab Quality Report details</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Lab Quality Report Master </li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addproducts">
                        <a href="#addNewUser" data-toggle="tab">Add New Lab Quality</a>
                    </li>
                      <li class="" id="Tab_QCUpload">
                        <a href="#QC_Upload" data-toggle="tab">Import QC Report </a>
                    </li>
                    <li class="" id="tab_modifyproducts">
                        <a href="#modifyUser" data-toggle="tab">View/Modify Lab Quality</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <!-- //Add new user tab -->
                      <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <div class="tab-pane active" id="addNewUser">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Lab Quality Details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField3" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                </header>

                                <div class="panel-body">
                                    <div class="form-horizontal adminex-form">
                                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                             <ContentTemplate>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Commodity Name : </label>
                                             <div class="col-sm-10">
                                                        <asp:DropDownList ID="selectcommodityname" runat="server" Font-Bold="True" AutoPostBack="True"
                                                            CssClass="form-control m-bot15" x>
                                                            <%--OnSelectedIndexChanged="cmbdrdSupCntrId_SelectedIndexChanged"--%>
                                                        </asp:DropDownList>
                                                    </div>
                                            
                                        </div>
                                        <%-- <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Upload Report (PDF)
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtPass" name="txtPass" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" TextMode="Password" placeholder="Enter Password" data-original-title="Enter Password"></asp:TextBox>
                                            </div>
                                        </div>--%>

                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">
                                                Upload Report (PDF)
                                                       <span style="color: red">*</span>
                                            </label>

                                            <div class="col-sm-2">
                                                <button class="btn gbtns" type="button"
                                                    data-toggle="modal" data-target="#modReport" btn-action-image="Class"
                                                    runat="server" id="btnReport">
                                                    <i class="fa fa-folder-open"></i>Browse
                                                </button>
                                            </div>

                                            <label class="col-sm-2 control-label">Path </label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtAReportPath" name="txtAReportPath" readonly="true" runat="server"
                                                    CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Display an Image Path"
                                                    data-original-title="Display an Image Path " Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                       <%-- <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Date : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dt" runat="server" />
                                            </div>
                                        </div>--%>
                                                   </ContentTemplate>
                                              </asp:UpdatePanel>
                                    </div>
                                </div>

                            </section>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <section class="panel">
                                    <div class="panel-body" style="text-align: center">
                                        <button class="pnl-opener btn gbtn" type="button"
                                            btn-action="New"
                                            data-open-on="Save"
                                            runat="server" id="btnSave" onserverclick="btnSave_ServerClick">
                                            New 
                                        </button>
                                        <button class="btn btn-info" type="button"
                                            runat="server" id="btnClear" onserverclick="btnClear_ServerClick">
                                            Clear 
                                        </button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Lab Quality .
                                        </div>
                                    </div>

                                </section>
                                <!-- //panel -->
                            </div>
                            <!-- //Grid 12 -->
                        </div>
                        <!--//row buttons -->
                        <!-- end of Add New User Code Here -->
                    </div>

                      <div class="tab-pane" id="QC_Upload">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="Div2">
                              Press to read data from excel sheet. Upload QC Report
                            </div>
                              <div class="tab-pane" id="importproducts">

                       <%-- <div class="form-group">
                            <div class="alert alert-info" style="padding: 8px;" runat="server" id="Div1">
                                Press to read data from excel sheet.
                            </div>
                        </div>--%>
                        <div class="form-horizontal adminex-form">
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">Select an Excel Sheet: </label>
                                <div class="col-sm-10">
                                    <asp:FileUpload ID="FileUploadControl" runat="server" class="default" />
                                </div>
                                <br />
                                <%--<label class="red">*Note : Excel named MStore.xlxs or MStoreBusiness.xlxs</label>--%>
                            </div>

                         <%--   <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">Select an Images: </label>
                                <div class="col-sm-10">
                                    <input type="file" multiple="multiple" name="fileuploadimages" id="file1" runat="server" />
                                </div>
                                <br />
                                <label class="red">*Note : Image size should be between 32X32 and 43X43 pixels.</label>
                            </div>--%>

                            <div class="form-group">
                                <div class="col-sm-10"></div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="panel-body" style="text-align: center">
                                <button class="btn btn-success" type="button"
                                    data-toggle="modal" data-target="#modPasswordBox" btn-action-image="Class"
                                    runat="server" id="btnImportToExcel">
                                    <%--onserverclick="btnRead_ServerClick"--%>
                                    <i class="fa fa-table"></i>Import From Excel
                                </button>
                            </div>
                        </div>

                                      <div style="float:right" > 
                                 <asp:Label ID="Label1" runat="server">Sample File for QC Report Upload: </asp:Label>   
                                    
                                 <button id="Button1"  class="pnl-opener btn gbtn" type="button" runat="server" onserverclick="DownloadFile">Download </button>
                                    </div>
                    </div>
                        </div>
                          </div>

                    <div class="tab-pane" id="modifyUser">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdLab" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    OnRowEditing="GridView1_RowEditing"
                                    OnRowUpdating="GridView1_RowUpdating1"
                                    OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="Labid" runat="server" Text='<%# Eval("iLabId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Commodity Name">
                                            <ItemTemplate>
                                                <asp:Label ID="cmdtyname" runat="server" Text='<%# Eval("sCommodityName") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:label ID="edtcmdtyname" runat="server" Text='<%# Bind("sCommodityName") %>'>
                                                </asp:label>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    <%--     <asp:TemplateField HeaderText="Upload Report">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="imgPath" Value ='<%# Eval("sUploadReport") %>' runat="server" />
                                                    <asp:HiddenField ID="imgOriginalPath" Value ='<%# Eval("sUploadReport") %>' runat="server" />
                                                <asp:Image ID="ImgCat" runat="server"  AlternateText=" " ImageUrl='<%# Eval("sUploadReport") %>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderText="View Documents">
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowGallery1" RowIndex='<%# ((GridViewRow) Container).RowIndex %>'
                                                    runat="server" Text="View Documents" OnClick="btnShowGallery_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       
                                           <asp:TemplateField HeaderText="Predefined Img" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="LogoName" Value ='<%# Eval("sUploadReport") %>' runat="server" />
                                                <button class="btn gbtns" type="button" id="btnLogoShow" 
                                                runat="server"  >
                                                     <i class='<%# (Eval("sUploadReport")) %>'> </i>
                                                </button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                 
                                              
                                           
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" Visible="false" HeaderText="Selection" ControlStyle-CssClass="column-full-width" >
                                              <ItemTemplate>
                                                <asp:Label ID="Selection" runat="server" ></asp:Label>
                                            </ItemTemplate>

                                                <EditItemTemplate>
                                                    
                                                    <div class="action-buttons-group">
                                                     
                                                        <button class="btn gbtns" type="button" 
                                                            data-toggle="modal"  data-target="#modReport" 
                                                            runat="server" id="btnImage">
                                                            Browse CIg <i class="fa fa-eye-slash"></i>
                                                        </button>
                                                    </div>
                                                </EditItemTemplate>
                                            </asp:TemplateField>


                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                        <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-lab-id='<%# Eval("iLabId") %>'
                                                    href="javascript:;#addNewUser"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-lab-id='<%# Eval("iLabId") %>'
                                                    data-commodity-name='<%# Eval("sCommodityName") %>' href="javascript:;">Delete</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>


                        </div>
                    </div>
                            <div runat="server" id="editHidDiv" visible="true">
                        <asp:HiddenField ID="HidEdit" runat="server" Value="" />
                        <asp:HiddenField ID="HidIsEditMode" runat="server" Value="" />
                        <asp:HiddenField ID="HidBnkId" runat="server" Value="" />
                   
                    </div>
                </div>
            </div>
        </section>
        <!-- Panel Body Main -->
    </div>
    <!-- Main Row -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteLabQuality" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">Delete the Lab Quality.</h4>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="form-group">
                            <h3>Are you sure you want to delete Lab Quality?</h3>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Lab ID </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelLabQltyID" name="txtDelLabQltyID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Id -->
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Commodity Name </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelLabQltyName" name="txtDelLabQltyName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Name -->

                        <div class="form-group">
                            <div class="row">
                                <div class="panel-body" style="text-align: center">
                                    <button class="btn btn-success" type="button"
                                        runat="server" id="btnDeleteLabQlty" onserverclick="btnDeleteLabQlty_ServerClick">
                                        <i class="fa fa-trash"></i>Delete Lab Quality
                                    </button>
                                    <button class="btn btn-danger" type="button"
                                        id="btnCancelDeleteLabQlty">
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
      <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modPasswordBox" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <center><h4 class="modal-title">Category (Business)</h4></center>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-group">
                            <label for="inputPWd">Please Input Login Password to proceed : </label>
                            <asp:TextBox TextMode="Password" ID="txtCode" AutoComplete="New-Password" name="txtCode" class="form-control" runat="server"></asp:TextBox><br />
                            <br />
                            <label runat="server" id="lblMsgError" class="red" visible="false">* Password is mandatory for upload.</label>
                        </div>
                        <div class="col-lg-offset-2 col-lg-10">
                            <button class="btn btn-success" type="button"
                                runat="server" id="btnImportonPassword" onserverclick="btnImportonPassword_ServerClick">
                                <i class="fa fa-upload"></i>Upload
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
      <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="UserDocuments" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">Document Gallery</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-6 form-group">
                                        <div class="alert toss alert-block fade in">
                                            <%--  <div class="row">--%>
                                            <label class="control-label">PAN Image</label>
                                            <%-- <div class="col-md-9">--%>
                                            <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                                <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 150px; height: 100px;">

                                                    <asp:Image ID="ImgCoverImage" runat="server" AlternateText="No Image" />
                                                </div>
                                                <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                            </div>
                                            <!-- //file upload-->
                                            <div class="">
                                                <span id="enLargeImg"><a id="enlargeImgLnk1" class="btn gbtn1" href="#" target="_blank" runat="server" style="text-decoration: none;">View</a></span>
                                            </div>
                                        </div>
                                        <!-- //alert-success div-->
                                    </div>
                                    <!-- //form-group -->


                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-9">
                                                <%--   <button id="btnImageModify" type="submit" class="btn btn-primary" runat="server" data-toggle="modal" data-target="#modSendingMessage" onserverclick="btnImageModify_ServerClick">Upload</button>--%>
                                                <button id="btnImageCancel" type="button" class="btn gbtn1" data-dismiss="modal">Cancel</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

     <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modReport" class="modal fade">
                        <div class="modal-dialog" style="width: 280px;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblimgnm" runat="server" Text="Select Image Gst "></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div role="form">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                                        <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 75px; height: 75px;">
                                                            <asp:Image ID="MainImage" runat="server" ImageUrl="http://www.placehold.it/300x250/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                                        </div>
                                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 75px; max-height: 75px; line-height: 20px;"></div>
                                                        <div>
                                                            <span class="btn btn-default btn-file">
                                                                <span class="fileupload-new"><i class="fa fa-picture-o"></i>Select image</span>
                                                                <span class="fileupload-exists"><i class="fa fa-undo"></i>Change</span>
                                                                <asp:HiddenField ID="txtImgPathMain" runat="server"></asp:HiddenField>
                                                                <asp:FileUpload ID="FileMainImage1" runat="server" class="default" />
                                                            </span>
                                                            <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i>Remove</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <button class="btn gbtn" type="button"
                                                        runat="server" onserverclick="btnSaveImgUpload" id="b1">
                                                        <i class="fa fa-floppy-o"></i>Save 
                                                    </button>
                                                    <%-- <button class="btn btn-success" type="button"
                                                        runat="server" id="btnSaveFileCancel">
                                                        <i class="fa fa-times"></i>Cancel 
                                                    </button>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">

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

    <!--dynamic table-->
    <script type="text/javascript" src="js/pagesjs/LabQualityReport.js"></script>
</asp:Content>
