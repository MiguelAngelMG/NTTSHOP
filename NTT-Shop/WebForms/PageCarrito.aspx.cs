
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
using System.Windows.Forms;
using NTT_Shop.Models;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Runtime.CompilerServices;

namespace NTT_Shop.WebForms
{
    public partial class PageCarrito : System.Web.UI.Page
    {
        ModelDAC model = new ModelDAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] == null && Session["session-language"] == null)
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    LoadListView();

                    User user = model.GetUser((int)Session["session-id"]);
                    txtCiudad.Text = user.Town;
                    txtCodigoPostal.Text = user.PostalCode;
                    txtProvincia.Text = user.Province;
                    txtDireccion.Text = user.Address;
                    Nombre.Text = user.Name;
                    Apellidos.Text = user.Surname1 + " " + user.Surname2;
                    txtEmail.Text = user.Email;
                    txtPhone.Text = user.Phone;
                }
                else
                {

                }
            }
            else
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }

        }

        private void Cantidad()
        {
       
        }

        public void LoadListView()
        {

            List<ElementosCarrito> listProduct = Session["session-compra"] as List<ElementosCarrito>;
            List<Tuple<Product,int>> listaDef = new List<Tuple<Product,int>>();
            string language = (string)Session["session-language"];
            foreach (ElementosCarrito e in listProduct)
            {
                Product product = model.GetProduct(e.idProduct, language);
                listaDef.Add(new Tuple<Product, int>(product,e.cantidad));
            }
            ListViewProductos.DataSource = listaDef;
            ListViewProductos.DataBind();
        }
        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {


            bool insertado = false;
            string message = "";

            List<ElementosCarrito> listProduct = Session["session-compra"] as List<ElementosCarrito>;
            if (listProduct.Count > 0)
            {
                string language = (string)Session["session-language"];
                int idUser = (int)Session["session-id"];
                List<OrderDetail> ordersDetails = new List<OrderDetail>();

                foreach (ElementosCarrito elemento in listProduct)
                {

                    Product product = model.GetProduct(elemento.idProduct, language);
                    OrderDetail newOrderDeatil = new OrderDetail()
                    {
                        
                        idProduct = elemento.idProduct,
                        Price = product.rates[0].price,
                        Units = elemento.cantidad,
                        product = product,
                    };
                    ordersDetails.Add(newOrderDeatil);
                }

                Order order = new Order()
                {
                    
                    idUser = idUser,
                    orderDate = DateTime.Now,
                    orderStatus = 1,
                    TotalPrice = 0,
                    orderDetails = ordersDetails
                    
                };
                order.status.idStatus = 0;
                order.status.description = "string";
                order.calcularPriceTotal();
                insertado = model.InsertOrder(order);

                if (insertado)
                {
                    listProduct.Clear();
                    
                    Session["session-id"] = listProduct;


                    string script = "alert(\"Pedido creado! " + message + "\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);


                    LoadListView();
                }
                else
                {
                    string script = "alert(\"Error: No se ha podido realizar el pedido! " + message + "\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "alert(\"Error: Debes añadir un producto al carrito para poder realizar el pedido" + message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }



        }

        protected void lblCantidad_TextChanged(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.TextBox txtCantidad = (System.Web.UI.WebControls.TextBox)sender; 
            System.Web.UI.WebControls.ListViewItem item = (System.Web.UI.WebControls.ListViewItem)txtCantidad.NamingContainer; 
            System.Web.UI.WebControls.Label lblPrecioUnitario = (System.Web.UI.WebControls.Label)item.FindControl("lblPrice"); 
            System.Web.UI.WebControls.Label lblIdProduct = (System.Web.UI.WebControls.Label)item.FindControl("lblIdProduct");
            string language = (string)Session["session-language"];
            Product product = model.GetProduct(int.Parse(lblIdProduct.Text), language);
            double precioUnitario = double.Parse(product.rates[0].price.ToString());

           
            int cantidad = int.Parse(txtCantidad.Text);
            System.Web.UI.WebControls.Label lblTotalPrice = (System.Web.UI.WebControls.Label)item.FindControl("lblTotalPrice");
            lblTotalPrice.Text = (cantidad * precioUnitario).ToString();

            int idProduct = int.Parse(lblIdProduct.Text);
            List<ElementosCarrito> listProduct = Session["session-compra"] as List<ElementosCarrito>;
            // Buscar el elemento en la lista que coincide con el idProduct
            ElementosCarrito elementoEncontrado = listProduct.FirstOrDefault(x => x.idProduct == idProduct);

            elementoEncontrado.cantidad = cantidad;
               

        }
        protected void btnEliminar_click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btnEliminar = (System.Web.UI.WebControls.Button)sender;
            System.Web.UI.WebControls.ListViewItem item = (System.Web.UI.WebControls.ListViewItem)btnEliminar.NamingContainer;
            int indiceItem = item.DataItemIndex;

            System.Web.UI.WebControls.Label lblIdProduct = (System.Web.UI.WebControls.Label)item.FindControl("lblIdProduct");
            int idProduct = int.Parse(lblIdProduct.Text);
            List<ElementosCarrito> listProduct = Session["session-compra"] as List<ElementosCarrito>;

            int indiceElemento = listProduct.FindIndex(x => x.idProduct == idProduct);

            listProduct.RemoveAt(indiceElemento);

            LoadListView();
        }

        

        //protected void GridViewEjemplo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    //Obtener objeto fila seleccionado para actualizar:
        //    ListView objFila = this.ListViewProductos.Rows[e.RowIndex];

        //    //Obtener valores del objeto fila modificados en pantalla:
        //    HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");
        //    System.Web.UI.WebControls.TextBox description = (System.Web.UI.WebControls.TextBox)objFila.FindControl("txtDescription");           
        //    System.Web.UI.WebControls.TextBox iso = (System.Web.UI.WebControls.TextBox)objFila.FindControl("txtISO");

        //    Language language = new Language()
        //    {
        //        idLanguage = int.Parse(hddIdLanguage.Value),
        //        descripcion = description.Text,
        //        iso = iso.Text.ToLower(),
        //    };

        //    string message = "";

        //    if (1 > 0)
        //    {
        //        bool actualizado = model.UpdateLanguage(language, out message);

        //        if (actualizado)
        //        {
        //            //En este punto, llamar a métodos de API/Base de datos para actualizar los datos.
        //            //Obtener datos de hddIdLanguage -> hddIdLanguage.value;
        //            //Obtener datos de description -> description.Text;n
        //            //Obtener datos de iso -> iso.Text;

        //            // Mostrar mensaje:
        //            string script = "alert(\"Actualizado Correctamente!\");";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

        //            //Recargar Grid en modo no-edición
        //            this.GridViewEjemplo.EditIndex = -1;
        //            LoadListView();
        //        }
        //        else
        //        {
        //            string script = "alert(\"Error: No se ha actualizado Correctamente! " + message + "\");";
        //            ScriptManager.RegisterStartupScript(this, GetType(),
        //                                  "ServerControlScript", script, true);

        //            LoadListView();
        //        }
        //    }
        //    else
        //    {

        //        string script = "alert(\"Error: No se ha actualizado Correctamente! " + message +  "\");";
        //        ScriptManager.RegisterStartupScript(this, GetType(),
        //                              "ServerControlScript", script, true);

        //        LoadListView();

        //    }
        //}

        //protected void GridViewEjemplo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    this.GridViewEjemplo.EditIndex = -1;
        //    GridViewRow objFila = this.GridViewEjemplo.Rows[e.RowIndex];
        //    HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");
        //    //Borrar invocando a método de api de borrado con el id:
        //    bool eliminado = model.DeleteLanguage(int.Parse(hddIdLanguage.Value));

        //    if (eliminado)
        //    {
        //        // Mostrar mensaje:
        //        string script = "alert(\"Eliminado Correctamente!\");";
        //        ScriptManager.RegisterStartupScript(this, GetType(),
        //                              "ServerControlScript", script, true);

        //        LoadListView();

        //    }
        //    else
        //    {
        //        string script = "alert(\"Error: No se ha eliminado Correctamente!\");";
        //        ScriptManager.RegisterStartupScript(this, GetType(),
        //                              "ServerControlScript", script, true);
        //    }


        //}

    }
}