# SchoolProject.api

**SchoolProject.api** is a backend Web API built using **ASP.NET Core**, designed to manage and streamline operations within a school system.  
It provides a set of RESTful endpoints that handle key entities such as **students, teachers, subjects, enrollments, and grades**, enabling efficient communication between the database and frontend interfaces.

---

## 🎯 Project Purpose

The goal of this project is to create a modular and scalable backend solution for a school management system.  
It serves as the foundation for building modern educational applications — allowing administrators, teachers, and students to interact through a structured API.

---

## ⚙️ Key Features

- **Student Management:** Add, update, view, and delete student records.  
- **Teacher Management:** Manage teacher information and their assigned subjects.  
- **Course & Subject Management:** Create and organize subjects and courses.  
- **Enrollment System:** Register students to courses and track their progress.  
- **Grades Handling:** Record and retrieve student grades with validation.  
- **Authentication & Authorization (Optional):** Secure endpoints using JWT tokens.  
- **Clean Architecture:** Organized using layers for Core, Data, Infrastructure, Service, and API for better maintainability.  
- **Entity Framework Core:** For ORM and database management.  
- **Swagger Integration:** For interactive API documentation and testing.

---

## 🧱 Architecture Overview

The project follows a **multi-layered architecture**, ensuring separation of concerns:

1. **Core Layer:**  
   Contains domain models, DTOs, and interfaces.

2. **Data Layer:**  
   Implements repositories and manages database access using Entity Framework Core.

3. **Service Layer:**  
   Handles business logic and data validation.

4. **Infrastructure Layer:**  
   Provides dependency injection, logging, and cross-cutting configurations.

5. **API Layer:**  
   Exposes endpoints, handles requests/responses, and integrates with Swagger.

---

## 🧩 Why This Project

This project demonstrates practical implementation of **clean architecture**, **repository pattern**, and **dependency injection** within a real-world scenario.  
It can be extended or customized for use in actual school management systems, learning platforms, or admin dashboards.

---

## 🚀 Future Enhancements

- Add role-based authentication (Admin / Teacher / Student).  
- Integrate attendance tracking and timetable management.  
- Add notifications or messaging between teachers and students.  
- Build a connected frontend (e.g., React or Angular) using this API.

---

## 👨‍💻 Author

**Mohamed Mongy**  
Software Engineer | ASP.NET Developer  
[GitHub Profile](https://github.com/M-Mongy)
