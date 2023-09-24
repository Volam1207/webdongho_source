DROP DATABASE WatchStore
CREATE DATABASE WatchStore;

USE WatchStore;

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY,
    CategoryName NVARCHAR(50) NOT NULL
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    CategoryID INT,
    Price DECIMAL(10, 2) NOT NULL,
	Brand NVARCHAR(30) NOT NULL,
    Description NVARCHAR(MAX),
    StockQuantity INT,
    ImageURL NVARCHAR(MAX),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Address NVARCHAR(MAX),
	Role NVARCHAR(20) NOT NULL CHECK (Role IN ('user', 'admin', 'employee'))
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    UserID INT,
    OrderDate DATETIME NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    OrderStatus NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    Price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE ShoppingCartItems (
    CartItemID INT PRIMARY KEY,
    UserID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Dummy data
INSERT INTO Categories (CategoryID, CategoryName)
VALUES
    (1, 'Diving Watch'),
    (2, 'Mechanical Watch'),
    (3, 'Smart Watche'),
    (4, 'Luxury Watche'),
    (5, 'Automatic Watch');


INSERT INTO Products (ProductID, ProductName, CategoryID, Price, Brand, Description, StockQuantity, ImageURL)
VALUES
    (1, 'Apple Watch SE GEN 1', 3, 128.99, 'Apple', 'WHY APPLE WATCH SE — It has essential features to keep you connected, active, and healthy. SE is 2x faster and has a 30% larger
     display than Series 3. And safety features like fall detection and Emergency SOS can get you help when you need it.', 50, 'https://m.media-amazon.com/images/I/71lU6IcsUcL._AC_UF894,1000_QL80_.jpg'),
    (2, 'Redmi Watch 2 Lite', 3, 68.99, 'Xiaomi', 'The Redmi Watch 2 Lite is a budget fitness tracker with a large 1.55-inch HD display.
    It has over 100 fitness modes, 5 ATM water resistance, SpO₂ measurement, 24-hour heart rate tracking, multi-system standalone GPS, 
    and up to 10 days of battery life. It also supports Strava, Apple Health, 
    and incoming messages and calls.', 30, 'https://xiaomistoreph.com/cdn/shop/products/Redmi_Watch2Lite_WBG_Black_3_1000x1000.jpg?v=1681466419https://m.media-amazon.com/images/I/71lU6IcsUcL._AC_UF894,1000_QL80_.jpg'),
    (3, 'Omega Seamaster Ploprof 1200M Co-Axial Master Chonometer Mens Watch', 1, 17350.99,'Omega', 'A feature-rich smartwatch with health tracking and notifications.', 20, 'https://m.media-amazon.com/images/I/41vagbj7fqL._SR600%2C315_PIWhiteStrip%2CBottomLeft%2C0%2C35_SCLZZZZZZZ_FMpng_BG255%2C255%2C255.jpg'),
    (4, 'Luxury Gold WatchFORSINING Automatic Watch', 2, 199.99,'FORSINING', 'An exquisite gold watch with intricate details.', 10, 'https://m.media-amazon.com/images/I/81siZSKeJ1L._AC_UL1500_.jpg');

INSERT INTO Users (UserID, Username, Password, FirstName, LastName, Email, Phone, Address, Role)
VALUES
    (1, 'admin', '$2a$11$Mx6gMswrRuJt0Zt9bzfTQuTxO1WC3TryQ/QjnM9WkKKteXrnkBwqa', 'Harry', 'Maguire', 'harrymaguire@mu.com', '0903817263', '310 Q12, TP HCM', 'admin'),
    (2, 'User', '$2a$11$Mx6gMswrRuJt0Zt9bzfTQuTxO1WC3TryQ/QjnM9WkKKteXrnkBwqa', 'Lee', 'Sin', 'leesin@lol.com', '0935000111', '123 Nguyen Van Linh, TP HCM','User'),
    (3, 'employee', '$2a$11$Mx6gMswrRuJt0Zt9bzfTQuTxO1WC3TryQ/QjnM9WkKKteXrnkBwqa', 'Doraemon', 'Cat', 'meotier3tonbanhran@future.com', '0123456789', '789 Tran Duy Hung, Cau Giay','employee');


INSERT INTO Orders (OrderID, UserID, OrderDate, TotalAmount, OrderStatus)
VALUES
    (1, 1, '2023-08-28 10:00:00', 1798.98, 'Processing'),
    (2, 2, '2023-08-29 15:30:00', 2492.98, 'Shipped');


INSERT INTO OrderItems (OrderItemID, OrderID, ProductID, Quantity, Price)
VALUES
    (1, 1, 1, 2, 34701.99),
    (2, 1, 3, 1, 139.99),
    (3, 2, 2, 3, 79.99),
    (4, 2, 4, 1, 999.99);

INSERT INTO ShoppingCartItems (CartItemID, UserID, ProductID, Quantity)
VALUES
    (1, 1, 3, 2),
    (2, 2, 1, 1);