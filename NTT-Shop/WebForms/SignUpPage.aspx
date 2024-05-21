<%@ Page Language="C#" MasterPageFile="MasterLoginPage.master" AutoEventWireup="true" Title="Registro" CodeBehind="SignUpPage.aspx.cs" Inherits="NTT_Shop.WebForms.SignUpPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <link rel="stylesheet" href="style.css"/>
            <section id="main-content">
                <article>
                    <header>
                        <h1 style="text-align: center;   font-size: 24px;">Registro</h1>
                    </header>
                    <div class="container-Register">
                          <div class="contentRegister">
                                <div class="valuesRegister">
                                    <label style="float:left">Login:</label>
                                    <asp:TextBox ID="txtLogin" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float:left">Password:</label>
                                    <asp:TextBox ID="txtPassword" MaxLength="100" TextMode="Password" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float:left">Nombre:</label>
                                    <asp:TextBox ID="txtNombre" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float:left">Apellido:</label>
                                    <asp:TextBox ID="txtApellido" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float:left">Email:</label>
                                    <asp:TextBox ID="txtEmail" MaxLength="100" runat="server"></asp:TextBox>
                                    <br />
                                    <label style="float: left;">Lenguaje</label><br /><br />
<asp:DropDownList ID="cboxLanguage" runat="server" style="width: 200px; float: left;" ></asp:DropDownList>


                                </div>
                            </div>
                            <div class="footerRegister">
                                <asp:Button runat="server" Text="Tengo cuenta" CssClass="sing-up" OnClick="Back_Click" />
                                <asp:Button runat="server" Text="Registrar" CssClass="sing-in" OnClick="Registro_Click" />
                          </div>
                    </div>
                    <br />
                    <br />
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
