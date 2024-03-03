# GuardedGates

GuardedGates is a secure and efficient project that demonstrates the integration of various technologies to ensure the security of your applications. The project consists of two main parts: a frontend built with Python (Flask) and a backend built with .NET.

## Features

- **JWT Authentication**: GuardedGates implements JSON Web Token (JWT) authentication for secure access to the backend services.
- **ClamAV Integration**: The backend utilizes ClamAV, a powerful open-source antivirus engine, to scan files for viruses.
- **Ocelot API Gateway**: Ocelot is used as an API gateway to streamline requests and responses across different services.
- **Dapper ORM**: The project utilizes Dapper, a simple object mapper for .NET, to access and manipulate data in the SQL Server database.
- **Secure File Upload**: Securely upload files to the server, ensuring they are scanned for viruses before processing.
- **Weather Web Application**: Included is a simple weather web application to demonstrate the functionality of the Ocelot API Gateway.

## Components

- **DapperMVCLearning.AuthenticationService**: Class Library project used for JWT authentication.
- **DapperMVCLearning.Data**: Class Library project used to access data from the SQL Server database using Dapper.
- **DapperMVCLearning.Ocelot**: API Gateway for routing requests and responses.
- **DapperMVCLearning.UI**: Main web app project containing controllers for handling person, login, and file upload.
- **DapperMVCLearning.VirusScanner**: Class Library project used to access ClamAV running in a Docker container.
- **DapperMVCLearning.WeatherWebApplication**: A simple demo app for testing the Ocelot API Gateway.

## Setup Instructions

1. Clone the repository: `gh repo clone AdityaDwivediAtGit/GuardedGates`
2. Set up the backend:
   - Ensure you have SQL Server installed and running.
   - Update the connection string in `appsettings.json` in the `DapperMVCLearning.Data` project.
   - Run the database migrations using scripts provided in the `DapperMVCLearning.Data` project.
3. Set up the frontend:
   - Install the required Python packages: `pip install -r requirements.txt`.
   - Start the Flask app: `python app.py`.
4. Access the frontend at `http://localhost:1234` and explore the features.

## Database Setup

- Use the `DapperSqlScript.sql` file to create tables and stored procedures in your SQL Server database.

## Usage

- **JWT Authentication**: Generate a JWT token using the login functionality and use it to access protected routes.
- **File Upload**: Upload files through the frontend, ensuring they are scanned for viruses before processing.
- **Ocelot API Gateway**: Test the redirection of requests and responses using the Weather Web Application.

## Credits

This project was created by Aditya Dwivedi for educational and demonstration purposes. It incorporates various technologies to showcase best practices in security and application development.

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT). Feel free to use, modify, and distribute the code for your own projects.
