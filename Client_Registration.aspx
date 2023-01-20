<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminEx.Master" CodeBehind="Client_Registration.aspx.cs" Inherits="Admin_CommTrex.UserMaster" %>

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


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <%--<link href="js/websitejs/sumoselect.css" rel="stylesheet" />--%>



    <script type="text/javascript">
        $(document).ready(function () {
            $(<%=listbox_comm_dealt.ClientID%>).SumoSelect({ selectAll: true, okCancelInMulti: true });
        });
    </script>
</asp:Content>

<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>Add/Modify/Delete Client Registration details</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Client Registration Master </li>
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
                        <a href="#addNewUser" data-toggle="tab">Add New Client</a>
                    </li>
                    <%--  <li class="" id="tab_DocAuthorize">
                        <a href="#ClientVerify" data-toggle="tab"> Client Varification </a>
                    </li>--%>
                    <li class="" id="tab_modifyproducts">
                        <a href="#modifyUser" data-toggle="tab">View/Modify Client</a>
                    </li>
                </ul>
            </header>
             
            <div class="panel-body">

                <div class="tab-content">

                    <!-- //Add new user tab -->
                    <div class="tab-pane active" id="addNewUser">

                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Client Registration details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField3" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                </header>

                                <div class="panel-body">

                                    <div class="form-horizontal adminex-form">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Client Type : </label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="dllclientType" runat="server" Font-Bold="True"
                                                            CssClass="form-control tooltips">
                                                            <asp:ListItem Value="0" Text="Select Client Type"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Buyer"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Seller"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Trader"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Company Name : </label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtcompnm" name="txtcompnm" runat="server" CssClass="form-control tooltips"
                                                            data-toggle="tooltip" title="" placeholder="Company Name" data-original-title="Company Name"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Contact person name : </label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtcon_pername" name="txtcon_pername" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Name of Contact person" data-original-title="Name of Contact person"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Landline number : </label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtphnno" name="txtphnno" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                          MaxLength="14" data-toggle="tooltip" placeholder="Contact number" data-original-title="Contact person number"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revContNum4" runat="server"
                                                            ControlToValidate="txtphnno" ErrorMessage=" Contact number!!!" 
                                                            ValidationExpression="^[0-9]{11,14}$"></asp:RegularExpressionValidator>
                                                       
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtphnno2" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                            MaxLength="14" data-toggle="tooltip" title="" placeholder="Contact number 2" data-original-title="Contact person number"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                            ControlToValidate="txtphnno2" ErrorMessage="Invalid Contact number!!!" 
                                                            ValidationExpression="^[0-9]{11,14}$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 col-md-2 control-label">Mobile number : </label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtmob" ReadOnly="false" MaxLength="10" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Mobile Number"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                            ControlToValidate="txtmob" ErrorMessage="Invalid Mobile number!!!"
                                                            ValidationExpression="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator>
                                                        <%--ValidationExpression="^\+(?:[0-9]●?){6,14}[0-9]$"></asp:RegularExpressionValidator>--%>
                                                    </div>

 <%--                                                   <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlccode" Visible="false" runat="server" Font-Bold="true" AutoPostBack="true"
                                                            OnSelectedIndexChanged="droptotextbox"
                                                            CssClass="form-control tooltips">

                                                            <asp:ListItem Value="1" Text="+91"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>--%>


                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Email ID: </label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtEmail" name="txtEmail" TextMode="Email" runat="server" CssClass="form-control tooltips"
                                                            data-toggle="tooltip" title="" placeholder="Email" data-original-title="Email"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                                            ControlToValidate="txtEmail" ErrorMessage="Invalid Email Id!!!"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                     
                                                    <label class="col-sm-2 col-sm-2 control-label">Address Line 1: 
                                                       <span style="color: red">*</span>
                                                    </label>
                                                    
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtaddress" name="txtaddress" runat="server" CssClass="form-control tooltips"
                                                            data-toggle="tooltip" title="" placeholder="House no., Compartment name " data-original-title="Company Name"></asp:TextBox>
                                                    </div>

                                                </div>
                                                   <div class="form-group">
                                                       
                                                    <label class="col-sm-2 col-sm-2 control-label">Address Line 2: 
                                                           <span style="color: red">*</span>
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtaddress22" runat="server" CssClass="form-control tooltips"
                                                            data-toggle="tooltip" title="" placeholder="Street Name"></asp:TextBox>
                                                    </div>
                                                </div>

                                                 <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Address Line 3: </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtaddress3" runat="server" CssClass="form-control tooltips"
                                                            data-toggle="tooltip" title="" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">

                                                    <label class="col-sm-2 col-sm-2 control-label">Postal Code: </label>
                                                    <div class="col-sm-2">

                                                        <asp:TextBox ID="pincode" MaxLength="6" AutoPostBack="true" TextMode="Number" runat="server" CssClass="form-control tooltips"
                                                            data-toggle="tooltip" OnTextChanged="GetPincodeData" title="" placeholder="Postal Code" data-original-title="Company Name">
                                                   
                                                        </asp:TextBox>

                                                    </div>

                                                    <label class="col-sm-2 col-sm-2 control-label">Area: </label>
                                                    <div class="col-sm-2">

                                                        <asp:TextBox ID="txtaddress2" runat="server" Font-Bold="True" AutoPostBack="True"
                                                            CssClass="form-control tooltips">
                                                        </asp:TextBox>

                                                    </div>

                                                </div>

                                                <div class="form-group">

                                                    <label class="col-sm-2 col-sm-2 control-label">Country: </label>
                                                    <div class="col-sm-2">

                                                        <asp:TextBox ID="Country" runat="server" Font-Bold="True" AutoPostBack="True"
                                                            CssClass="form-control m-bot15">
                                                        </asp:TextBox>

                                                    </div>

                                                    <label class="col-sm-2 col-sm-2 control-label">State: </label>
                                                    <div class="col-sm-2">


                                                        <asp:TextBox ID="State" runat="server" Font-Bold="True" AutoPostBack="True"
                                                            CssClass="form-control m-bot15">
                                                        </asp:TextBox>

                                                    </div>

                                                    <label class="col-sm-2 col-sm-2 control-label">City: </label>
                                                    <div class="col-sm-2">
                                                        <%-- <asp:TextBox ID="txtcompaddr3" name="txtcompaddr" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Company Address3" data-original-title="Company Address3"></asp:TextBox>--%>

                                                        <asp:TextBox ID="City" runat="server" Font-Bold="True" AutoPostBack="True"
                                                            CssClass="form-control m-bot15">
                                                        </asp:TextBox>

                                                    </div>

                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Dealing in Commodities: </label>
                                                    <%-- <div class="col-sm-10">
                                               <asp:TextBox ID="txtcomm_dealt" name="txtcomm_dealt" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Commodities dealt in" data-original-title="Commodities dealt in"></asp:TextBox>
                                            </div> --%>

                                                    <div class="col-sm-10">
                                                        <script type="text/javascript">
                                                            $(document).ready(function () {
                                                                $(<%=listbox_comm_dealt.ClientID%>).SumoSelect({ selectAll: true });
          });
    </script>

                                                         <asp:ListBox ID="listbox_comm_dealt" runat="server" Font-Bold="True" selectionmode="Multiple"
                                                            Style="border-radius: 14px" CssClass="form-control tooltips"></asp:ListBox>

                                                        
      

                                                       <%-- <asp:DropDownList ID="lbox_comm_dealt" runat="server" Font-Bold="True"
                                                            Style="border-radius: 14px" CssClass="form-control tooltips">
                                                        </asp:DropDownList>--%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label padding: 0px;">GST Number: </label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGST" MaxLength="15" name="txtGST" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="GST Number" data-original-title="GST"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="rgxAadhaar" runat="server" ControlToValidate="txtGST"
                                                            ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"
                                                            ErrorMessage="Invalid GST Number." ForeColor="Red"></asp:RegularExpressionValidator>

                                                    </div>

                                                    <label class="col-sm-2 control-label">
                                                        Upload GST Proof: 
                                                       <%--<span style="color: red">*</span>--%>
                                                    </label>

                                                    <div class="col-sm-2">
                                                        <button class="btn gbtns" type="button"
                                                            data-toggle="modal" data-target="#modReport" btn-action-image="Class"
                                                            runat="server" id="btnReport">
                                                            <i class="fa fa-folder-open"></i>Browse
                                                        </button>
                                                    </div>

                                                    <%--  <label class="col-sm-2 control-label">Path </label>--%>
                                                    <div class="col-sm-3">

                                                        <asp:TextBox ID="txtAGSTPath" name="txtAGSTPath" runat="server" ReadOnly="true"
                                                            CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Display an Image Path"
                                                            data-original-title="Display an Image Path " Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Company Registration Number: </label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtComRegNum" MaxLength="21" name="txtGST" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Company Registration Number" data-original-title="GST"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtComRegNum"
                                                            ValidationExpression="[uUL]{1}[0-9]{5}[A-Z]{2}[0-9]{4}[A-Z]{3}[0-9]{6}$"
                                                            ErrorMessage="Invalid Company Number." ForeColor="Red"></asp:RegularExpressionValidator>
                                                    </div>

                                                    <label class="col-sm-2 control-label">
                                                        Upload Company Registration Proof:
                                                      <%-- <span style="color: red">*</span>--%>
                                                    </label>

                                                    <div class="col-sm-2">
                                                        <button class="btn gbtns" type="button"
                                                            data-toggle="modal" data-target="#ModAgsImg" btn-action-image="Class"
                                                            runat="server" id="Button1">
                                                            <i class="fa fa-folder-open"></i>Browse 
                                                        </button>
                                                    </div>

                                                    <%--  <label class="col-sm-2 control-label">Path </label>--%>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtAROCPath" name="txtAROCPath" runat="server" ReadOnly="true"
                                                            CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Display an Image Path"
                                                            data-original-title="Display an Image Path " Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Warehouse: </label>
                                                    <div class="col-sm-6">
                                                        <%--<asp:ListBox ID="listddl_warehouse" runat="server" SelectionMode="Multiple" Font-Bold="True"
                                                            Style="border-radius: 14px" CssClass="form-control tooltips"></asp:ListBox>--%>
                                                        <asp:DropDownList ID="ddl_warehouse" runat="server" Font-Bold="True"
                                                            Style="border-radius: 14px" CssClass="form-control tooltips">
                                                        </asp:DropDownList>

                                                    </div>

                                                    <%--  <asp:TextBox ID="txtwarehouse" name="txtwarehouse" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Warehouse areas/cities" data-original-title="Warehouse areas/cities"></asp:TextBox>--%>
                                                    <%--required things - need to add btn for multiple warehouse addr & to add multiple textboxes in one set--%>
                                                </div>


                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Warehouse Location: </label>
                                                    <div class="col-sm-4">
                                                        <%--  <asp:DropDownList ID="DropDownList1" runat="server" Font-Bold="True" AutoPostBack="True"
                                                            CssClass="form-control m-bot15"></asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtwarehouseLoc" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Warehouse areas/cities" data-original-title="Warehouse areas/cities"></asp:TextBox>
                                                        <%--required things - need to add btn for multiple warehouse addr & to add multiple textboxes in one set--%>
                                                    </div>
                                                </div>

                                                 <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Status: </label>

                                                    <div class="col-sm-4">
                                                 <select id="mySelection" CssClass="form-control tooltips" onchange="ChangeText();" style="width: 100%;">
                                                    <option disabled="disabled"   selected="selected">Select Status</option>                 
                                                    <option value="1">Active</option>
                                                     <option value="2">Suspend</option>
                                                     <option value="3">Delete</option>
                                                  </select>                                 
                                                    </div>
                                                     <label id="status" style="color:red" class="col-sm-3 col-sm-3 control-label"></label>
                                                </div>
                                              
                                                <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
                                                <script type="text/javascript">
                                                    function ChangeText() {
                                                        var contentText;
                                                        var selectedITem = '';
                                                        var parm = document.getElementById("mySelection");
                                                        selectedITem = parm.options[parm.selectedIndex].text;
                                                       
                                                        if (selectedITem == 'Active') {
                                                            document.getElementById("status").innerHTML = "Current Status : Active";
                                                        }
                                                        else if (selectedITem == 'Suspend') {
                                                            document.getElementById("status").innerHTML = "Current Status : Suspend";
                                                        }
                                                        else if (selectedITem == 'Delete') {
                                                            document.getElementById("status").innerHTML = "Current Status : Delete";
                                                        }
                                                    }
                                                </script>
                                                 

                                                <%--  <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">Role : </label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="ddl_Role" runat="server" Font-Bold="True"
                                                            CssClass="form-control m-bot15">
                                                            <asp:ListItem Value="0" Text="-- Select Role --"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>

                                                <div class="form-group">
                                                    <%--<label class="col-sm-2 col-sm-2 control-label">Status : </label>--%>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtstatus" Visible="false" runat="server" CssClass="form-control tooltips"
                                                            data-toggle="tooltip" title="" placeholder="Status"></asp:TextBox>
                                                    </div>

                                                </div>

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
                                            New</button>
                                        <button class="btn btn-info" type="button"
                                            runat="server" id="btnClear" onserverclick="btnClear_ServerClick">
                                            Clear <i class="fa fa-refresh"></i>
                                        </button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Client.
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

                    <div class="tab-pane" id="modifyUser">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">

                                <asp:GridView ID="grdUser" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    DataKeyNames="iClientId"
                                    OnRowDataBound="grdUser_RowDataBound"
                                    OnRowEditing="grdUser_RowEditing" OnRowCancelingEdit="grdUser_RowCancelingEdit"
                                    RowStyle-CssClass="gradeA" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
                                    CssClass="dynamic-table-grid display table table-bordered table-striped">
                                    <Columns>


                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" HeaderText="Sr No" runat="server" Text='<%# Eval("iClientId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Client Type">
                                            <ItemTemplate>
                                                <asp:Label ID="gClientType" runat="server" Text='<%# Eval("SClientType") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--                                            <EditItemTemplate>
                                                <asp:DropDownList ID="EditClientType" AutoPostBack="true" runat="server" Font-Bold="True" CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="--Select Client Type--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Buyer"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Seller"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Trader"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="SCompany" runat="server" Text='<%# Eval("sCompany") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--    <EditItemTemplate>
                                                <asp:TextBox ID="Companyname" runat="server" Text='<%# Bind("sCompany") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Person Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("sContactPerson") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--  <EditItemTemplate>
                                                <asp:TextBox ID="txtUserName" runat="server" Text='<%# Bind("sContactPerson") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Landline Number 1">
                                            <ItemTemplate>
                                                <asp:Label ID="ContactNumber" runat="server" Text='<%# Eval("sContactNumber") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--  <EditItemTemplate>
                                                <asp:TextBox ID="txtUserNumber" runat="server" Text='<%# Bind("sContactNumber") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revContNumb2" runat="server"
                                                    ControlToValidate="txtUserNumber" ErrorMessage="Please Enter Correct Contact number!!!"
                                                    ValidationExpression="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Landline Number 2">
                                            <ItemTemplate>
                                                <asp:Label ID="ContactNumber2" runat="server" Text='<%# Eval("sContactNumber2") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--  <EditItemTemplate>
                                                <asp:TextBox ID="txtUserNumber2" runat="server" Text='<%# Bind("sContactNumber2") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revContNumb3" runat="server"
                                                    ControlToValidate="txtUserNumber2" ErrorMessage="Please Enter Correct Contact number!!!"
                                                    ValidationExpression="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Mobile Number">
                                            <ItemTemplate>
                                                <asp:Label ID="mobnumb" runat="server" Text='<%# Eval("sMobileNumber") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%-- <EditItemTemplate>
                                                <asp:TextBox ID="txtUserMobNumber" runat="server" Text='<%# Bind("sMobileNumber") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revMobNum" runat="server"
                                                    ControlToValidate="txtUserMobNumber" ErrorMessage="Please Enter Correct Mobile number!!!"
                                                    ValidationExpression="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Email ID">
                                            <ItemTemplate>
                                                <asp:Label ID="emailid" runat="server" Text='<%# Eval("sEmailid") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%--  <EditItemTemplate>

                                                <asp:TextBox ID="edtEmail" runat="server" Text='<%# Bind("sEmailid") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                                    ControlToValidate="edtEmail" ErrorMessage="Please Enter Valid Email ID !!!"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Address Line 1">
                                            <ItemTemplate>
                                                <asp:Label ID="ComLocAdd" runat="server" Text='<%# Eval("sCompanyAdd") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--    <EditItemTemplate>
                                                <asp:TextBox ID="edtComLocAdd" runat="server" Text='<%# Bind("sCompanyAdd") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address Line 2">
                                            <ItemTemplate>
                                                <asp:Label ID="ComLocAdd2" runat="server" Text='<%# Eval("sAddressLine2") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--    <EditItemTemplate>
                                                <asp:TextBox ID="edtComLocAdd" runat="server" Text='<%# Bind("sCompanyAdd") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address Line 3">
                                            <ItemTemplate>
                                                <asp:Label ID="ComLocAdd3" runat="server" Text='<%# Eval("sAddressLine3") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--    <EditItemTemplate>
                                                <asp:TextBox ID="edtComLocAdd2" runat="server" Text='<%# Bind("sCompanyAdd2") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Postal Code">
                                            <ItemTemplate>
                                                <asp:Label ID="postalcode" runat="server" Text='<%# Eval("sPostalCode") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--  <EditItemTemplate>
                                                <asp:DropDownList ID="ddlpostalcode" runat="server">
                                                    <asp:ListItem Value="0" Text="--Select Pincode--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="+91"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="+52"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="+11"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="+1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="CompanyAdd" runat="server" Text='<%# Eval("sComapnyAddress") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%-- <EditItemTemplate>
                                                <asp:Label ID="CountryForEdit" Visible="false" runat="server" Text='<%# Bind("sComapnyAddress") %>'></asp:Label>

                                                <asp:DropDownList ID="edtdllCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryForEdit_SelectedIndexChanged" runat="server" Font-Bold="True" CssClass="form-control m-bot15">
                                                </asp:DropDownList>

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="State">
                                            <ItemTemplate>
                                                <asp:Label ID="CompanyAdd2" runat="server" Text='<%# Eval("sComapnyAddress2") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%-- <EditItemTemplate>
                                             
                                                <asp:Label ID="edtlblState" Visible="false" runat="server" Text='<%# Bind("sComapnyAddress2") %>'></asp:Label>

                                                <asp:DropDownList ID="edtdllState" AutoPostBack="true" OnSelectedIndexChanged="ddlStateForEdit_SelectedIndexChanged"
                                                    runat="server" Font-Bold="True" CssClass="form-control m-bot15">
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="City">
                                            <ItemTemplate>
                                                <asp:Label ID="CompanyAdd3" runat="server" Text='<%# Bind("sComapnyAddress3") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--   <EditItemTemplate>
                                               
                                                <asp:Label ID="edtlblCity" Visible="false" runat="server" Text='<%# Bind("sComapnyAddress3") %>'></asp:Label>
                                                <asp:DropDownList ID="edtdllCity" AutoPostBack="true" runat="server" Font-Bold="True" CssClass="form-control m-bot15">
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Comodity Details">
                                            <ItemTemplate>
                                                <asp:Label ID="sCommodityDealt" runat="server" Text='<%# Eval("sCommodityDealt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%--  <EditItemTemplate>
                                                <asp:Label ID="edtsCommodityDealt" runat="server" Text='<%# Bind("sCommodityDealt") %>'>
                                                </asp:Label>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GST Number">
                                            <ItemTemplate>
                                                <asp:Label ID="gstnum" runat="server" Text='<%# Eval("sGSTnumber") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--     <EditItemTemplate>
                                                <asp:TextBox ID="edtgstNum" runat="server" Text='<%# Bind("sGSTnumber") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <%--   <asp:TemplateField HeaderText="Gst Img" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="imgPath" Value='<%# Eval("sGstUplaodDoc") %>' runat="server" />
                                                <asp:HiddenField ID="imgOriginalPath" Value='<%# Eval("sGstUplaodDoc") %>' runat="server" />
                                                <asp:Image ID="ImgCat" runat="server" AlternateText=" " ImageUrl='<%# Eval("sGstUplaodDoc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderText="View Documents">
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowGallery1" RowIndex='<%# ((GridViewRow) Container).RowIndex %>'
                                                    runat="server" Text="View Documents" OnClick="btnShowGallery_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Predefined Img" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="LogoName" Value='<%# Eval("sGstUplaodDoc") %>' runat="server" />
                                                <button class="btn gbtns" type="button" id="btnLogoShow"
                                                    runat="server">
                                                    <i class='<%# (Eval("sGstUplaodDoc")) %>'></i>
                                                </button>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" Visible="false" HeaderText="Selection gst" ControlStyle-CssClass="column-full-width">
                                            <ItemTemplate>
                                                <div class="action-buttons-group">

                                                    <br />
                                                    <br>
                                                    <button class="btn gbtns" type="button"
                                                        data-toggle="modal" data-target="#modReport"
                                                        runat="server" id="btnImage">
                                                        Browse gst <i class="fa fa-eye-slash"></i>
                                                    </button>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Company Registration No.">
                                            <ItemTemplate>
                                                <asp:Label ID="comregnum" runat="server" Text='<%# Eval("sComRegNum") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%-- <EditItemTemplate>
                                                <asp:TextBox ID="edtcomregnum" runat="server" Text='<%# Bind("sComRegNum") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderText="View Documents">
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowGallery2" RowIndex='<%# ((GridViewRow) Container).RowIndex %>'
                                                    runat="server" Text="View Documents" OnClick="btnShowGallery_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Predefined Img" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="LogoName1" Value='<%# Eval("sRocUploadDoc") %>' runat="server" />
                                                <button class="btn gbtns" type="button" id="btnLogoShow1"
                                                    runat="server">
                                                    <i class='<%# (Eval("sRocUploadDoc")) %>'></i>
                                                </button>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" Visible="false" HeaderText="Selection ast" ControlStyle-CssClass="column-full-width">
                                            <ItemTemplate>
                                                <div class="action-buttons-group">

                                                    <br />
                                                    <br>
                                                    <button class="btn gbtns" type="button"
                                                        data-toggle="modal" data-target="#ModAgsImg"
                                                        runat="server" id="btnImage1">
                                                        Browse ast <i class="fa fa-eye-slash"></i>
                                                    </button>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Warehouse">
                                            <ItemTemplate>
                                                <asp:Label ID="Warehouse" runat="server" Text='<%# Eval("sWarehouse") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:Label ID="lblsWarehouse" Visible="false" runat="server" Text='<%# Bind("sWarehouse") %>'> </asp:Label>
                                                <asp:DropDownList ID="edtddlsWarehouse" AutoPostBack="true" runat="server" Font-Bold="True" CssClass="form-control m-bot15">
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Warehouse Location">
                                            <ItemTemplate>
                                                <asp:Label ID="WarehouseLoc" runat="server" Text='<%# Eval("sWarehouseLoc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:TextBox ID="edtWarehouseLoc" runat="server" Text='<%# Bind("sWarehouseLoc") %>'>
                                                </asp:TextBox>
                                               
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <%--                                        <asp:TemplateField HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="Role" runat="server" Text='<%# Eval("sRole") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Data Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("sStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit" runat="server" data-toggle="tab"
                                                    data-user-id='<%# Eval("iClientId") %>'
                                                    href="javascript:;#addNewUser">Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Delete">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-user-id='<%# Eval("iClientId") %>'
                                                    data-user-name='<%# Eval("sContactPerson") %>' href="javascript:;">Delete &nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--  <asp:TemplateField HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderText="AMO">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="lblcoreteam" runat="server" OnClick="return false;" Checked='<%#(Convert.ToBoolean( Eval("EM_bitIsCoreTeam"))) %>' Style="width: 16px; height: 16px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#(Convert.ToBoolean( Eval("EM_bitIsCoreTeam"))) %>' Style="width: 16px; height: 16px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                        <%--     <asp:CommandField ControlStyle-CssClass="btn gbtns" ButtonType="Button" ShowEditButton="true" HeaderText="Approve" />
                                        <asp:CommandField ControlStyle-CssClass="btn gbtns" ButtonType="Button" ShowDeleteButton="true" HeaderText="Reject" />--%>
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

                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteClient" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">Delete the Client.</h4>
                                </div>
                                <div class="modal-body">

                                    <div role="form">
                                        <div class="form-group">
                                            <h3>Are you sure you want to delete Client?</h3>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2 col-sm-2 control-label">Client ID </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtDelClientID" name="txtDelClientID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Id -->
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2 col-sm-2 control-label">Client Person Name </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtDelClientName" name="txtDelClientName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Name -->

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="panel-body" style="text-align: center">
                                                    <button class="btn btn-success" type="button"
                                                        runat="server" id="btnDeleteClient" onserverclick="btnDeleteClient_ServerClick">
                                                        <i class="fa fa-trash"></i>Delete Client
                                                    </button>
                                                    <button class="btn btn-danger" type="button"
                                                        id="btnCancelDeleteClient">
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
                                                                <asp:FileUpload ID="FileMainImage1" Height="26px" accept=".pdf .jpg .png" runat="server" class="default" />
                                                                
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
                                                    <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                                        <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 75px; height: 75px;">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="http://www.placehold.it/300x250/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                                        </div>
                                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 75px; max-height: 75px; line-height: 20px;"></div>
                                                        <div>
                                                            <span class="btn btn-default btn-file">
                                                                <span class="fileupload-new"><i class="fa fa-picture-o"></i>Select PDF</span>
                                                                <span class="fileupload-exists"><i class="fa fa-undo"></i>Change</span>
                                                                <asp:HiddenField ID="pdfpath4" runat="server"></asp:HiddenField>
                                                                
                                                               <asp:FileUpload ID="FileUpload4" runat="server" class="default" />
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



                    <div class="tab-pane" id="ClientVerify">

                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="HEading">
                                Add Details
                            </div>

                            <div class="row">
                                <section class="panel" id="SecDetails">
                                    <header class="panel-heading">
                                        Client Verification details
							             
                                    </header>

                                    <div class="panel-body">
                                        <div class="form-horizontal adminex-form">

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Client Contact Number: </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="VerifyNumText" TextMode="Number" MaxLength="10" name="txtNumber" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                        data-toggle="tooltip" title="" placeholder="User Number" data-original-title="User Number"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                        ControlToValidate="VerifyNumText" ErrorMessage="Invalid Contact Number !!! "
                                                        ValidationExpression="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row" id="sendOtpRow" style="text-align: center;">
                                                    <button class="btn gbtn" type="button" onclick="return sendOtpForAcceptPrev();"
                                                        runat="server" id="btnSendOtp">
                                                        Send OTP <i class="fa fa-send-o"></i>
                                                    </button>
                                                    &nbsp;&nbsp;
                                                <label id="cntDwnLbl" class="retry-countdown"></label>
                                                </div>
                                                <div class="row">
                                                    <label id="otpSentLbl" class="col-sm-12 control-label lbl-pad-top" style="text-align: center; display: none">OTP sent on selected mobile number.</label>
                                                </div>
                                            </div>
                                            <div id="aadhar_data" style="display: none">
                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label padding: 0px;">Aadhar Number: </label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="Adhar_Txt" MaxLength="15" name="txtGST" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Aadhar Card" data-original-title="Aadhar"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtGST"
                                                            ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"
                                                            ErrorMessage="Invalid GST Number." ForeColor="Red"></asp:RegularExpressionValidator>

                                                    </div>

                                                    <label class="col-sm-2 control-label">
                                                        Upload Aadhar Proof: 
                                                       <%--<span style="color: red">*</span>--%>
                                                    </label>

                                                    <div class="col-sm-2">
                                                        <button class="btn gbtns" type="button"
                                                            data-toggle="modal" data-target="#modReport" btn-action-image="Class"
                                                            runat="server" id="Button2">
                                                            <i class="fa fa-folder-open"></i>Browse
                                                        </button>
                                                    </div>

                                                    <%--  <label class="col-sm-2 control-label">Path </label>--%>
                                                    <div class="col-sm-3">

                                                        <asp:TextBox ID="AdharDoc_txt" name="txtAGSTPath" runat="server" ReadOnly="true"
                                                            CssClass="form-control tooltips" data-trigger="hover"
                                                            data-toggle="tooltip" title="" placeholder="Display an Image Path"
                                                            data-original-title="Display an Image Path " Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 mt1" runat="server">
                                                    <button class="btn gbtn" type="button" style="margin-left: 70px;" runat="server" id="Btn_ChangePass" onserverclick="btnSubmit_click">Save</button>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </section>
                            </div>
                        </div>
                    </div>

                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="ModAgsImg" tabindex="-1" class="modal fade">
                        <div class="modal-dialog" style="width: 280px;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">
                                        <asp:Label ID="Label1" runat="server" Text="Select Image For AGC "></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div role="form">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                                        <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 75px; height: 75px;">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="http://www.placehold.it/300x250/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                                        </div>
                                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 75px; max-height: 75px; line-height: 20px;"></div>
                                                        <div>
                                                            <span class="btn btn-default btn-file">
                                                                <span class="fileupload-new"><i class="fa fa-picture-o"></i>Select image</span>
                                                                <span class="fileupload-exists"><i class="fa fa-undo"></i>Change</span>
                                                                <asp:HiddenField ID="txtImgPathMain1" runat="server"></asp:HiddenField>
                                                                <asp:FileUpload ID="FileUpload2" runat="server" class="default" />
                                                               
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
                                                    <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                                        <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 75px; height: 75px;">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="http://www.placehold.it/300x250/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                                        </div>
                                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 75px; max-height: 75px; line-height: 20px;"></div>
                                                        <div>
                                                            <span class="btn btn-default btn-file">
                                                                <span class="fileupload-new"><i class="fa fa-picture-o"></i>Select PDF</span>
                                                                <span class="fileupload-exists"><i class="fa fa-undo"></i>Change</span>
                                                                <asp:HiddenField ID="txtpdfpath" runat="server"></asp:HiddenField>
                                                                
                                                               <asp:FileUpload ID="FileUpload3" runat="server" class="default" />
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
                                                        runat="server" onserverclick="btnSaveImgUpload" id="b2">
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

                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modUserDocuments" class="modal fade">
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
                </div>
            </div>
        </section>
        <!-- Panel Body Main -->
    </div>
    <!-- Main Row -->

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
    <script type="text/javascript" src="js/pagesjs/UserMaster.js"></script>


    <%--<script type="text/javascript" src="js/websitejs/jquery.sumoselect.min.js"></script>--%>

   
    <script type="text/javascript" src="js/websitejs/jquery.sumoselect.min.js"></script>
    <link href="js/websitejs/sumoselect.css" rel="stylesheet" />
   


</asp:Content>
