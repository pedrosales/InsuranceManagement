# Insurance Management - .NET 8, Clean Architecture, DDD, CQRS, EF Core, RabbitMQ

This repository provides a skeleton for managing insurance proposals and contracting, using:

- .NET 8
- Clean Architecture + DDD + CQRS
- EF Core with PostgreSql
- Messaging with RabbitMQ (MassTransit)
- Two microservices: ProposalService and ContractService
- Shared event contracts
- Dockerfiles and docker-compose

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
