<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    ValidateRequest="false" CodeBehind="Blog.aspx.cs" Inherits="Admin_CommTrex.Blog" %>

<asp:Content ID="contHeadContent" ContentPlaceHolderID="contHeadContent" runat="server">

    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />

    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />

    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />
    <link href="css/jquery-te-1.4.0.css" rel="stylesheet" />
    <style>
        .column-full-width {
            white-space: nowrap;
        }

        #modEditBlog.modal-dialog {
            width: 900px;
        }

        .lblPos {
            margin-right: 20px;
            width: 85px;
        }

        .lblleftPos {
            margin-left: 20px;
            width: 70px;
        }

        .txtleft {
            margin-left: 20px;
            width: 350px;
        }
    </style>

</asp:Content>
<asp:Content ID="CntAdminEx_Header" ContentPlaceHolderID="CntAdminEx_Header" runat="server">

    <div class="page-heading pt">
        <h3>Add/Modify/Delete Blog</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Blog </li>
        </ul>
    </div>

</asp:Content>
<asp:Content ID="CntAdminEx_Body" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addBlog">
                        <a href="#addBlog" data-toggle="tab">Add Blog</a>
                    </li>
                    <%--                          <li class="" id="tab_excelimportCategory">
                            <a href="#importproducts" data-toggle="tab">Import Category From Excel</a>
                        </li>--%>
                    <li class="" id="tab_modifyBlog">
                        <a href="#modifyBlog" data-toggle="tab">View Blog</a>
                    </li>
                </ul>
            </header>
            <div class="panel-body">
                <div class="tab-content">
                    <div class="tab-pane active" id="addBlog">
                        <!-- Naina's Add Product Code Here -->
                        <div class="row">
                            <section class="panel" id="pnlFreeBlog">
                                <header class="panel-heading">
                                    Blog details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-up"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                </header>
                                <div class="panel-body collapse">
                                    <div class="form-horizontal adminex-form">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">ID </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtBlogID" name="txtBlogID" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="false"
                                                    data-toggle="tooltip" title="" placeholder="Blog id" data-original-title="Generated category code when Product Category is added">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Blog Title</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtName" name="txtName" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="false"
                                                    data-toggle="tooltip" title="" placeholder="Blog Title" data-original-title="Blog Title">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Date</label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dtDate" disabled="disabled" class="custom-input" runat="server" />
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Blog Paragragh 1</label>
                                            <%--   <div class="col-sm-3">
                                                <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpFS" Enabled="false">
                                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Arial" Value="Arial"></asp:ListItem>
                                                    <asp:ListItem Text="Verdana" Value="Verdana"></asp:ListItem>
                                                    <asp:ListItem Text="Impact" Value="Impact"></asp:ListItem>
                                                    <asp:ListItem Text="Georgia" Value="Georgia"></asp:ListItem>
                                                    <asp:ListItem Text="Oswald" Value="Oswald"></asp:ListItem>
                                                    <asp:ListItem Text="Comic Sans MS" Value="Comic Sans MS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                            <%--  <div class="col-sm-3">
                                                <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpSize" Enabled="false">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                            <div class="col-sm-9">
                                                <%--                                              <asp:TextBox ID="txtBlogPara" onblur="Test()" CssClass="form-control tooltips formatableTA" name="txtBlogPara" Font-Bold="true" Font-Italic="true" runat="server" TextMode="MultiLine" data-trigger="hover" data-toggle="tooltip" title=""
                                                    Height="75" Width="100%" placeholder="Blog Paragraph 1" data-original-title="Organization Address" Style="display: none;"></asp:TextBox><br />--%>
                                                <textarea id="txtPara1" name="txtPara1"></textarea>
                                                <asp:HiddenField ID="hidPara1" runat="server" ValidateRequestMode="Disabled" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Blog Paragragh 2</label>
                                            <%--  <div class="col-sm-3">
                                                <asp:DropDownList runat="server" ID="drpFS1" CssClass="form-control m-bot15" Enabled="false">
                                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Arial" Value="Arial"></asp:ListItem>
                                                    <asp:ListItem Text="Verdana" Value="Verdana"></asp:ListItem>
                                                    <asp:ListItem Text="Impact" Value="Impact"></asp:ListItem>
                                                    <asp:ListItem Text="Georgia" Value="Georgia"></asp:ListItem>
                                                    <asp:ListItem Text="Oswald" Value="Oswald"></asp:ListItem>
                                                    <asp:ListItem Text="Comic Sans MS" Value="Comic Sans MS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList runat="server" ID="drpSize1" CssClass="form-control m-bot15" Enabled="false">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                            <div class="col-sm-9">
                                                <%-- <asp:TextBox ID="txtBlogPara2" CssClass="form-control tooltips formatableTA" name="txtBlogPara2" runat="server" TextMode="MultiLine" data-trigger="hover" data-toggle="tooltip" title=""
                                                    Height="75" Width="100%" placeholder="Blog Paragraph 2" data-original-title="Organization Address" Style="resize: none;"></asp:TextBox><br />--%>
                                                <textarea id="txtPara2" name="txtPara2" validateinput="false"></textarea>
                                                <asp:HiddenField ID="hidPara2" runat="server" ValidateRequestMode="Disabled" />


                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Blog Paragragh 3</label>
                                            <%-- <div class="col-sm-3">
                                                <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpFS2" Enabled="false">
                                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Arial" Value="Arial"></asp:ListItem>
                                                    <asp:ListItem Text="Verdana" Value="Verdana"></asp:ListItem>
                                                    <asp:ListItem Text="Impact" Value="Impact"></asp:ListItem>
                                                    <asp:ListItem Text="Georgia" Value="Georgia"></asp:ListItem>
                                                    <asp:ListItem Text="Oswald" Value="Oswald"></asp:ListItem>
                                                    <asp:ListItem Text="Comic Sans MS" Value="Comic Sans MS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpSize2" Enabled="false">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>--%>
                                            <div class="col-sm-9">
                                                <%--    <asp:TextBox ID="txtBlogPara3" CssClass="form-control tooltips formatableTA" name="txtBlogPara3" runat="server" TextMode="MultiLine" data-trigger="hover" data-toggle="tooltip" title=""
                                                    Height="75" Width="100%" placeholder="Blog Paragraph 3" data-original-title="Organization Address" Style="resize: none;"></asp:TextBox><br />--%>
                                                <textarea id="txtPara3" name="txtPara3"></textarea>
                                                <asp:HiddenField ID="hidPara3" runat="server" />


                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Image Alignment </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList runat="server" ID="drpImgAlignment" CssClass="form-control m-bot15" Enabled="false">
                                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Left" Value="left"></asp:ListItem>
                                                    <asp:ListItem Text="Right" Value="right"></asp:ListItem>
                                                    <asp:ListItem Text="center" Value="center"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Select an Image </label>
                                            <div class="col-sm-10">
                                                <button class="btn gbtns" type="button"
                                                    data-toggle="modal" data-target="#modBlogImages" btn-action-image="Class"
                                                    runat="server" id="btnSelectICon">
                                                    <i class="fa fa-folder-open"></i>Browse Images
                                                </button>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Image Path </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtImageText" name="txtImageText" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Display an Image Path or Icon Name" data-original-title="Display an Image Path or Icon Name" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%--  <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">URL</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtURL" Visible="false" name="txtURL" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="false"
                                                    data-toggle="tooltip" title="" placeholder="URL" data-original-title="URL">
                                                </asp:TextBox>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </section>

                            <!--//row buttons -->
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <section class="panel">
                                    <div class="panel-body" style="text-align: center">
                                        <button class="pnl-opener btn gbtn" type="button"
                                            btn-action="New"
                                            data-open-on="Save" data-open-panels="pnlFreeBlog"
                                            runat="server" id="btnSave" onserverclick="btnSave_ServerClick">
                                            New <i class="fa fa-plus-square"></i>
                                        </button>
                                        <button class="btn gbtn1" type="button"
                                            runat="server" id="btnClear" onclick="clear()" onserverclick="btnClear_ServerClick">
                                            Clear <i class="fa fa-refresh"></i>
                                        </button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="alert toss" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to add Blog.
                                        </div>
                                    </div>
                                </section>
                                <!-- //panel -->
                            </div>
                            <!-- //Grid 12 -->
                        </div>

                        <!--//Form-horizonal-->
                    </div>
                    <!--//panel body-->
                    <!--//panel-->

                    <div class="tab-pane" id="modifyBlog">
                        <div class="form-group">
                            <div class="alert toss" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                        </div>
                        <div class="adv-table nice-scroll-grid">
                            <asp:GridView ID="grdBlog" runat="server"
                                EnableModelValidation="True" AutoGenerateColumns="False"
                                DataKeyNames="ID"
                                RowStyle-CssClass="gradeA"
                                class="dynamic-table-grid display table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="hdrAlgnCntrStyle" FooterStyle-CssClass="hdrAlgnCntrStyle">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                                                    <b><asp:Label ID="FtrLblSum" runat="server" Text='Sum'></asp:Label></b>
                                                                </FooterTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBlogId" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Blog Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBlogTitle" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date","{0:yyyy-MM-dd}") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <%--                                    <asp:TemplateField HeaderText="Blog Paragraph 1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBlogPara1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    --%>
                                    <%--                                    <asp:TemplateField HeaderText="Font Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFontSize" runat="server" Text='<%# Eval("FontSize") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Font Style">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFontStyle" runat="server" Text='<%# Eval("FontStyle") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    --%>
                                    <%--                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Selection" ControlStyle-CssClass="column-full-width">
                                        <ItemTemplate>
                                            <a id="A1" class="image" runat="server"
                                                data-blog-id='<%# Eval("ID") %>'
                                                data-blog-image='<%# Eval("Image") %>'
                                                href="javascript:;">Browse Img</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                        <ItemTemplate>
                                            <a id="A1" class="edit"
                                                data-blog-id='<%# Eval("ID") %>'
                                                data-blog-name='<%# Eval("Name") %>'
                                                data-blog-date='<%# Eval("Date","{0:yyyy-MM-dd}") %>'
                                                data-blog-descr='<%# Eval("Description") %>'
                                                data-blog-fontsize='<%# Eval("FontSize") %>'
                                                data-blog-fontstyle='<%# Eval("FontStyle") %>'
                                                data-blog-descr1='<%# Eval("Description1") %>'
                                                data-blog-fontsize1='<%# Eval("FontSize1") %>'
                                                data-blog-fontstyle1='<%# Eval("FontStyle1") %>'
                                                data-blog-descr2='<%# Eval("Description2") %>'
                                                data-blog-fontsize2='<%# Eval("FontSize2") %>'
                                                data-blog-fontstyle2='<%# Eval("FontStyle2") %>'
                                                data-blog-image='<%# Eval("Image") %>'
                                                data-blog-imagealign='<%# Eval("ImageAlign") %>'
                                                data-blog-isactive='<%# Eval("IsActive") %>'
                                                data-info-rowindex='<%# ((GridViewRow) Container).RowIndex %>'
                                                href="javascript:;">Edit</a>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                        <ItemTemplate>
                                            <a id="A1" class="delete" runat="server"
                                                data-blog-id='<%# Eval("ID") %>'
                                                data-blog-name='<%# Eval("Name") %>'
                                                data-blog-image='<%# Eval("Image") %>'
                                                href="javascript:;">Delete</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                        </div>
                        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteConfirm" class="modal fade">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                        <h4 class="modal-title">Delete the Blog. </h4>
                                    </div>
                                    <div class="modal-body">

                                        <div role="form">
                                            <div class="form-group">
                                                <h3>Are you sure you want to delete Blog with following details?</h3>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2 col-sm-2 control-label">ID </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDelBlogID" name="txtDelBlogID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <asp:HiddenField ID="txtDelBlogIDHidden" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2 col-sm-2 control-label">Blog Title </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtDelBlogName" name="txtDelBlogName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <asp:HiddenField ID="txtDelBlogPath" runat="server"></asp:HiddenField>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Name -->


                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="panel-body" style="text-align: center">
                                                        <button class="btn gbtn" type="button"
                                                            runat="server" id="btnDeleteBlog" onserverclick="btnDeleteBlog_ServerClick">
                                                            <i class="fa fa-trash"></i>Delete Blog
                                                        </button>
                                                        <button class="btn gbtn1" type="button"
                                                            id="btnCancelDeleteBlog">
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
                    <!-- Modify product tab-->

                </div>
                <!--//row basic details-->

                <!--//row buttons -->

            </div>
            <!--//Add products tab-->






            <!-- Modify product tab-->
        </section>
        <!-- Panel Body Main -->
    </div>
    <!-- Main Row -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modEditBlog" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 900px">
                <div class="modal-header">
                    <%--  <button aria-hidden="true" data-dismiss="modal" class="close" type="button"></button>--%>
                    <h4 class="modal-title">Modify  Blog</h4>
                </div>
                <div class="modal-body" style="width: 900px">
                    <div class="form-inline" role="form">
                        <section class="panel">
                            <div class="panel-body">
                                <div class="col-lg-12">
                                    <div class="alert toss" runat="server" id="updateActionDivDis">
                                        Modify Blog.
                                    </div>
                                </div>

                                <div class="row">
                                    <section class="panel">
                                        <header class="panel-heading">
                                            Blog  Details
							                 <span class="tools pull-right">
                                                 <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                 <span class="collapsible-server-hidden">
                                                     <asp:HiddenField runat="server" ID="HiddenField4" EnableViewState="true" Value="c" />
                                                 </span>
                                             </span>
                                        </header>
                                        <div class="panel-body">
                                            <div class="form-inline" role="form">
                                                <asp:HiddenField ID="HiddenImage" runat="server" />
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label style="margin-left: 20px; font: bolder">ID </label>
                                                        <asp:TextBox Style="margin-left: 85px; width: 245px;" Enabled="false" ID="txtEditBlogID" name="txtEditBlogID" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="HiddenFieldBlog" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldForDialogOpenClose" runat="server" Value="c" />

                                                    </div>
                                                </div>
                                                <div style="padding-bottom: 15px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">

                                                    <div class="row">
                                                        <label class="lblPos">Blog Title</label>
                                                        <asp:TextBox Style="width: 250px;" ID="txtEditName" name="txtEditName" runat="server" CssClass="form-control"></asp:TextBox>

                                                        <label class="lblleftPos" style="margin-left: 11px">Date</label>
                                                        <input type="date" id="dtEditDate" class="custom-input" runat="server" />
                                                    </div>

                                                </div>

                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Blog Paragragh 1</label>
                                                            <%--   <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpEditFS">
                                                                <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                                <asp:ListItem Text="Arial" Value="Arial"></asp:ListItem>
                                                                <asp:ListItem Text="Verdana" Value="Verdana"></asp:ListItem>
                                                                <asp:ListItem Text="Impact" Value="Impact"></asp:ListItem>
                                                                <asp:ListItem Text="Georgia" Value="Georgia"></asp:ListItem>
                                                                <asp:ListItem Text="Oswald" Value="Oswald"></asp:ListItem>
                                                                <asp:ListItem Text="Comic Sans MS" Value="Comic Sans MS"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpEditSize">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                                <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                                <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            
                                                            <asp:TextBox ID="txtEditBlogPara1" name="txtEditBlogPara1" runat="server" TextMode="MultiLine" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title=""
                                                                Height="75" Width="30%" placeholder="Blog Paragraph 1" data-original-title="Organization Address" Style="resize: none;"></asp:TextBox><br />
                                                            --%>
                                                            <textarea id="edtTxtPara1" name="edtTxtPara1"></textarea>
                                                            <asp:HiddenField ID="edtHidPara1" runat="server" />
                                                        </div>
                                                    </div>

                                                    <div style="padding-bottom: 20px"></div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Blog Paragragh 2</label>
                                                            <%--   <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpEditFS1">
                                                                <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                                <asp:ListItem Text="Arial" Value="Arial"></asp:ListItem>
                                                                <asp:ListItem Text="Verdana" Value="Verdana"></asp:ListItem>
                                                                <asp:ListItem Text="Impact" Value="Impact"></asp:ListItem>
                                                                <asp:ListItem Text="Georgia" Value="Georgia"></asp:ListItem>
                                                                <asp:ListItem Text="Oswald" Value="Oswald"></asp:ListItem>
                                                                <asp:ListItem Text="Comic Sans MS" Value="Comic Sans MS"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpEditSize1">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                                <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                                <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtEditBlogPara2" name="txtEditBlogPara2" runat="server" TextMode="MultiLine" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title=""
                                                                Height="75" Width="30%" placeholder="Blog Paragraph 2" data-original-title="Organization Address" Style="resize: none;"></asp:TextBox><br />--%>
                                                            <textarea id="edtTxtPara2" name="edtTxtPara2"></textarea>
                                                            <asp:HiddenField ID="edtHidPara2" runat="server" />

                                                        </div>
                                                    </div>

                                                    <div style="padding-bottom: 20px"></div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Blog Paragragh 3</label>
                                                            <%--    <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpEditFS2">
                                                                <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                                <asp:ListItem Text="Arial" Value="Arial"></asp:ListItem>
                                                                <asp:ListItem Text="Verdana" Value="Verdana"></asp:ListItem>
                                                                <asp:ListItem Text="Impact" Value="Impact"></asp:ListItem>
                                                                <asp:ListItem Text="Georgia" Value="Georgia"></asp:ListItem>
                                                                <asp:ListItem Text="Oswald" Value="Oswald"></asp:ListItem>
                                                                <asp:ListItem Text="Comic Sans MS" Value="Comic Sans MS"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" CssClass="form-control m-bot15" ID="drpEditSize2">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                                <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                                <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtEditBlogPara3" name="txtEditBlogPara3" runat="server" TextMode="MultiLine" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title=""
                                                                Height="75" Width="30%" placeholder="Blog Paragraph 3" data-original-title="Organization Address" Style="resize: none;"></asp:TextBox><br />
                                                            --%>
                                                            <textarea id="edtTxtPara3" name="edtTxtPara3"></textarea>
                                                            <asp:HiddenField ID="edtHidPara3" runat="server" />
                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Image Alignment</label>

                                                            <asp:DropDownList runat="server" ID="drpEditImageAlign" CssClass="form-control m-bot15">
                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Left" Value="Left"></asp:ListItem>
                                                                <asp:ListItem Text="Right" Value="Right"></asp:ListItem>
                                                                <asp:ListItem Text="center" Value="center"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                   <%--  <div style="padding-bottom: 20px;"></div>--%>
                                                   <div class="col-lg-12" style="vertical-align: middle; display:none;">
                                                        <div class="row">
                                                            <label class="lblPos">Select an Image</label>
                                                            <button class="btn gbtns" type="button"
                                                                data-toggle="modal" data-target="#modBlogImages" btn-action-image="Class"
                                                                runat="server" id="btnEditSelectionIcon">
                                                                <i class="fa fa-folder-open"></i>Browse Images
                                                            </button>
                                                        </div>
                                                    </div>
                                                   <%-- <div style="padding-bottom: 20px"></div>--%>
                                                    <div class="form-group" runat="server" style="display:none;">
                                                        <label class="lblPos" runat="server" style="width: auto; display: none;">Image Path</label>
                                                        <asp:TextBox ID="edttxtImageText" name="edttxtImageText" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Display an Image Path or Icon Name" data-original-title="Display an Image Path or Icon Name" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <section class="panel">
                                        <div class="panel-body" style="text-align: center">
                                            <button class="btn gbtn" type="button"
                                                runat="server" id="btnEditSave" onserverclick="btnEditSave_ServerClick">
                                                Save
                                                    <i class="fa fa-plus-square"></i>
                                            </button>
                                            <button data-dismiss="modal" class="btn gbtn1" type="button"
                                                id="btnCancel">
                                                Cancel <i class="fa fa-times-circle"></i>
                                            </button>
                                        </div>
                                    </section>
                                </div>
                            </div>
                            <!-- Action Buttons -->
                        </section>
                    </div>
                </div>
            </div>
            <!-- //modal body -->
        </div>
        <!-- //modal content -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modBlogImages" class="modal fade">
        <div class="modal-dialog" style="width: 280px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblimgnm" runat="server" Text="Select Blog Images"></asp:Label></h4>
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

                                                <asp:HiddenField ID="hidBlogId" runat="server"></asp:HiddenField>
                                                <asp:FileUpload ID="FileMainImage" runat="server" class="default" />
                                            </span>
                                            <a href="#" class="btn gbtn1 fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i>Remove</a>
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
                                        <i class="fa fa-floppy-o"></i>Save 
                                    </button>
                                    <button data-dismiss="modal" class="btn gbtn1" type="button"
                                        runat="server" id="btnSaveFileCancel">
                                        <i class="fa fa-times"></i>Cancel 
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
    <%--//Modal ends--%>
</asp:Content>
<asp:Content ID="Cnt_PageLevelScripts" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    <script src="js/websitejs/jquery-te-1.4.0.min.js"></script>
    <!--js for Productcategory initialization -->
    <script src="js/pagesjs/Blog.js"></script>
    <script type="text/javascript">
        function ShowImagePreview(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#ctl00_CntAdminEx_Body_imagePreview').prop('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
    <!--file upload-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-fileupload.min.js"></script>
    <!--tags input-->
    <%--  <script src="AdminExContent/js/jquery-tags-input/jquery.tagsinput.js"></script>
    <script src="AdminExContent/js/tagsinput-init.js"></script>--%>
    <!--bootstrap input mask-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-inputmask/bootstrap-inputmask.min.js"></script>


    <!--dynamic table-->
    <script type="text/javascript" src="AdminExContent/js/advanced-datatable/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="AdminExContent/js/data-tables/DT_bootstrap.js"></script>

    <!--font awesome picker creation-->
    <script type="text/javascript" src="js/pagesjs/Fontawesome.js"></script>


</asp:Content>
