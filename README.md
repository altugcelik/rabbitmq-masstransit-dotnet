# rabbitmq-masstransit-dotnet
RabbitMQ + MassTransit (.NET Core) – Full-Feature


#  Event‑Driven Microservices with RabbitMQ & MassTransit (.NET 8)

This repository demonstrates a production‑grade event‑driven architecture
using RabbitMQ and MassTransit.

## Features
- Retry & Delayed Retry
- Dead Letter Queues
- Saga State Machine
- Correlation & Idempotency
- Dockerized infrastructure

> If you want to *understand* RabbitMQ instead of just using it — this repo is for you.

## Architecture Overview

API Gateway publishes commands to RabbitMQ.

Each microservice:
- Consumes messages
- Executes business logic
- Writes domain events to the Outbox

A background worker safely publishes events to RabbitMQ.

Saga orchestrates the entire order lifecycle.