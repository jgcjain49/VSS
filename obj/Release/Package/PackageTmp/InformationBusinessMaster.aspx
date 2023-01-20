<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminEx.Master"  CodeBehind="InformationBusinessMaster.aspx.cs" Inherits="Admin_CommTrex.InformationBusinessMaster" %>


<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />
    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $("[id$=txtFromdate]").datepicker({
                dateFormat: 'mm/dd/yy'
            });

            $("[id$=txtTodate]").datepicker({
                dateFormat: 'mm/dd/yy'
            });

            $("[id$=txtModifyFromdate]").datepicker({
                dateFormat: 'mm/dd/yy'
            });

            $("[id$=txtModifyTodate]").datepicker({
                dateFormat: 'mm/dd/yy'
            });
        });
    </script>

     <style>
         .column-full-width {
             white-space: nowrap;
         }

         #modEditInformation .modal-dialog {
             width: 1000px;
         }

         .lblPos {
             margin-right: 20px;
             width: 85px;
             font: bold;
         }

         .lblleftPos {
             margin-left: 20px;
             width: 70px;
             font: bold;
         }

         .txtleft {
             margin-left: 20px;
             width: 350px;
         }
     </style>

</asp:Content>

    <asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
         <div class="page-heading pt">
                <h3>Add/Modify/Delete Business Information details</h3>
                <ul class="breadcrumb">
                        <li>
                            <a href="#"> Master </a>
                        </li>
                        <li class="active"> Business Information Master </li>
                </ul>
        </div>
    </asp:Content>

    <asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
           <asp:HiddenField ID="activeTab" EnableViewState ="true" runat="server" value="addproducts"/>
         <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading custom-tab ">
                    <ul class="nav nav-tabs">
                        <li class="active" id="tab_addproducts">
                            <a href="#addproducts" data-toggle="tab">Add Information</a>
                        </li>
                        <li class="" id="Li1">
                            <a href="#importproducts" data-toggle="tab">Import Information From Excel</a>
                        </li>
                        <li class="" id="tab_modifyproducts">
                            <a href="#modifyproducts" data-toggle="tab">View/Modify Information</a>
                        </li>
                    </ul>
                </header>
                <div class="panel-body">
                    <div class="tab-content">
                         <!-- Naina's Add Product Code Here -->
                        <div class="tab-pane active" id="addproducts">
                            <div class="row">
                                <div class="col-lg-12">
                                <section class="panel" id="pnlInformationBusinessMaster">
                                    <header class="panel-heading">
                                        Information details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-up"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                    </header>
                                    <div class="panel-body collapse">
                                        <div class="form-horizontal adminex-form">

                                           <%--<div class="panel-bod">
                                                <div class="alert alert-info" runat="server" id="actionInfo">
                                                     Click New to Add Products
                                                </div>
                                            </div>--%>
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">ID </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtInformationID" name="txtInformationID" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="false"
                                                        data-toggle="tooltip" title="" placeholder="Autogenerated Information id" data-original-title="Generated Information ID"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Category </label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="cmbCategorySelection" runat="server" Font-Bold="True" AutoPostBack="True" OnSelectedIndexChanged="cmbCategorySelection_SelectedIndexChanged" 
                                                        CssClass="form-control m-bot15">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">SubCategory</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="drpSubCategorySelection" runat="server" Font-Bold="True" AutoPostBack="True"
                                                        CssClass="form-control m-bot15">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                                                              
                                        <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Name </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtInfoName" name="txtInfoName" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Information Name" data-original-title="Name of the Information"></asp:TextBox>
                                                </div>
                                               <div style="padding-bottom:60px"></div>

                                                <label class="col-sm-2 col-sm-2 control-label">Reg  Name </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtInfoRegName" name="txtInfoRegName" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Information Name " data-original-title="Regional Name of the Information"></asp:TextBox>
                                                </div>
                                            </div>

                      
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">City </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtCity" name="txtCity" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="City " data-original-title="City"></asp:TextBox>
                                                </div>

                                                   <div style="padding-bottom:60px"></div>

                                                <label class="col-sm-2 col-sm-2 control-label">Reg City </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtRegCity" name="txtCity" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Regional City" data-original-title="Regional Name of Society"></asp:TextBox>
                                                </div>
                                          </div>

                                            <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Address </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtInfoAdd" name="txtInfoAdd" runat="server" CssClass="form-control" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Address" data-original-title="Address"></asp:TextBox>
                                                    </div>
                                                        <div style="padding-bottom:60px"></div>
                                                   <label class="col-sm-2 col-sm-2 control-label">Reg Address </label>
                                                    <div class="col-sm-10">
                                                             <asp:TextBox ID="txtAddReg" name="txtAddReg" runat="server" CssClass="form-control" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Regional Address" data-original-title="Address in Regional Language"></asp:TextBox>
                                                    </div>
                                            </div>

                                          <%--  <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Address Regional </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtAddReg" name="txtAddReg" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Regional Address" data-original-title="Address in Regional Language"></asp:TextBox>
                                                </div>
                                            </div>--%>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Email </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtEmail" name="txtEmail" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Email" data-original-title="Email"></asp:TextBox>
                                                </div>
                                            </div>

                                             <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Links (or Url) </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtUrl" name="txtUrl" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="URL of website" data-original-title="Website URL"></asp:TextBox>
                                                </div>
                                            </div>
                                        
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Contacts</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPhone1" placeholder="Phone 1"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPhone2" placeholder="Phone 2"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPhone3" placeholder="Phone 3"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="regChkPhone3" ControlToValidate="txtPhone3" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                         <asp:RegularExpressionValidator ID="regChkPhone1" ControlToValidate="txtPhone1" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                         <asp:RegularExpressionValidator ID="regChkPhone2" ControlToValidate="txtPhone2" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                          

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Latitude </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtLatitude" name="txtLatitude" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Location Longitude " data-original-title="Longitude of Location"></asp:TextBox>
                                                     <asp:RegularExpressionValidator ID="regChkLatitude" ControlToValidate="txtLatitude" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="((\d+)+(\.\d+))$"></asp:RegularExpressionValidator>
                                                </div>

                                                <label class="col-sm-2 col-sm-2 control-label">Longitude </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtLongitude" name="txtLongitude" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Location Latitude " data-original-title="Latitude of Location"></asp:TextBox>
                                                     <asp:RegularExpressionValidator ID="regChkLongitude" ControlToValidate="txtLongitude" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="((\d+)+(\.\d+))$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">PinCode </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtPinCode" name="txtPinCode" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="PinCode" data-original-title="PinCode"></asp:TextBox>
                                                     <asp:RegularExpressionValidator ID="regChkPinCode" ControlToValidate="txtPinCode" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                </div>

                                                <label class="col-sm-2 col-sm-2 control-label">Reg PinCode </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtRegionalPinCode" name="txtRegionalPinCode" runat="server" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Regional PinCode" data-original-title="Regional PinCode"></asp:TextBox>
                                                </div>
                                            </div>

                                            
                                             <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Type of Information </label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="drdInformationType" runat="server" Enabled="false" Font-Bold="True" AutoPostBack="False"
                                                        CssClass="form-control m-bot15">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                        <!--//Form-horizonal-->
                                    </div>
                                    <!--//panel body-->
                                </section>
                                <!--//panel-->
                            </div>
                                </div> <!--//row basic details-->
  
                            <div class="row">
                                <div class="col-lg-12">
                                    <section class="panel" id="pnlInformationBusinessMasterDetails">
                                        <header class="panel-heading">
                                            Additional  details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-up"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField2" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                           
                                        </header>
                                         
                                        <div class="panel-body collapse" style="text-align: center">
                                             

                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtExtraField">Label1 Details</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_1" placeholder="Label Name"></asp:TextBox>
                                                         <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_1" placeholder="Value"></asp:TextBox>
                                                       
                                                    </div>
                                                </div>
                                                <div style="padding-bottom:40px"></div>
                                                 <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtExtraField"> Label1  RegDetails</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_1" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_1" placeholder="Reg Value"></asp:TextBox>
                                               
                                                    </div>
                                                </div>

                                            </div>
                                              <div style="padding-bottom:60px"></div>
                                              <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label2 Details </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_2" placeholder="Label Name "></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_2" placeholder="Value"></asp:TextBox>
                                                    </div>
                                                </div>
                                           
                                                <div style="padding-bottom:40px"></div>
                                      
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label2  RegDetails </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_2" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_2" placeholder="Reg Value"></asp:TextBox>
                                                    </div>
                                                </div>
                                             </div>
                                                <div style="padding-bottom:60px"></div>
                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label3 Details </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_3" placeholder="Label Name"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_3" placeholder="Label  Value"></asp:TextBox>

                                                    </div>
                                                </div>
                                                     <div style="padding-bottom:40px"></div>

                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label3 RegDetails </label>
                                                    <div class="col-sm-10">
                                                        
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_3" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_3" placeholder="Label RegValue"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                                <div style="padding-bottom:60px"></div>
                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label4 Details </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_4" placeholder="Label Name"></asp:TextBox>
                                                         <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_4" placeholder="Label Value"></asp:TextBox>
                                                       
                                                    </div>
                                                </div>
                                                 <div style="padding-bottom:40px"></div>
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label4 RegDetails</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_4" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_4" placeholder="Label RegValue"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                               <div style="padding-bottom: 60px"></div>
                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label5 Details</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_5" placeholder="Label Name"></asp:TextBox>
                                                         <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_5" placeholder="Label Value"></asp:TextBox>
                                                    </div>
                                                </div> 
                                                 <div style="padding-bottom:40px"></div>
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label5 RegDetails</label>
                                                    <div class="col-sm-10">
                        
                                                         <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_5" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_5" placeholder="Label RegValue"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                               <div style="padding-bottom: 60px"></div>
                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label6 Details </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_6" placeholder="Label Name"></asp:TextBox>
                                                       
                                                         <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_6" placeholder="Label Value"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 40px"></div>

                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label6 RegDetails</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_6" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_6" placeholder="Label RegValue"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                             <div style="padding-bottom: 60px"></div>
                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label7 Details </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_7" placeholder="Label Name"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_7" placeholder="Label Value"></asp:TextBox>       

                                                    </div>
                                                </div>
                                                <div style="padding-bottom: 40px"></div>
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label7 RegDetails</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_7" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_7" placeholder="Label RegValue"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div style="padding-bottom: 60px"></div>
                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label8 Details</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_8" placeholder="Label Name"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_8" placeholder="Label Value"></asp:TextBox>
                                                      
                                                    </div>
                                                </div>
                                                 <div style="padding-bottom: 40px"></div>
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label8 RegDetails</label>
                                                    <div class="col-sm-10">
                                                          <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_8" placeholder="Label RegName"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_8" placeholder="Label RegValue"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                             <div style="padding-bottom: 60px"></div>
                                              <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label9 Details</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_9" placeholder="Label Name"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_9" placeholder="Label Value"></asp:TextBox>
                                                    </div>
                                                </div>

                                                  <div style="padding-bottom: 40px"></div>
                                                  <div class="form-inline" role="form">
                                                      <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label9 RegDetails</label>
                                                      <div class="col-sm-10">
                                                           <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_9" placeholder="Label RegName"></asp:TextBox>
                                                          <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_9" placeholder="Label RegValue"></asp:TextBox>
                                                      </div>
                                                  </div>
                                            </div>

                                                <div style="padding-bottom: 60px"></div>
                                            <div class="form-group">
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label10 Details</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraField_10" placeholder="Name 10"></asp:TextBox>
                                                          <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValue_10" placeholder="Value 10"></asp:TextBox>
                                                      
                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 40px"></div>
                                                <div class="form-inline" role="form">
                                                    <label class="col-sm-2 col-sm-2 control-label" for="txtPhone1">Label10 RegDetails</label>
                                                    <div class="col-sm-10">
                                                         <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldReg_10" placeholder="Regional Name 10"></asp:TextBox>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraFieldValueReg_10" placeholder="Regional Value 10"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </section>
                                </div>
                            </div><%--row end--%>

                            <div class="row">
                                <div class="col-lg-12">
                                    <section class="panel" id="pnlInformationBusinessMasterPaymentsDetails">
                                        <header class="panel-heading">
                                            Payment  details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-up"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField6" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                           
                                        </header>

                                          <div class="panel-body collapse" >
                                               <div class="form-horizontal adminex-form">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 col-sm-2 control-label">Is Paid ? </label>
                                                        <div class="col-sm-10">
                                                             <asp:DropDownList ID="drIsPaid" runat="server" Font-Bold="True" AutoPostBack="true" CssClass="form-control m-bot15" OnSelectedIndexChanged="drIsPaid_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                <asp:ListItem Value="2" Selected="True" Text="No"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Amount Paid :  </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtAmount" name="txtAmount" runat="server" Enabled="false" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Amount paid" data-original-title="Amount paid"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">From Date :  </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtFromdate" name="txtFromdate" runat="server" Enabled="false" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Start date" data-original-title="Start date"></asp:TextBox>
                                                    </div>
                                                </div>
                                               <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">To Date :  </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtTodate" name="txtTodate" runat="server" Enabled="false" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="End date" data-original-title="End date"></asp:TextBox>
                                                    </div>
                                                </div>
                                                </div>
                                          </div>
                                     </section>
                                </div>
                            </div><%--row end--%>

                            <!--Added Extra Fields here-->
                            <div class="row">
                                <div class="col-lg-12">
                                    <section class="panel">
                                        <div class="panel-body" style="text-align:center">
                                            <button class="pnl-opener btn gbtn" type="button"
                                                            btn-action="New"  data-open-on="Save" data-open-panels="pnlInformationBusinessMaster,pnlInformationBusinessMasterDetails"
                                                            onserverclick="btnSave_ServerClick"
                                                            runat="server" id="btnSave">
                                                            <i class="fa fa-plus-square"></i> New
                                            </button>
                                            <button class="btn gbtn1" type="button"
                                                            runat="server" id="btnClear" onserverclick="btnClear_ServerClick" >
                                                            Clear <i class="fa fa-refresh"></i>
                                            </button>
