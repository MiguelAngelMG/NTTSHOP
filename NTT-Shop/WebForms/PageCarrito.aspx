<%@ Page Language="C#" MasterPageFile="MasterPage.master"  AutoEventWireup="true" Title="MasterPage Example" CodeBehind="PageCarrito.aspx.cs" Inherits="NTT_Shop.WebForms.PageCarrito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
		        <article>
			        <header>
				        <h1>TERMINA TU COMPRA</h1>
			        </header>
             
			        <div class="tabla1">
				        Listado:
                    <asp:ListView ID="ListViewProductos" runat="server">
                        <LayoutTemplate>
                            <div>
                                <table>
                                    <tr>
                                        <th>Producto</th>
                                        <th>Title</th>
                                        <th>Descripción</th>
                                        <th>Precio</th>
                                        <th>Cantidad</th>
                                        <th>Precio Total</th>
                                        <th>Editar</th>
                                       
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                        <tr>
                             <td><asp:Label ID="lblIdProduct" runat="server" Text='<%# Eval("Item1.IdProduct") %>'></asp:Label></td>
                            <td><%# Eval("Item1.description[0].title") %></td>
                            <td><%# Eval("Item1.description[0].description") %></td>
                            <td><asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Item1.rates[0].price") + "€"  %>'></asp:Label></td>
                            <a href="PageLanguage.aspx">PageLanguage.aspx</a>
                            <td><asp:TextBox ID="lblCantidad" runat="server" Text='<%# Eval("Item2") %>' type="number" min="1" max='<%# Eval("Item1.stock") %>' AutoPostBack="True" OnTextChanged="lblCantidad_TextChanged"></asp:TextBox></td>
                                <asp:Label ID="lblStockError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            <td><asp:Label ID="lblTotalPrice" runat="server" Text='<%# (Convert.ToDecimal(Eval("Item2")) * Convert.ToDecimal(Eval("Item1.rates[0].price"))).ToString() + "€"  %>'></asp:Label></td>
                            <td><asp:Button ID="btn_Eliminar" runat="server" Text="🗑️" OnClick="btnEliminar_click"/></td>
                        </tr>
                    </ItemTemplate>
                    </asp:ListView>

			        </div>
			              <div>
                      <h2>Datos de Dirección</h2>
                      <asp:TextBox ID="Nombre" runat="server" CssClass="form-control" placeholder="Nombre" Enabled="false"></asp:TextBox>
                      <asp:TextBox ID="Apellidos" runat="server" CssClass="form-control" placeholder="Apellidos" Enabled="false"></asp:TextBox>                     
                      <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Dirección" Enabled="false"></asp:TextBox>
                      <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" placeholder="País" Enabled="false"></asp:TextBox>
                      <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" placeholder="Ciudad" Enabled="false"></asp:TextBox>
                      <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Código Postal" Enabled="false"></asp:TextBox>
                      <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="ejemplo@ejemplo.com" Enabled="false"></asp:TextBox>
                      <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="111111111" Enabled="false"></asp:TextBox> 
                  </div>

                  <div>
                      <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click" CssClass="btn btn-primary" />
                   </div>
		        </article>
	
	        </section>         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
