<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecificationValueMaster.aspx.cs" MasterPageFile="~/AdminEx.Master" MaintainScrollPositionOnPostback="true" Inherits="Admin_CommTrex.SpecificationValueMaster" %>

<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    <!-- For grid stylings as dynamic table-->
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />

    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-inp
        ut/jquery.tagsinput.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>Add/Modify/Delete Product Specification Values</h3>
        <ul class="breadcrumb">
                <li>
                    <a href="#"> Master </a>
                </li>
                <li class="active"> Specification Value Master </li>
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
                            <a href="#addspecifyCategory" data-toggle="tab">Add Specification Value</a>
                        </li>
<%--                         <li class="" id="tab_excelimportCategory">
                            <a href="#importproducts" data-toggle="tab">Import Specification Category From Excel</a>
                        </li>--%>
                    <li class="" id="tab_modifyproducts">
                            <a href="#modifyspecifyCategory" data-toggle="tab">View Product Specification</a>
                        </li>
                    </ul>
                </header>
                <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane active" id="addspecifyCategory">
                           <%--Add Product Category Specification--%>
                            <div class="row">
                                <section class="panel" id="pnlFreeCategoryMaster">
                                    <header class="panel-heading">
                                        Specification Value
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                    </header>
                                    <div class="panel-body collapse">
                                        <div class="form-horizontal adminex-form">

                                               <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Information</label>
                                                <div class="col-sm-10">
                                                    <asp:dropdownlist id="drpInfoselection" runat="server" font-bold="True" enabled="true" autopostback="True" cssclass="form-control m-bot15" onselectedindexchanged="drpInfoselection_SelectedIndexChanged1">
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>

                                             <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Product Category</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="drdCategoryType" runat="server" Font-Bold="True"  AutoPostBack="True"  CssClass="form-control m-bot15" OnSelectedIndexChanged="drdCategoryType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                                                                   
                                             <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Product SubCategory</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="drdSubCategoryType" runat="server" Font-Bold="True"  AutoPostBack="True"  CssClass="form-control m-bot15" OnSelectedIndexChanged="drdSubCategoryType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div> 
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Product Name</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="drdProductName" runat="server" Font-Bold="True"  AutoPostBack="True"  OnSelectedIndexChanged="drdProductName_SelectedIndexChanged" CssClass="form-control m-bot15">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfProductId" runat="server" Value=""></asp:HiddenField>
                                                </div>
                                            </div> 
        
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Specification Type</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="drdSpecification" runat="server" Font-Bold="True"  AutoPostBack="True"  CssClass="form-control m-bot15" OnSelectedIndexChanged="drdSpecification_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>                                                                                
                                                      <br /><br />
                                            <div class="form-group">
                                                 <div class="col-md-12" >
                                                     <%--<asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder> --%>
                                                     
                                                     <%--Gridview of Specification--%>
                                                            <div class="adv-table nice-scroll-grid">

                                <asp:GridView ID="grdProductSpecificationValue" runat="server" 
                                    EnableModelValidation="True"  AutoGenerateColumns="False"                                      
                                    OnRowEditing="grdProductSpecificationValue_RowEditing"
                                    OnRowDataBound="grdProductSpecificationValue_RowDataBound"
                                    OnRowUpdating="grdProductSpecificationValue_RowUpdating" 
                                    OnRowCancelingEdit="grdProductSpecificationValue_RowCancelingEdit" 
                                    Visible="false" 
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                    <Columns>
                                         <asp:TemplateField HeaderText="Specification Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCategoryEngName" runat="server" Text='<%# Eval("PSSCM_vCharSubCat_NameEn") %>'></asp:Label>
                                                <asp:HiddenField ID="hfSubcategoryId" runat="server" Value='<%# Eval("PSSCM_bIntSubCatId") %>'></asp:HiddenField> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Value (English)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCategoryEngValue" runat="server" Text='<%# Eval("PSDM_vCharValue_En") %>'></asp:Label>
                                            </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:TextBox ID="txtSubCategoryEngValue" runat="server" Text='<%# Eval("PSDM_vCharValue_En") %>'></asp:TextBox>
                                             </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Specification Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCategoryRegName" runat="server" Text='<%# Eval("PSSCM_nVarSubCat_NameReg") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Value (Regional)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCategoryRegValue" runat="server" Text='<%# Eval("PSDM_nVarValue_Reg") %>'></asp:Label>
                                            </ItemTemplate>
                                              <EditItemTemplate>
                                                 <asp:TextBox ID="txtSubCategoryRegValue" runat="server" Text='<%# Eval("PSDM_nVarValue_Reg") %>'></asp:TextBox>
                                             </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" HeaderStyle-CssClass="nosort"/>
                                    </Columns>
                                </asp:GridView>
                                                            </div>
                                                                <%--OnRowCommand="grdProductSpecification_RowCommand"--%>

                                                     <%--End of Gridview of Specification--%>

                                                 </div>
                                            </div><!--//form group end div -->


                                          </div>
                                    </div> <%--End of panel body collapse--%>
                                           <!--//panel body-->
                                </section>
                                <!--//panel-->
                            </div> <!--//row basic details-->

                            <div class="row">
                                <div class="col-lg-12">
                                    <section class="panel">
                                        <div class="panel-body">
                                            <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                              Set Specification for each product
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>                   
                        </div>
                     <%-- End of add products--%>

                          <div class="tab-pane" id="modifyspecifyCategory">
             <div class="col-lg-6">
                <section class="panel">
                    <header class="panel-heading">
                        VIEW SPECIFICATIONS
                    </header>
                    <div class="panel-body">
                        
                        <form role="form">
                            <div class="form-group">
                                <label for="drProductViewSpecification">Select the Product Name</label>
                                 <asp:DropDownList ID="drProductViewSpecification" runat="server" Font-Bold="True"  AutoPostBack="false"  CssClass="form-control m-bot15" >
                                         </asp:DropDownList>
                                         <asp:HiddenField ID="hfProductViewId" runat="server" Value=""></asp:HiddenField>
                            </div>
                              <button id="btnViewProductSpecification" type="submit" class="btn btn-primary" runat="server" onserverclick="btnViewProductSpecification_ServerClick">English</button>
                            <button id="btnViewProductSpecify" type="submit" class="btn btn-primary" runat="server" onserverclick="btnViewProductSpecify_ServerClick">Regional</button>
                         <br /><br />
<%--                            <div class="form-group">
                                     <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h3 class="panel-title"><asp:Label ID="lblProductName" runat="server"></asp:Label></h3>
                                        </div>
                                        <div class="panel-body">--%>
                                               <div class="form-group">
                                                 <div class="col-md-12" >
                                            <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>
                                                 </div>
                                               </div>
<%--                                        </div>
                                    </div>
                            </div>--%>
                         </form>

                    </div> <%--end of panel body--%>
                </section>
            </div>

                               <div class="form-group">
                                    <div class="alert alert-info" runat="server" id="updateActionDiv">
                                        Select Product whose specification has been set.
                                    </div>                              
                                </div>

                           </div> 
  
                        <!-- Modify product tab-->

                         </div>                    
                </div>
            </section> <!-- Panel Body Main -->
        </div> <!-- Main Row -->
    <%--</form>--%>




</asp:Content>
<asp:Content ID="contChild_PageLevelScripts" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    
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


    <!--dynamic table initialization -->
    <%--<script type="text/javascript" src="js/pagesjs/SpecificationValue.js"></script>--%>
    <script type="text/javascript"  src="js/pagesjs/SpecificationCategoryMaster.js"></script>
</asp:Content>




