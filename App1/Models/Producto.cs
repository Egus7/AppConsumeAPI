using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Producto
    {
        public string Prod_id { get; set; }

        public string Prod_nombre { get; set; }

        public string Prod_descripcion { get; set; }

        public bool Prod_iva { get; set; }

        public string Prod_costo { get; set; }

        public string Prod_pvp { get; set; }

        public bool Prod_estado { get; set; }

        public int Prod_stock { get; set; }

        public string Categoriacat_id { get; set; }

    }
}
