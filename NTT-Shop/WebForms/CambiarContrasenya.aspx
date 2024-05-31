<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeBehind="CambiarContrasenya.aspx.cs" Inherits="NTT_Shop.WebForms.CambiarContrasenya" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="row justify-content-center">
     <div class="col-md-4">
         <h2 class="text-center">Cambiar Contraseña</h2>
         <hr />
         <div class="form-group">
              <asp:Label runat="server" ID="lblusuario" Text="Nombre Usuario:" Font-Bold="True" class="form-label" />
              <asp:TextBox runat="server" ID="txtUser" class="form-control" disabled="true" />
         </div>
         <div class="form-group">
             <asp:Label runat="server" ID="lblcontrasenya" Text="Nueva contraseña" class="form-label fw-bold"/><br />
             <asp:TextBox runat="server" ID="txtContrasenya" class="form-control" />
             <asp:RegularExpressionValidator ID="revCp" runat="server" Display="Dynamic" ControlToValidate="txtContrasenya"  ErrorMessage="Debe de contener mínimo 10 caracteres, una mayúscula y un número." ValidationExpression="^(?=.*[A-Z])(?=.*\d).{10,}$" ForeColor="Red" />
             <asp:RequiredFieldValidator ID="rfvContra" runat="server" Display="Dynamic" ControlToValidate="txtContrasenya" ForeColor="Red"  ErrorMessage="Debes de introducir la nueva contraseña." />
         </div>
         
         <div class="col-12 d-grid" style="margin-top:10px;">
            <asp:Button runat="server" ID="btnCambiar" Text="Actualizar contraseña" class="btn btn-outline-dark" OnClick="btnCambiar_Click" />
         </div>
         <div class="col-12 d-grid" style="margin-top:10px;">
             <asp:Button runat="server" ID="btnVolver" Text="Volver" class="btn btn-outline-secondary" OnClick="btnVolver_Click" />
         </div>

     </div>
             <asp:Label runat="server" ID="lblCorrecto"  class="alert alert-success" style="margin-top:20px;"/><br />
             <asp:Label runat="server" ID="lblError"  class="alert alert-danger"/><br />
   

 </div>
</asp:Content>
