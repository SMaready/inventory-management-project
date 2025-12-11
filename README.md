# Inventory Management System

A full-stack inventory management application built for CPSC 362 (Foundations of Software Engineering). This system allows organizations to track inventory items across multiple locations, manage stock levels, and maintain comprehensive audit trails of inventory changes.

## Overview

The Inventory Management System provides a centralized platform for managing inventory items and their locations. It enables users to create, read, update, and delete inventory items; organize them by location; and view real-time inventory status through an intuitive web dashboard.

## Key Features

- **Inventory Item Management**: Create, retrieve, update, and delete inventory items with detailed metadata
- **Location Management**: Organize inventory across multiple locations
- **Stock Tracking**: Monitor on-hand quantities and inventory status
- **Real-time Dashboard**: View inventory overview and location details
- **Search & Filter**: Quickly locate items and filter by various criteria
- **Audit Trail**: Track creation and modification metadata (created by, created on)
- **Status Management**: Items can be tracked by status (e.g., active, inactive, archived)

---

## Technology Stack

### Backend
- **Language**: C# (.NET 9)
- **Framework**: ASP.NET Core
- **Database**: Entity Framework Core with in-memory database (configurable for SQLite)
- **Architecture**: Minimal APIs with feature-based organization
- **Validation**: FluentValidation
- **Documentation**: OpenAPI/Swagger

### Frontend
- **Language**: JavaScript
- **Framework**: React 19+
- **Build Tool**: Vite
- **Styling**: CSS
- **HTTP Client**: Fetch API

---

## API Endpoints

### Inventory Items

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/inventory` | Retrieve all inventory items |
| GET | `/api/inventory/{id}` | Retrieve a specific inventory item by ID |
| POST | `/api/inventory` | Create a new inventory item |
| PUT | `/api/inventory` | Update an existing inventory item |
| DELETE | `/api/inventory/{sku}` | Delete an inventory item by SKU |

### Locations

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/locations` | Retrieve all inventory locations |
| GET | `/api/locations/{id}` | Retrieve a specific location by ID |
| POST | `/api/locations` | Create a new location |
| PUT | `/api/locations` | Update an existing location |
| DELETE | `/api/locations/{id}` | Delete a location |

---

## Backend Architecture

### Design Patterns & Principles

1. **Feature-Based Organization**: Code is organized by feature (Inventory, Location) rather than technical layer, improving maintainability and scalability
2. **Command Query Separation (CQRS-lite)**: Read operations (queries) and write operations (commands) are separated into distinct endpoints and handlers
3. **Dependency Injection**: All services are registered and injected via ASP.NET Core's built-in DI container
4. **Handler Pattern**: Command and query operations are executed through dedicated handler classes
5. **Validation Pattern**: FluentValidation rules are defined separately and injected into endpoints
6. **Entity Framework Core**: Object-relational mapping for database operations with LINQ support

### Key Interfaces & Abstractions

- **ICommandHandler<TCommand, TResult>**: Generic interface for handling command operations
  - Validates incoming command
  - Performs business logic
  - Persists data to database
  - Returns result

- **AbstractValidator<T>** (FluentValidation): Base class for all validators
  - Defines validation rules for commands
  - Provides fluent API for rule definition
  - Supports custom error messages

### Project Structure

```
Backend/
├── InventoryManagement.Api/
│   ├── Program.cs                 # Application entry point & configuration
│   ├── Database/
│   │   ├── InventoryManagementDbContext.cs    # EF Core DbContext
│   │   ├── DatabaseSeeder.cs                  # Sample data initialization
│   │   └── MockDatabase.cs                    # Mock data definitions
│   ├── Features/
│   │   ├── Inventory/
│   │   │   ├── RegisterInventoryFeature.cs    # Service registration
│   │   │   ├── Commands/                      # Create/Update/Delete commands
│   │   │   ├── Handlers/                      # Command execution logic
│   │   │   ├── Endpoints/                     # HTTP route mappings
│   │   │   ├── Models/                        # Domain models
│   │   │   └── Validators/                    # FluentValidation rules
│   │   ├── Location/                          # Similar structure to Inventory
│   │   └── Shared/                            # Shared interfaces & utilities
│   └── Migrations/                            # EF Core database migrations
```

### Core Business Logic

**Inventory Item Creation Flow**:
1. Client sends POST request with `CreateInventoryItemCommand` payload
2. Endpoint handler receives command and injects validator + handler
3. Handler validates command using injected `CreateInventoryItemCommandValidator`
4. If valid, creates `InventoryItem` entity with:
   - User-provided fields: SKU, Name, Description, OnHandQuantity, Status, LocationId
   - Auto-generated fields: CreatedOn (UTC now), CreatedBy (from HttpContext.User)
