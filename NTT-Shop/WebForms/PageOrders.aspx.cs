
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
    public partial class PageOrders : System.Web.UI.Page
    {
        ModelDAC model = new ModelDAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    LoadListView();

                    User user = model.GetUser((int)Session["session-id"]);
                    VerIdOrder.Text = "";
                    verFecha.Text = "";
                    verEstado.Text = "";
                    verPrecio.Text = "";
                   
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

        public void LoadListView()
        {
            List<Order> orders = model.GetAllOrder();
            List<Order> listaDef = new List<Order>();
            string language = (string)Session["session-language"];
            int idUser = (int)Session["session-id"];
            foreach (Order e in orders)
            {
              if(e.idUser == idUser)
                {
                    e.status = model.GetOrderStatus(e.orderStatus);
                    listaDef.Add(e);
                }
            }
            ListViewOrder.DataSource = listaDef;
            ListViewOrder.DataBind();
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
            double precioUnitario = double.Parse(lblPrecioUnitario.Text);

           
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
        protected void verDetalles_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btnVerDetalles = (System.Web.UI.WebControls.LinkButton)sender;
            System.Web.UI.WebControls.ListViewItem item = (System.Web.UI.WebControls.ListViewItem)btnVerDetalles.NamingContainer;
            int indiceItem = item.DataItemIndex;

            System.Web.UI.WebControls.Label lblIdOrder = (System.Web.UI.WebControls.Label)item.FindControl("lblIdOrder");
            

            Order order = model.GetOrder(int.Parse(lblIdOrder.Text));

            VerIdOrder.Text = order.idOrder.ToString();

            DateTime fecha = order.orderDate;
            string textoFecha = fecha.ToString("dd/MM/yyyy"); // Cambia el formato según tus necesidades
            verFecha.Text = textoFecha;

            order.status = model.GetOrderStatus(order.orderStatus);
            verEstado.Text = order.status.description;
            verPrecio.Text = order.TotalPrice.ToString();

            List<OrderDetail> list = order.orderDetails.ToList();

            ListViewOrderDetail.DataSource = list;
            ListViewOrderDetail.DataBind();
        }
    }
}