# to-de-ferias-booking
Microsserviço responsável pelo gerenciamento de reservas e hóspedes.

[![CodeFactor](https://www.codefactor.io/repository/github/wesleycosta/to-de-ferias-booking/badge)](https://www.codefactor.io/repository/github/wesleycosta/to-de-ferias-booking)
[![Build Status](https://wlcosta.visualstudio.com/ToDeFeriasBooking/_apis/build/status/to-de-ferias-booking-ci?branchName=main)](https://wlcosta.visualstudio.com/ToDeFeriasBooking/_build/latest?definitionId=7&branchName=main)

## Arquitetura
Arquitetura construida com preocupações de separação de responsabilidades, seguindo as boas práticas de SOLID e Clean Code.

- Hexagonal Architecture
- Domain Driven Design
- Domain Events
- Domain Notification
- Domain Validations
- CQRS
- Retry Pattern
- Unit of Work
- Repository

<p align="center">
  <img src="./docs/architecture-diagram.png" />
</p>

- **SPA**: Front-end em Angular
- **API Gateway**: API gateway com Ocelot
- **Booking**: Microsserviço responsável pelo gerenciamento de reservas e hóspedes
- **Governance**: Microsserviço responsável pelo gerenciamento de quartos e interdições
- **Notifications**: Microsserviço responsável pelo envio de notificações aos hóspedes

## Componentes
- AutoFixture
- AutoMapper
- ELK
- EntityFramework
- FluentValidation
- MediatR
- Moq
- NetDevPack.Brasil
- Serilog
- Swagger
- XUnit

## Diagrama de classes
<p align="center">
  <img src="./docs/class-diagram.png" />
</p>