5. Entity persisted via `SaveChangesAsync()`
6. Returns created item with HTTP 201 Created response

---

## Frontend Application

### Architecture Overview

The frontend is a single-page application (SPA) built with React, featuring component-based architecture with centralized state management via React hooks (useState, useEffect).

### Frontend Structure

```
inventory-ui/
├── src/
│   ├── App.jsx                 # Root component, state management, routing
│   ├── components/
│   │   ├── Dashboard.jsx       # Main dashboard overview
│   │   ├── ItemsTable.jsx      # Inventory items display & management
│   │   ├── LocationTables.jsx  # Location management interface
│   │   ├── Sidebar.jsx         # Navigation menu
│   │   └── Sidebar.css         # Sidebar styling
│   ├── App.css                 # Global styles
│   ├── index.css               # Reset & base styles
│   └── main.jsx                # React entry point
├── package.json                # Dependencies & build scripts
├── vite.config.js              # Vite build configuration
└── index.html                  # HTML template
```

### Component Responsibilities

1. **App.jsx** (Root Container)
   - Manages global state: `locations`, `items`, `selectedPage`, `searchQuery`, `status`, `err`, `collapsed`
   - Handles initial data fetching from `/api/locations` and `/api/items`
   - Routes between pages via `selectedPage` state
   - Manages sidebar collapse/expand state
   - Provides loading, error, and success status feedback
   - Passes state & handlers to child components

2. **Dashboard.jsx** (Home Page)
   - Displays inventory overview/summary
   - Shows key statistics and metrics
   - Acts as the landing page when app loads

3. **ItemsTable.jsx** (Inventory Management)
   - Displays all inventory items in a table format
   - Features:
     - Search/filter capability integrated with parent's `searchQuery` state
     - Create new item form
     - Update existing item form
     - Delete item functionality
     - Real-time data synchronization with backend
   - Handles API calls for CRUD operations
   - Updates parent state on successful changes

4. **LocationTables.jsx** (Location Management)
   - Displays all locations in a table format
   - Features:
     - Location creation
     - Location updates
     - Location deletion
     - Location listing
   - Manages location-specific data

5. **Sidebar.jsx** (Navigation)
   - Primary navigation menu
   - Page selection buttons: Dashboard, Items, Locations
   - Collapse/expand toggle
   - Reflects current page selection

### Frontend Functionality

- **Data Fetching**: Uses Fetch API to communicate with backend REST endpoints
- **State Management**: Component state for UI state (loading, errors, current page) and application data (items, locations)
- **Search**: Real-time search across inventory items via `searchQuery` state
- **CRUD Operations**:
  - **Create**: Modal/form for adding new items or locations
  - **Read**: Table display of all items and locations
  - **Update**: Inline or modal editing for existing records
  - **Delete**: Remove items or locations with confirmation
- **Error Handling**: Displays user-friendly error messages when API calls fail
- **Loading States**: Shows loading indicators during data fetches
- **Responsive UI**: Sidebar can be collapsed to maximize content space

### Data Flow

```
App.jsx (State Hub)
  ├── Sidebar.jsx (Navigation)
  ├── Dashboard.jsx (Overview)
  ├── ItemsTable.jsx (Items CRUD)
  └── LocationTables.jsx (Locations CRUD)
```

The App component is the central state holder. Child components receive state & handlers as props, call handlers for user actions, and the App component manages all API communication and state updates.

---

## Key Design Points

1. **Separation of Concerns**: Backend separates validation logic from business logic; frontend separates navigation from data management
2. **Reusability**: Validator and handler classes are registered as services and injected where needed
3. **Scalability**: Feature-based backend organization makes it easy to add new features (Reports, Analytics, etc.) without affecting existing code
4. **Maintainability**: Clear folder structure and naming conventions make code easy to understand and modify
5. **Testability**: Handlers and validators can be unit tested in isolation by mocking dependencies
6. **Audit Trail**: All items track creation metadata (who created, when), supporting compliance and troubleshooting
7. **Stateless API**: Backend API is stateless and uses minimal APIs (ASP.NET Core's lightweight endpoint approach) for better performance

---

## Getting Started

### Backend
```bash
cd Backend/InventoryManagement.Api
dotnet run
```
API will be available at `https://localhost:5001` (or as configured in launchSettings.json)

### Frontend
```bash
cd inventory-ui
npm install
npm run dev
```
Frontend will be available at `http://localhost:5173` (Vite default)

### OpenAPI Documentation
When the backend is running in development mode, visit `https://localhost:5001/openapi/v1.json` to see API documentation.

---

## Dependencies

### Backend
- ASP.NET Core 9.0
- Entity Framework Core
- FluentValidation

### Frontend
- React 19+
- Vite 7+
- ESLint (for code quality)
