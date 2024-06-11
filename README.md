# Scribeo Back-End

## Description
Scribeo est une application de prise de notes inspirée de Pinterest. Ce dépôt contient le code du back-end, développé avec ASP.NET Core.

## Fonctionnalités
- API RESTful pour la gestion des utilisateurs et des notes
- Authentification et autorisation des utilisateurs
- Connexion à une base de données pour persister les données des utilisateurs et des notes

## Dépendances
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` : Pour la gestion de l'authentification et de l'identité
- `Microsoft.EntityFrameworkCore.SqlServer` : Pour l'utilisation de SQL Server avec Entity Framework Core

Pour voir toutes les dépendances, consultez le fichier `*.csproj`.

## Installation

### Prérequis
- .NET 5.0 ou supérieur doit être installé sur votre machine
- SQL Server doit être installé et configuré

### Étapes d'installation

1. Clonez le dépôt
   ```bash
   git clone https://github.com/Alexandre-png/Astate.git
   
2. Installez les dépendances
   ```bash
   dotnet restore
   
3. Mettez à jour la base de données
   ```bash
   dotnet ef database update

## Configuration
### Chaîne de Connexion
Assurez-vous de mettre à jour la chaîne de connexion à la base de données MySql dans le fichier appsettings.json :

  ```json
{
    "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "TokenSettings": {
    "SecretKey": "d6277453565151e5301dea8bd3953e2f"
  },

  "ConnectionStrings": {
    "DefaultConnection": "server=votre-serveur;port=port-du-serveur;database= la-base-de-donnée;user= votre-utilisateur;password=mot-de-passe-de-l'utilisateur"
  },
  
  "AllowedHosts": "*"
  
}
```
## Structure du projet 
```
backend/
├── Controllers/             # Contient les contrôleurs API
├── Data/                    # Contient le contexte de l'instance de la base de données
├── Models/                  # Contient les modèles de données
├── Services/                # Contient les services pour la logique métier
└── wwwroot/                 # Contient les fichiers où sont sauvegardées les images
```

## Contribution
Les contributions sont les bienvenues ! Veuillez ouvrir une issue pour discuter des changements que vous souhaitez apporter.

## Licence
Ce projet est un projet de formation personnel et est mis à disposition en open source. Vous êtes libre d'utiliser, copier, modifier et distribuer ce code pour tout usage.


