# Backend System Design Assignment – Mini Appointment Booking System

## 🎯 Objective

Design a scalable backend system for booking doctor appointments at multiple clinics. Focus
on backend architecture, data modeling, and scalability. You are expected to propose a clean
and extensible design, keeping in mind real-world constraints.

## 💼 Business Scenario

You’re building the backend of a platform where:
- Each clinic has multiple doctors.
- Each doctor can define available time slots.
- A patient can search and book an available slot.
- Each slot must be booked by only one patient.
- New clinics and doctors can be added dynamically.

You do not need to implement authentication, front-end, or third-party integrations.

## 🔍 Your Tasks

1. **Entity Design & Data Modeling** (30–45 mins)
Identify and define key entities: e.g., Clinic, Doctor, Patient, Schedule, Appointment. Design a
relational database schema with appropriate relationships, constraints, and indexes.

2. **System Architecture** (60–75 mins)
Present a high-level architecture showing different backend layers:
    - API Layer (REST/gRPC)
    - Service Layer (e.g., Booking Service)
    - Data Layer (DB + optional cache)
    - Background workers (optional)
Provide a diagram or flow showing the booking operation.
Describe how you prevent double bookings and ensure data consistency.

3. **Scalability & Extensibility** (30–40 mins)
Propose how the system could scale:
    - Support thousands of users and clinics
    - Use caching, queues, replication, or load balancers
Describe how the system could later support features like:
    - SMS/Email reminders
    - Online consultations
    - Payment integration

## ✅ Submission Guidelines
- Submit a PDF or Markdown document.
- Diagrams can be hand-drawn or digital.
- Clearly label each section.
- Focus on clarity over code.

## 🔍 Evaluation Focus
  - Data Modeling
  - System Design
  - Scalability Strategy
  - Extensibility
  - Communication Clarity