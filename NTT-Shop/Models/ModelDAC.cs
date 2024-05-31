using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace NTT_Shop.Models
{
    public class ModelDAC
    {
        private string baseUrl = "https://localhost:7077/api/";
        #region Peticiones Users
        public User GetUser(int idUser)
        {
            User user = new User();
            try
            {
                string url = baseUrl + "Users/getUser/" + idUser;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    user = json["user"].ToObject<User>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los idiomas: {ex.Message}");
            }

            return user;
        }
        public bool UpdateUser(User user, out string message)
        {
            bool insertado = false;
            message = "";
            // esto nos permite poder poner lo de Language{
            //   idLanguage = "", descripcion = "", iso "" }
            var userData = new { user = user };

            string jsonData = JsonConvert.SerializeObject(userData);

            string url = baseUrl + "Users/updateUser";

            try
            {
                //HTTP put
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                // Escribimos el cuerpo del mensaje
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode statusCode = httpResponse.StatusCode;
                message = httpResponse.StatusDescription;

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
                        message = "Error de la API: " + errorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al realizar la solicitud: " + ex.Message);
                message = ex.Message;
            }

            return insertado;
        }
        public List<User> GetAllUser()
        {
            List<User> usuarios = new List<User>();

            try
            {
                string url = baseUrl + "Users/getAllUsers";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    var usuarioArray = json["usersList"].ToObject<JArray>();
                    usuarios = usuarioArray.ToObject<List<User>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
            }

            return usuarios;
        }
        #endregion
        #region Peticiones Rate
        public Rate GetRate(int idRate)
        {
            Rate rateResult = new Rate();
            try
            {
                string url = baseUrl + "Rates/getRate/" + idRate;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    rateResult = json["getRates"].ToObject<Rate>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el rate: {ex.Message}");
            }

            return rateResult;
        }
        #endregion
        #region Orders

        public bool InsertOrder(Order order)
        {
            bool insertado = false;

            var orderDATA = new { order = order };

            string jsonData = JsonConvert.SerializeObject(orderDATA);

            string url = baseUrl + "Orders/insertOrder";

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

        public List<Order> GetAllOrder()
        {
            List<Order> orders = new List<Order>();

            try
            {
                string url = baseUrl + "Orders/getAllOrders";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    var ordertArray = json["ordersList"].ToObject<JArray>();
                    orders = ordertArray.ToObject<List<Order>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los pedidos: {ex.Message}");
            }

            return orders;
        }
        public Order GetOrder(int idOrder)
        {
            Order orders = new Order();
            try
            {

                string url = baseUrl + "Orders/getOrder/" + idOrder;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    orders = json["order"].ToObject<Order>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el order: {ex.Message}");
            }

            return orders;
        }

        #endregion
        #region OrdersDetails
        #endregion
        #region OrdersStatus
        public OrderStatus GetOrderStatus(int idStatus)
        {
            OrderStatus status = new OrderStatus();
            try
            {

                string url = baseUrl + "Orders/getOrderStatus/" + idStatus ;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    status = json["status"].ToObject<OrderStatus>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el estado del pedido: {ex.Message}");
            }

            return status;
        }
        #endregion
        #region Peticiones Products

        public List<Product> GetAllProducts(string language)
        {
            List<Product> products = new List<Product>();

            try
            {
                string url = baseUrl + "Products/getAllProducts/" + language;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    var productArray = json["productsList"].ToObject<JArray>();
                    products = productArray.ToObject<List<Product>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
            }

            return products;
        }
        public Product GetProduct(int idProduct, string language)
        {
            Product product = new Product();
            try
            {
               
                string url = baseUrl + "Products/getProduct/" + idProduct + "/" + language;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    product = json["getProduct"].ToObject<Product>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el producto: {ex.Message}");
            }

            return product;
        }

        #endregion
        #region Peticiones Language
        public List<Language> GetAllLanguage()
        {
            List<Language> languages = new List<Language>();

            try
            {
                string url = baseUrl + "Language/getAllLanguages";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    var languageArray = json["languageList"].ToObject<JArray>();
                    languages = languageArray.ToObject<List<Language>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los idiomas: {ex.Message}");
            }

            return languages;
        }

        public bool UpdateLanguage(Language language, out string message)
        {
            bool insertado = false;
            message = "";
            // esto nos permite poder poner lo de Language{
            //   idLanguage = "", descripcion = "", iso "" }
            var languageData = new { language = language };

            string jsonData = JsonConvert.SerializeObject(languageData);

            string url = baseUrl + "Language/updateLanguage";

            try
            {
                //HTTP put
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                // Escribimos el cuerpo del mensaje
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode statusCode = httpResponse.StatusCode;
                message = httpResponse.StatusDescription;

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
                        message = "Error de la API: " + errorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al realizar la solicitud: " + ex.Message);
                message = ex.Message;
            }

            return insertado;
        }

        public bool DeleteLanguage(int id)
        {
            bool eliminado = false;

            try
            {

                string url = baseUrl + "Language/deleteLanguage/" + id;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "DELETE";
                httpRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode statusCode = httpResponse.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    eliminado = true;
                }
                else
                {

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

            return eliminado;
        }

        public bool InsertLanguage(Language language)
        {
            bool insertado = false;

            // esto nos permite poder poner lo de Language{
            //   idLanguage = "", descripcion = "", iso "" }
            var languageData = new { language = language };

            string jsonData = JsonConvert.SerializeObject(languageData);

            string url = baseUrl + "Language/insertLanguage";

            try
            {
                //HTTP POST
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




        #endregion
    }
}