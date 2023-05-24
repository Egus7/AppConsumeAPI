using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Paginas.ConsumeApi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagApi : ContentPage
    {
        public PagApi()
        {
            InitializeComponent();
        }

        private string URL = "https://apiinterna.azurewebsites.net/api";

        private Models.Producto[] Productos { get; set; }

        private void Button_Leer(object sender, EventArgs e)
        {
            using (var wc = new WebClient()) {
                wc.Headers.Add("Content-Type", "application/json");
                var json = wc.DownloadString(URL + "/Productoes/" + txtId.Text);
                var producto = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Producto>(json);
                if(producto !=null)
                {
                    txtId.Text = producto.Prod_id;
                    txtNombre.Text = producto.Prod_nombre;
                    txtDescripcion.Text = producto.Prod_descripcion;
                    txtIva.Text = producto.Prod_iva.ToString();
                    txtCosto.Text = producto.Prod_costo;
                    txtPvp.Text = producto.Prod_pvp;
                    txtEstado.Text = producto.Prod_estado.ToString();
                    txtStock.Text = producto.Prod_stock.ToString();
                    // Obtener el nombre de la categoría
                    string nombreCategoria = ObtenerNombreCategoria(producto.Categoriacat_id);
                    txtCategoria.Text = nombreCategoria;
                }
                else
                {
                    txtId.Text = "";
                    txtNombre.Text = "";
                    txtDescripcion.Text = "";
                    txtIva.Text = "";
                    txtCosto.Text = "";
                    txtPvp.Text = "";
                    txtEstado.Text = "";
                    txtStock.Text = "";
                    txtCategoria.Text = "";
                }
            }
        }

        private string ObtenerNombreCategoria(string categoriaId)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                var json = wc.DownloadString(URL + "/Categorias/" + categoriaId);
                var categoria = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Categoria>(json);
                return categoria?.Cat_nombre;
            }
        }

        private void Button_Insertar(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");

                // Crear un objeto Producto con los datos del formulario
                var nuevoProducto = new Models.Producto
                {
                    Prod_id = txtId.Text,
                    Prod_nombre = txtNombre.Text,
                    Prod_descripcion = txtDescripcion.Text,
                    Prod_iva = Convert.ToBoolean(txtIva.Text),
                    Prod_costo = txtCosto.Text,
                    Prod_pvp = txtPvp.Text,
                    Prod_estado = Convert.ToBoolean(txtEstado.Text),
                    Prod_stock = Convert.ToInt32(txtStock.Text),
                    Categoriacat_id = txtCategoria.Text
                };

                // Serializar el objeto Producto a formato JSON
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(nuevoProducto);

                // Enviar la solicitud POST a la API para insertar el nuevo producto
                var respuesta = wc.UploadString(URL + "/Productoes", "POST", json);

                if (!string.IsNullOrEmpty(respuesta))
                {
                    // Manejar la respuesta de la API después de insertar el producto
                    // (puede ser necesario realizar alguna acción adicional)
                    Console.WriteLine("Producto insertado correctamente.");
                }
                else
                {
                    Console.WriteLine("Error al insertar el producto.");
                }
            }
        }

        private void Button_Actualizar(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");            

                // Crear un objeto Producto con los datos del formulario
                var productoActualizado = new Models.Producto
                {
                    Prod_id = txtId.Text,
                    Prod_nombre = txtNombre.Text,
                    Prod_descripcion = txtDescripcion.Text,
                    Prod_iva = Convert.ToBoolean(txtIva.Text),
                    Prod_costo = txtCosto.Text,
                    Prod_pvp = txtPvp.Text,
                    Prod_estado = Convert.ToBoolean(txtEstado.Text),
                    Prod_stock = Convert.ToInt32(txtStock.Text),
                    Categoriacat_id = txtCategoria.Text
                };

                // Serializar el objeto Producto a formato JSON
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(productoActualizado);

                try
                {
                    // Enviar la solicitud PUT a la API para actualizar el producto
                    var respuesta = wc.UploadString(URL + "/Productoes/" + txtId.Text, "PUT", json);

                    if (!string.IsNullOrEmpty(respuesta))
                    {
                        Console.WriteLine("Producto actualizado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("Error al actualizar el producto.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error de conexión: " + ex.Message);
                }
            }
        }

        private void Button_Eliminar(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                // Obtener el ID del producto a eliminar
                string productId = txtId.Text;

                try
                {
                    // Enviar la solicitud DELETE a la API para eliminar el producto
                    wc.UploadString(URL + "/Productoes/" + productId, "DELETE", "");

                    Console.WriteLine("Producto eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar el producto: " + ex.Message);
                }
            }
        }
    }
}