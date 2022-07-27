using ApiParanaBanco.Controllers;
using ApiParanaBanco.Model;
using Newtonsoft.Json;
using System.Text;

namespace TestesUnitariosApiParanaBanco
{
    [TestClass]
    public class TestesClientesController
    {
        ClientesController Cliente = new();

        #region testes diretamente no controller
        [TestMethod]
        public void TesteGetClientes()
        {
            Cliente.Get();
        }

        [TestMethod]
        public void TestePostcliente()
        {
            var cliente = new Cliente
            {
                Nome = "Ewerton",
                Email = "ewerton@guimares.com.br"
            };
            Cliente.Post(cliente);
        }

        [TestMethod]
        public void TesteGetClienteEmail()
        {
            Cliente.Get("ewerton@guimaraes.com.br");
        }

        [TestMethod]
        public void TestePutCliente()
        {
            var cliente = new Cliente
            {
                Nome = "Ewerton Guimarães",
                Email = "ewerton@guimares.com.br"
            };
            Cliente.Put(1, cliente);
        }

        [TestMethod]
        public void TesteDeleteCliente()
        {
            Cliente.Delete("ewerton@guimaraes.com.br");
        }

        #endregion

        #region testes consumindo a API
        HttpClient http = new HttpClient { BaseAddress = new Uri("https://ewertondev.com.br/api/clientes/") };



        [TestMethod]
        public void TesteGetClientesApi()
        {
            var response = http.GetAsync("").Result.StatusCode.ToString();
            Assert.AreEqual("OK", response);
        }

        [TestMethod]
        public void TestePostclienteApi()
        {
            var cliente = new Cliente
            {
                Nome = "Ewerton Guimarães",
                Email = "ewerton@guimares.com.br"
            };
            var json = JsonConvert.SerializeObject(cliente);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = http.PostAsync("", httpContent).Result.StatusCode.ToString(); ;

            Assert.AreEqual("OK", response);
        }

        [TestMethod]
        public void TesteGetClienteEmailApi()
        {
            var result = http.GetAsync("ewerton@guimaraes.com.br").Result;
            var statusCode = result.StatusCode.ToString();
            Assert.AreEqual("OK", statusCode);
        }

        [TestMethod]
        public void TestePutClienteApi()
        {
            var cliente = new Cliente
            {
                Nome = "Ewerton Guimarães",
                Email = "ewertonguimaraes2@gmail.com"
            };
            var json = JsonConvert.SerializeObject(cliente);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = http.PutAsync("1", httpContent).Result.StatusCode.ToString(); ;

            Assert.AreEqual("OK", response);
        }

        [TestMethod]
        public void TesteDeleteClienteApi()
        {
            var response = http.DeleteAsync("ewerton@guimaraes.com.br").Result.StatusCode.ToString();

            Assert.AreEqual("OK", response);
        }
        #endregion
    }
}