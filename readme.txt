# PROYECTO - PRÁCTICA 1 GRUPO 4
# PROGRAMACIÓN AVANZADA WEB

## 1. INTEGRANTES DEL GRUPO
- ARAYA MURILLO THOMAS
- BONILLA MIRANDA MAUREEN
- LLAGUNO THOMAS ESTEBAN
- VALENCIA SANCHEZ NICOLAS

## 2. REPOSITORIO
URL: https://github.com/ThomasProgramer07/Practica1_Grupo4_PrograAvanzadaWeb

## 3. ESPECIFICACIÓN DEL PROYECTO

### a. Arquitectura del Proyecto
El proyecto implementa una arquitectura en capas (Layered Architecture) con separación de responsabilidades:

- **WebAvanzadaIICuatrimestre (Presentación)**: Aplicación web ASP.NET Core Razor Pages que maneja la interfaz de usuario y las solicitudes HTTP a través de controladores.

- **WebAvanzadaIICuatrimestre.BLL (Business Logic Layer)**: Capa de lógica de negocio que contiene los servicios que procesan las reglas de negocio y orquestan las operaciones entre la capa de presentación y la capa de datos.

- **WebAvanzadaIICuatrimestre.DAL (Data Access Layer)**: Capa de acceso a datos que gestiona la persistencia usando Entity Framework Core con SQLite. Contiene el DbContext, las entidades del modelo de datos y los repositorios.

El proyecto gestiona un sistema de lavado de vehículos con entidades principales: Clientes, Teléfonos, Dueños y Carros, implementando relaciones uno a muchos entre estas entidades.

### b. Libraries y Paquetes NuGet Utilizados
- **Entity Framework Core**: ORM para el acceso a datos y gestión de la base de datos
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.Sqlite (proveedor de base de datos SQLite)
  - Microsoft.EntityFrameworkCore.Tools (herramientas para migraciones)

- **AutoMapper**: Mapeo automático entre entidades de dominio y DTOs (Data Transfer Objects)
  - AutoMapper
  - AutoMapper.Extensions.Microsoft.DependencyInjection

- **Microsoft.Data.Sqlite**: Proveedor de datos para SQLite

### c. Principios SOLID y Patrones de Diseño Utilizados

**Principios SOLID:**
- **Single Responsibility Principle (SRP)**: Cada clase tiene una única responsabilidad. Los repositorios manejan solo el acceso a datos, los servicios solo la lógica de negocio, y los controladores solo la presentación.

- **Dependency Inversion Principle (DIP)**: Las capas superiores dependen de abstracciones (interfaces) en lugar de implementaciones concretas. Se utiliza inyección de dependencias para proporcionar las implementaciones.

- **Open/Closed Principle (OCP)**: El uso de interfaces y repositorios permite extender funcionalidades sin modificar el código existente.

**Patrones de Diseño:**
- **Repository Pattern**: Abstrae el acceso a datos proporcionando una interfaz entre la lógica de negocio y la capa de datos (ClienteRepositorio, DuennoRepositorio, CarroRepositorio).

- **Service Layer Pattern**: La capa BLL implementa servicios que encapsulan la lógica de negocio y coordinan operaciones entre repositorios.

- **Data Transfer Object (DTO) Pattern**: Uso de DTOs (ClienteDto, CarroDto, DuennoDto) para transferir datos entre capas sin exponer las entidades de dominio directamente.

- **Dependency Injection Pattern**: Configuración de servicios y repositorios mediante el contenedor de dependencias de ASP.NET Core, promoviendo bajo acoplamiento y alta testabilidad.

- **Unit of Work Pattern** (implícito): Entity Framework Core DbContext actúa como unidad de trabajo gestionando transacciones y cambios en las entidades.