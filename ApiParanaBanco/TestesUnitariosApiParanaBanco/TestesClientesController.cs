using ApiParanaBanco.Controllers;
using ApiParanaBanco.Model;

namespace TestesUnitariosApiParanaBanco
{
    [TestClass]
    public class TestesClientesController
    {
        ClientesController Cliente = new();
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
    }
}