<%--                                            <button class="pnl-opener btn btn-success" type="button"
                                                            btn-action="ImportFromExcel"  data-open-on="DeleteExcel" data-open-panels="pnlInformationBusinessMaster,pnlInformationBusinessMasterDetails"
                                                            onserverclick="btnImportToExcel_ServerClick"
                                                            runat="server" id="btnImportToExcel">
                                                            <i class="fa fa-upload"></i> Import From Excel
                                            </button>--%>
                                        </div>
                                        <div class="panel-body">
                                            <div class="alert toss" style="padding: 8px;" runat="server" id="actionInfo">
                                                Press new to add Information.
                                            </div>
                                        </div>
                                    </section> <!-- //panel -->
                                </div><!-- //Grid 12 -->
                            </div><!--//row buttons -->

                            <!-- Thumbnail view modal -->

                   
                            <!-- //Thumbnail view modal -->

                            <!-- //Naina's Add Product Code Here -->

                     


                        </div> <!--//Add products tab-->


                               <div class="tab-pane" id="importproducts">
                            <div class="row">
                                <section class="panel" id="pnlInformation">
                                    <header class="panel-heading">
                                        Information details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField3" EnableViewState="true" Value="o" />
                                                </span>
                                            </span>
                                    </header>
                                    <div class="panel-body">
                                        <div class="form-horizontal adminex-form">
                           
                               <div class="row">
                                <div class="col-lg-12">
                                    <section class="panel">
                                        <div class="panel-body">
               
                           <%--             </div>
                                         <div class="panel-body" >--%>
                                                <%-- <form class="form-inline" role="form">   --%>           
                                                     <%-- <div class="form-group">  
                                                           <div class="col-md-6">
                                                           <label>Select an Excel Sheet (MStoreInfo.xlxs) : </label>
                                                           </div> <%--col-sm-2 col-sm-2 control-label--%>
                                                            <%--<div class="col-md-12">
                                                                <asp:FileUpload ID="FileUploadControl1" runat="server" class="default" />
                                                            </div>
                                                      </div>--%>
                                                        <div class="form-group">
                                                            <label class="col-sm-2 col-sm-2 control-label">Select an Excel Sheet: </label>
                                                            <div class="col-sm-10">
                                                                <asp:FileUpload ID="FileUploadControl1" runat="server" class="default" />
                                                            </div>
                                                            <br />
                                                            <label style="color: red;">*Note : Excel named Information.csv</label>
                                                        </div>
                                            <div  class="form-group" style="text-align:center;">
                                              <button class="btn gbtn" type="button" 
                                                    data-toggle="modal" data-target="#modPasswordBox3"  btn-action-image="Class" 
                                                    runat="server" id="btnImportToExcel">
                                                    <i class="fa fa-folder-open"></i> Import From Excel
                                                </button>
                                            </div>
                                                 <%--</form>--%>
