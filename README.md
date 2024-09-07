# Practica01

Tomando como base el Problema 1.5 de la guía de estudios correspondiente a la unidad temática N° 1, se pide:

## Estructura del proyecto

Crear un Proyecto de Consola con la siguiente estructura de carpetas:

```plaintext
proyecto [Practica01]:
│
├── dominio
│   └── clases del dominio del problema
│
├── datos
│   ├── interfaces
│   └── clases concretas para los repositorios de datos
│
└── servicios
    └── clases de servicio necesarias para gestionar el CRUD de la entidad principal

```
## Requisitos adicionales

1. **Base de datos**:  
   Crear una base de datos con la tabla transaccional y las tablas de soporte necesarias.

2. **Procedimientos almacenados**:  
   Desarrollar los procedimientos almacenados para operaciones de consulta y actualización tanto de las tablas transaccionales como de las de soporte. Utilizar los componentes ADO.NET desde C#.

3. **Clase `Program`**:  
   Incluir una clase `Program` que permita ejecutar los métodos de servicio definidos en la capa de negocio, mostrando las salidas por consola.
