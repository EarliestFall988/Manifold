# React-ASP Framework

I know ASP.NET has a template for React - this is not that. I built this framework from scratch, so it's clean and simple.

## Contents

- [Getting Started](#getting-started)
- [Migrations](#migrations)
- [Adding a Feature](#adding-a-feature-end-to-end)
- [UI Conventions](#ui-conventions)
- [UI Framework](#ui-framework-react)
  - [Vite](#vite)
  - [Tailwind CSS](#tailwind-css)
  - [Tanstack Router](#tanstack-router)
  - [Tanstack Query](#tanstack-query)
  - [Axios](#axios)
  - [Shadcn UI](#shadcn-ui)
  - [Phosphor Icons](#phosphor-icons)
  - [Auto-animate](#auto-animate)
- [API Framework](#api-framework-aspnet-core)
  - [OData](#odata)
  - [Type Generator](#type-generator)
  - [Entity Framework Core](#entity-framework-core)
  - [SQLite](#sqlite)

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)

### Running the app

```bash
git clone https://github.com/your-username/react-asp-framework.git
cd react-asp-framework
```

Start the API:

```bash
cd api/Api.Web
dotnet run
```

Migrations run automatically on startup. The database defaults to a local `app.db` SQLite file. To use a different database, set a `ConnectionStrings.Default` value in `appsettings.json`.

CORS is already configured for the default Vite dev ports (`localhost:5173` and `localhost:5174`) in `appsettings.Development.json`.

Start the UI (in a separate terminal):

```bash
cd ui
npm install
```

Create a `.env` file in the `ui/` directory — this is gitignored and won't exist after a fresh clone:

```bash
VITE_API_URL=http://localhost:5289
```

Then run:

```bash
npm run dev
```

> **Note:** The API adds a 500ms artificial delay in development to make loading states easier to catch. It's removed in production.

## Migrations

When you add new models to the API, TypeScript types are automatically generated on the frontend whenever the API project builds. You'll find them in `ui/src/types/api.generated.ts`.

This is not typical for most TypeScript stacks - and it's a big deal. Frontend devs don't need to open Postman or dig through backend code to figure out the shape of the data. The types just show up, and TypeScript will tell you immediately if something is wrong. Teams can move really fast with this setup.

To add a new migration after updating your models:

```bash
cd api/Api.Web
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Adding a Feature (End-to-End)

Here's the full loop for adding something new to the app:

### Backend

1. Add a model to `api/Api.Web/Models/`
2. Add a `DbSet` for it in `AppDbContext`
3. Run a migration (`dotnet ef migrations add <Name>`)
4. Register the entity set in `Program.cs` with the OData model builder
5. Create a controller in `api/Api.Web/Controllers/` that extends `ODataController`

### Frontend

1. Build and run the API — TypeScript types for your new model will be generated automatically in `ui/src/types/api.generated.ts`
2. Add an API function in `ui/src/api/` using Axios and the generated types
3. Wrap it in a Tanstack Query hook in `ui/src/hooks/`
4. Create a route file in `ui/src/routes/` and use the hook

## UI Conventions

### Path Alias

`@/` maps to `ui/src/` throughout the codebase. For example:

```ts
import { type WeatherForecast } from "@/types/api.generated";
```

### OData Response Shape

OData wraps all list responses in a consistent envelope. There's a typed helper for this:

```ts
import type { ODataResponse } from "@/types/odata";

axios.get<ODataResponse<WeatherForecast>>("/odata/WeatherForecast")
```

## UI Framework (React)

These are the tools I personally consider essential for building world-class React applications. I've used all of them in production and they work great together.

### [Vite](https://vitejs.dev/)

Handles building, bundling, and hot module replacement. Fast dev experience, optimized production builds, and a great plugin system. Check out `vite.config.ts` to see how it's configured.

### [Tailwind CSS](https://tailwindcss.com/)

Handles all the styling. The best way to describe Tailwind is zen mode CSS — I will fight to put this on any new project.

### Tanstack Libraries

#### [Tanstack Router](https://tanstack.com/router/latest)

File-based routing that maps the file structure to the URL structure. Easy to navigate, even when you're new to the codebase.

#### [Tanstack Query](https://tanstack.com/query/latest)

Handles data fetching and caching - the most frustrating part of any frontend app. With this you don't need Redux, Zustand, or any other state management library.

### [Axios](https://axios-http.com/)

Makes API calls to the backend. Great for handling authentication tokens and other request complexities.

### [Shadcn UI](https://ui.shadcn.com/)

A component library built on Radix UI and Tailwind. Pre-built components that are easy to customize and extend.

### [Phosphor Icons](https://phosphoricons.com/)

My go-to icon library. Over 1,400 icons, consistent design language, multiple weights, and a great React package.

### [Auto-animate](https://auto-animate.formkit.com/)

Adds smooth animations to lists and DOM changes with a single line of code. Drop it on a parent element and everything inside animates automatically.


## API Framework (ASP.NET Core)

### [OData](https://learn.microsoft.com/en-us/odata/webapi-8/overview)

Lets the frontend query exactly the data it needs — no over-fetching or under-fetching.

```http
/odata/WeatherForecast?$top=5
/odata/WeatherForecast?$filter=TemperatureC gt 25
/odata/WeatherForecast?$orderby=Date desc
```

### Type Generator

A custom tool that generates TypeScript interfaces directly from the C# models on every build. Keeps the frontend and backend in sync automatically.

Two attributes are available to control generation on a per-property basis:

- `[TsIgnore]` — excludes a property from the generated interface
- `[TsName("newName")]` — overrides the property name in the generated interface

### [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

The ORM used to interact with the database. Handles migrations, change tracking, and querying through a strongly-typed DbContext.

### [SQLite](https://www.sqlite.org/)

A lightweight, file-based database that's easy to get running locally. For production you'll swap this out for SQL Server, but SQLite keeps the setup simple during development.
