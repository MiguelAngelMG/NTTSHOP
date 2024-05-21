
using NTT_Shop.WebForms;
using System;
using NTT_Shop.Models.Entities;
using NTT_Shop.Models;
using System.Web.ModelBinding;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Collections.Generic;

namespace NTT_Shop.WebForms
{
    public partial class DataUserPage : System.Web.UI.Page
    {
        ModelDAC model = new ModelDAC();
        private string baseUrl = "https://localhost:7077/api/"; 
        User userData = new User();
        
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    LoadPageView();
                }
               
            }
            else
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }

        }
        public void LoadPageView()
        {
            enableTextBox(false);
            btn_Edit.Text = "Editar";
            userData = model.GetUser(Session["session-id"].GetHashCode());

            txtLogin.Text = userData.Login;
            txtPassword.Text = userData.Password;
            txtNombre.Text = userData.Name;
            txtApellido.Text = userData.Surname1;
            txtSegundoApellido.Text = userData.Surname2;
            txtCodigoPostal.Text = userData.PostalCode;
            txtDireccion.Text = userData.Address;
            txtCiudad.Text = userData.Town;
            txtProvincia.Text = userData.Province;
            txtPhone.Text = userData.Phone;
            txtEmail.Text = userData.Email;
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            if(btn_Edit.Text == "Editar")
            { 
                enableTextBox(true);
                btn_Edit.Text = "Actualizar";
                txtLogin.Enabled = false;
                txtPassword.Enabled = false;
                txtEmail.Enabled = false;
            }
            else
            {
                userData = model.GetUser(Session["session-id"].GetHashCode());

                User userActual = new User()
                {
                    PkUser = userData.PkUser,
                    Languages= userData.Languages,
                    Rate = userData.Rate,
                    Login = txtLogin.Text,
                    Password = userData.Password,
                    Address = txtDireccion.Text,
                    Name = txtNombre.Text,
                    Surname1 = txtApellido.Text,
                    Surname2 = txtSegundoApellido.Text,
                    PostalCode = txtCodigoPostal.Text,
                    Town = txtCiudad.Text,
                    Province = txtProvincia.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text

                };
                if (UpdateUserValidation(userActual))
                {
                    bool actualizado = model.UpdateUser(userActual, out string message);
                    
                    if (actualizado)
                    {
                            string script = "alert(\"Actualizado Correctamente!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                            LoadPageView();
                    }
                    else
                    {
                            string script = "alert(\"Error: No se ha actualizado Correctamente! " + message + "\");";
                            ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);

                            LoadPageView();
                    }
                    
                }
                
                enableTextBox(false);
            }
           
            
        }
        protected void BackDataUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Example.aspx");
        }

        public void enableTextBox(bool type)
        {
           
            txtLogin.Enabled = type;
            txtPassword.Enabled = type;
            txtNombre.Enabled = type;
            txtApellido.Enabled = type;
            txtSegundoApellido.Enabled = type;
            txtCodigoPostal.Enabled = type;
            txtDireccion.Enabled = type;   
            txtCiudad.Enabled = type;
            txtProvincia.Enabled = type;
            txtPhone.Enabled = type;   
            txtEmail.Enabled = type;
        }

        private bool UpdateUserValidation(User request)
        {
            if (request != null
                && !string.IsNullOrWhiteSpace(request.Login)
                && !string.IsNullOrWhiteSpace(request.Name)
                && !string.IsNullOrWhiteSpace(request.Surname1)
                && !string.IsNullOrWhiteSpace(request.Email)
                && !string.IsNullOrWhiteSpace(request.Languages)
                && request.PkUser > 0
                && request.Rate != 0)
            {
                if (!string.IsNullOrWhiteSpace(request.PostalCode) || !string.IsNullOrWhiteSpace(request.Phone))
                {
                    if(!string.IsNullOrWhiteSpace(request.PostalCode) && !string.IsNullOrWhiteSpace(request.Phone))
                    {
                        if (Regex.IsMatch(request.PostalCode, @"^\d{5}(\d{1})?$") && Regex.IsMatch(request.Phone, @"^[0-9]{9}$"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.PostalCode))
                    {
                        if (Regex.IsMatch(request.PostalCode, @"^\d{5}$"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if ( Regex.IsMatch(request.Phone, @"^[0-9]{10}$"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}