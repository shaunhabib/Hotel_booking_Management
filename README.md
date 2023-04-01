# Hotel Booking Web Api

## Features of this project
- Clean Architecture
- CQRS Mediator Design pattern
- SOLID principles
- Serilog 
- EntityFrameWork Core
- Repository pattern
- Unit of work
- Api version
- SQL Server/ MYSQL


## Project overview
#### Core.Application.Contracts
- All the commands and queries

#### Core.Application
- All the handlers and business logic

#### Core.Domain.Persistence
- All the entities/tables and repositories

#### Infrastructure.Persistence
- Context file, migrations and repository implementations

#### Web.Framework
- Service dependency injections

#### Web.Api
- UI presentation
