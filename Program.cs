using Practica01.datos;
using Practica01.dominio;
using Practica01.servicios;

ServicioArticulo servicioArticulo = new ServicioArticulo();
ServicioFormaPago servicioFormaPago = new ServicioFormaPago();
ServicioFactura servicioFactura = new ServicioFactura();
ServicioDetalleFactura servicioDetalleFactura = new ServicioDetalleFactura();

/*
 *  ARTICULOs
 */

Articulo articulo1 = new Articulo
{
    Nombre = "Computadora",
    PrecioUnitario = 1000000
};

Articulo articulo2 = new Articulo
{
    Nombre = "Televisor",
    PrecioUnitario = 50000
};

Articulo articulo3 = new Articulo
{
    Nombre = "Silla",
    PrecioUnitario = 300000
};

//CREAMOS ARTICULO
servicioArticulo.Crear(articulo1);
servicioArticulo.Crear(articulo2);
servicioArticulo.Crear(articulo2);

//EDITAMOS ARTICULO
articulo1.Id = 1;
articulo1.PrecioUnitario = 1999999.99m;

servicioArticulo.Editar(articulo1);

//Eliminamos ARTICULO
servicioArticulo.Eliminar(2);

//TRAEMOS TODOS LOS ARTICULOS
List<Articulo> articulos = servicioArticulo.ObtenerTodo();

foreach (var articulo in articulos)
{
    Console.WriteLine($"Articulo Id: {articulo.Id}, Nombre: {articulo.Nombre}, Precio unitario: {articulo.PrecioUnitario}");
}

/*
 * CRUD FormasPago
 */

FormaPago formaPago1 = new FormaPago
{
    Nombre = "Credito"
};

FormaPago formaPago2 = new FormaPago
{
    Nombre = "Débito"
};

FormaPago formaPago3 = new FormaPago
{
    Nombre = "Efectivo"
};

//CREAMOS FORMA DE PAGO
servicioFormaPago.Crear(formaPago1);
servicioFormaPago.Crear(formaPago2);
servicioFormaPago.Crear(formaPago3);

//EDITAMOS FORMA DE PAGO
FormaPago formaPago = new FormaPago();
formaPago.Id = 3;
formaPago.Nombre = "Cheque";

servicioFormaPago.Editar(formaPago);

//Eliminamos FORMA DE PAGO
servicioFormaPago.Eliminar(2);

//TRAEMOS TODAS LAS FORMAS DE PAGO
List<FormaPago> formaPagos = servicioFormaPago.ObtenerTodo();

foreach (var fp in formaPagos)
{
    Console.WriteLine($"Forma de pago Id: {fp.Id}, Nombre: {fp.Nombre}");
}

/*
 * CRUD Facturas
 */

Factura factura1 = new Factura()
{
    FormaPago = new FormaPago() { Id = 1},
    Cliente = "Julian",
    Detalles = new List<DetalleFactura>()
    {
        new DetalleFactura()
        {
            Articulo = new Articulo(){ Id = 1},
            Cantidad = 2,
            PrecioVenta = 2000
        },
        new DetalleFactura()
        {
            Articulo = new Articulo(){ Id = 3},
            Cantidad = 1,
            PrecioVenta = 2400.20M
        }
    }
};


//CREAMOS FACTURA
servicioFactura.Crear(factura1);

//ELIMINAMOS UNA FACTURA
//servicioFactura.Eliminar(2);

//TRAEMOS TODAS LAS FACTURAS
List<Factura> facturas = servicioFactura.ObtenerTodo();

foreach (Factura factura in facturas)
{
    Console.WriteLine($"nroFactura: {factura.NroFactura}, fecha: {factura.Fecha}, forma de pago: {factura.FormaPago.Nombre}, cliente: {factura.Cliente}");

    foreach (DetalleFactura detalle in factura.Detalles)
    {
        Console.WriteLine($"Nombre del articulo: {detalle.Articulo.Nombre}, cantidad: {detalle.Cantidad}, precio de venta: {detalle.PrecioVenta}");
    }
}






