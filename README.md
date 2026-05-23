# 📦 Smart Logistics System

A modern, enterprise-grade logistics and supply chain management backend built with **.NET 10**, adhering strictly to the principles of **Clean Architecture**, **Domain-Driven Design (DDD)**, and **CQRS**.

---

## 🚀 Key Architectural Pillars

*   **Clean Architecture:** Clear separation of concerns ensuring the core business logic (Domain) remains independent of frameworks, databases, and UI implementations.
*   **Domain-Driven Design (DDD):** Rich domain models with encapsulation (`private setters`), bounded contexts, Aggregate Roots, and Immutable Value Objects.
*   **CQRS Pattern:** Complete segregation of read and write operations powered by **MediatR**, leading to optimized performance and highly maintainable use cases.
*   **Robust Data Integrity:** Automatic input validation via a custom **FluentValidation Pipeline Behavior** acting before the persistence layer.

---

## 🛠️ Tech Stack & Ecosystem

*   **Framework:** .NET 8 Web API
*   **Database & ORM:** SQL Server & Entity Framework Core (EF Core)
*   **Patterns:** CQRS, Mediator, Unit of Work, Generic & Custom Repositories
*   **Libraries:** FluentValidation, MediatR
*   **IDE & OS:** JetBrains Rider on macOS

---

## 📂 Project Architecture Layout

The solution is divided into 4 main layers following the standard Clean Architecture diagram:

```text
├── 1. Domain         # Core Entities, Enums, Value Objects & Domain Exceptions (Zero Dependencies)
├── 2. Application    # CQRS Features (Commands/Queries), Handlers, Validators & Interfaces
├── 3. Infrastructure # DbContext, Fluent API Configurations, Migrations & Repository Implementations
└── 4. WebApi         # REST Controllers, API Routing, Middleware & Auth Schemas
```

---

## 🎯 Core Features Implemented

> 🏛️ **1. Responsible Management System (Stakeholders)**
> *   **Dynamic Role-Based Entity:** A single, flexible core module capable of managing multiple stakeholder types (**Drivers**, **Shipping Companies**, **Warehouses**, and **Customs Agents**) without redundant tables.
> *   **Shipping Integration (Value Object):** An immutable Domain Value Object that handles dynamic external API credentials, Webhook URLs, and API Base URLs for third-party shipping giants (e.g., *Aramex, DHL, FedEx*).
> *   **Administrative Control:** Strict endpoint security ensuring that sensitive integration configurations are accessible solely by authorized `Admin` roles.

---

> 👥 **2. Customer & Balance Management**
> *   **Commercial Profiles:** Manages distinct customer accounts while tracking critical business metrics like `CreditLimit` and `CurrentBalance`.
> *   **Organic Scaling:** Financial balances and limits are designed to dynamically scale and recalculate in sync with live shipments and transactional logs.

---

> 💬 **3. Integrated Support Chat Engine (DDD Compliant)**
> *   **Order-Specific Rooms:** Fully encapsulated `ChatRoom` Aggregate Root that links and tracks support communications per individual order and client context.
> *   **Strict Encapsulation:** Messages are modified exclusively through domain boundaries (`AddMessage()`). Relational data is persisted safely into the database using **EF Core Backing Fields (`_messages`)** to strictly protect collection encapsulation.
> *   **Automated System Logs:** Native capability to inject automated system status updates directly into the stream (e.g., *`"SYSTEM: This conversation has been closed."`*).

---

## 🔒 Fluent API & Database Modeling

We enforce high performance, data density optimization, and relational safety directly via EF Core Fluent API configurations:

*   **Precise Datatypes:** Standardized `decimal(18,2)` fields across all financial balances, limits, and credits to eliminate rounding discrepancies.
*   **Memory Footprint Optimization:** Strict `HasMaxLength()` constraints applied to all strings to prevent memory waste in SQL Server.
*   **Data Loss Prevention:** Enforced **`DeleteBehavior.Restrict`** rules across all core operational tables to prevent dangerous accidental cascading deletes on live production data.

---

## 🚀 Getting Started

### Prerequisites
*   .NET 8 SDK
*   SQL Server (LocalDB or a Docker Container)

### Setup & Installation

1. **Clone the repository:**
```bash
   git clone [https://github.com/your-username/smart-logistics.git](https://github.com/your-username/smart-logistics.git)
   cd smart-logistics





