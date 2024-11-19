Ejecutar el script "script bd" en SQL Server Management Studio.
Luego abrir la solución en Visual Studio y ejecutar el programa.

Si ocurre el error:
No se puede procesar el archivo frmMenu.resx porque está en Internet o en una zona restringida, o bien tiene la marca de la Web. Quite esta marca si desea procesar los archivos.

Debe desbloquear el archivo en cuestion. Para eso en la carpeta "Carpeta Sistema de Ventas", seleccione el archivo.resx y abra sus propiedades, seleccione "Desbloquear" en la opcion de Seguridad.
Haga esto por cada archivo que le de error.

Si el programa no funciona probar con poner la carpeta "Idiomas" en 
\Carpeta Sistema de Ventas\bin\Debug


Usuarios:
Admin
clave123

Admin2
claveadmin2

Por defecto los usuarios creados combina el
DNI + Apellido
Aunque el usuario puede cambiar la clave si lo desea.
Después de 3 intentos fallidos al Iniciar sesión se bloquea al usuario, el Admin puede desbloquearlo y se le reseteará la calve por defecto al usuario.
