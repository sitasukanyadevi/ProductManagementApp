Create Database ProductApp

use ProductApp

Create Table PApp
(
Product_Id int identity Primary key,
Product_Name varchar(50),
Product_Description varchar(50),
Product_Quantity int,
Product_Price bigint
)


select * from PApp