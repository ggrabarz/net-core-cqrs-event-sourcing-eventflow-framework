# Net Core CQRS & Event Sourcing in EventFlow framework

## CQRS

CQRS separates reads and writes into different models, using __commands__ to update data, and __queries__ to read data.

## Event Sourcing

Ensures that all changes to application state are stored as a sequence of events

## This repository

This repository implement CQRS with separate read and write models and uses event sourcing using [EventFlow framework](https://github.com/eventflow/EventFlow)

## Getting started

1. Download repository 
2. Download .Net Core SDK (in the moment of writing 3.0.100) and docker (linux containers)
3. Open in VS 2019 or newer & start docker
4. Start debugging
5. Use postman collection for testing 

## Note for my future self

- EventFlow framework force using autofac DI framework

## Source and related materials

- https://bitbucket.org/publicwichary/net-core-cqrs-event-sourcing
see also this repository for hand written implementation of cqrs and event sourcing
- https://multizone.atlassian.net/wiki/spaces/KD/pages/571604993/CQRS+Event+sourcing