<%--                                             <button class="pnl-opener btn btn-success" type="button"
                                                            btn-action="ImportFromExcel"  data-open-on="DeleteExcel" data-open-panels="pnlInformationBusinessMaster,pnlInformationBusinessMasterDetails"
                                                            onserverclick="btnImportToExcel_ServerClick"
                                                            runat="server" id="btnImportToExcel">
                                                            <i class="fa fa-upload"></i> Import From Excel
                                            </button>--%>
                                        </div>
                                    </section> <!-- //panel -->
                                             <div class="alert toss" style="padding: 8px;" runat="server" id="Div2">
                                                Press to read data from excel sheet.
                                            </div>
                                </div><!-- //Grid 12 -->
                            </div><!--//row buttons -->


                                        </div>
                                    </div>
                                    </section>
                            </div>
                           </div>
                       

     <%-- Model for excel password box--%>
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modPasswordBox3" class="modal fade">
  <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                            <h4 class="modal-title">Information (Business)</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form class="form-horizontal" role="form" >                                                   
                                                       <div class="form-group">
                                                            <label for="inputPWd">Please Input Login Password to proceed : </label>                                                            
                                                            <asp:TextBox TextMode="Password" ID="txtPwdCode" AutoComplete="New-Password" name="txtPwdCode" class="form-control" runat="server" ></asp:TextBox><br /><br />
                                                           <label runat="server" id="lblMsgError" style="color:red;" visible="false">* Password is mandatory for upload.</label>
                                                      
                                                      <div class="col-lg-12" style="text-align:center;">
                                                                        <button class="btn gbtn" type="button"
                                                                        runat="server" id="btnImportonPassword"  onserverclick="btnImportonPassword_ServerClick" >
                                                                        <i class="fa fa-upload"></i> Upload
                                                                        </button>
                                                      </div>    
                                                     </div>                                          
                                            </form>
                                        </div>
                                    </div>
      </div>
      </div>
    <%--End model for excel password box--%>
                    
                                                <!--Select image form modal-->
                                     <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myFileModal" class="modal fade">
                                        <div class="modal-dialog" style="width:680px;">
                                            <div class="modal-content" style="height:200px;">
                                                <div class="modal-header">
                                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                                    <h4 class="modal-title"><label id="lblImageFormTile" runat="server">Browse Excel Sheet</label></h4>
                                                </div>
                                               
                                                 <div class="modal-body">
                                                     <form class="form-horizontal" role="form">
                                                     <div class="form-group">               
                                                       <div class="col-md-4">
                                                          <%--<input id="fileupload" type="file" class="default"  runat="server"/>--%>
                                                           <asp:FileUpload ID="FileUploadControl" runat="server" class="default" />
                                                       <%--    <asp:FileUpload ID="fileuploadimages" runat="server"  />--%>
                                                           <%--<input type="file" multiple="multiple" name="fileuploadimages" id="fileuploadimages" runat="server" />--%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">  
                                                                 <div class="row">
                                                                    <div class="panel-body" style="text-align:right">
                                                                        <button id="btnRead" type="submit" class="btn btn-primary" runat="server" onserverclick="btnRead_ServerClick" data-toggle="modal" data-target="modSendingMessage" >Read</button>
                                                                    </div>
                                                                 </div>
                                                      </div>
                                                         
                                                         <!--//row buttons -->

                                                       </form>
                                                    </div>
                                             </div>
                                        </div>
                                        </div>
                          <!--End of Select image form modal-->


                                                <!--Begin of image loading form-->

       <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modSendingMessage" class="modal fade" style="width:100%;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Details Reading in progress...</h4>
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
                        <!--End of image loading form-->

                        <!--//Modify Product tab-->
                        <div class="tab-pane" id="modifyproducts">
                            <div class="form-group">
                                <div class="alert toss" runat="server" id="updateActionDivDis">
                                    Click on respective Modify / Delete Information.
                                </div>
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdInfo" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    DataKeyNames="Info_Type"
                                    OnRowEditing="grdInfo_RowEditing"
                                    OnRowUpdating="grdInfo_RowUpdating"
                                    OnRowCancelingEdit="grdInfo_RowCancelingEdit"
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfoID" runat="server" Text='<%# Eval("Info_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cat Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCatName" runat="server" Visible ="true"  Text='<%# Eval("Cat_Name")  %>' ></asp:Label>
                                               <%--  <asp:DropDownList ID="cmbCategory" runat="server" Font-Bold="True" AutoPostBack="true"  OnSelectedIndexChanged="cmbCategory_SelectedIndexChanged"  Visible ="false"
                                                    CssClass="form-control m-bot15">
                                                </asp:DropDownList>--%>
                                            </ItemTemplate>

                                         <%--    <EditItemTemplate>
                                               <asp:DropDownList ID="cmbCategory" runat="server" Font-Bold="True" AutoPostBack="true"  OnSelectedIndexChanged="cmbCategory_SelectedIndexChanged" 
                                                    CssClass="form-control m-bot15">
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sub Cat Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCatName" runat="server" Text='<%# Eval("Sub_CatName") %>'></asp:Label>
                                               <%-- <asp:DropDownList ID="cmbSubCategory" runat="server" Font-Bold="True" Visible ="false"
                                                    CssClass="form-control m-bot15">
                                                </asp:DropDownList>--%>

                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:DropDownList ID="cmbSubCategory" runat="server" Font-Bold="True"
                                                    CssClass="form-control m-bot15">
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfoName" runat="server" Text='<%# Eval("Info_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInfoName" runat="server" Text='<%# Bind("Info_Name") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Reg Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfoRegName" runat="server" Text='<%# Eval("Info_RegName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInfoRegName" runat="server" Text='<%# Bind("Info_RegName") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                         <%--<asp:TemplateField HeaderText="City">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("Info_City") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInfoCity" runat="server" Text='<%# Bind("Info_City") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>


                                         <%--<asp:TemplateField HeaderText="Reg City">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfoRegCity" runat="server" Text='<%# Eval("Info_RegCity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInfoRegCity" runat="server" Text='<%# Bind("Info_RegCity") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                      <%--  <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfoAdd" runat="server" Text='<%# Eval("Info_Add") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInfoAdd" runat="server" Text='<%# Bind("Info_Add") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>


                                     <%--   <asp:TemplateField HeaderText="Reg Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfoRegAdd" runat="server" Text='<%# Eval("Info_AddRegName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInfoGrdRegAdd" runat="server" Text='<%# Bind("Info_AddRegName") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                       <%-- <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Phone1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone1" runat="server" Text='<%# Eval("Info_Phone1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPhone1" runat="server" Text='<%# Bind("Info_Phone1") %>'>
                                                </asp:TextBox>
                                                  <asp:RegularExpressionValidator ID="regrdChkPhone1" ControlToValidate="txtPhone1" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

