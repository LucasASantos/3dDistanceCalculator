
# 3d Distance Calculator

## Objective
This project was developed for an example of the use of **layered development**. Here we have a base project with a simple business rule with an architecture that is posible extends and scale according to project grow.

## Stack

### .Net5

### Asp.net Core 

## Architecture

The project follows the DDD N-Layer Architecture

![alt tag](./docs/img/ddd.jpeg)

* Presentation
	* Controlles(It's responsible for controller the views and call the application services)
	* Views (It's responsible for implement and use all informations got by controller)
* Application
	* Models (It's responsible for describe presentation data)
	* Factory (**An example** It's responsible for implement the Pattern Factory)
	* Map (It's responsible for mappin prensetation data and dommain data using AutoMapper)
	* Service (It's responsible for take all operations and services to down layer)
	* Tasks (It's responsible for implement all tasks of used many recourses)
* Domain
	* Models
	* DomainService (It's responsible for implement all data operations and validations and services to down layer)
	* Repository (It's responsible for implements all repositorycontracts)
	* Validation
* Data
	It's responsible to implement data cominication 
	* (It has many peculiarities from the ORM framework, so it don't need to describe)
* Cross-Cutting
	It's responsabe to take item thought the layers


## Next steps 

The project has many improve points, but I think the first step is generate an API and remove the presentation maybe using new technologies like Angular or React

## Frameworks
* AutoMapper
* FluentValidator
* EntityFrameworkCore 
* Native Dependency Injector
