# Photon

**Photon** is a lightweight in-memory relational database engine built from the ground up in C# for .NET.

The goal of Photon is to provide a simple, strongly typed database experience using standard C# POCO models while exploring the internal concepts behind relational database systems, including table management, metadata, querying, indexing, relationships, and persistence.

> Photon is currently under active development and is not intended for production use.

---

## Overview

Photon automatically discovers model types and creates in-memory tables for them at startup.

Models are standard C# classes, with attributes used to describe database-specific metadata such as primary keys.

```csharp
public class Customer
{
    [PhotonPrimaryKey]
    public int CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime CreatedDate { get; set; }
}
```

Photon uses the model type itself to identify the associated table and stores strongly typed models within the database.

---

## Current Features

Photon currently provides the foundations of an in-memory relational database engine, including:

- Automatic POCO model discovery
- Automatic in-memory table generation
- Generic strongly typed tables
- Attribute-based primary key configuration
- Metadata generation through reflection
- Cached table and column metadata
- Primary key validation
- Strongly typed CRUD operations
- Duplicate primary key detection
- Missing primary key detection
- Custom Photon exceptions
- Centralized data validation
- Initial expression-tree query parsing architecture
- Logical and comparison query operator representation
- Separation between storage, metadata, validation, and query components

---

## Basic Usage

Create a Photon database instance:

```csharp
PhotonCore photon = new PhotonCore();
```

### Insert

Photon retrieves the configured primary key directly from the model.

```csharp
Customer customer = new Customer
{
    CustomerId = 1,
    FirstName = "Eric",
    LastName = "Reyneke",
    Email = "eric@example.com",
    CreatedDate = DateTime.Now
};

photon.Add(customer);
```

### Retrieve

```csharp
Customer customer =
    photon.Retrieve<Customer>(1);
```

### Try Retrieve

```csharp
bool customerFound =
    photon.TryRetrieve<Customer>(
        1,
        out Customer customer);

if (customerFound)
    Console.WriteLine(customer.FirstName);
```

### Check for a Primary Key

```csharp
bool exists =
    photon.ContainsKey<Customer>(1);
```

### Retrieve All

```csharp
IReadOnlyCollection<Customer> customers =
    photon.RetrieveAll<Customer>();
```

### Update

```csharp
Customer customer =
    photon.Retrieve<Customer>(1);

customer.Email = "newemail@example.com";

photon.Update(customer);
```

### Remove

```csharp
photon.Remove<Customer>(1);
```

---

## Querying

Photon is being designed around a deferred, SQL-inspired query API.

The intended API will allow queries such as:

```csharp
List<Product> products = photon
    .From<Product>()
    .Where(product =>
        product.Price >= 2000M &&
        product.IsActive == true)
    .OrderBy(product => product.Price)
    .ToList();
```

Query construction will be deferred until a terminal operation such as `ToList()` is called.

Photon's query API is also intended to use strongly typed query stages so that invalid query ordering can be restricted at compile time.

Conceptually:

```text
From<TModel>()
      |
      v
Query Start
      |
    Where
      |
      v
Where Query
      |
   OrderBy
      |
      v
Ordered Query
      |
   ToList()
      |
      v
Execution
```

---

## Expression Trees

Photon does not intend to treat query predicates purely as compiled delegates.

Instead, query expressions such as:

```csharp
product =>
    product.Price > 1000M &&
    product.IsActive == true
```

can be inspected and translated into an internal Photon representation.

Conceptually:

```text
C# Expression
      |
      v
PhotonExpressionVisitor
      |
      v
Query Condition
      |
      +-- Price > 1000
      |
      +-- AND
      |
      +-- IsActive == true
      |
      v
Query Execution
```

This architecture provides a foundation for future query planning and index-aware execution.

---

## Architecture

Photon is divided into focused internal components rather than placing database responsibilities directly inside the public API.

```text
PhotonCore
    |
    +-- Tables
    |     |
    |     +-- Table Generation
    |     +-- PhotonTable<TModel>
    |
    +-- Metadata
    |     |
    |     +-- Metadata Generation
    |     +-- Metadata Cache
    |     +-- Table Metadata
    |     +-- Column Metadata
    |
    +-- Data
    |     |
    |     +-- CRUD Operations
    |
    +-- Validation
    |     |
    |     +-- Data Validation
    |     +-- Primary Key Validation
    |
    +-- Querying
          |
          +-- Query Expressions
          +-- Query Conditions
          +-- Query Operators
          +-- Query Execution
```

`PhotonCore` acts as the primary public-facing database interface while the underlying database functionality is delegated to specialized internal components.

---

## Table Storage

Each discovered model receives its own strongly typed Photon table.

Conceptually:

```csharp
PhotonTable<Customer>
PhotonTable<Product>
PhotonTable<Order>
```

Rows are currently stored using an integer primary key:

```csharp
Dictionary<int, TModel>
```

The database itself maintains the generated tables using their model types:

```csharp
Dictionary<Type, IPhotonTable>
```

This allows Photon to locate tables without requiring manually assigned table identifiers.

---

## Metadata

Photon generates metadata once during database initialization and caches the result.

For example, a model such as:

```csharp
public class Product
{
    [PhotonPrimaryKey]
    public int ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}
```

produces metadata describing:

```text
Table: Product

Primary Key:
    ProductId : Int32

Columns:
    ProductId : Int32
    Name      : String
    Price     : Decimal
    IsActive  : Boolean
```

Caching this information prevents Photon from repeatedly performing reflection during normal database operations.

---

## Project Goals

Photon is being developed as both a functional .NET library and an exploration of how relational database engines work internally.

Planned areas of development include:

- Deferred query execution
- SQL-inspired strongly typed query chaining
- Expression-tree parsing
- Query execution
- Ordering
- Filtering
- Result limiting
- Grouping
- Aggregate functions
- Foreign key metadata
- Relationship validation
- Joins
- Indexes
- Index-aware query execution
- Query planning
- Persistent storage
- Database snapshots
- Write-ahead logging
- Transaction support
- Concurrency management

The intention is to introduce these features progressively while keeping individual components focused and maintainable.

---

## Example Data Model

Photon is currently being developed and tested using a small relational commerce model:

```text
Customer
    |
    +---- Address
    |
    +---- Order
             |
             +---- OrderItem
                       |
                       +---- Product
```

This model provides a testing foundation for future relationship, foreign key, join, indexing, grouping, and query-planning functionality.

---

## Project Status

Photon is currently in the **early development / experimental stage**.

The core table, metadata, validation, and CRUD architecture is implemented.

The query engine is currently under development, with expression-tree parsing forming the foundation for Photon-native query execution.

APIs and internal architecture may change significantly as development continues.

---

## Technology

Photon is written in **C#** and targets **.NET**.

The project intentionally builds many database concepts directly rather than wrapping an existing database engine. This allows the project to explore concepts such as:

- Table storage
- Metadata systems
- Query parsing
- Query execution
- Indexing
- Relationships
- Persistence
- Transaction processing

---

## Contributing

Photon is currently a personal experimental project and is under active development.

Issues, ideas, and discussions are welcome as the project evolves.
