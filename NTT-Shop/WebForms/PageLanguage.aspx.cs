
using NTT_Shop.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using NTT_Shop.Models.Entities;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using NTT_Shop.Models;

namespace NTT_Shop.WebForms
{
    public partial class PageLanguage : System.Web.UI.Page
    {
        ModelDAC model = new ModelDAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    LoadGridView();
                }

            }
            else
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }

        }

        public void LoadGridView()
        {
            //Obtener datos de base de datos y cargarlos en el grid, ejemplo:
            List<Language> languageList = model.GetAllLanguage();

            GridViewEjemplo.DataSource = languageList;
            GridViewEjemplo.DataBind();
        }

        protected void GridViewEjemplo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewEjemplo.PageIndex = e.NewPageIndex;
            LoadGridView();
        }

        protected void GridViewEjemplo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridViewEjemplo.EditIndex = e.NewEditIndex;
            LoadGridView();
        }

        protected void GridViewEjemplo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Obtener objeto fila seleccionado para actualizar:
            GridViewRow objFila = this.GridViewEjemplo.Rows[e.RowIndex];

            //Obtener valores del objeto fila modificados en pantalla:
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");
            System.Web.UI.WebControls.TextBox description = (System.Web.UI.WebControls.TextBox)objFila.FindControl("txtDescription");           
            System.Web.UI.WebControls.TextBox iso = (System.Web.UI.WebControls.TextBox)objFila.FindControl("txtISO");

            Language language = new Language()
            {
                idLanguage = int.Parse(hddIdLanguage.Value),
                descripcion = description.Text,
                iso = iso.Text.ToLower(),
            };

            string message = "";

            if (validationLanguage(language.descripcion, language.iso,  out message))
            {
                bool actualizado = model.UpdateLanguage(language, out message);

                if (actualizado)
                {
                    //En este punto, llamar a métodos de API/Base de datos para actualizar los datos.
                    //Obtener datos de hddIdLanguage -> hddIdLanguage.value;
                    //Obtener datos de description -> description.Text;n
                    //Obtener datos de iso -> iso.Text;

                    // Mostrar mensaje:
                    string script = "alert(\"Actualizado Correctamente!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                    //Recargar Grid en modo no-edición
                    this.GridViewEjemplo.EditIndex = -1;
                    LoadGridView();
                }
                else
                {
                    string script = "alert(\"Error: No se ha actualizado Correctamente! " + message + "\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);

                    LoadGridView();
                }
            }
            else
            {

                string script = "alert(\"Error: No se ha actualizado Correctamente! " + message +  "\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);

                LoadGridView();

            }
        }

        protected void GridViewEjemplo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.GridViewEjemplo.EditIndex = -1;
            GridViewRow objFila = this.GridViewEjemplo.Rows[e.RowIndex];
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");
            //Borrar invocando a método de api de borrado con el id:
            bool eliminado = model.DeleteLanguage(int.Parse(hddIdLanguage.Value));

            if (eliminado)
            {
                // Mostrar mensaje:
                string script = "alert(\"Eliminado Correctamente!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);

                LoadGridView();

            }
            else
            {
                string script = "alert(\"Error: No se ha eliminado Correctamente!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }


        }

        protected void btnNewLanguage_Click(object sender, EventArgs e)

        //Tarea al hacer insert, si el iso ya existe, en vez de introducir uno nuevo, actualizar el que tenemos . (OPCIONAL)

        {
            
            string description = txtDescription.Text;
            string iso = txtISO.Text;

            
            Language language = new Language()
            {
                idLanguage = 0,
                descripcion = description,
                iso = iso.ToLower(),
            };

            bool insertado = false;
            string message = "";

            if (validationLanguage(language.descripcion, language.iso, out message))
            {
               
                List<Language> languageList = model.GetAllLanguage();

                if (languageList.Any(l => l.iso == language.iso))
                {
                    var languageAntes = languageList.FirstOrDefault(l => l.iso == language.iso);
                    message = "El iso " + language.iso + " ya existe, deseas actualizar esté idioma?";
                    bool respuesta = false;
                    string script = "";

                    DialogResult resultado = MessageBox.Show(message, "Alerta", MessageBoxButtons.YesNo);
                    if(resultado == DialogResult.Yes)
                    {
                        respuesta = true;
                    }


                    if (respuesta)
                    {
                        language.idLanguage = languageAntes.idLanguage;
                        bool actualizado = model.UpdateLanguage(language, out message);
                        

                        if (actualizado)
                        {

                            script = "alert(\"Actualizado Correctamente!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                           
                            this.GridViewEjemplo.EditIndex = -1;
                            LoadGridView();
                        }
                        else
                        {
                            script = "alert(\"Error: No se ha actualizado Correctamente! " + message + "\");";
                            ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);

                            LoadGridView();
                        }
                    }
                }
                else
                {
                    insertado = model.InsertLanguage(language);

                    if (insertado)
                    {
                      
                        txtDescription.Text = string.Empty;
                        txtISO.Text = string.Empty;

                      
                        string script = "alert(\"Insertado Correctamente!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);

                        
                        LoadGridView();
                    }
                    else
                    {
                        string script = "alert(\"Error: No se ha insertado Correctamente! " + message + "\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                    }
                }
            }
            else
            {
                string script = "alert(\"Error: No se ha insertado Correctamente! " + message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
                
            
        }
        private bool validationLanguage(string descripción , string iso, out string message)
        {
            bool esValido = false;
            message = "";

            if (
                !string.IsNullOrWhiteSpace(descripción)
               && !string.IsNullOrWhiteSpace(iso)
               && !iso.Any(char.IsDigit)
               && !descripción.Any(char.IsDigit)
              )
            {
                esValido = true;
            }
            else
            {
                esValido = false;
                message = "Errores: ";
                if(string.IsNullOrWhiteSpace(iso) || iso.Any(char.IsDigit))
                {
                    if (string.IsNullOrWhiteSpace(iso))
                    {
                        message += "- Falta introducir ISO. ";
                    }
                    if(iso.Any(char.IsDigit))
                    {
                        message += "- Iso no puede contener numeros. ";
                    }
                }
                if (string.IsNullOrWhiteSpace(descripción) || descripción.Any(char.IsDigit))
                {
                    if (string.IsNullOrWhiteSpace(descripción))
                    {
                        message += "- Falta introducir Descripción. ";
                    }
                    if(descripción.Any(char.IsDigit))
                    {
                        message += "- Descripción no puede contener numeros. ";
                    }
                }
                message += "";
            }
            return esValido;
        }

        

    }
}