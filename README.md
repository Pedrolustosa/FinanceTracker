# FinanceTracker

[![Angular](https://img.shields.io/badge/Angular-18.1.4-red)](https://angular.dev/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3.3-blueviolet)](https://getbootstrap.com/docs/5.3/getting-started/introduction/)
[![MDB Angular](https://img.shields.io/badge/MDB_Angular-5.2.0-yellow)](https://mdbootstrap.com/docs/angular/)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-purple)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

FinanceTracker is a personal finance management platform designed to help users manage their income and expenses efficiently. The application allows users to track financial transactions, categorize expenses, generate financial reports, and communicate with financial advisors. The platform provides real-time notifications and a user-friendly interface for an enhanced experience.

## Features

1. **User Registration and Login:**
   - Secure user authentication using ASP.NET Core Identity.
   - User profile management.

2. **Income and Expense Management:**
   - Add, edit, and delete income and expense records.
   - Categorize transactions (e.g., food, transportation, entertainment).
   - Display financial summaries, including current balance, total income, and expenses.

3. **Graphical and Report Visualization:**
   - Generate monthly and yearly financial reports.
   - Display interactive charts using libraries like Chart.js or ngx-charts.
   - Export reports to PDF or Excel.

4. **Alerts and Notifications:**
   - Real-time notifications using SignalR for pending transactions, payment deadlines, and financial goals.
   - Customizable alerts for users.

5. **Transaction Search and Filters:**
   - Advanced search functionality to find specific transactions.
   - Filters by category, date, and amount.

6. **Receipt and Proof Upload:**
   - Upload photos of receipts and payment proofs.
   - Display a gallery of images associated with transactions.

7. **Private Messaging System:**
   - Real-time chat with financial advisors using SignalR.
   - Message history and management.

8. **Deployment:**
   - Application deployment.

## Technologies Used

- **Backend:**
  - ASP.NET Core Web API
  - Entity Framework Core
  - AutoMapper
  - SignalR

- **Frontend:**
  - Angular
  - ngx-bootstrap/Bootstrap 5 for UI components, responsive and attractive UI
  - Reactive Forms and Template Forms for form handling
  - Chart.js for data visualization

- **Database:**
  - SQL Server

## Development Steps

1. **Setting Up the Developer Environment:**
   - Install and configure the .NET SDK and CLI.
   - Install and configure Node.js and Angular CLI.
   - Create ASP.NET Core Web API and Angular projects using CLI.

2. **Backend Implementation:**
   - Create data models and configure Entity Framework Core.
   - Implement API endpoints for managing income, expenses, categories, and reports.
   - Configure authentication and authorization with ASP.NET Core Identity.
   - Integrate AutoMapper for DTO and domain model mapping.
   - Configure SignalR for real-time notifications.

3. **Frontend Implementation:**
   - Set up the Angular project and install necessary dependencies.
   - Create components for main functionalities (registration, login, income, expenses, reports, etc.).
   - Implement Reactive Forms and Template Forms for transaction forms.
   - Integrate chart libraries for financial data visualization.
   - Add UI components from Angular Material or ngx-bootstrap.
   - Configure routing and route guards for secured routes.

4. **Integration and Testing:**
   - Test integration between frontend and backend.
   - Perform usability testing and bug fixing.
   - Test functionality on various devices to ensure responsiveness.

5. **Deployment:**
   - Configure project for deployment.
   - Deploy the application and test in a production environment.

## Getting Started

Follow these steps to get started with FinanceTracker:

1. **Clone the repository:**
   ``` bash
       git clone https://github.com/yourusername/FinanceTracker.git
   ```
   ``` bash
       cd FinanceTracker
   ```

1. **Clone the repository:**
   - Navigate to the FinanceTracker.Api folder and create migration for DB.
      ``` bash
          dotnet ef migrations add InitialCreat.v1 -o Data/Migrations
      ```
      ``` bash
          dotnet ef database update
      ```
   - Restore dependencies and run the project:
      ``` bash
          dotnet restore
      ```
      ``` bash
          dotnet run
      ```
      
      
2. **Frontend Setup:**
   - Navigate to the FinanceTracker.Client folder.
   - Install dependencies and run the Angular app:
      ``` bash
          npm install
      ```
      ``` bash
          ng serve
      ```

3. **Open the Application:**
   - Open your browser and navigate to http://localhost:4200.

### **Folder Structure**
    - `FinanceTracker.Api` - ASP.NET Core Web API project
    - `FinanceTracker.Client` - Angular project

## Contributing
    - Contributions are welcome! Please read the CONTRIBUTING.md for details on the code of   conduct and the process for submitting pull requests.

## License
    - This project is licensed under the MIT License - see the LICENSE file for details.
