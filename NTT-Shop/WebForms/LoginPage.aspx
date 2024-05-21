<%@ Page Language="C#" MasterPageFile="MasterLoginPage.master" AutoEventWireup="true" Title="Login" CodeBehind="LoginPage.aspx.cs" Inherits="NTT_Shop.WebForms.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <link rel="stylesheet" href="style.css"/>
            <section id="main-content">
                <article>
                    <header>
                        <h1 style="text-align: center;   font-size: 24px;">Bienvenido a NTTSHOP</h1>
                    </header>
                    <div class="container-login">
                          <div class="contentLogin">
                                <h1>INICIO</h1>
                                <div class="valuesLogin">
                                     <label style="float:left"">Login:</label>
                                     <asp:TextBox ID="txtUser" Text="Miguelangel" MaxLength="100" runat="server" Placeholder="Login"></asp:TextBox>
                                    <label style="float:left">Password:</label>
                                    <!-- TextMode="Password" !-->
                                     <asp:TextBox ID="txtPassword" Text="Miguelmiguel1!" MaxLength="100"   runat="server" Placeholder="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="footerLogin">
                                <asp:Button runat="server" Text="Register" CssClass="sing-up" OnClick="SignUp_Click" />
                                <asp:Button runat="server" Text="SING IN" CssClass="sing-in" OnClick="SignIn_Click" />
                          </div>
                    </div>
                    <br />
                    <br />
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>