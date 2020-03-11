
# Título del Proyecto

_Prueba de evolutionscsas.com_

## Comenzando 🚀
Ruta del proyecto online [aquí](https://evolutiontest.herokuapp.com)

### Pre-requisitos 📋
_Luego de clonar el repositorio:_
_¿Qué cosas se necesitan para instalar el software de manera local?_
* [dotnet](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-3.0.100-windows-x64-installer) - Sdk para utilizar .Net Core
* Sql Server

Se puede comprobar la instalación en la carpeta del proyecto **WebApplication** con el comando
```
dotnet --version
```

### Instalación 🔧
En la raíz del proyecto se encuentra el archivo **database.sql**, donde se encuentra la estructura y datos de la base de datos, _construir usando este sql_.

* _Modificar la conexión a la base de datos, en el archivo **appsettings.json**_
* _Ubicarse en la carpeta **WebApplication** y ejecutar los siguientes comandos_
```
dotnet restore
dotnet run
```

Ahora podrás acceder al **https://localhost:5001** para tener acceso a la Api.

---
⌨️ con ❤️ basado en  [Villanuevand](https://gist.github.com/Villanuevand/6386899f70346d4580c723232524d35a#file-readme-espanol-md) 😊
