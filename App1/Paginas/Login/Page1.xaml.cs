using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Paginas.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var datos = new NameValueCollection();
            datos.Add("usuario", "Gustavo");
            datos.Add("clave", "1234");
            datos.Add("direccion", "Av 29 de Junio");
            datos.Add("telefono", "0956256326");

            var mp = new MainPage();
            mp.Datos = datos;
            mp.Usuario = "jjkl";
            mp.Clave = "123";

            Navigation.PushAsync(mp);
        }
    }
}