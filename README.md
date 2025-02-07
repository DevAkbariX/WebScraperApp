# WebScraperApp

WebScraperApp is a background web scraping application that fetches and stores gaming news articles from a selected website every 5 minutes. The application ensures that the scraped data, including the title, description, and URL, are stored in a database, and duplicate articles are prevented from being saved. It leverages Clean Architecture to ensure maintainability and uses automatic migrations to keep the database schema up-to-date.

## Features

- Scrapes gaming news articles every 5 minutes.
- Fetches title, description, and URL of the latest articles.
- Checks for duplicates based on the article URL before saving.
- Uses automatic migrations for database schema updates.
- Implements logging for tracking the scraping process.
- Follows Clean Architecture for maintainable code structure.
- Built with Object-Oriented Programming principles to make the code modular and reusable.

## Technologies Used
- .NET Core
- HtmlAgilityPack for web scraping
- Microsoft.Extensions.Logging for logging
- Entity Framework Core for database interactions
- BackgroundService for running scraping tasks in the background

## Setup and Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/DevAkbariX/WebScraperApp.git
    ```

2. Install dependencies:

    ```bash
    dotnet restore
    ```

3. Set up the database and apply migrations:

    ```bash
    dotnet ef database update
    ```

4. Run the application:

    ```bash
    dotnet run
    ```

The application will now start scraping news articles every 5 minutes and saving them to the database.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
