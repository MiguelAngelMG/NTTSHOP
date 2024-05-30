
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
using System.ComponentModel;
using System.Data;

namespace NTT_Shop.WebForms
{
    public partial class PageProducts : System.Web.UI.Page
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
        protected void GridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductos.PageIndex = e.NewPageIndex;
            LoadGridView();
        }
        public void LoadGridView()
        {
            //Obtener datos de base de datos y cargarlos en el grid, ejemplo:
            string language = (string) Session["session-language"];
            User user = model.GetUser((int)Session["session-id"]);

            List<Product> productList = model.GetAllProducts(language);
            List<Product> productListShow = new List<Product>();
            foreach (Product product in productList)
            {
                foreach (var rate in product.rates)
                {
                    if (rate.idRate == user.Rate && product.enabled is true)
                    {
                        productListShow.Add(product);

                    }
                }
            }
            GridViewProductos.DataSource = productListShow;
            GridViewProductos.DataBind();
        }

        protected void GridViewEjemplo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductos.PageIndex = e.NewPageIndex;
            LoadGridView();
        }

        protected void GridViewEjemplo_RowEditing(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            // Obtiene la fila correspondiente al índice
            GridViewRow row = GridViewProductos.Rows[rowIndex];

            // Encuentra los controles dentro de la fila
            HiddenField hddIdLanguage = (HiddenField)row.FindControl("hddIdProduct");
            System.Web.UI.WebControls.TextBox txtCantidades = (System.Web.UI.WebControls.TextBox)row.FindControl("txtCantidades");

            // Obtiene los valores de los controles
            string idProduct = hddIdLanguage.Value;
            string cantidades = txtCantidades.Text;

            List<ElementosCarrito> listaCompra = Session["session-compra"] as List<ElementosCarrito>;
            ElementosCarrito elementosCarrito = new ElementosCarrito()
            {
                idProduct = int.Parse(idProduct),
                cantidad = int.Parse(cantidades)
            };


            var existeProducto = listaCompra.FirstOrDefault(item => item.idProduct == elementosCarrito.idProduct);

            if (existeProducto != null)
            {
                existeProducto.cantidad += elementosCarrito.cantidad;
            }
            else
            {
                listaCompra.Add(elementosCarrito);
            }


            Session["session-compra"] = listaCompra;

        }


        protected void Anyadir_producto(object sender, GridViewDeleteEventArgs e)
        {
            this.GridViewProductos.EditIndex = -1;
            GridViewRow objFila = this.GridViewProductos.Rows[e.RowIndex];
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdProduct");
         
            
        }
       

    }
}