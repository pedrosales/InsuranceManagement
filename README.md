# Insurance Management - .NET 8, Clean Architecture, DDD, CQRS, EF Core, RabbitMQ

This repository provides a skeleton for managing insurance proposals and contracting, using:

- .NET 8
- Clean Architecture + DDD + CQRS
- EF Core with SQL Server
- Messaging with RabbitMQ (MassTransit)
- Two microservices: ProposalService and ContractService
- Shared event contracts
- Dockerfiles and docker-compose

Major changes:

- Removed Unit of Work and all repository classes/interfaces.
- CQRS handlers access EF Core DbContext directly (DbContext.Set<TEntity>).
- API wires ProposalDbContext/ContractDbContext and exposes DbContext to handlers.
- Tests use EF Core InMemory where needed.

## Structure

```
.
├─ docker-compose.yml
├─ src
│  ├─ Shared
│  │  └─ Contracts
│  │     ├─ Contracts.csproj
│  │     └─ Events
│  │        ├─ ContractCreated.cs
│  │        ├─ ProposalCreated.cs
│  │        └─ ProposalStatusChanged.cs
│  ├─ ProposalService
│  │  ├─ ProposalService.Domain
│  │  │  ├─ ProposalService.Domain.csproj
│  │  │  └─ Entities
│  │  │     ├─ Proposal.cs
│  │  │     └─ ProposalStatus.cs
│  │  ├─ ProposalService.Application
│  │  │  ├─ ProposalService.Application.csproj
│  │  │  ├─ DTOs
│  │  │  │  └─ ProposalDto.cs
│  │  │  ├─ Commands
│  │  │  │  ├─ ChangeProposalStatusCommand.cs
│  │  │  │  └─ CreateProposalCommand.cs
│  │  │  ├─ Queries
│  │  │  │  ├─ GetProposalByIdQuery.cs
│  │  │  │  └─ ListProposalsQuery.cs
│  │  │  ├─ Handlers
│  │  │  │  ├─ ChangeProposalStatusHandler.cs
│  │  │  │  ├─ CreateProposalHandler.cs
│  │  │  │  ├─ GetProposalByIdHandler.cs
│  │  │  │  └─ ListProposalsHandler.cs
│  │  │  └─ Mappings
│  │  │     └─ ProposalProfile.cs
│  │  ├─ ProposalService.Infrastructure
│  │  │  ├─ ProposalService.Infrastructure.csproj
│  │  │  └─ Persistence
│  │  │     ├─ ProposalDbContext.cs
│  │  │     └─ Configurations
│  │  │        └─ ProposalEntityTypeConfiguration.cs
│  │  └─ ProposalService.Api
│  │     ├─ ProposalService.Api.csproj
│  │     ├─ Program.cs
│  │     ├─ appsettings.json
│  │     ├─ Controllers
│  │     │  └─ ProposalsController.cs
│  │     └─ Dockerfile
│  └─ ContractService
│     ├─ ContractService.Domain
│     │  ├─ ContractService.Domain.csproj
│     │  └─ Entities
│     │     └─ Contract.cs
│     ├─ ContractService.Application
│     │  ├─ ContractService.Application.csproj
│     │  ├─ DTOs
│     │  │  └─ ContractDto.cs
│     │  ├─ Commands
│     │  │  └─ ContractProposalCommand.cs
│     │  ├─ Queries
│     │  │  └─ ListContractsQuery.cs
│     │  └─ Handlers
│     │     ├─ ContractProposalHandler.cs
│     │     └─ ListContractsHandler.cs
│     ├─ ContractService.Infrastructure
│     │  ├─ ContractService.Infrastructure.csproj
│     │  ├─ Persistence
│     │  │  ├─ ContractDbContext.cs
│     │  │  └─ Configurations
│     │  │     └─ ContractEntityTypeConfiguration.cs
│     │  └─ Integrations
│     │     └─ ProposalStatusClient.cs
│     └─ ContractService.Api
│        ├─ ContractService.Api.csproj
│        ├─ Program.cs
│        ├─ appsettings.json
│        ├─ Controllers
│        │  └─ ContractsController.cs
│        └─ Dockerfile
└─ tests
   ├─ ProposalService.UnitTests
   │  ├─ ProposalService.UnitTests.csproj
   │  └─ ProposalDomainTests.cs
   └─ ContractService.UnitTests
      ├─ ContractService.UnitTests.csproj
      └─ ContractDomainTests.cs
```

## Running with Docker

Prerequisites:

- Docker and Docker Compose
- .NET 8 SDK (optional for dev)

1. Start infra (SQL Server and RabbitMQ):

```
docker compose up -d sqlserver rabbitmq
```

2. Build and run services:

```
docker compose up -d proposal.api contract.api
```

- RabbitMQ Management UI: http://localhost:15672 (guest/guest)
- ProposalService Swagger: http://localhost:5001/swagger
- ContractService Swagger: http://localhost:5002/swagger

## EF Core migrations (local)

Example (ProposalService):

```
cd src/ProposalService/ProposalService.Infrastructure
dotnet ef migrations add Initial --startup-project ../ProposalService.Api
dotnet ef database update --startup-project ../ProposalService.Api
```

Repeat for ContractService.

Notes

- JSON enum serialization is configured as strings in both APIs.
- For production, use env vars/KeyVault for secrets.
- Consider adding health checks, OpenTelemetry, retries (Polly), and idempotency.
