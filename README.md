# rabbitmq-masstransit-dotnet
RabbitMQ + MassTransit (.NET Core) – Full-Feature


#  Event‑Driven Microservices with RabbitMQ & MassTransit (.NET 8)

This repository is **not** a hello‑world demo.

It is a **production‑grade reference architecture** that demonstrates how to:

- Build event‑driven microservices
- Guarantee message delivery (Outbox Pattern)
- Handle failures with Retry & Dead Letter Queues
- Orchestrate long‑running processes with Saga State Machines


> If you want to *understand* RabbitMQ instead of just using it — this repo is for you.

## Architecture Overview

API Gateway publishes commands to RabbitMQ.

Each microservice:
- Consumes messages
- Executes business logic
- Writes domain events to the Outbox

A background worker safely publishes events to RabbitMQ.

Saga orchestrates the entire order lifecycle.