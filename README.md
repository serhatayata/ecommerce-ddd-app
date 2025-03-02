## What is DDD ?
Domain-Driven Design (DDD) is a software development approach that focuses on understanding and modeling the real-world domain (business logic) of an application. It was introduced by Eric Evans in his book Domain-Driven Design: Tackling Complexity in the Heart of Software.

- ### Ubiquitous Language

  A common language used by developers, domain experts, and stakeholders to ensure clear communication and understanding.

- ### Bounded Context

  Defines clear boundaries around a particular part of the domain, ensuring that concepts have consistent meanings within those boundaries.

- ### Entities and Value Objects

  #### Entities : Objects with a distinct identity (e.g., a Customer with a unique ID).
  #### Value Objects : Objects that don’t have a unique identity (e.g., Money, Address).

- ### Aggregates

  A cluster of related domain objects that should be treated as a single unit (e.g., an Order with OrderItems).

- ### Factory

  Used to create complex domain objects while ensuring business rules are enforced.
Helps keep the domain model clean by abstracting object creation logic.

- ### Repositories

  A pattern for managing domain objects' persistence, typically abstracting database interactions.

- ### Domain Services

  Business logic that doesn’t naturally fit within an entity or value object.

- ### Application Services

  Used to coordinate tasks between domain objects and interact with infrastructure layers (e.g., APIs, databases).

- ### Event-Driven Architecture

  Using domain events to notify different parts of the system about changes (e.g., "OrderPlaced" event triggers a notification).

## What is context mapping ?

Context Mapping is a technique in Domain-Driven Design (DDD) used to define and visualize how different Bounded Contexts interact within a system. Since large systems typically consist of multiple bounded contexts (subdomains with their own models and rules), context mapping helps to understand and manage their relationships.

#### Why is Context Mapping Important?

- Clarifies boundaries between different teams and subsystems.
- Identifies dependencies and integration points.
- Helps manage communication patterns between different domains.
- Prevents misalignment between teams working on different models.

#### Common Context Mapping Patterns

- #### Partnership
  - Two teams work together closely and align their models.
  - They have shared goals and mutual success or failure.
  - Example: A Payments team and an Invoicing team working in sync.

- #### Shared Kernel
  - Two teams share a small subset of the domain model.
  - This requires strong coordination to ensure consistency.
  - Example: A Customer Management System and a Billing System sharing core customer data.

- #### Customer-Supplier
  - One context (Supplier) provides data or services to another (Customer).
  - The Supplier does not depend on the Customer.
  - Example: An Inventory Service providing stock data to an E-commerce Platform.

- #### Conformist
  - The Customer context has no control over the Supplier’s model.
  - It must adapt to the Supplier’s API without influencing its design.
  - Example: A third-party payment gateway where you must follow their API without modification.
    
- #### Anti-Corruption Layer (ACL)
  - A protective layer that translates one context’s model into another’s.
  - Prevents the pollution of one model with concepts from another.
  - Example: A legacy system integrating with a modern microservice using an ACL.

- #### Open Host Service (OHS)
  - A service exposes an open and well-defined API for multiple consumers.
  - Helps decouple systems while providing a stable contract.
  - Example: A REST API for user authentication, used by multiple applications.

- #### Published Language
  - A standardized protocol is used for communication.
  - Often seen in event-driven architectures using domain events.
  - Example: A banking system using ISO 20022 messaging for transactions.


