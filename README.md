# Manifold

Manifold is a rapid application development stack built for enterprise teams that need to move fast without sacrificing type safety or developer control. One C# model drives everything downstream — database schema, TypeScript interfaces, and React Query hooks are all generated automatically on every build. No API versioning discussions, no syncing a separate schema file, no Postman.

The target is the gap between "enterprise tooling" (OutSystems, PowerApps — slow, locked-in, low developer trust) and "build it from scratch" (full control, but weeks of boilerplate before you ship anything). Manifold sits in the middle: real code, real stack, but the repetitive parts automated away.

## Axioms

This stack is opinionated by design. The opinions are:

- **Code first** - the C# model is the source of truth. Everything downstream (database schema, TypeScript types, React hooks) flows from it automatically.
- **Migrations as the contract** - when you change a model and run a migration, the frontend picks up the new types on the next build. No API versioning discussions, no syncing a separate schema file, no Postman.
- **A query layer, not a REST layer** - OData means the backend exposes data, not opinions about how the frontend should consume it. Filtering, sorting, and pagination are the frontend's problem.
- **A thin wrapper, not a framework** - Axios + React Query aren't hidden behind abstractions. You're writing the same patterns the docs show, just with the boilerplate generated for you.
- **No manual loading and error states** - React Query handles all of it. `isLoading`, `error`, and `data` are just there.
- **No global state management for server data** - React Query's cache replaces Redux/Zustand for anything that comes from an API. You don't need a store for data that already lives on the server.

The full loop looks like this: add a C# model → run a migration → build the API → import the generated hook → done. No context switching, no duplicate effort, no runtime surprises from mismatched types. For a small team moving fast, that's a significant advantage.

## Contents

- [Manifold](#manifold)
  - [Axioms](#axioms)
  - [Contents](#contents)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Running the app](#running-the-app)
  - [Migrations](#migrations)
  - [Adding a Feature (End-to-End)](#adding-a-feature-end-to-end)
    - [Backend](#backend)
    - [Frontend](#frontend)
  - [UI Conventions](#ui-conventions)
    - [Path Alias](#path-alias)
    - [OData Response Shape](#odata-response-shape)
  - [UI Framework (React)](#ui-framework-react)
    - [Vite](#vite)
    - [Tailwind CSS](#tailwind-css)
    - [Tanstack Libraries](#tanstack-libraries)
      - [Tanstack Router](#tanstack-router)
      - [Tanstack Query](#tanstack-query)
    - [Axios](#axios)
    - [Shadcn UI](#shadcn-ui)
    - [Phosphor Icons](#phosphor-icons)
    - [Auto-animate](#auto-animate)
  - [API Framework (ASP.NET Core)](#api-framework-aspnet-core)
    - [OData](#odata)
    - [Custom Endpoints](#custom-endpoints)
    - [Type Generator](#type-generator)
    - [Entity Framework Core](#entity-framework-core)
    - [SQLite](#sqlite)

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)

### Running the app

```bash
git clone https://github.com/your-username/manifold.git
cd manifold
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

Create a `.env` file in the `ui/` directory - this is gitignored and won't exist after a fresh clone:

```bash
VITE_API_URL=http://localhost:5289
```

Then run:

```bash
npm run dev
```

> **Note:** The API adds a 500ms artificial delay in development to make loading states easier to catch. It's removed in production.

## Migrations

When you add new models to the API, two files are automatically generated on the frontend whenever the API project builds:

- `ui/src/types/api.generated.ts` - TypeScript interfaces for every model
- `ui/src/hooks/api.generated.ts` - a ready-to-use React Query hook for every model

This is not typical for most TypeScript stacks - and it's a big deal. Frontend devs don't need to open Postman or dig through backend code to figure out the shape of the data. The types and hooks just show up, and TypeScript will tell you immediately if something is wrong. Teams can move really fast with this setup.

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

1. Build and run the API - types and hooks are generated automatically
2. Import your hook from `ui/src/hooks/api.generated.ts` and use it in a route

```ts
import { useWeatherForecast } from "@/hooks/api.generated";

const { data, isLoading, error } = useWeatherForecast();

// or with OData query params
const { data } = useWeatherForecast("$top=5&$orderby=Date desc");
```

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

Handles all the styling. The best way to describe Tailwind is zen mode CSS - I will fight to put this on any new project.

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

Lets the frontend query exactly the data it needs - no over-fetching or under-fetching.

```http
/odata/WeatherForecast?$top=5
/odata/WeatherForecast?$filter=TemperatureC gt 25
/odata/WeatherForecast?$orderby=Date desc
```

### Custom Endpoints

Not everything fits the OData model. Sign-in, file uploads, actions that trigger side effects — these belong on regular REST endpoints alongside the OData routes.

Since `AddControllers()` and `MapControllers()` are already configured, you just add a controller that extends `ControllerBase` instead of `ODataController`:

```csharp
// Controllers/AuthController.cs
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("signin")]
    public IActionResult SignIn([FromBody] SignInRequest request)
    {
        // ...
        return Ok(new SignInResponse(token));
    }
}
```

The convention is:

- `/odata/*` — entity CRUD via OData controllers
- `/api/*` — custom operations via plain `ControllerBase` controllers

For the request/response types, put them in `api/Api.Web/Models/` and tag them with `[TsQueryGenIgnore]` (see [Type Generator](#type-generator)) so the TypeScript interface is generated but no OData hook is created for them.

### Type Generator

A custom tool that runs on every build and generates two files from the C# models:

- `ui/src/types/api.generated.ts` - TypeScript interfaces
- `ui/src/hooks/api.generated.ts` - React Query hooks with optional OData query param support

The generator handles both `class` and `record` declarations. Several attributes are available to control generation behavior:

**Property-level:**

- `[TsIgnore]` - excludes a property from the generated interface
- `[TsName("newName")]` - overrides the property name in the generated interface

**Class/record-level:**

- `[TsQueryGenIgnore]` - generates the TypeScript interface but skips OData hook generation. Use this on request/response types for custom endpoints that aren't part of the OData entity model.

```csharp
[TsQueryGenIgnore]
public record SignInRequest(string Email, string Password);

[TsQueryGenIgnore]
public record SignInResponse(string Token);
```

### [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

The ORM used to interact with the database. Handles migrations, change tracking, and querying through a strongly-typed DbContext.

### [SQLite](https://www.sqlite.org/)

A lightweight, file-based database that's easy to get running locally. For production you'll swap this out for SQL Server, but SQLite keeps the setup simple during development.