<%--                                        <asp:TemplateField HeaderText="Phone2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone2" runat="server" Text='<%# Eval("Info_Phone2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPhone2" runat="server" Text='<%# Bind("Info_Phone2") %>'>
                                                </asp:TextBox>
                                                  <asp:RegularExpressionValidator ID="reggrdChkPhone2" ControlToValidate="txtPhone2" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone3" runat="server" Text='<%# Eval("Info_Phone3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPhone3" runat="server" Text='<%# Bind("Info_Phone3") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="reggrdChkPhone3" ControlToValidate="txtPhone3" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                       <%-- <asp:TemplateField HeaderText="Longitude">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLongitude" runat="server" Text='<%# Eval("Info_Longitude") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLongitude" runat="server" Text='<%# Bind("Info_Longitude") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="reggrdChkLongitude" ControlToValidate="txtLongitude" runat="server" ErrorMessage="Only Decimal Numbers allowed" ValidationExpression="((\d+)+(\.\d+))$"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                      <%--  <asp:TemplateField HeaderText="Latitude">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLatitude" runat="server" Text='<%# Eval("Info_Latitude") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLatitude" runat="server" Text='<%# Bind("Info_Latitude") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="reggrdChkLatitude" ControlToValidate="txtLatitude" runat="server" ErrorMessage="Only Decimal Numbers allowed" ValidationExpression="((\d+)+(\.\d+))$"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PinCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPinCode" runat="server" Text='<%# Eval("IM_vCharPincode_En") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPinCode" runat="server" Text='<%# Bind("IM_vCharPincode_En") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="reggrdPinCode" ControlToValidate="txtPinCode" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                      <%--  <asp:TemplateField HeaderText="Reg PinCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegPinCode" runat="server" Text='<%# Eval("IM_nVarPincode_Reg") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRegPinCode" runat="server" Text='<%# Bind("IM_nVarPincode_Reg") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                     <%--   <asp:TemplateField HeaderText="Lable1 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel1" runat="server" Text='<%# Eval("IM_ExtraLabel1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel1" runat="server" Text='<%# Bind("IM_ExtraLabel1") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lable1 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal1" runat="server" Text='<%# Eval("IM_ExtraVal1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal1" runat="server" Text='<%# Bind("IM_ExtraVal1") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>



                                   <%--     <asp:TemplateField HeaderText="Lable1 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg1" runat="server" Text='<%# Eval("IM_RegExtraLabel1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg1" runat="server" Text='<%# Bind("IM_RegExtraLabel1") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                    --%>

                                      <%--  <asp:TemplateField HeaderText="Lable1 RegValue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal1" runat="server" Text='<%# Eval("IM_ExtraValReg1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal1" runat="server" Text='<%# Bind("IM_ExtraValReg1") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable2 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel2" runat="server" Text='<%# Eval("IM_ExtraLabel2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel2" runat="server" Text='<%# Bind("IM_ExtraLabel2") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>


                                         <%--  <asp:TemplateField HeaderText="Lable2 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal2" runat="server" Text='<%# Eval("IM_ExtraVal2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal2" runat="server" Text='<%# Bind("IM_ExtraVal2") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable2 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg2" runat="server" Text='<%# Eval("IM_RegExtraLabel2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg2" runat="server" Text='<%# Bind("IM_RegExtraLabel2") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                     
<%--                                        <asp:TemplateField HeaderText="Lable2 ValueReg">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal2" runat="server" Text='<%# Eval("IM_ExtraValReg2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal2" runat="server" Text='<%# Bind("IM_ExtraValReg2") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable3 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel3" runat="server" Text='<%# Eval("IM_ExtraLabel3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel3" runat="server" Text='<%# Bind("IM_ExtraLabel3") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>


                                        <%--  <asp:TemplateField HeaderText="Lable3 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal3" runat="server" Text='<%# Eval("IM_ExtraVal3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal3" runat="server" Text='<%# Bind("IM_ExtraVal3") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable3 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg3" runat="server" Text='<%# Eval("IM_RegExtraLabel3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg3" runat="server" Text='<%# Bind("IM_RegExtraLabel3") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                      

                                      <%--  <asp:TemplateField HeaderText="Lable3 RegValue ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal3" runat="server" Text='<%# Eval("IM_ExtraValReg3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal3" runat="server" Text='<%# Bind("IM_ExtraValReg3") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable4 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel4" runat="server" Text='<%# Eval("IM_ExtraLabel4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel4" runat="server" Text='<%# Bind("IM_ExtraLabel4") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                         <%--   <asp:TemplateField HeaderText="Lable4 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal4" runat="server" Text='<%# Eval("IM_ExtraVal4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal4" runat="server" Text='<%# Bind("IM_ExtraVal4") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable4 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg4" runat="server" Text='<%# Eval("IM_RegExtraLabel4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg4" runat="server" Text='<%# Bind("IM_RegExtraLabel4") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>
   
<%--
                                        <asp:TemplateField HeaderText="Lable4 RegValue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal4" runat="server" Text='<%# Eval("IM_ExtraValReg4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal4" runat="server" Text='<%# Bind("IM_ExtraValReg4") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable5 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel5" runat="server" Text='<%# Eval("IM_ExtraLabel5")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel5" runat="server" Text='<%# Bind("IM_ExtraLabel5") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Lable5 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal5" runat="server" Text='<%# Eval("IM_ExtraVal5") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal5" runat="server" Text='<%# Bind("IM_ExtraVal5") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

