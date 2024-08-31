using Practica01.dominio;
using Practica01.servicios;


ServicioArticulo servicioArticulo = new ServicioArticulo();


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

//CREAMOS ARTICULO
servicioArticulo.Crear(articulo1);
servicioArticulo.Crear(articulo2);

//EDITAMOS ARTICULO
//Se agrega la m para que el numero no se tome como tipo double
articulo1.Id = 1;
articulo1.PrecioUnitario = 1999999.99m;

servicioArticulo.Editar(articulo1);

//Eliminamos ARTICULO
servicioArticulo.Eliminar(2);

//TRAEMOS TODOS LOS ARTICULOS
List<Articulo> articulos = servicioArticulo.ObtenerTodo();

Console.WriteLine(articulos);



