This ASP.NET Core API is designed to interact with the CraveIt client, providing a range of endpoints to support user authentication, quizzes, and quiz results.
<br>
## API Endpoints
### User Authentication
* __POST /api/user/login:__ Validate a user's email and password and returns a JSON Web Token (JWT) token with an expiration date if the authentication is successful.
* __POST /api/user/register:__  Perform user registration and automatically log in the user.
### Quizzes
* __GET /api/quizzes:__ Retrieve all quizzes associated with the user.
* __GET /api/quizzes/{id}:__ Retrieve a specific quiz by its ID.
* __POST /api/quizzes:__ Handle the processing of a quiz submission, which includes calling the Yelp Fusion API, and returns a result based on the submission criteria.
### Results
* __GET /api/results:__ Retrieve all saved quiz results
* __GET /api/results/{id}:__ Retrieve a specific quiz result by its ID.
* __POST /api/results:__ Add a quiz result to the database
* __DELETE /api/results/{id}:__ Delete a quiz result by ID
## Architecture 
The CraveIt API follows a layered architecture, consisting of the following components:
* __Controllers:__ These components define the API endpoints and handle the incoming HTTP requests.
* __Services:__ The service layer contains classes and methods that perform various processing tasks for the API. It encapsulates the business logic and coordinates interactions between the controllers and repositories.
* __Repositories:__ The repository layer provides classes and methods for database operations. It abstracts away the underlying data access technologies and provides an interface for data access.
* __Models:__ These are the abstractions of database tables, representing the entities used within the system.
* __ViewModels:__ The view models are classes used for returning data specifically for API calls. They define the structure and format of the data sent back to the client.
* __Helpers:__ The helper methods are utility functions that support functionality in other files. They provide reusable code snippets for common operations.
* __Data__: The data folder contains the database context, which represents the connection to the underlying database.
* __Migrations__: This folder contains files that represent incremental changes to the database schema over time. Each migration file applies a specific modification to the database.
