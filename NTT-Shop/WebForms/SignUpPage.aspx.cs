
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
using System.Text.RegularExpressions;
using System.Xml.Linq;
using NTT_Shop.Models;

namespace NTT_Shop.WebForms
{
    public partial class SignUpPage : System.Web.UI.Page
    {
        private string baseUrl = "https://localhost:7077/api/"; 
        ModelDAC model = new ModelDAC();
        protected void Page_Load(object sender, EventArgs e)
        {
         

            if (!IsPostBack)
            {

            }
            List<Language> languages = model.GetAllLanguage();
            foreach (Language language in languages)
            {
                ListItem item = new ListItem(language.descripcion, language.iso);
                cboxLanguage.Items.Add(item);
            }
        }

        protected void Registro_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string contraseña = txtPassword.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string emeal = txtEmail.Text;
            string languageIso = cboxLanguage.SelectedValue;


            //Crear nuevo elemento invocando método API de inserción
            User user = new User()
            {
                PkUser = 0,
                Login = login,
                Password = contraseña,
                Name = nombre,
                Surname1 = apellido,
                Surname2 = "",
                Address = "",
                Province = "",
                Town = "",
                PostalCode = "",
                Phone = "",
                Email = emeal,
                Languages = languageIso,
                Rate = 1
            };

            bool insertado = false;
            string message = "";


            if (InsertUserValidation(user))
            {
                insertado = InsertUser(user);

                if (insertado)
                {
                    //Borrar campos
                    txtApellido.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtLogin.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtNombre.Text = string.Empty;

                    string script = "alert(\"Usuario creado! " + message + "\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);

                    Response.Redirect("~/WebForms/LoginPage.aspx");

                }
                else
                {
                    string script = "alert(\"Error: No se ha podido crear la cuenta! " + message + "\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "alert(\"Error: Debes de rellenar todos los campos y la contraseña debe de contener minimo una mayuscula, minuscula y numero " + message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
           

        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/LoginPage.aspx");
        }


        public bool LoginUser(string user, string password)
        {
            bool existeUsuario = false;

            try
            {
                string url = baseUrl + "UserLogin/getLogin/" + user + "/" + password;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode statusCode = httpResponse.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    existeUsuario = true;
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

        private bool InsertUser(User user)
        {
            bool insertado = false;

            var userData = new { user = user };

            string jsonData = JsonConvert.SerializeObject(userData);

            string url = baseUrl + "Users/insertUser";

            try
            {
                
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                // Escribimos el cuerpo del mensaje
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode statusCode = httpResponse.StatusCode;

                // Si es OK
                if (statusCode == HttpStatusCode.OK)
                {
                    insertado = true;
                }
                else
                {
                    // Si hay un error, leer el mensaje de error 
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string errorMessage = streamReader.ReadToEnd();
                        Console.WriteLine("Error de la API: " + errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al realizar la solicitud: " + ex.Message);
            }

            return insertado;
        }

        private bool InsertUserValidation(User request)
        {
            if (request != null
                && !string.IsNullOrWhiteSpace(request.Login)
                && !string.IsNullOrWhiteSpace(request.Password)
                && !string.IsNullOrWhiteSpace(request.Name)
                && !string.IsNullOrWhiteSpace(request.Surname1)
                && !string.IsNullOrWhiteSpace(request.Email)
                && !string.IsNullOrWhiteSpace(request.Languages)
                && request.Rate != 0
                && ValidationPassword(request.Password))
            {
                // Todos los campos son válidos, no se requiere ningún cambio en el estilo del borde
                return true;
            }
            else
            {
                // Al menos uno de los campos es inválido, establece el estilo del borde en rojo para el TextBox correspondiente
                if (request != null)
                {
                    if (string.IsNullOrWhiteSpace(request.Login))
                        SetErrorBorder(txtLogin);
                    if (string.IsNullOrWhiteSpace(request.Password))
                        SetErrorBorder(txtPassword);
                    if (string.IsNullOrWhiteSpace(request.Name))
                        SetErrorBorder(txtNombre);
                    if (string.IsNullOrWhiteSpace(request.Surname1))
                        SetErrorBorder(txtApellido);
                    if (string.IsNullOrWhiteSpace(request.Email))
                        SetErrorBorder(txtEmail);
                    //if (string.IsNullOrWhiteSpace(request.Languages))
                    //    SetErrorBorder(txtLanguages);
                }
                return false;
            }
        }

        private bool ValidationPassword(string password)
        {
            string regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{1,}$"; //Debe tener minimo una mayuscula, minuscula y numero 

            if (Regex.IsMatch(password, regex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        protected void SetErrorBorder(System.Web.UI.WebControls.TextBox textBox)
        {
            textBox.Style["border"] = "1px solid red"; 
        }

        protected void SetNormalBorder(System.Web.UI.WebControls.TextBox textBox)
        {
            textBox.Style.Remove("border"); 
        }
    }
}