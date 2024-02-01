Prueba Técnica Desarrollador de Servicios Web Junior

'Estado de cuenta de una tarjeta de crédito' 
En esta prueba técnica, el objetivo es desarrollar una aplicación web utilizando tecnologías .NET (Razor, 
jQuery, MVC) y una REST API que interactúe con una base de datos SQL Server utilizando procesos 
almacenados con PL/SQL. 

ARQUITECTURA DE LA SOLUCIÓN:

La aplicación se desarrollo en ASP.NET MVC con Razor y jQuery.

Para la implementación de APIs se utilizó la herramienta Swagger la cual permitió tener la documentación
de cada API alojada, asi mismo se utilizó Postman y se creo una colección para que permitiera probar
cada API creada, con el fin de que tuviera una funcionalidad completa. Para probar la colección de Postman 
se debe importar la colección dentro del mismo.

Entre algunos endpoints de la API que se implementaron:
- GET/api/Tarjeta-de-credito/Listado: Obtiene el Listado de Tarjetas de Crédito de con su correspondiente número de tarjeta, nombre y apellido.
- GET/api/Tarjeta-de-credito/EstadoCuenta: Obtiene el listado de movimientos dentro de una cuenta de un cliente.
- POST/api/Tarjeta-de-credito/GuardarMovimiento/{id}: Almacena un movimiento ya sea un registro de pago o compra.
- PUT/api/Tarjeta-de-credito/EditarMovimiento/{id}: Actualiza la información de un movimiento según su ID.
- DELETE/api/Tarjeta-de-credito/EliminarMovimiento/{id}: Elimina un movimiento según su ID.
