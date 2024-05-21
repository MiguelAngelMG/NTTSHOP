<%@ Page Language="C#" MasterPageFile="MasterPage.master"  AutoEventWireup="true" Title="Pedidos" CodeBehind="PageOrders.aspx.cs" Inherits="NTT_Shop.WebForms.PageOrders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
		        <article>
			        <header>
				        <h1>HISTORICO DE PEDIDOS</h1>
			        </header>
             
			        <div class="tabla1">
				        <h2>Pedidos:</h2>
                    <asp:ListView ID="ListViewOrder" runat="server">
                        <LayoutTemplate>
                            <div>
                                <table>
                                    <tr>
                                        <th>Numero de pedido</th>
                                        <th>Fecha de Pedido</th>
                                        <th>Estado</th>                          
                                        <th>Total Precio</th>
                                        <th>Ver</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                        <tr>
                             <td><asp:Label ID="lblIdOrder" runat="server" Text='<%# Eval("idOrder") %>'></asp:Label></td>
                            <td><%# Eval("orderDate") %></td>
                            <td><%# Eval("status.description") %></td>
                            <td><asp:Label ID="lblPrice" runat="server" Text='<%# Eval("TotalPrice") + "€"  %>'></asp:Label></td>
                             <td> <asp:LinkButton ID="btnVerDetalles" runat="server" Text="Ver Detalles" OnClick="verDetalles_Click" CommandArgument='<%# Eval("idOrder") %>' style="display: block; width: 100%; height: 100%; text-align: left;"></asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                    </asp:ListView>

			        </div>
			          <div>
                      <h2>Datos de Pedido:
                      </h2>
                      <asp:Label ID="txtIdOrder" Text="Id Order:" runat="server" CssClass="form-control" ></asp:Label>
                      <asp:Label ID="VerIdOrder" runat="server" CssClass="form-control" ></asp:Label>
                      <asp:Label ID="txtfech" Text="Fecha:" runat="server" CssClass="form-control" ></asp:Label>                 
                      <asp:Label ID="verFecha" runat="server" CssClass="form-control" ></asp:Label>        
                      <asp:Label ID="txtEstado"  Text="Estado:" runat="server" CssClass="form-control" ></asp:Label>                         
                      <asp:Label ID="verEstado" runat="server" CssClass="form-control" ></asp:Label>
                      <asp:Label ID="txtPrecio"  Text="Precio:" runat="server" CssClass="form-control" ></asp:Label>
                      <asp:Label ID="verPrecio" runat="server" CssClass="form-control" ></asp:Label>
                      
                    
                       </div>

                         <div class="tabla1">
                           <asp:ListView ID="ListViewOrderDetail" runat="server">
                          <LayoutTemplate>
                              <div>
                                  <table>
                                      <tr>
                                          <th>Producto</th>
                                          <th>Title</th>
                                          <th>Descripción</th>
                                          <th>Cantidad</th>
                                          <th>Precio Total</th>
                     
                                      </tr>
                                      <tr id="itemPlaceholder" runat="server"></tr>
                                  </table>
                              </div>
                          </LayoutTemplate>
                          <ItemTemplate>
                          <tr>
                              <td><%# Eval("idProduct") %></td>
                              <td><%# Eval("product.description[0].title") %></td>
                              <td><%# Eval("product.description[0].description") %></td>
                              <td><%# Eval("Units") + " und." %></td>
                              <td><%# Eval("Price")+ "€"  %></td>
                          </tr>
                      </ItemTemplate>
                  </asp:ListView>
                   </div>
		        </article>
	
	        </section>         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
