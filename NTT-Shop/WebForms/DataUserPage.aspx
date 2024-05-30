<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Title="Cuenta" CodeBehind="DataUserPage.aspx.cs" Inherits="NTT_Shop.WebForms.DataUserPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <link rel="stylesheet" href="style.css"/>
            <section id="main-content">
                <article>
                    <header>
                        <h1 style="text-align: center;   font-size: 24px;">Datos privados</h1>
                    </header>
                    <div class="container-Register">
                          <div class="contentRegister">
                                <div class="valuesRegister">
                                    <label style="float:left">Login:</label>
                                    <asp:TextBox ID="txtLogin" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <div class="col-5 d-grid gap-2" style="margin-top: 10px;">
                                        <asp:Button runat="server" ID="btnCambiarC" Text="Cambiar Contraseña"  OnClick="btnCambiarC_Click"  class="btn btn-outline-secondary"/>
                                    </div>
                                    <br />
                                    <label style="float:left">Nombre:</label>
                                    <asp:TextBox ID="txtNombre" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float:left">Apellido:</label>
                                    <asp:TextBox ID="txtApellido" MaxLength="100" runat="server"></asp:TextBox> 
                                     <br />
                                    <label style="float:left">Segundo Apellido:</label>
                                    <asp:TextBox ID="txtSegundoApellido" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float:left">Dirección</label>
                                    <asp:TextBox ID="txtDireccion" MaxLength="100" runat="server"></asp:TextBox>  
                                    <br />
                                    <label style="float:left">Ciudad</label>
                                    <asp:TextBox ID="txtCiudad" MaxLength="100" runat="server"></asp:TextBox> 
                                    <br />
                                    <label style="float:left">Provincia</label>
                                    <asp:TextBox ID="txtProvincia" MaxLength="100" runat="server"></asp:TextBox>
                                     <br />
                                    <label style="float:left">Codigo Postal</label>
                                    <asp:TextBox ID="txtCodigoPostal" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float:left">Telefono</label>
                                    <asp:TextBox ID="txtPhone" MaxLength="100" runat="server"></asp:TextBox>

                                    <br />
                                    <label style="float:left">Email:</label>
                                    <asp:TextBox ID="txtEmail" MaxLength="100" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="footerRegister">
                                <asp:Button ID= "btn_BackDataUser" runat="server" Text="Volver" CssClass="sing-up" OnClick="BackDataUser_Click" />
                                <asp:Button ID= "btn_Edit" runat="server" Text="Editar" CssClass="sing-in" OnClick="Edit_Click" />
                          </div>
                    </div>
                    <br />
                    <br />
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
