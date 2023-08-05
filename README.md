# Discoteque-Dotnet

This is my repository for learning and practicing about .Net with C# in the Backend's BootCamp from WomenWhoCode sponsored by Perficient.


## Checklist

* [x] Las canciones deben tener un album asociado. Al enviarlo con un album que no existe, capturar la excepción y devolverla. check sin mostrar el error
* [x] Los tours deben todos estar en el futuro del 2021. check
* [x] Al crear un album nuevo, el album debe estar entre 1905 y 2023. Si está fuera de ese rango debe fallar. check
* [x] Al album le vamos a agregar un costo. En forma de número. El costo no puede ser negativo y puede tener dos decimales. Lo vamos a hacer por defecto en 50.000. Check
* [x] la fecha la vamos a devolver en ISO 9000. YYYY-MM-DD
* [x] Vamos a agregar una acción para agregar canciones en bache o bulto. Que en vez de aceptar una sola, acepte una lista de canciones. Esta lista se pone toda en la cola y luego se guarda. Si alguna falla debe devolverse la excepción y asegurarse que no se guardo ninguna.
* [x] Los artistas no pueden tener un nombre superior a los 100 carácteres. -- ya pero en model
* [x] Los albums no pueden contener en su título la siguiente palabra: Revolución, Poder, Amor y Guerra.
* [x] Todas las validaciones se deben hacer en el service (requerimiento)

* [] Crear el basemessage para todas las entidades.
* [] convertir el basemessage a basemessage genérico y utilizar uno solo para todos las entidades. Convertir el BuildResponse a una forma genérica totalmente de manera de que sea una clase estática en utils y poderla invocar desde todos los servicios
