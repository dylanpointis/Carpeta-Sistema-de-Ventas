namespace BE
{
    public class BEProducto
    {
        public long CodigoProducto{ get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set;}
        public string Color { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public int StockMax { get; set; }
        public int StockMin { get; set; }
        public int Almacenamiento { get; set; }
        public bool BorradoLogico { get; set; }

        public BEProducto(long codigoProducto, string modelo, string descripcion, string marca, string color, double precio, int stock, int smin, int smax, int almacenamiento, bool borrado) 
        {
            this.CodigoProducto = codigoProducto;
            this.Modelo = modelo;
            this.Descripcion = descripcion;
            this.Marca = marca;
            this.Color = color;
            this.Precio = precio;
            this.Stock = stock;
            this.StockMin = smin;
            this.StockMax = smax;
            this.Almacenamiento = almacenamiento;
            this.BorradoLogico =borrado;
        }

    }
}
