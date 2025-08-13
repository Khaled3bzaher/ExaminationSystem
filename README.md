# Examination System 🎓

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet\&logoColor=white)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-20-DD0031?logo=angular\&logoColor=white)](https://angular.dev)
[![MongoDB](https://img.shields.io/badge/MongoDB-Atlas-47A248?logo=mongodb\&logoColor=white)](https://www.mongodb.com/atlas)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-Worker-orange?logo=rabbitmq\&logoColor=white)](https://www.rabbitmq.com/)

A full‑stack online examination platform built with **.NET 9** and **Angular 20** following **Clean Architecture**. It supports **Students** and **Admins**, real‑time notifications, automated evaluation, and a clean, scalable backend powered by the **Specification Pattern** with **generic projection**. Notifications are persisted in **MongoDB** and pushed live via **SignalR**.

> **Frontend Repository**: [https://github.com/khaled3bzaher/ExaminationSystemAngular](https://github.com/Khaled3bzaher/ExaminationSystemAngular)

---

## ✨ Features

* **Authentication & Authorization**: JWT with role‑based access (Admin/Student).
* **Student**:

  * Take timed exams (auto‑submit on timeout).
  * Persisted answers with resume support.
  * Preview submitted Exams & scores.
* **Admin**:

  * CRUD for Subjects, Questions, Choices.
  * Manage Activation of Students.
* **Real‑time**: SignalR pushes (new exam started, results).
* **Automated Evaluation**: Background **Worker Service** via **RabbitMQ** calculates scores and returns results.
* **Search, Sort, Pagination** across lists.
* **Global API Contract** & **Exception Handling**.
* **Notifications storage in MongoDB** for flexible, high‑throughput writes.

---

## 🧭 Why Specification + Generic Projection

The **Specification Pattern** centralizes query intent (filters, includes, ordering), enabling:

* Reusable, testable query definitions.
* Separation of concerns between application logic and data access.
* **Generic projection**: one spec can both filter entities **and** project directly to DTOs using a typed `Select` expression — avoiding over‑fetching and reducing manual mapping.

> **Benefit**: The API returns exactly the fields the UI needs, with no extra mapping step and minimal database round‑trips.

---

## 🔧 Tech Stack

* **Backend**: ASP.NET Core 9, **EF Core**, FluentValidation, **Specification Pattern**, MongoDB, SignalR, RabbitMQ
* **Database**: SQL Server (domain), **MongoDB** (notifications)
* **Frontend**: Angular 20 (standalone components), **Tailwind CSS**, PrimeNG
* **Auth**: JWT , Identity

---

## 👤 Author

**Khaled A. Abd‑Elzaher** — Full‑Stack .NET Developer intern @ Atos

* LinkedIn: [https://www.linkedin.com/in/khaled3bzaher](https://www.linkedin.com/in/khaled3bzaher/)
* GitHub: [https://github.com/khaled3bzaher](https://github.com/khaled3bzaher)
