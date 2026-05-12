# ToDoList.NET

API REST de gestion de tâches développée avec ASP.NET Core et PostgreSQL dans le but d’apprendre les fondamentaux du backend moderne en .NET.

## Technologies utilisées

- ASP.NET Core (.NET 10)
- Entity Framework Core
- PostgreSQL
- FluentValidation
- JWT Authentication
- BCrypt
- Swagger / Swashbuckle

---

## Fonctionnalités

### Authentification

- Inscription utilisateur
- Connexion avec JWT
- Hash des mots de passe avec BCrypt
- Routes protégées via `[Authorize]`

### Gestion des tâches

- Création de tâches
- Modification de tâches
- Suppression de tâches
- Affichage des tâches
- Système multi-utilisateur sécurisé

### Validation

Validation des requêtes avec FluentValidation :

- Email valide
- Password minimum
- Champs requis

---

## Architecture

```txt
Src/
├── Contracts/
│   ├── Requests/
│   └── Responses/
├── Controllers/
├── Services/
├── Validators/
├── Mappers/
├── Models/
```

### Séparation des responsabilités

- Controllers → gestion HTTP
- Services → logique métier
- Validators → validation des données
- Mappers → transformation Models ↔ DTOs
- Models → entités de base de données

---

## Authentification JWT

Le token JWT contient :

- User Id
- Email
- Username

Les routes Tasks sont protégées et filtrées automatiquement selon l’utilisateur connecté.

---

## Base de données

PostgreSQL utilisé avec Entity Framework Core.

Migrations EF Core incluses dans le projet.

---

## Lancer le projet

### 1. Cloner le repository

```bash
git clone https://github.com/XaFiXxx/ToDoListDotNet.git
cd ToDoListDotNet
```

### 2. Configurer la base PostgreSQL

Modifier `appsettings.json` :

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=todo_db;Username=postgres;Password=your_password"
}
```

---

### 3. Appliquer les migrations

```bash
dotnet ef database update
```

---

### 4. Lancer le projet

```bash
dotnet run
```

---

## Swagger

Swagger est disponible automatiquement en développement :

```txt
https://localhost:xxxx/swagger
```

---

## Tests API

Les endpoints ont été testés avec :

- Swagger
- Postman

---

## Objectif du projet

Ce projet a été réalisé dans le but d’apprendre :

- ASP.NET Core
- Entity Framework Core
- Architecture backend
- Authentification JWT
- Sécurisation d’API REST
- Bonnes pratiques backend .NET

---

## Auteur

Benjamin / XaFiXxx
