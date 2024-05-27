# TaskManager

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![GitHub stars](https://img.shields.io/github/stars/DenisGas/TaskManager.svg)](https://github.com/DenisGas/TaskManager/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/DenisGas/TaskManager.svg)](https://github.com/DenisGas/TaskManager/network)
[![GitHub issues](https://img.shields.io/github/issues/DenisGas/TaskManager.svg)](https://github.com/DenisGas/TaskManager/issues)

![Preview image](https://github.com/DenisGas/TaskManager/assets/81939899/60252d5a-de11-4cef-80dc-dc7537fb1b51)


Welcome to TaskManager, a simple and efficient task management application built using Visual Studio and Entity Framework.

Sure, here are the steps to set up and run your task manager using Visual Studio and Entity Framework:

### 1. Database Configuration

Open the `appsettings.json` file in your Visual Studio solution. Define the connection string to your database. For example:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagerDb;Trusted_Connection=True;"
},
```

### 2. Creating Migrations

Open the Package Manager Console in Visual Studio, usually found under Tools -> NuGet Package Manager -> Package Manager Console. Enter the following command to create a new migration based on changes in your data model:

```bash
EntityFrameworkCore\Add-Migration InitialCreate
```

### 3. Applying Migrations

After creating the migration, enter the following command to apply the migration to the database:

```bash
EntityFrameworkCore\Update-Database
```

This will create the necessary tables in the database according to your data model.

### 4. Using the Application

Now your task manager is ready to use. Run the program through Visual Studio, and you'll be able to work with your tasks and categories.

These are the basic steps to set up and run your task manager in Visual Studio using Entity Framework. Don't forget to make any additional configurations and add features as needed for your application.

### 5. Repositories for working with data

#### `CategoryRepos` (Category Repository)

This repository is responsible for managing task categories.

- `AllCategories`: A list of all categories.
- `GetCategoryById(int categoryId)`: Get a category by its identifier.
- `AddCategory(Category category category)`: Add a new category.
- `DeleteCategory(int categoryId)`: Delete a category by identifier.

#### `TaskRepos` (TaskRepository).

This repository is responsible for managing tasks.

- `Tasks`: A list of all tasks.
- `getCompletedTasks`: A list of completed tasks.
- `getUnCompletedTasks`: A list of unfinished tasks.
- `GetObjectTask(int id)`: Get task object by id.
- `AddTask(MyTask newTask)`: Add a new task.
- `UpdateTask(MyTask updatedTask)`: Update an existing task.
- `DeleteTask(int id)`: Delete a task by identifier.

### Database and Tables

#### `DbObject` (Database Object)

This class initializes and populates the database.

- `Initial`: Method to initialize the database. Creates and adds categories if they do not exist.

#### `AppDbContent` (Application Database Context)

This class represents the application database context.

- `Tasks`: The dataset for tasks.
- `Categories`: The dataset for categories.

The `OnModelCreating` method is used to set up a many-to-many relationship between tasks and categories, allowing you to store information about the relationships between them in the `TaskCategories` table. 

Defining classes and interfaces in your application is key to managing tasks and categories. Here is a nicer and more structured description of these components:

#### Data Models

#### `MyTask`.
- `Id`: Unique task identifier
- `Title`: Task title (required field)
- `Description`: Description of the task
- `CreatedDate`: Date when the task was created
- `UpdatedDate`: Date when the task was last updated.
- `DueDate`: Due date of the task (mandatory field)
- `IsCompleted`: Completion status of the task
- `CategoryIds`: List of category identifiers to which the task belongs
- `Categories`: List of categories associated with the task
- `Priority`: Task priority (high, medium, low)

#### `Category`
- `Id`: Unique identifier of the category
- `CategoryName`: Category name
- `Description`: Description of the category
- `Tasks`: List of tasks belonging to this category

### Interfaces and repositories

#### `IAllTasks`.
- `Tasks`: List of all tasks
- `getCompletedTasks`: List of completed tasks
- `getUnCompletedTasks`: List of unfinished tasks
- `GetObjectTask(int id)`: Get task object by id
- `AddTask(MyTask newTask)`: Add new task
- `UpdateTask(MyTask updatedTask)`: Update an existing task
- `DeleteTask(int id)`: Delete task by identifier

#### `ITasksCategory` (Task Categories)
- `AllCategories`: List of all categories
- `GetCategoryById(int categoryId)`: Get a category by identifier
- `AddCategory(Category category)`: Add a new category
- `DeleteCategory(int categoryId)`: Delete a category by identifier

These classes and interfaces provide a convenient way to interact with task and category data in your application.

### Controllers

#### `TasksController`.

This controller is responsible for managing the tasks in the application.

- `Index`: Action to redirect to a list of tasks.
- `List`: Action to display a list of tasks with filters applied.
- `Delete`: Action to display the task deletion page.
- `DeleteConfirmed`: Action to confirm the deletion of the task.
- `Edit`: Action to display the task edit page.
- `Edit` (POST): Action to save the edited task.
- `UpdateStatus`: Action to update the status of the task.
- `Create`: Action to display the new task creation page.
- `Create` (POST): Action to save the new task.
- `Details`: Action to display detailed information about the task.

#### `HomeController` (HomeController).

This controller manages the application's home page.

- `Index`: Action to display the home page with today's tasks and the last 10 tasks.
- `Privacy`: Action to display the privacy policy page.
- `Error`: Action to handle errors and display the appropriate page.

These controllers provide request routing and view management for tasks and the home page of your application.