<%--                                        <asp:TemplateField HeaderText="Lable5 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg5" runat="server" Text='<%# Eval("IM_RegExtraLabel5") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg5" runat="server" Text='<%# Bind("IM_RegExtraLabel5") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable5 RegValue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal5" runat="server" Text='<%# Eval("IM_ExtraValReg5") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal5" runat="server" Text='<%# Bind("IM_ExtraValReg5") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable6 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel6" runat="server" Text='<%# Eval("IM_ExtraLabel6") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel6" runat="server" Text='<%# Bind("IM_ExtraLabel6") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable6 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal6" runat="server" Text='<%# Eval("IM_ExtraVal6") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal6" runat="server" Text='<%# Bind("IM_ExtraVal6") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable6 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg6" runat="server" Text='<%# Eval("IM_RegExtraLabel6") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg6" runat="server" Text='<%# Bind("IM_RegExtraLabel6") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                       
                                        <asp:TemplateField HeaderText="Lable6 RegValue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal6" runat="server" Text='<%# Eval("IM_ExtraValReg6") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal6" runat="server" Text='<%# Bind("IM_ExtraValReg6") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable7 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel7" runat="server" Text='<%# Eval("IM_ExtraLabel7") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel7" runat="server" Text='<%# Bind("IM_ExtraLabel7") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Lable7 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal7" runat="server" Text='<%# Eval("IM_ExtraVal7") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal7" runat="server" Text='<%# Bind("IM_ExtraVal7") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable7 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg7" runat="server" Text='<%# Eval("IM_RegExtraLabel7") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg7" runat="server" Text='<%# Bind("IM_RegExtraLabel7") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>       

                                        <asp:TemplateField HeaderText="Lable7 RegValue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal7" runat="server" Text='<%# Eval("IM_ExtraValReg7") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal7" runat="server" Text='<%# Bind("IM_ExtraValReg7") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Label8 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel8" runat="server" Text='<%# Eval("IM_ExtraLabel8") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel8" runat="server" Text='<%# Bind("IM_ExtraLabel8") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable8 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal8" runat="server" Text='<%# Eval("IM_ExtraVal8") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal8" runat="server" Text='<%# Bind("IM_ExtraVal8") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable8 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg8" runat="server" Text='<%# Eval("IM_RegExtraLabel8") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg8" runat="server" Text='<%# Bind("IM_RegExtraLabel8") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable8 RegVal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal8" runat="server" Text='<%# Eval("IM_ExtraValReg8") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal8" runat="server" Text='<%# Bind("IM_ExtraValReg8") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable9 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel9" runat="server" Text='<%# Eval("IM_ExtraLabel9") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel9" runat="server" Text='<%# Bind("IM_ExtraLabel9") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lable9 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal9" runat="server" Text='<%# Eval("IM_ExtraVal9") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal9" runat="server" Text='<%# Bind("IM_ExtraVal9") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Lable9 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg9" runat="server" Text='<%# Eval("IM_RegExtraLabel9") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg9" runat="server" Text='<%# Bind("IM_RegExtraLabel9") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                    
                                        <asp:TemplateField HeaderText="Label9 RegVal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal9" runat="server" Text='<%# Eval("IM_ExtraValReg9") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal9" runat="server" Text='<%# Bind("IM_ExtraValReg9") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable10 Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabel10" runat="server" Text='<%# Eval("IM_ExtraLabel10 ")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabel10" runat="server" Text='<%# Bind("IM_ExtraLabel10") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Lable10 Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelVal10" runat="server" Text='<%# Eval("IM_ExtraVal10") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelVal10" runat="server" Text='<%# Bind("IM_ExtraVal10") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Lable10 RegName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelReg10" runat="server" Text='<%# Eval("IM_RegExtraLabel10") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelReg10" runat="server" Text='<%# Bind("IM_RegExtraLabel10") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lable10  RegVal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtraLabelRegVal10" runat="server" Text='<%# Eval("IM_ExtraValReg10") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExtraLabelRegVal10" runat="server" Text='<%# Bind("IM_ExtraValReg10") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                     <%--   <asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A2" class="edit" 
                                                    data-info-id='<%# Eval("Info_ID") %>'
                                                    data-info-name='<%# Eval("Info_Name") %>'
                                                    data-info-regname='<%# Eval("Info_RegName") %>'
                                                    data-info-city='<%# Eval("Info_City") %>'
                                                    data-info-regcity='<%# Eval("Info_RegCity ")%>'
                                                    data-info-add='<%# Eval("Info_Add") %>'
                                                    data-info-regadd='<%# Eval("Info_AddRegName") %>'
                                                    data-info-email='<%# Eval("Email") %>'
                                                    data-info-phone1='<%# Eval("Info_Phone1") %>'
                                                    data-info-phone2='<%# Eval("Info_Phone2") %>'
                                                    data-info-phone3='<%# Eval("Info_Phone3") %>'
                                                    data-info-lat='<%# Eval("Info_Longitude") %>'
                                                    data-info-long='<%# Eval("Info_Latitude") %>'
                                                    data-info-pincode='<%# Eval("IM_vCharPincode_En") %>'
                                                    data-info-regpincode='<%# Eval("IM_nVarPincode_Reg") %>'
                                                    data-info-extralabel1='<%# Eval("IM_ExtraLabel1") %>'
                                                    data-info-extraval1='<%# Eval("IM_ExtraVal1") %>'
                                                    data-info-regextralabel1='<%# Eval("IM_RegExtraLabel1") %>'
                                                    data-info-regextraval1='<%# Eval("IM_ExtraValReg1") %>'
                                                    data-info-extralabel2='<%# Eval("IM_ExtraLabel2") %>'
                                                    data-info-extraval2='<%# Eval("IM_ExtraVal2") %>'
                                                    data-info-regextralabel2='<%# Eval("IM_RegExtraLabel2") %>'
                                                    data-info-regextraval2='<%# Eval("IM_ExtraValReg2") %>'
                                                    data-info-extralabel3='<%# Eval("IM_ExtraLabel3") %>'
                                                    data-info-extraval3='<%# Eval("IM_ExtraVal3") %>'
                                                    data-info-regextralabel3='<%# Eval("IM_RegExtraLabel3") %>'
                                                    data-info-regextraval3='<%# Eval("IM_ExtraValReg3") %>'
                                                    data-info-extralabel4='<%# Eval("IM_ExtraLabel4") %>'
                                                    data-info-extraval4='<%# Eval("IM_ExtraVal4") %>'
                                                    data-info-regextralabel4='<%# Eval("IM_RegExtraLabel4") %>'
                                                    data-info-regextraval4='<%# Eval("IM_ExtraValReg4") %>'
                                                    data-info-extralabel5='<%# Eval("IM_ExtraLabel5") %>'
                                                    data-info-extraval5='<%# Eval("IM_ExtraVal5") %>'
                                                    data-info-regextralabel5='<%# Eval("IM_RegExtraLabel5") %>'
                                                    data-info-regextraval5='<%# Eval("IM_ExtraValReg5") %>'
                                                    data-info-extralabel6='<%# Eval("IM_ExtraLabel6") %>'
                                                    data-info-extraval6='<%# Eval("IM_ExtraVal6") %>'
                                                    data-info-regextralabel6='<%# Eval("IM_RegExtraLabel6") %>'
                                                    data-info-regextraval6='<%# Eval("IM_ExtraValReg6") %>'
                                                    data-info-extralabel7='<%# Eval("IM_ExtraLabel7") %>'
                                                    data-info-extraval7='<%# Eval("IM_ExtraVal7") %>'
                                                    data-info-regextralabel7='<%# Eval("IM_RegExtraLabel7") %>'
                                                    data-info-regextraval7='<%# Eval("IM_ExtraValReg7") %>'
                                                    data-info-extralabel8='<%# Eval("IM_ExtraLabel8") %>'
                                                    data-info-extraval8='<%# Eval("IM_ExtraVal8") %>'
                                                    data-info-regextralabel8='<%# Eval("IM_RegExtraLabel8") %>'
                                                    data-info-regextraval8='<%# Eval("IM_ExtraValReg8") %>'
                                                    data-info-extralabel9='<%# Eval("IM_ExtraLabel9") %>'
                                                    data-info-extraval9='<%# Eval("IM_ExtraVal9") %>'
                                                    data-info-regextralabel9='<%# Eval("IM_RegExtraLabel9") %>'
                                                    data-info-regextraval9='<%# Eval("IM_ExtraValReg9") %>'
                                                    data-info-extralabel10='<%# Eval("IM_ExtraLabel10") %>'
                                                    data-info-extraval10='<%# Eval("IM_ExtraVal10") %>'
                                                    data-info-regextralabel10='<%# Eval("IM_RegExtraLabel10") %>'
                                                    data-info-regextraval10='<%# Eval("IM_ExtraValReg10") %>'
                                                    data-info-cat='<%# Eval("Cat_ID") %>'
                                                    data-info-subcat='<%# Eval("SubCat_ID") %>'
                                                    data-info-infourl='<%# Eval("IM_vCharUrl") %>'
                                                   <%-- data-info-ispaidtype='<%# Eval("IM_bitIsPaid") %>'
                                                    data-info-modifyamt='<%# Eval("IM_decAmt") %>'
                                                    data-info-modifyfrmdate='<%# Eval("IM_dtFromDate") %>'
                                                    data-info-modifytodate='<%# Eval("IM_dtToDate") %>'--%>
                                                    data-info-rowindex= '<%# ((GridViewRow) Container).RowIndex %>'
                                                    href="javascript:;">Edit</a>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-info-id='<%# Eval("Info_ID") %>'
                                                    data-info-name='<%# Eval("Info_Name") %>' href="javascript:;">Delete</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteInformation" class="modal fade">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                            <h4 class="modal-title">Delete the Information.</h4>
                                        </div>
                                        <div class="modal-body">

                                            <div role="form">
                                                <div class="form-group">
                                                    <h3>Are you sure you want to delete Information with following details?</h3>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2 col-sm-2 control-label">ID </label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="txtDelInfoID" name="txtDelInfoID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            <asp:HiddenField ID="txtInfoHidden" runat="server" />
                                                        </div>
                                                    </div>
                                                </div> <!-- Id -->
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2 col-sm-2 control-label">Info Name </label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="txtDelInfoName" name="txtDelInfoName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div> <!-- Name -->
                                          
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="panel-body" style="text-align: center">
                                                            <button class="btn gbtn" type="button"
                                                                runat="server" id="btnDeleteInfo" onserverclick="btnDeleteInfo_ServerClick"> 
                                                                <i class="fa fa-trash"></i> Delete Category
                                                            </button>
                                                            <button class="btn gbtn1" type="button"
                                                                id="btnCancelDeleteInfo">
                                                                Cancel <i class="fa fa-times-circle"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div> <!-- Action Buttons -->

                                            </div><!-- //form -->
                                        </div><!-- //modal body -->
                                    </div> <!-- //modal content -->
                                </div> <!-- //modal dialog -->
                            </div> <!-- //dialog -->
                        </div> <!-- Modify product tab-->
                    </div>
                </div>
            </section> <!-- Panel Body Main -->
        </div> <!-- Main Row -->

        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modEditInformation" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--  <button aria-hidden="true" data-dismiss="modal" class="close" type="button"></button>--%>
                        <h4 class="modal-title">Modify  Information</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-inline" role="form">
                            <section class="panel">
                                <div class="panel-body">
                                    <div class="col-lg-12" >
                                        <div class="alert toss" runat="server" id="updateActionDiv" >
                                            Modify Information.
                                        </div>
                                    </div>


                                    <div class="row">
                                        
                                        <section class="panel">
                                            <header class="panel-heading">
                                                Information  Details
							                                        <span class="tools pull-right">
                                                                        <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                                        <span class="collapsible-server-hidden">
                                                                            <asp:HiddenField runat="server" ID="HiddenField4" EnableViewState="true" Value="c" />
                                                                        </span>
                                                                    </span>

                                            </header>
                                            <div class="panel-body">

                                                <div class="form-inline" role="form">



                                                    <asp:HiddenField ID="HiddenFieldForDialogOpenClose" runat="server" Value="c" />
                                                    <asp:HiddenField ID="HiddenSubCat" runat="server" />

                                                    <div class="form-group">


                                                        <div class="row">
                                                            <label style="margin-left: 20px; font: bolder">ID </label>
                                                            <asp:TextBox Style="margin-left: 85px" ID="txtEditInfoID" name="txtEditInfoID" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="HiddenFieldInfo" runat="server" />

                                                        </div>
                                                    </div>

                                                    <div style="padding-bottom: 15px"></div>

                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Category</label>
                                                                <asp:DropDownList Style="width: 300px" ID="drpCategorySelect" AutoPostBack="true" OnSelectedIndexChanged="drpCategorySelect_SelectedIndexChanged" runat="server" Font-Bold="True"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <label style="margin-left: 68px">Sub Cat</label>
                                                                <asp:DropDownList Style="margin-left: 40px; width: 300px" ID="drpSubCategory" runat="server" Font-Bold="True"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <%--  <div class="form-group">
                                                                                    <label class="col-sm-2 col-sm-2 control-label" for="drpCategorySelect">Category</label> 
                                                                                     <asp:DropDownList ID="drpCategorySelect" runat="server" Font-Bold="True" 
                                                                                    CssClass="form-control">
                                                                                </asp:DropDownList> 
                                                                             </div>--%>

                                                    <%--      <div class="form-group">
                                                                                    <label class="col-sm-2 col-sm-2 control-label" for="drpSubCategory">Category</label> 
                                                                                     <asp:DropDownList ID="drpSubCategory" runat="server" Font-Bold="True" 
                                                                                    CssClass="form-control">
                                                                                </asp:DropDownList> 
                                                                             </div>--%>


                                                    <!-- Id -->
                                                    <div style="padding-bottom: 50px"></div>

                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Name</label>
                                                                <asp:TextBox Style="width: 300px" ID="txtEditInfoName"  name="txtEditInfoName" runat="server" CssClass="form-control"></asp:TextBox>

                                                                <label class="lblleftPos" style="margin-left: 68px">Reg Name </label>
                                                                <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditRegInfoName" name="txtEditRegInfoName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Name </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoName" name="txtEditInfoName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                    <!-- Name -->

                                                    <%--  <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Name </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditRegInfoName" name="txtEditRegInfoName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                    <!-- Name -->
                                                    <div style="padding-bottom: 50px"></div>

                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">City</label>
                                                                <asp:TextBox Style="width: 300px" ID="txtEditInfoCity" name="txtEditInfoCity" runat="server" CssClass="form-control"></asp:TextBox>

                                                                <label class="lblleftPos" style="margin-left: 68px">Reg City</label>
                                                                <asp:TextBox Style="margin-left: 20px; width: 300px" ID="txtEditInfoRegCity" name="txtEditInfoRegCity" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>


                                                    <%--    <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">City </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoCity" name="txtEditInfoCity" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!-- Name -->--%>


                                                    <%-- <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg City </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegCity" name="txtEditInfoRegCity" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                    <!-- Name -->
                                                    <div style="padding-bottom: 50px"></div>

                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Add</label>
                                                                <asp:TextBox Style="width: 300px" ID="txtEditInfoAdd" name="txtEditInfoAdd" runat="server" CssClass="form-control"></asp:TextBox>

                                                                <label class="lblleftPos" style="margin-left: 68px">Reg Add</label>
                                                                <asp:TextBox Style="margin-left: 20px; width: 300px" ID="txtEditInfoRegAdd" name="txtEditInfoRegAdd" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>





                                                    <%--  <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Add </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoAdd" name="txtEditInfoAdd" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                    <!-- Name -->

                                                    <%--<div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Add </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegAdd" name="txtEditInfoRegAdd" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                    <!-- Name -->
                                                    <div style="padding-bottom: 50px"></div>
                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Email</label>
                                                                <asp:TextBox Style="width: 300px" ID="txtEditInfoEmail" name="txtEditInfoEmail" runat="server" CssClass="form-control"></asp:TextBox>

                                                                
                                                                 <label  class="lblleftPos" style="margin-left: 68px">Info Type</label>                                                
                                                                <asp:HiddenField ID="hfInfotype" runat="server" />  
                                                                    <asp:DropDownList ID="drdModifyInfoType" runat="server"  Font-Bold="True" AutoPostBack="False"  OnSelectedIndexChanged="drdModifyInfoType_SelectedIndexChanged"
                                                                        CssClass="form-control" Style="margin-left:20px;width: 300px">
                                                                    </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div style="padding-bottom: 50px"></div>
                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Phone</label>
                                                                <asp:TextBox Style="width: 200px" ID="txtEditInfoPhone1" name="txtEditInfoPhone1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox Style="margin-left: 30px" ID="txtEditInfoPhone2" name="txtEditInfoPhone2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox Style="margin-left: 30px" ID="txtEditInfoPhone3" name="txtEditInfoPhone3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEditInfoPhone3" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEditInfoPhone1" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtEditInfoPhone2" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>

                                                            </div>
                                                        </div>
                                                    </div>


                                                    <%-- <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Email </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoEmail" name="txtEditInfoEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!-- Name -->

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Phone 1 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoPhone1" name="txtEditInfoPhone1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                    <!-- Name -->


                                                    <%--  <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Phone 2 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoPhone2" name="txtEditInfoPhone2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                    <%-- <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Phone 3 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoPhone3" name="txtEditInfoPhone3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                                    <div style="padding-bottom: 50px"></div>
                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Longitude</label>
                                                                <asp:TextBox Style="width: 300px" ID="txtEditInfoLongitude" name="txtEditInfoLongitude" runat="server" CssClass="form-control"></asp:TextBox>
                                                        
                                                                <label class="lblleftPos" style="margin-left: 68px">Latitude</label>
                                                                <asp:TextBox Style="margin-left: 20px; width: 300px" ID="txtEditInfoLatitude" name="txtEditInfoLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEditInfoLongitude" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="((\d+)+(\.\d+))$"></asp:RegularExpressionValidator>  
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtEditInfoLatitude" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="((\d+)+(\.\d+))$"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div style="padding-bottom: 50px"></div>
                                                    <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Pin Code</label>
                                                                <asp:TextBox Style="width: 300px" ID="txtEditInfoPinCode" name="txtEditInfoPinCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                              
                                                                <label class="lblleftPos" style="margin-left: 68px">Reg Pin </label>
                                                                <asp:TextBox Style="margin-left: 20px; width: 300px" ID="txtEditInfoRegPinCode" name="txtEditInfoRegPinCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtEditInfoPinCode" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                   <div style="padding-bottom: 30px"></div>
                                                   <div class="col-lg-12" style="vertical-align: middle">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="lblPos">Website URL</label>
                                                                <asp:TextBox Style="width: 500px" ID="txtModifyUrl" name="txtModifyUrl" runat="server" CssClass="form-control"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                    </div>


                                                <%--  <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Longitude </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoLongitude" name="txtEditInfoLongitude" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                <%--                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Latitude </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoLatitude" name="txtEditInfoLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%-- <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">PinCode </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoPinCode" name="txtEditInfoPinCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%-- <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg PinCode </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegPinCode" name="txtEditInfoRegPinCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                            </div>
                                        </section>
                                    </div>
                            <%--    </div>--%>

                                <div class="row">
                                    <section class="panel">
                                        <header class="panel-heading">
                                            Additional  Details
							                                        <span class="tools pull-right">
                                                                        <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                                        <span class="collapsible-server-hidden">
                                                                            <asp:HiddenField runat="server" ID="HiddenField5" EnableViewState="true" Value="c" />
                                                                        </span>
                                                                    </span>
                                        </header>

                                        <div class="panel-body">
                                            <div class="form-inline" role="form">


                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 1</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel1" Style="width: 300px" name="txtEditInfoExtraLabel1" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px" >Value 1 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal1" name="txtEditInfoExtraVal1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 1</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel1" Style="width: 300px" name="txtEditInfoRegExtraLabel1" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 1 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal1" name="txtEditInfoRegExtraVal1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 2</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel2" Style="width: 300px" name="txtEditInfoExtraLabel2" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 2 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal2" name="txtEditInfoExtraVal2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 2</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel2" Style="width: 300px" name="txtEditInfoRegExtraLabel2" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 2 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal2" name="txtEditInfoRegExtraVal2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>



                                                <%--   <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 1 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel1" name="txtEditInfoExtraLabel1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%--         <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 1 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal1" name="txtEditInfoExtraVal1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%--<div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 1 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel1" name="txtEditInfoRegExtraLabel1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 1 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal1" name="txtEditInfoRegExtraVal1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>



                                                <%--   <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 2 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel2" name="txtEditInfoExtraLabel2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                <%-- <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 2 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal2" name="txtEditInfoExtraVal2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%--                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 2 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel2" name="txtEditInfoRegExtraLabel2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                <%--  <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 2 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal2" name="txtEditInfoRegExtraVal2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 3</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel3" Style="width: 300px" name="txtEditInfoExtraLabel3" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 3 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal3" name="txtEditInfoExtraVal3" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 3</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel3" Style="width: 300px" name="txtEditInfoRegExtraLabel3" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 3 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal3" name="txtEditInfoRegExtraVal3" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 4</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel4" Style="width: 300px" name="txtEditInfoExtraLabel4" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 4 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal4" name="txtEditInfoExtraVal4" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 4</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel4" Style="width: 300px" name="txtEditInfoRegExtraLabel4" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 4 </label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal4" name="txtEditInfoRegExtraVal4" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>





                                                <%-- <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 3 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel3" name="txtEditInfoExtraLabel3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 3 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal3" name="txtEditInfoExtraVal3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%--  <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 3 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel3" name="txtEditInfoRegExtraLabel3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 3 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal3" name="txtEditInfoRegExtraVal3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%--
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 4 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel4" name="txtEditInfoExtraLabel4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 4 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal4" name="txtEditInfoExtraVal4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 4 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel4" name="txtEditInfoRegExtraLabel4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 3 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal4" name="txtEditInfoRegExtraVal4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>


                                                <%--
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 5 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel5" name="txtEditInfoExtraLabel5" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 5 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal5" name="txtEditInfoExtraVal5" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 5 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel5" name="txtEditInfoRegExtraLabel5" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 5 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal5" name="txtEditInfoRegExtraVal5" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>



                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 5</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel5" Style="width: 300px" name="txtEditInfoExtraLabel5" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 5</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal5" name="txtEditInfoExtraVal5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 5</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel5" Style="width: 300px" name="txtEditInfoRegExtraLabel5" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 5</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal5" name="txtEditInfoRegExtraVal5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 6</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel6" Style="width: 300px" name="txtEditInfoExtraLabel6" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 6</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal6" name="txtEditInfoExtraVal6" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 6</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel6" Style="width: 300px" name="txtEditInfoRegExtraLabel6" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 6</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal6" name="txtEditInfoRegExtraVal6" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 7</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel7" Style="width: 300px" name="txtEditInfoExtraLabel7" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos"  style="margin-left: 68px">Value 7</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal7" name="txtEditInfoExtraVal7" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 7</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel7" Style="width: 300px" name="txtEditInfoRegExtraLabel7" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos"  style="margin-left: 68px">Value 7</label>
                                                            <asp:TextBox class="txtEditInfoRegExtraVal7" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal7" name="txtEditInfoRegExtraVal7" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 8</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel8" Style="width: 300px" name="txtEditInfoExtraLabel8" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 8</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal8" name="txtEditInfoExtraVal8" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 8</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel8" Style="width: 300px" name="txtEditInfoRegExtraLabel8" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 8</label>
                                                            <asp:TextBox class="txtEditInfoRegExtraVal8" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal8" name="txtEditInfoRegExtraVal8" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 9</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel9" Style="width: 300px" name="txtEditInfoExtraLabel9" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 9</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal9" name="txtEditInfoExtraVal9" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 9</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel9" Style="width: 300px" name="txtEditInfoRegExtraLabel9" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 9</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal9" name="txtEditInfoRegExtraVal9" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 50px"></div>

                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Label 10</label>
                                                            <asp:TextBox ID="txtEditInfoExtraLabel10" Style="width: 300px" name="txtEditInfoExtraLabel10" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 10</label>
                                                            <asp:TextBox class="txtleft" Style="width: 300px; margin-left: 20px" ID="txtEditInfoExtraVal10" name="txtEditInfoExtraVal10" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div style="padding-bottom: 50px"></div>
                                                <div class="col-lg-12" style="vertical-align: middle">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="lblPos">Reg Label 10</label>
                                                            <asp:TextBox ID="txtEditInfoRegExtraLabel10" Style="width: 300px" name="txtEditInfoRegExtraLabel10" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label class="lblleftPos" style="margin-left: 68px">Value 10</label>
                                                            <asp:TextBox class="txtEditInfoRegExtraVal10" Style="width: 300px; margin-left: 20px" ID="txtEditInfoRegExtraVal10" name="txtEditInfoRegExtraVal10" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 6 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel6" name="txtEditInfoExtraLabel6" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 6 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal6" name="txtEditInfoExtraVal6" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 6 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel6" name="txtEditInfoRegExtraLabel6" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 6 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal6" name="txtEditInfoRegExtraVal6" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                <%--                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 7 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel7" name="txtEditInfoExtraLabel7" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 7 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal7" name="txtEditInfoExtraVal7" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 7 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel7" name="txtEditInfoRegExtraLabel7" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 7 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal7" name="txtEditInfoRegExtraVal7" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                <%--                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 8 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel8" name="txtEditInfoExtraLabel8" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 8 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal8" name="txtEditInfoExtraVal8" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 8 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel8" name="txtEditInfoRegExtraLabel8" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 8 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal8" name="txtEditInfoRegExtraVal8" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 9 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel9" name="txtEditInfoExtraLabel9" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 9 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal9" name="txtEditInfoExtraVal9" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 9 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel9" name="txtEditInfoRegExtraLabel9" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 9 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal9" name="txtEditInfoRegExtraVal9" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Label 10 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraLabel10" name="txtEditInfoExtraLabel10" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Extra Value 10 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoExtraVal10" name="txtEditInfoExtraVal10" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Label 10 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraLabel10" name="txtEditInfoRegExtraLabel10" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-2 col-sm-2 control-label">Reg Value 10 </label>
                                                                                <div class="col-sm-10">
                                                                                    <asp:TextBox ID="txtEditInfoRegExtraVal10" name="txtEditInfoRegExtraVal10" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>
                                            </div>
                                        </div>
                                    </section>
                                </div>

                                 <div class="row">
                                <%--<div class="col-lg-12">--%>
                                    <section class="panel">
                                        <header class="panel-heading">
                                            Payment  details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-up"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField7" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                           
                                        </header>

                                          <div class="panel-body collapse">
                                              <div class="form-horizontal adminex-form">  
                                                   <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                      <label class="lblPos">Is Paid ? </label>                                                                     
                                                                      <asp:DropDownList ID="drModifyIsPaid" runat="server" Font-Bold="True" AutoPostBack="true" CssClass="form-control m-bot15" OnSelectedIndexChanged="drModifyIsPaid_SelectedIndexChanged">
                                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Selected="True" Text="No"></asp:ListItem>
                                                                     </asp:DropDownList>                                                                   
                                                            </div>
                                                        </div>
                                                    </div>
                                                     <div style="padding-bottom: 20px"></div>                                                        
                                                 <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                      <label class="lblPos">Amount (Rs) </label>                                                                      
                                                                      <asp:TextBox ID="txtModifyAmount" name="txtModifyAmount" runat="server" Enabled="false" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Amount paid" data-original-title="Amount paid"></asp:TextBox>                                                                      
                                                            </div>
                                                        </div>
                                                  </div>
                                                  <div style="padding-bottom: 20px"></div> 
                                                   <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                      <label class="lblPos">From Date</label>                                                                       
                                                                      <asp:TextBox ID="txtModifyFromdate" name="txtModifyFromdate" runat="server" Enabled="false" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="Start date" data-original-title="Start date"></asp:TextBox>
                                                                      <label style="color:red">(mm/dd/yyyy)</label>                                                                      
                                                            </div>
                                                        </div>
                                                  </div>
                                                  <div style="padding-bottom: 20px"></div>                                                  
                                                   <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                      <label class="lblPos">To Date</label>                                                                      
                                                                      <asp:TextBox ID="txtModifyTodate" name="txtModifyTodate" runat="server" Enabled="false" CssClass="form-control tooltips" data-trigger="hover" data-toggle="tooltip" title="" placeholder="End date" data-original-title="End date"></asp:TextBox>                                                                      
                                                                      <label style="color:red">(mm/dd/yyyy)</label> 
                                                            </div>
                                                        </div>
                                                  </div>
                                                </div>
                                          </div>
                                     </section>
                                <%--</div>--%>
                            </div><%--row end--%>


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
                                                <button class="btn gbtn1" type="button"
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
        </div> <!-- //dialog -->
                 
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
    <!--dynamic table initialization -->
    <script src="js/pagesjs/Information.js"></script>
</asp:Content>