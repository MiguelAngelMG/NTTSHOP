
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
    public partial class LoginPage : System.Web.UI.Page
    { 
        ModelDAC model = new ModelDAC();
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                Session["session-compra"] = null;
                Session["session-language"] = null;
                Session["session-id"] = null;

            }
            Session["session-compra"] = null;
            Session["session-language"] = null;
            Session["session-id"] = null;
        }

        protected void SignIn_Click(object sender, EventArgs e)
        {
            string usuario = txtUser.Text;
            string contraseña = txtPassword.Text;

            bool login = LoginUser(usuario, contraseña);

            if (login)
            { 

                Response.Redirect("~/WebForms/PageLanguage.aspx");
                
            }
            else
            {

            }
        }
        protected void SignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/SignUpPage.aspx");
        }

        public bool LoginUser(string user, string password)
        {
            bool existeUsuario = false;

            try
            {
                Session["session-id"] = null;
                Session["session-language"] = null;
               
                string url = "https://localhost:7077/api/UserLogin/getLogin/" + user + "/" + password;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode statusCode = httpResponse.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    existeUsuario = true;

                    int idUser;
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var json = JObject.Parse(result);
                        idUser = json["idUser"].ToObject<int>();

                    }

                    Session["session-id"] = idUser;
                    User userlogin = model.GetUser(idUser);
                    List<ElementosCarrito> carrito = new List<ElementosCarrito>();
                    Session["session-compra"] = carrito;
                    Session["session-language"] = userlogin.Languages;
                }
                else
                {
                    string script = "alert(\"Error: Usuario o Password incorrecto! \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al hacer la solicitud: {ex.Message}");
                string script = "alert(\"Error: Usuario o Password incorrecto! \");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }

            return existeUsuario;
        }
      
    }
}