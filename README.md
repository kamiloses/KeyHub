
A web application built with ASP.NET Core, functioning as an online store for
computer games. Users can create accounts, log in, add funds to their account,
and purchase games with applied discounts and current stock levels. The system
allows searching and filtering games by price, genre, platform, and sorting by
various criteria. Purchase history is paginated and stored in In-Memory Cache.
Administrators have access to a management panel where they can add new
games along with images.






<h1 align="center">🚀 How to Run the Application</h1>

Follow these steps to run the application locally.

---

## 1️⃣ Clone the Repository

Clone the repository to your local machine:

```
git clone https://github.com/kamiloses/KeyHub.git
```

---

## 2️⃣ Change Directory

Navigate to the project folder:

```
cd KeyHub.Market
```

---

## 3️⃣ Apply Entity Framework Migrations

Create the database and apply migrations:

```
dotnet ef migrations add InitialCreate
```

---

## 4️⃣ Build and Start with Docker Compose

Build and run the application using Docker:

```
docker-compose up --build
```

---

## 5️⃣ Access the Application

Open your browser and go to:

[http://localhost:8081/](http://localhost:8081/)








                   


<br><br><br><br><br><br><br><br><br>
       <h1 align="center">💡 How does the application work?</h1>

## 1. Home Page
Initially, you will be redirected to the Home Page, where you can see games available at the biggest discounts.

![Home Page](https://github.com/user-attachments/assets/667d35e1-d398-4b84-b225-95fef3f1547d)

---

## 2. Account Management

You can either create an account or log in, depending on your situation. Validation has been implemented on both the client and server sides.  

By default, two users have already been created for you, so you can log in immediately without registering a new account:

- **Admin user**
  - Username: `admin`
  - Email: `admin@gmail.com`
  - Password: `Admin123!`
  - Role: Admin
  - Balance: 1000

- **Normal user**
  - Username: `user`
  - Email: `user@gmail.com`
  - Password: `User123!`
  - Role: User
  - Balance: 500

This allows you to test both admin and regular user functionalities right away.

![Account Management](https://github.com/user-attachments/assets/99355ca9-1abb-4cc1-ac9f-59cfbd094e97)
---

## 3. Starting Balance
After a successful login, you will receive **500 PLN** to start with, which you can use to buy your first game.

![Starting Balance](https://github.com/user-attachments/assets/07b3ded4-abb3-4774-adee-4907993d0ec0)

---

## 4. Purchase History
You can check games you have bought previously. Caching has been added for better performance.

![Purchase History](https://github.com/user-attachments/assets/72644725-da18-451e-9063-a0dece047c1f)

---

## 5. Game Filtering
You can check games according to your filters.

![Game Filtering](https://github.com/user-attachments/assets/7ee5102c-4df8-4b05-a6a4-97c5bee91579)

---

## 6. Pagination
Pagination has been added to improve the performance of fetching data from the database.

![Pagination](https://github.com/user-attachments/assets/d590f6e1-3747-47f1-872c-111557369a55)

---

## 7. Purchase a Game
After purchasing a game, your balance will decrease accordingly, and the game will appear in your purchase history.  
*Note: Game keys were not implemented.*

![Purchase Game](https://github.com/user-attachments/assets/b04d3a3c-51d1-4860-8b15-d73cc15bb9a4)

---

## 8. Admin: Publish a Game
As an admin, you have access to a special button that allows you to publish a new game for sale.

![Admin Publish Game](https://github.com/user-attachments/assets/9ed62ff8-d654-40e6-bef3-9007411d6f00)


<br><br><br><br>




## Technologies Used

The project is built with the following technologies:

- **ASP.NET Core MVC (Razor Views)**
- **Entity Framework Core & SQL Server**
- **ASP.NET Identity**
- **In-Memory Cache**
- **xUnit & Moq**
