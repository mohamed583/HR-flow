# Documentation d’exécution du projet HRFlow

## 1. Exécution du backend seul

### Prérequis

Avant de commencer, installez les packages NuGet requis avec les versions spécifiques. Exécutez les commandes suivantes à la racine du projet :

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
dotnet add src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj package itext7 --version 9.2.0
```

### Configuration de la base de données

Passez vers le dossier appsettings.json situé dans backend-projetdev.API et changer la connectionStrings par:

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=backendProjetdev;Trusted_Connection=True;TrustServerCertificate=True"
  },
```

Appliquez les migrations pour créer la base de données :

```bash

dotnet ef database update --project src/backend-projetdev.Infrastructure --startup-project src/backend-projetdev.API --context ApplicationDbContext
```

⚠️ Il n’est pas nécessaire d’appliquer manuellement les migrations à chaque modification, elles sont automatiquement appliquées au build.

Pour créer une nouvelle migration (remplacez [migrationName] par votre nom de migration) :

```bash
dotnet ef migrations add [migrationName] --project src/backend-projetdev.Infrastructure/backend-projetdev.Infrastructure.csproj --startup-project src/backend-projetdev.API --context ApplicationDbContext
```

### Connexion à la base de données

Pour visualiser les données dans SQL Server, utilisez SQL Server Management Studio (SSMS) ou l’outil intégré de Visual Studio.

Voici les informations de connexion dans appsettings.json :

![image](https://github.com/user-attachments/assets/281e5924-1a38-4504-9bd2-7f3f14f71b77)

### Accès à la documentation Swagger

Lancez le backend et ouvrez dans votre navigateur :

http://localhost:xxxx

Remplacez xxxx par le port configuré (visible dans la console au démarrage ou dans launchSettings.json).

### Exécuter des requêtes API localement sans Swagger ni Postman

Ouvrez le fichier backend-projetdev.http dans Visual Studio Code (avec l’extension REST Client installée).

Pour envoyer une requête :

- Ouvrez le fichier
- Localisez la requête souhaitée
- Cliquez sur Send Request au-dessus de la méthode (ou utilisez Ctrl+Alt+R)

## 2. Exécution du frontend

### Prérequis

Installez SQL Server Management Studio (SSMS) version 20 (disponible ici) :

[SQL Server Management Studio 20](https://sqlserverbuilds.blogspot.com/2018/01/sql-server-management-studio-ssms.html)

Lancez un container SQL Server pour la base de données :

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPass123!" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

### Changement de la configuration de la base de donnée dans le backend

Ouvrez le projet backend et passez vers le dossier appsettings.json situé dans backend-projetdev.API et changer la connectionStrings par:

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=host.docker.internal,1433;Database=MonAppDb;User Id=sa;Password=YourPass123!;TrustServerCertificate=True;"
},
```

### Démarrage du backend en container Docker

Positionnez-vous dans le dossier backend :

```bash
cd backend-projet-dev
```

Créez l’image Docker :

```bash
docker build -t hrflow-backend:latest .
```

Lancez le container backend :

```bash
docker run -d -p 5000:5000 hrflow-backend:latest
```

### Configuration pour visualiser la base dans SSMS

Utilisez les paramètres suivants pour vous connecter sur SSMS (mot de passe : YourPass123!) :

![image](https://github.com/user-attachments/assets/7c4fe327-81ee-4f43-97de-c6b87ff096d4)

## 3. Exécution complète du projet (backend + frontend)

### Changement de la configuration de la base de donnée dans le backend

Ouvrez le projet backend et passez vers le dossier appsettings.json situé dans backend-projetdev.API et changer la connectionStrings par:

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=MonAppDb;User Id=sa;Password=YourPass123!;TrustServerCertificate=True;"
},
```

### Option 1 : via Docker Compose

Dans la racine du projet, lancez :

```bash
docker-compose up --build
```

Pour arrêter :

```bash
docker-compose down
```

Pour arrêter et supprimer les volumes persistants :

```bash
docker-compose down -v
```

### Option 2 : via Docker Swarm

Ouvrez 3 terminaux :

- Dans le dossier frontend, créez l’image frontend :

```bash
docker build -t hrflow-frontend:latest .
```

- Dans le dossier backend, créez l’image backend :

```bash
docker build -t hrflow-backend:latest .
```

- Dans la racine du projet, déployez la stack :

```bash
docker stack deploy -c docker-compose.swarm.yml hrflow
```

Pour arrêter la stack :

```bash
docker stack rm hrflow
```

### Configuration pour visualiser la base dans SSMS

Utilisez les paramètres suivants pour vous connecter sur SSMS (mot de passe : YourPass123!) :

![image](https://github.com/user-attachments/assets/7c4fe327-81ee-4f43-97de-c6b87ff096d4)

## 4. Création d’un utilisateur admin

Pour créer un admin manuellement :

- Effectuez une inscription (register) pour créer un candidat.

- Ouvrez la base de données (via SSMS) et dans la table Personne changez la colonne Discriminator de Candidat en Employe pour cet utilisateur.

- Dans la table AspNetUsersRoles :

  - Remplacez l’id du rôle Candidat par l’id du rôle Admin (récuperer à tarversla table AspNetRoles) dans la table AspNetUserRoles.

  - Ajoutez une ligne avec l’id utilisateur et l’id rôle Employe dans la meme table.

- Pour obtenir un accés avec les nouveaux rôles, reconnectez-vous.

## 5. Création d’un openRouter API key

- passez vers [OpenRouter](https://openrouter.ai/settings/keys) pour creer une clé API
- Insérer la clé dans appsettings.json situé dans backend-projetdev.API du projet backend dans la variable OpenRouterApiKey
