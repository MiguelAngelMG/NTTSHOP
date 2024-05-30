<%@ Page Language="C#" MasterPageFile="MasterPage.master"  AutoEventWireup="true" Title="Productos" CodeBehind="PageProducts.aspx.cs" Inherits="NTT_Shop.WebForms.PageProducts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
		        <article>
			        <header>
				        <h1>Productos</h1>
			        </header>      
                    <br />
                    <br />
			        <div class="tabla1">
				       <h2>Listado de productos:</h2> 
                        <asp:GridView ID="GridViewProductos" AutoGenerateColumns="False" 
                        DataKeyNames="idProduct"
                        ShowHeader="false" EnableViewState="true" runat="server" PageSize="10" AllowPaging="True"
                        OnPageIndexChanging="GridViewProductos_PageIndexChanging" AllowSorting="True" ShowHeaderWhenEmpty="False" OnRowCommand="GridViewEjemplo_RowEditing">

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
               
                                    <asp:Label ID="lbIdProduct" Text='<%# Eval("idProduct") %>' runat="server" />
                                    <asp:HiddenField ID="hddIdProduct" Value='<%# Eval("idProduct").ToString()%>' runat="server" />
                                </ItemTemplate>
                                <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
            
                                    <asp:Label ID="lbTitulo" Text='<%# Eval("description[0].title") %>' runat="server" />
                                </ItemTemplate>
                                <ControlStyle Width="40%" />
                                <ItemStyle Width="40%" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
            
                                    <asp:Label ID="lbDescripcion" Text='<%# Eval("description[0].description") %>' runat="server" />
                                </ItemTemplate>
                                <ControlStyle Width="40%" />
                                <ItemStyle Width="40%" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
               
                                    <asp:Label ID="lbDisponible" Text='<%# Eval("enabled") %>' runat="server" />
                                </ItemTemplate>
                                <ControlStyle Width="20%" />
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
             
                                    <asp:Label ID="lbPrice" Text='<%# Eval("rates[0].price") + "€" %>' runat="server" />
                                </ItemTemplate>
                                <ControlStyle Width="20%" />
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>
        
                           <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCantidades" runat="server" Text="1" CssClass="txtCantidades" type="number" min="1" max='<%# Eval("stock") %>' /> 
                                </ItemTemplate>
                                <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                            <ItemTemplate>
                               <asp:LinkButton ID="lnkObtenerValores" runat="server" CommandName="ObtenerValores" CommandArgument='<%# Container.DataItemIndex %>' Text="🛒" />
                            </ItemTemplate>
                             </asp:TemplateField>
 
                        </Columns>
                    </asp:GridView>

			        </div>
			      
		        </article>
	        </section>         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
