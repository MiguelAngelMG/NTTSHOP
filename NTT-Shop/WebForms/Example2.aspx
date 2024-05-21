<%@ Page Language="C#" MasterPageFile="MasterPage.master"  AutoEventWireup="true" Title="MasterPage Example" CodeBehind="Example2.aspx.cs" Inherits="NTT_Shop.WebForms.Example2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
		        <article>
			        <header>
				        <h1>Ejemplo 2</h1>
			        </header>

                    Ejemplo Formulario:
                    <table border="1" cellpadding="10" cellspacing="0" >
                        <tr>
                            <td>
                                <asp:Label ID="lbDescription" runat="server">Descripcion</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbISO" runat="server">ISO</asp:Label>
                            </td>
                            <td>                               
                            </td>  
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDescription" MaxLength="100" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtISO" MaxLength="2" Width="20%" runat="server"></asp:TextBox>
                            </td>                          
                            <td class="CELDA7 width4" align="left">
                               <asp:Button id="btnNewLanguage" runat="server" OnClick="btnNewLanguage_Click" Text="Insertar Nuevo" />
                            </td>
                    
                        </tr>
                    </table>

                    <br />
                    <br />
			
			        <div class="content">
				        Ejemplo GRIDVIEW:
                        <asp:GridView id="GridViewEjemplo" AutoGenerateColumns="False" 
                        DataKeyNames="idLanguage"
                        ShowHeader="false" EnableViewState="true" runat="server" PageSize="5" AllowPaging="True"
                        OnPageIndexChanging="GridViewEjemplo_PageIndexChanging" AllowSorting="True" ShowHeaderWhenEmpty="False"
                        OnRowEditing="GridViewEjemplo_RowEditing"
                        OnRowUpdating="GridViewEjemplo_RowUpdating"
                        OnRowDeleting="GridViewEjemplo_RowDeleting"
                        Width="100%" PagerStyle-CssClass="paginacion">

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbIdLanguage" Text='<%# Eval("idLanguage").ToString()%>' runat="server" />
                                    <asp:HiddenField ID="hddIdLanguage" Value='<%# Eval("idLanguage").ToString()%>' runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lbIdLanguageEdit" Text='<%# Eval("idLanguage").ToString()%>' runat="server" />
                                    <asp:HiddenField ID="hddIdLanguage" Value='<%# Eval("idLanguage").ToString()%>' runat="server" />
                                </EditItemTemplate>
                                <ControlStyle Width="20%" />
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>

                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:Label ID="lbDescription" Text='<%# Eval("description")%>' runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="txtDescription" MaxLength="65"  runat="server" CssClass="CELDA7 width98" Text='<%# Eval("description")%>' autocomplete="off"></asp:TextBox> 
                                </EditItemTemplate>
                                <ControlStyle Width="60%" />
                                <ItemStyle Width="60%" />
                            </asp:TemplateField>

                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:Label ID="lbISO" CssClass="" Text='<%# Eval("iso").ToString()%>' runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox id="txtISO" MaxLength="2"  runat="server" Text='<%# Eval("iso")%>' autocomplete="off"></asp:TextBox> 
                                </EditItemTemplate>
                                 <ControlStyle Width="20%" />
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>
                                                       

                            <asp:CommandField ShowEditButton="True" ShowCancelButton="False" ShowDeleteButton="True" ButtonType="Link" 
                              />
                        </Columns>
                    </asp:GridView>

			        </div>
			
		        </article>
	
	        </section>         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
