
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class Example2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] == null && Session["session-language"] == null)
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }
            if (!IsPostBack && Session["session-id"] != null)
            {
                LoadGridView();
            }
            else
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }
        }

        public void LoadGridView()
        {
            //Obtener datos de base de datos y cargarlos en el grid, ejemplo:
            Console.Write("HOLA");
            List<Language> exampleList = new List<Language>();

            Language language1 = new Language();
            language1.idLanguage = 1;
            language1.descripcion = "Español";
            language1.iso = "es";

            Language language2 = new Language();
            language2.idLanguage = 2;
            language2.descripcion = "Inglés";
            language2.iso = "en";

            Language language3 = new Language();
            language3.idLanguage = 3;
            language3.descripcion = "Catalán";
            language3.iso = "ca";

            Language language4 = new Language();
            language4.idLanguage = 4;
            language4.descripcion = "Francés";
            language4.iso = "fr";

            Language language5 = new Language();
            language5.idLanguage = 5;
            language5.descripcion = "Gallego";
            language5.iso = "ga";

            Language language6 = new Language();
            language6.idLanguage = 6;
            language6.descripcion = "Alemán";
            language6.iso = "al";

            exampleList.Add(language1);
            exampleList.Add(language2);
            exampleList.Add(language3);
            exampleList.Add(language4);
            exampleList.Add(language5);
            exampleList.Add(language6);

            GridViewEjemplo.DataSource = exampleList;
            
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
            TextBox description = (TextBox)objFila.FindControl("txtDescription");           
            TextBox iso = (TextBox)objFila.FindControl("txtISO");

            //En este punto, llamar a métodos de API/Base de datos para actualizar los datos.
            //Obtener datos de hddIdLanguage -> hddIdLanguage.value;
            //Obtener datos de description -> description.Text;
            //Obtener datos de iso -> iso.Text;

            // Mostrar mensaje:
            string script = "alert(\"Actualizado Correctamente!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            //Recargar Grid en modo no-edición
            this.GridViewEjemplo.EditIndex = -1;
            LoadGridView();
        }

        protected void GridViewEjemplo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.GridViewEjemplo.EditIndex = -1;
            GridViewRow objFila = this.GridViewEjemplo.Rows[e.RowIndex];
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");

            //Borrar invocando a método de api de borrado con el id:
            // hddIdLanguage.value;


            // Mostrar mensaje:
            string script = "alert(\"Eliminado Correctamente!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        protected void btnNewLanguage_Click(object sender, EventArgs e)
        {
            //Recoger valores formulario:
            string description = txtDescription.Text;
            string iso = txtISO.Text;

            //Crear nuevo elemento invocando método API de inserción

            //Borrar campos
            txtDescription.Text = string.Empty;
            txtISO.Text = string.Empty;

            // Mostrar mensaje:
            string script = "alert(\"Insertado Correctamente!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            //Refrescar Grid
            LoadGridView();
        }
    }
}