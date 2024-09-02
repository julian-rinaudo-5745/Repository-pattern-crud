using Practica01.datos;
using Practica01.dominio;
using Practica01.servicios;


//ServicioArticulo servicioArticulo = new ServicioArticulo();
//ServicioFormaPago servicioFormaPago = new ServicioFormaPago();
ServicioFactura servicioFactura = new ServicioFactura();
///*
// * CRUD ARTICULOs
// */


//Articulo articulo1 = new Articulo
//{
//    Nombre = "Computadora",
//    PrecioUnitario = 1000000
//};

//Articulo articulo2 = new Articulo
//{
//    Nombre = "Televisor",
//    PrecioUnitario = 50000
//};

////CREAMOS ARTICULO
//servicioArticulo.Crear(articulo1);
//servicioArticulo.Crear(articulo2);

////EDITAMOS ARTICULO
////Se agrega la m para que el numero no se tome como tipo double
//articulo1.Id = 1;
//articulo1.PrecioUnitario = 1999999.99m;

//servicioArticulo.Editar(articulo1);

////Eliminamos ARTICULO
//servicioArticulo.Eliminar(2);

////TRAEMOS TODOS LOS ARTICULOS
//List<Articulo> articulos = servicioArticulo.ObtenerTodo();


//foreach (var articulo in articulos)
//{
//    Console.WriteLine($"Id: {articulo.Id}, Nombre: {articulo.Nombre}, Precio unitario: {articulo.PrecioUnitario}");
//}

///*
// * CRUD FormasPago
// */

//FormaPago formaPago = new FormaPago
//{
//    Nombre = "Credito"
//};

////CREAMOS FORMA DE PAGO
//servicioFormaPago.Crear(formaPago);

////EDITAMOS FORMA DE PAGO
//formaPago.Id = 3;
//formaPago.Nombre = "Cheque";

//servicioFormaPago.Editar(formaPago);

////Eliminamos FORMA DE PAGO
//servicioFormaPago.Eliminar(2);

////TRAEMOS TODAS LAS FORMAS DE PAGO
//List<FormaPago> formaPagos = servicioFormaPago.ObtenerTodo();

//foreach(var fp in formaPagos)
//{
//    Console.WriteLine($"Id: {fp.Id}, Nombre: {fp.Nombre}");
//}

/*
 * CRUD Facturas
 */

Factura factura = new Factura()
{
    FormaPago = new FormaPago()
    {
        Id = 1

    },
     Cliente = "Julian",
     Detalles = new List<DetalleFactura>()
     {
        new DetalleFactura()
        {
            Articulo = new Articulo() { Id = 1},
            Cantidad = 2,
            PrecioVenta = 2000  
        },
        new DetalleFactura()
        {
            Articulo = new Articulo() { Id = 2  },
            Cantidad = 1,
            PrecioVenta = 2400.20M
        }
    }
};

servicioFactura.Crear(factura);

