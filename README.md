# RetailFlow

Plateforme de gestion retail multi-tenant (produits, stock, ventes, marge) — projet de démonstration technique
couvrant une stack backend .NET en architecture en couches et un frontend React/TypeScript.

## Stack

- **Backend** : C# / ASP.NET Core / EF Core / PostgreSQL
- **Frontend** : React / TypeScript / Vite
- **Architecture** : Clean Architecture (Domain / Application / Infrastructure / Api)
- **Cible** : SaaS multi-tenant

## Architecture

```
React (Vite/TS)
      │  HTTP/REST
      ▼
RetailFlow.Api            → Controllers, configuration, middleware, CORS
      │
      ▼
RetailFlow.Application     → Services métier, DTOs, règles (PUMP, marge, stock)
      │
      ▼
RetailFlow.Domain          → Entités (Tenant, Store, Product, Inventory, StockMovement, Sale, SaleLine)
      │
      ▼
RetailFlow.Infrastructure  → EF Core, DbContext, migrations, accès PostgreSQL
```

Chaque couche ne dépend que des couches internes (Domain ne dépend de rien, Infrastructure implémente
les interfaces définies dans Application/Domain).

## Structure du repo

```
RetailFlow/
├── docs/                          # Diagrammes, notes d'architecture
├── src/
│   ├── api/
│   │   ├── RetailFlow.Api             # Point d'entrée, controllers, Program.cs
│   │   ├── RetailFlow.Application     # Services métier, DTOs
│   │   ├── RetailFlow.Domain          # Entités, règles métier pures
│   │   └── RetailFlow.Infrastructure  # EF Core, DbContext, migrations
│   └── frontend/
│       └── RetailFlow.Web             # Application React/TypeScript
└── tests/                         # Tests unitaires et d'intégration
```

## Lancement local

### Prérequis

- .NET SDK 10
- Node.js 20+
- PostgreSQL (local ou conteneur)
- Git

### Backend

```bash
cd src/api/RetailFlow.Api
dotnet restore
dotnet ef database update   # applique les migrations
dotnet run
```

L'API démarre sur `http://localhost:5000` (Swagger disponible sur `/swagger`).

### Frontend

```bash
cd src/frontend/RetailFlow.Web
npm install
cp .env.local.example .env.local   # renseigner VITE_API_URL si besoin
npm run dev
```

Le frontend démarre sur `http://localhost:5173`.

### Vérification rapide

```bash
curl http://localhost:5000/api/health
```

## Statut du projet

- [x] Bootstrap backend (API + Application + Domain + Infrastructure)
- [x] Bootstrap frontend React/TypeScript
- [x] Connexion API ↔ Frontend validée (`/api/health`)
- [ ] Modélisation du domaine (Product, Tenant, Store, Inventory, StockMovement, Sale)
- [ ] EF Core + migrations PostgreSQL
- [ ] CRUD produits/stock/ventes
- [ ] Logique métier PUMP/marge
- [ ] Multi-tenancy
- [ ] Tests
- [ ] CI/CD + déploiement Azure

## Licence

Projet de démonstration technique, usage personnel.
