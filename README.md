# Manifold

Manifold is a rapid application development stack built for enterprise teams that need to move fast without sacrificing type safety or developer control. One C# model drives everything downstream - database schema, TypeScript interfaces, and React Query hooks are all generated from a single command. No API versioning discussions, no syncing a separate schema file, no Postman.

The target is the gap between "enterprise tooling" (OutSystems, PowerApps - slow, locked-in, low developer trust) and "build it from scratch" (full control, but weeks of boilerplate before you ship anything). Manifold sits in the middle: real code, real stack, but the repetitive parts automated away.

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
  - [FAQ](#faq)

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

Migrations run automatically on startup. The database defaults to a local `app.db` SQLite file — this is for demo purposes only. For production use, swap in the EF Core provider for your database of choice and set `ConnectionStrings.Default` in `appsettings.json`.

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

When you add new models to the API, run the generator to produce:

- `ui/src/types/<ModelName>Type.ts` - TypeScript interface for the model (exported as `<ModelName>Type`)
- `ui/src/hooks/<ModelName>.ts` - React Query hooks for the model (reads and mutations)
- `api/Api.Web/Controllers/<ModelName>Controller.cs` - OData controller (skipped if it already exists)

```bash
cd ui
npm run gen:api
```

This is not typical for most TypeScript stacks - and it's a big deal. Frontend devs don't need to open Postman or dig through backend code to figure out the shape of the data. The types and hooks just show up, and TypeScript will tell you immediately if something is wrong. Teams can move really fast with this setup.

To add a new migration after updating your models, run these from the `ui/` directory:

```bash
npm run migrate:add -- <MigrationName>
npm run migrate:update
```

## Adding a Feature (End-to-End)

Here's the full loop for adding something new to the app:

### Backend

1. Add a model to `api/Api.Web/Models/` that implements `IAudit`
2. Add a `DbSet` for it in `AppDbContext`
3. Run a migration (`npm run migrate:add -- <Name>` from `ui/`, then `npm run migrate:update`)
4. Register the entity set in `Program.cs` with the OData model builder
5. Run `npm run gen:api` from `ui/` — generates the controller, TypeScript types, and React Query hooks

### Frontend

1. Import your hooks from `ui/src/hooks/<ModelName>.ts` and use them in a route

**Reading data:**

```ts
import { useWeatherForecast } from "@/hooks/WeatherForecast";

const { data, isLoading, error } = useWeatherForecast();

// with OData query params
const { data } = useWeatherForecast("$top=5&$orderby=Date desc");
```

**Mutating data:**

```ts
import { useCreateWeatherForecast, useUpdateWeatherForecast, useDeleteWeatherForecast } from "@/hooks/WeatherForecast";
import type { WeatherForecastType } from "@/types/WeatherForecastType";

const create = useCreateWeatherForecast();
create.mutate({ date: "2026-01-01", temperatureC: 22, summary: "Mild" });

const update = useUpdateWeatherForecast();
update.mutate({ key: 1, delta: { summary: "Hot" } });

const remove = useDeleteWeatherForecast();
remove.mutate(1);
```

The `delta` on update is typed as `Partial<T>` — TypeScript only allows fields that exist on the model, and the backend's `Delta<T>` applies only what was sent. No need to send the full object for a one-field change.

## UI Conventions

### Path Alias

`@/` maps to `ui/src/` throughout the codebase. For example:

```ts
import type { WeatherForecastType } from "@/types/WeatherForecastType";
```

### OData Response Shape

OData wraps all list responses in a consistent envelope. There's a typed helper for this:

```ts
import type { ODataResponse } from "@/types/odata";
import type { WeatherForecastType } from "@/types/WeatherForecastType";

axios.get<ODataResponse<WeatherForecastType>>("/odata/WeatherForecast")
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

Lets the frontend query exactly the data it needs - no over-fetching or under-fetching. OData also handles all standard CRUD mutations, so reads and writes stay on a consistent convention.

**Reads:**

```http
GET /odata/WeatherForecast?$top=5
GET /odata/WeatherForecast?$filter=TemperatureC gt 25
GET /odata/WeatherForecast?$orderby=Date desc
GET /odata/WeatherForecast(1)
```

**Mutations:**

```http
POST   /odata/WeatherForecast          - create, body is the full entity
PATCH  /odata/WeatherForecast(1)       - partial update, body is only changed fields
DELETE /odata/WeatherForecast(1)       - delete by id
```

`PATCH` uses ASP.NET OData's `Delta<T>` — only the fields present in the request body are applied. This means the frontend never has to fetch the full entity just to update one field.

### Custom Endpoints

Not everything fits the OData model. Sign-in, file uploads, actions that trigger side effects - these belong on regular REST endpoints alongside the OData routes.

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

- `/odata/*` - entity CRUD via OData controllers
- `/api/*` - custom operations via plain `ControllerBase` controllers

For the request/response types, put them in `api/Api.Web/Models/` and tag them with `[TsQueryGenIgnore]` (see [Type Generator](#type-generator)) so the TypeScript interface is generated but no OData hook is created for them.

### Type Generator

A custom tool (`npm run gen:api` from `ui/`) that generates one file per model from the C# models:

- `ui/src/types/<ModelName>Type.ts` - TypeScript interface (exported as `<ModelName>Type`)
- `ui/src/hooks/<ModelName>.ts` - React Query hooks with optional OData query param support

Each model gets its own file, so you can open, read, or manually edit a specific model's types or hooks without wading through a monolithic generated file.

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

#### A note on runtime validation (Zod etc.)

Manifold doesn't use Zod for generated mutations. The type safety is already guaranteed end-to-end — the TypeScript interface is generated from the same C# model that defines the database schema, `Delta<T>` enforces it on the way into the backend, and EF Core enforces it at the database level. Adding a runtime validation layer in the middle would be solving a problem that's already solved.

Zod is appropriate at real trust boundaries — user-typed form input, third-party API responses, webhook payloads. Use it there, not on internal CRUD operations.

### [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

The ORM used to interact with the database. Handles migrations, change tracking, and querying through a strongly-typed DbContext.

### [SQLite](https://www.sqlite.org/)

SQLite is used here for demo purposes only. It requires zero configuration and no running database server, which makes it ideal for getting the stack in front of people quickly.

For any real workload, replace it with whatever database fits your requirements — SQL Server, PostgreSQL, MySQL, etc. EF Core supports all of them with a provider swap and a connection string change. SQLite has meaningful behavioral differences from production-grade databases (case sensitivity, limited ALTER TABLE support, transaction behavior) that will surface if you try to carry it into production.

## FAQ

**Why are migration commands run from the `ui/` directory?**

Convenience. The alternative is a `Makefile` (not available on Windows without extra tooling) or a PowerShell script (not available on macOS/Linux without extra tooling). `package.json` scripts work everywhere Node is installed, which is already a prerequisite for running the frontend. It's a pragmatic middle ground — not a statement about where migrations conceptually belong.

---

**Why is my query only returning 100 results?**

`SetMaxTop(100)` is configured in `Program.cs`. OData requires an explicit max page size to prevent unbounded queries — without it, a client could request the entire table in one shot. 100 is the default; adjust it to fit your use case. For large datasets, use `$top` and `$skip` to paginate rather than raising the limit arbitrarily.

---

**How do I load related data (e.g., a student's courses)?**

Use OData's `$expand`. Navigation properties on your C# model are available for expansion as long as `.Expand()` is enabled (it is, in `Program.cs`):

```ts
const { data } = useStudent("$expand=Courses");
```

The expanded data comes back inline with each entity. On the TypeScript side, the generated type won't include the navigation property by default — you can add it manually to the generated interface, or add a `[TsName]`-annotated property and re-run `gen:api`.

---

**What is `Api.TypeGen`?**

It's a separate C# console project that lives at `api/Api.TypeGen/`. When you run `npm run gen:api`, it compiles and runs that project, which reflects over the models in `Api.Web/Models/`, reads the TypeGen attributes, and writes out the TypeScript files. If you need to change how types or hooks are generated — field casing, hook shape, added imports — that's where to look. It has no runtime role; it's purely a build-time tool.

---

**Why is `InsertedBy` required if there's no auth configured?**

`IAudit` is designed for apps that have an authenticated user to stamp on records. In a fully wired setup, `InsertedBy` and `UpdatedBy` get populated from the current user's identity — typically in a base controller or a `SaveChanges` override in `AppDbContext`.

Out of the box (no auth), you'll need to pass a placeholder value when creating records. The practical fix is to intercept it in `AppDbContext.SaveChanges()`:

```csharp
public override int SaveChanges()
{
    foreach (var entry in ChangeTracker.Entries<IAudit>())
    {
        if (entry.State == EntityState.Added)
        {
            entry.Entity.Inserted = DateTime.UtcNow;
            entry.Entity.InsertedBy = "system"; // replace with real user identity
        }
        if (entry.State == EntityState.Modified)
        {
            entry.Entity.Updated = DateTime.UtcNow;
            entry.Entity.UpdatedBy = "system";
        }
    }
    return base.SaveChanges();
}
```

Once auth is in place, swap `"system"` for the actual user claim.

---

**Why OData instead of GraphQL or tRPC?**

The short version: OData is like putting a SQL query in a URL. `$filter`, `$orderby`, `$top`, `$skip`, and `$select` map directly to `WHERE`, `ORDER BY`, `LIMIT`, `OFFSET`, and column selection. If you've written SQL, the mental model is already there.

A few reasons it fits this stack:

- **No schema to maintain.** GraphQL requires a schema file that has to stay in sync with the backend. OData derives its queryable surface directly from the EF model — the same model that drives the database. There's no separate layer to drift.
- **Standard protocol, not a framework.** OData is an IETF standard with first-class support in ASP.NET Core. tRPC requires TypeScript on both ends, which rules it out for a C# backend. GraphQL requires a resolver layer that adds meaningful complexity.
- **The frontend stays in control.** Filtering, sorting, pagination, and field selection are all client-driven via query params. The backend doesn't need to anticipate every combination — it just exposes the data.
- **Mutations are handled too.** OData's `Delta<T>` PATCH semantics give you type-safe partial updates without writing custom endpoints for every resource.

The tradeoff is that OData's query syntax is unfamiliar at first, and it's a worse fit for complex nested mutations or highly custom business logic — which is why the stack also supports plain REST endpoints for anything that doesn't fit the CRUD model.
