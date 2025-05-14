
# 🎓 Online Learning Platform

## 🚀 Overview
The **Online Learning Platform** is a web-based educational system designed to facilitate online learning through structured course management, student enrollment tracking, and interactive assessments. This platform provides seamless authentication, role-based access control, and a well-organized learning experience.

Built using **Clean Architecture**, the system ensures modularity, scalability, and maintainability by separating concerns into multiple layers.

---
## 📋 Documentation For Project  
- **LINK:** [E-Learning Documentation](https://docs.google.com/document/d/1Isy5c27E8eTTnMRU_O5s6biLVf4ZUhzS/edit)

## 🛠️ Technologies Used

### Backend:
- **Framework:** ASP.NET Core Web API  
- **Architecture:** Clean Architecture (API, Application, Domain, Infrastructure, Service)  
- **ORM:** Entity Framework Core  
- **Authentication:** ASP.NET Core Identity  

### Frontend:
- **Framework:** Angular  
- **Repository:** [E-Learning Frontend Repo](https://github.com/omarazam163/e-learning-front-end)

### Database:
- **SQL Server**

---

## 🌟 Features

✅ **User Authentication:** Secure registration, login, and role management  
✅ **Forgot Password with OTP:** Users can request a One-Time Password (OTP) via email to reset their forgotten password securely  
✅ **Course Management:** Listing, enrollment, and progress tracking  
✅ **Role-Based Access:** Instructor and Student roles  
✅ **Assignments & Quizzes:** Interactive learning assessments  
✅ **Reviews & Ratings:** Course feedback system  
✅ **Payment Integration:** Secure payment processing for premium courses  
✅ **PayPal Gateway Integration:** Supports PayPal payments for smooth and reliable transactions  
✅ **Purchase Confirmation Email:** Users receive an email notification after successfully purchasing a course  
✅ **Notification System:** Alerts for course updates, enrollments, and deadlines  

---

## 🏗️ Project Architecture (Clean Architecture)

The platform follows **Clean Architecture**, ensuring a well-structured and maintainable codebase:


📂 **OnlineLearningPlatform**  
┣ 📂 **API Layer** 🌍 - Handles HTTP Requests & Responses  
┣ 📂 **Core Layer** 🏗️ - Contains Use Cases & Business Logic  
┣ 📂 **Data Layer** 📌 - Defines Entities & Core Business Rules  
┣ 📂 **Infrastructure Layer** 🏢 - Handles Data Persistence, Authentication, Emailing, and Payment Integration  
┣ 📂 **Service Layer** ⚙️ - Contains Business Services & Processing Logic  

┣ 📂 **Testing Layer** ⚙️ - Core business logic is covered by unit tests to ensure functionality and reliability
---

## 🗄️ Database Schema

### 📋 Tables:

📌 **Users** - Stores user details (Students, Instructors, Admins)  
📌 **Courses** - Stores course details  
📌 **Enrollments** - Tracks student enrollments  
📌 **Lessons** - Course content sections  
📌 **Quizzes** - Contains course quizzes  
📌 **Questions** - Questions within quizzes  
📌 **Answers** - Student responses to questions  
📌 **Reviews** - User reviews for courses  
📌 **Payments** - Tracks course payments  
📌 **Notifications** - Stores notifications for users (New course updates, reminders, etc.)  
📌 **OTPRequests** - Stores OTP codes and expiration info for password reset requests  

---

## ⚡ Installation

1️⃣ Clone the repository:  
```bash
git clone https://github.com/AdelMuhammad-23/OnlineLearningPlatform.git
```

2️⃣ Navigate to the project folder:  
```bash
cd OnlineLearningPlatform
```

3️⃣ Run database migrations:  
```bash
dotnet ef database update
```

4️⃣ Run the API:
```bash
dotnet run
```

5️⃣ Clone the frontend repository:
```bash
git clone https://github.com/omarazam163/e-learning-front-end.git
```

6️⃣ Navigate to the frontend folder and install dependencies:
```bash
cd e-learning-front-end
npm install
```

7️⃣ Run the Angular application:
```bash
ng serve --open
```

---

## 🚀 Deployment
The platform can be deployed on:
- 🌍 **Azure**  
- ☁️ **AWS**  
- 🖥 **IIS (Internet Information Services)**  
- 🐳 **Docker** (Containerized Deployment)

---

### 🎯 Happy Learning! 🚀
