# BlogEngineApp
API REST para una aplicacion para que los usuarios realizen posteos en un blog
### Requisitos para ejecutar la api
Se necesita ejecutar el script de la base de datos (sql server) que subi aca llamado
"BlogBD.sql",asi ya se creara la base de datos con sus tablas y los datos.
Luego en el proyecto hay que configurar la conexion al sql server:
Para ello solamente hay que cambiar la informacion que ya esta seteada en el archivo "appsettings.json",
En la parte que dice  "BlogDBContext" hay que setearle el 'User ID' y 'Password' y ponerle el que tengan configurado en 
su sql server.
###Herramientas utilizadas para el proyecto
Se utilizo ASP.NET con lenguaje C# , IDE visual studio 2019.
AutoMapper para el mapeo entre los DTOs y Modelos.
ORM Entity Framework para el mapeo entre las tablas y los modelos.
SQL server para la base de datos.
Postman para el testeo de los endpoind de la Api. 
###Tiempo en realizar la prueba tecnica
Tarde en total 2 semanas desde que recibi el enunciado de la prueba tecnica.
###Extras
Subo un archivo "PostmanApiBlog.json" que es para importar ya las pruebas de los endpoint que estuve haciendo 
en postman.


