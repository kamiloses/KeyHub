
# Step 1: Clone the repository


# Step 2: Change directory
```bash
cd KeyHub.Market
```
# Step 3: Build and start with Docker Compose
```bash
docker-compose up --build
```
# Step 4: Run the app at http://localhost:8081/





































<h2>1.Initially, you will be redirected to the Home Page, where you can see games available at the biggest discounts.</h2>
<img width="1838" height="1012" alt="image" src="https://github.com/user-attachments/assets/667d35e1-d398-4b84-b225-95fef3f1547d" />
<br><br>

<h2>2.You can either create an account or log in, depending on your situation.Validation has been implemented on both the client and server sides.<h2>
<img width="1838" height="1012" alt="image" src="https://github.com/user-attachments/assets/99355ca9-1abb-4cc1-ac9f-59cfbd094e97" />
<br><br>

<h2>3.After a successful login, you will receive 500 PLN to start with, which you can use to buy your first game.</h2>
<img width="1838" height="1012" alt="image" src="https://github.com/user-attachments/assets/07b3ded4-abb3-4774-adee-4907993d0ec0" />
<br><br>

TODO
<img width="1836" height="1012" alt="image" src="https://github.com/user-attachments/assets/a61d3de8-5b41-4c67-9f87-4b11417361f1" /> <img width="1836" height="1012" alt="image" src="https://github.com/user-attachments/assets/c11ad64c-71a4-4b35-95ae-9c6dae7441f0" />
<br><br>


<h2>5.Pagination has been added to improve the performearce of fetching data from the database.</h2>
<img width="1847" height="661" alt="image" src="https://github.com/user-attachments/assets/d590f6e1-3747-47f1-872c-111557369a55" /> <img width="1699" height="996" alt="image" src="https://github.com/user-attachments/assets/7ee5102c-4df8-4b05-a6a4-97c5bee91579" />
<br><b>

<h2>6.After purchasing a game, your balance will decrease accordingly, and the game will appear in your purchase history.
(Note: Game keys were not implemented.)</h2>
<img width="1823" height="954" alt="image" src="https://github.com/user-attachments/assets/b04d3a3c-51d1-4860-8b15-d73cc15bb9a4" />
<br><br>


<h2>7.As an admin, you have access to a special button that allows you to publish a new game for sale.</h2>

<img width="1823" height="954" alt="image" src="https://github.com/user-attachments/assets/9ed62ff8-d654-40e6-bef3-9007411d6f00" />


Technologies used in the project:
- ASP.NET Core MVC (Razor Views)
- Entity Framework Core & SQL Server
- ASP.NET Identity
- In-Memory Cache
- xUnit & Moq

