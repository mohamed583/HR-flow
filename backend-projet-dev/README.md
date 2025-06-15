# Projet .NET - HR flow

Ce projet est une application .NET Core pour la gestion RH. Suivez les étapes ci-dessous pour configurer et exécuter le projet.

## Prérequis

Avant de commencer, vous devez installer les packages NuGet listés ci-dessous, avec les versions spécifiées.

### Installation des packages

Exécutez les commandes suivantes dans votre terminal à la racine du projet pour installer les packages requis :

```bash
dotnet add src/backend-projetdev.API/backend-projetdev.API.csproj package Microsoft.AspNetCore.OpenApi --version 9.0.5
dotnet add src/backend-projetdev.API/backend-projetdev.API.csproj package Microsoft.EntityFrameworkCore.Design --version 9.0.5
dotnet add src/backend-projetdev.API/backend-projetdev.API.csproj package Swashbuckle.AspNetCore --version 8.1.2

dotnet add src/backend-projetdev.Application/backend-projetdev.Application.csproj package AutoMapper --version 14.0.0
dotnet add src/backend-projetdev.Application/backend-projetdev.Application.csproj package FluentValidation --version 12.0.0
dotnet add src/backend-projetdev.Application/backend-projetdev.Application.csproj package FluentValidation.DependencyInjectionExtensions --version 12.0.0
dotnet add src/backend-projetdev.Application/backend-projetdev.Application.csproj package MediatR --version 11.1.0
dotnet add src/backend-projetdev.Application/backend-projetdev.Application.csproj package MediatR.Extensions.Microsoft.DependencyInjection --version 11.1.0
dotnet add src/backend-projetdev.Application/backend-projetdev.Application.csproj package Microsoft.AspNetCore.Http.Features --version 5.0.17

dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.5
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.AspNetCore.Http.Abstractions --version 2.3.0
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 9.0.5
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.EntityFrameworkCore --version 9.0.5
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.5
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.Extensions.Configuration --version 9.0.5
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.Extensions.Identity.Core --version 9.0.5
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package Microsoft.IdentityModel.Tokens --version 8.11.0
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package System.IdentityModel.Tokens.Jwt --version 8.11.0
```
### Configuration de la base de données
Une fois les packages installés, appliquez les migrations pour créer la base de données en exécutant la commande suivante :
```bash
dotnet ef database update --project src/backend-projetdev.Infrastructure --startup-project src/backend-projetdev.API --context ApplicationDbContext
```
Il n'est pas necessaire d'appliquer toujours les migrations même si il y'a des nouvelles car le projet migre automatiquements les migrations existantes lors du Build

Si vous souhaitez créer une migration, utilisez la commande suivante (remplacez [migrationName] par le nom de votre migration) :
```bash
dotnet ef migrations add [migrationName] --project src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj --startup-project src/backend-projetdev.API --context ApplicationDbContext
```

### Connexion à la base de données
Maintenant pour visualiser les données dans la base il faut se connecter sur SQL Server soit à travers SQL Server de Visual Studio 
ou bien à travers l'application SQL Server Management Studio et voila les données de connxion figurant dans appsettings.json

![image](https://github.com/user-attachments/assets/281e5924-1a38-4504-9bd2-7f3f14f71b77)

### Accès à la documentation Swagger
Une fois le projet lancé, vous pouvez accéder à l'interface Swagger pour tester les endpoints de l'API via l’URL suivante :
```bash
http://localhost:5254
```
Remplace xxxx par le port utilisé par ton application (visible dans la console au démarrage ou dans launchSettings.json).

### Exécuter des requêtes API localement sans Swagger ni Postman
Si vous souhaitez tester vos API localement sans passer par Swagger ni utiliser un outil externe comme Postman, vous pouvez utiliser le fichier backend-projetdev.http fourni dans le projet.

Ce fichier peut être ouvert avec Visual Studio Code (avec l'extension REST Client installée).

Pour envoyer une requête :

Ouvrez backend-projetdev.http

Localisez l'API que vous souhaitez tester

Cliquez sur le bouton "Send Request" qui apparaît au-dessus de la méthode (ou utilisez Ctrl+Alt+R)

Cela vous permet d'interagir directement avec vos endpoints dans un environnement simple et rapide.
