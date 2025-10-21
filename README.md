# Insurance Management - .NET 8, Clean Architecture, DDD, CQRS, EF Core, RabbitMQ

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
- .NET 8 SDK

## Running PostgreSQL with docker (local)

```
Create a named volume: docker volume create pgdata
Start PostgreSQL 16: docker run -d --name postgres -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=secret -e POSTGRES_DB=mydb -p 5432:5432 -v pgdata:/var/lib/postgresql/data --restart unless-stopped postgres:16
```

## EF Core migrations (local)

Example (ProposalService):

```
cd src\ProposalService\src\Infrastructure
dotnet ef migrations add Initial --startup-project ../Web.Api
dotnet ef database update --startup-project ../Web.Api
```

(Contract Service)

```
cd src\ContractService\src\Infrastructure
dotnet ef migrations add Initial --startup-project ../Web.Api
dotnet ef database update --startup-project ../Web.Api
```
