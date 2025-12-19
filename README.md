# rabbitmq-masstransit-dotnet
RabbitMQ + MassTransit (.NET Core) – Full-Feature

This project is an end-to-end, production-grade educational example designed to be showcased in a developer’s GitHub portfolio while demonstrating nearly all critical RabbitMQ features through real-world scenarios using MassTransit.

# Goal:
Not to say “I know RabbitMQ”, but to prove it with real architecture and code.

# Core Idea
Event-Driven Order Processing System

The project simulates a real-world architecture commonly used in e-commerce, fintech, and SaaS platforms, including:

Order creation

Payment processing

Stock reservation

Notification delivery

Failure handling & retry strategies

Long-running process orchestration using Saga State Machines

Guaranteed message delivery using the Outbox Pattern

Each step is intentionally designed to teach specific RabbitMQ and MassTransit concepts in a realistic and practical way.

# Technologies Used

.NET 8 (ASP.NET Core)

MassTransit

RabbitMQ

Docker & Docker Compose

PostgreSQL (Saga persistence)

Outbox Pattern (Transactional Outboxing) for reliable message publishing

# Architectural Highlights

Event-Driven Microservices

Publish / Subscribe & Command-based Messaging

Retry & Delayed Retry Policies

Dead Letter Queues (DLQ)

Saga State Machines for long-running workflows

Outbox Pattern to ensure atomicity between database transactions and message publishing

Correlation IDs for end-to-end traceability

Production-grade failure handling

# Why Outbox Pattern?

In distributed systems, database consistency and message delivery must be treated as a single atomic operation.

This project implements the Outbox Pattern, where:

Domain events are written to an Outbox table within the same database transaction

A background publisher reliably delivers these events to RabbitMQ

Message loss and double-publishing scenarios are eliminated

This provides effectively-once delivery guarantees, which is the practical standard in real production systems.

# What This Project Demonstrates

This repository shows that the developer:

Understands RabbitMQ beyond basic consumers

Can design fault-tolerant event-driven systems

Knows how to handle real production problems

Thinks in terms of distributed systems, not async CRUD

# Who Is This For?

Backend developers preparing for senior-level interviews

Engineers learning event-driven architecture

Teams looking for a reference implementation using RabbitMQ + MassTransit


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