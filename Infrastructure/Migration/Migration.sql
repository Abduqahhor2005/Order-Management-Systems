create table Customers
(
    id uuid primary key,
    FullName varchar unique,
    Email varchar unique,
    Phone varchar,
    CreatedAt date
);
create table Products
(
    id uuid Primary Key,
    Name varchar unique,
    Price float8,
    StockQuantity int,
    CreatedAt date
);
create table Orders
(
    Id uuid Primary Key,
    CustomerId uuid references Customers(id),
    TotalAmount float8,
    OrderDate date,
    Status varchar,
    CreatedAt date
);
create table OrderItems
(
    Id uuid Primary Key,
    OrderId uuid references Orders(id),
    ProductId uuid references Products(id),
    Quantity int,
    Price float8,
    CreatedAt date
);