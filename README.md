# Internship-7-Moodle

- Console .NET application that simulates basic Moodle system
- Implemented using Clean Architecture
- Data is stored in PostgreSQL database using Entity Framework Core

ER diagram:

<img width="400" height="555" alt="image" src="https://github.com/user-attachments/assets/1de5206f-3c98-47b8-83ae-f21a8661ec58" />




Architecture
- Domain - entities, enums, relations
- Application - main logic, services and validation
- Infrastructure - EF Core, DbContext, migrations, seed
- Presentation - console UI, menus, input/output

Authentication and Roles
- User login and registration
- Email and password validation
- Captcha during registration
- User roles - Student, Professor, Admin

Features:

- Student:
     - View enrolled courses
     - View notifications and materials
     - Private chat

- Professor:
     - View courses they teach
     - Course management
     - Add students to courses
     - Publish notifications
     - Add course materials
     - Private chat

- Admin:
     - User management
     - Delete users
     - Update user email
     - Change user role
     - Private chat
 
Steps for running the Application:
- Set up a PostgreSQL database 
- Configure the connection string in appsettings.json
- Run database migrations
  
You can use the following seeded users to test the application:

Admin
- Email: admin@moodle.test
- Password: admin123
- 
Professor
- Email: prof1@moodle.test
- Password: prof123
- 
Student
- Email: student1@moodle.test
- Password: student123

