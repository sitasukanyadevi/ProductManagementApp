using System;
using System.Data;
using System.Data.SqlClient;
namespace ProductManagementApp
{
    class ProductManagement
    {
        public void AddNewProduct(SqlConnection con)
        {
            var row = Program.ds.Tables[0].NewRow();
            Console.Write("Enter Product Name: ");
            row["Product_Name"] = Console.ReadLine();
            Console.Write("Enter Product Description: ");
            row["Product_Description"] = Console.ReadLine();
            Console.Write("Enter Product Quantity: ");
            row["Product_Quantity"] = Console.ReadLine();
            Console.Write("Enter Product Price: ");
            row["Product_Price"] = Console.ReadLine();

            Program.ds.Tables[0].Rows.Add(row);
            Console.WriteLine("Product inserted successfully.");
        }

        public void GetProduct(SqlConnection con)
        {
            Console.Write("Enter id that you want view : ");
            int id = Convert.ToInt16(Console.ReadLine());
            DataRow[] rows = Program.ds.Tables[0].Select($"Product_Id={id}");
            if (rows.Length > 0)
            {
                DataRow row = rows[0];
                Console.WriteLine($"{row["Product_Id"]} | {row["Product_Name"]} | {row["Product_Description"]} | {row["Product_Quantity"]} | {row["Product_Price"]}");
            }
            else
            {
                Console.WriteLine($"No Product found with id={id}");
            }

        }

        public void GetAllProducts(SqlConnection con)
        {
            for (int i = 0; i < Program.ds.Tables["PAppTable"].Rows.Count; i++)
            {
                for (int j = 0; j < Program.ds.Tables["PAppTable"].Columns.Count; j++)
                {
                    Console.Write($"{Program.ds.Tables["PAppTable"].Rows[i][j]} |");
                }
                Console.WriteLine();
            }
        }
        public void UpdateProduct(SqlConnection con)
        {
            Console.WriteLine("Enter the index row:");
            int i = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter column name: ");
            string Cname = Console.ReadLine();
            Console.Write("Enter new value that you want to update : ");
            string V = Console.ReadLine();

            Program.ds.Tables[0].Rows[i][Cname] = V;
            Console.WriteLine("Product updated successfully.");

        }
        public void DeleteProduct(SqlConnection con)
        {
            Console.Write("Enter Index of Id: ");
            int i = Convert.ToInt16(Console.ReadLine());
            Program.ds.Tables[0].Rows[i].Delete();
            Console.WriteLine("Record deleted successfully.");
        }
    }

  internal class Program
     {
      public static DataSet ds = new DataSet();
       static void Main(string[] args)
       {
            SqlConnection con = new SqlConnection("Server=US-8ZBJZH3; database=ProductApp; Integrated Security=true");

            ProductManagement ob = new ProductManagement();

            SqlDataAdapter adp = new SqlDataAdapter("Select * From PApp", con);

            adp.Fill(ds, "PAppTable");
            string s = "";
            do
            {
                Console.WriteLine("-----Welcome to Product Management App----");
                Console.WriteLine("1. Add New Product ");
                Console.WriteLine("2. Get Product ");
                Console.WriteLine("3. Get All Products ");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Delete Product ");
                Console.WriteLine("Enter ur choice");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            ob.AddNewProduct(con);
                            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                            adp.Update(ds,"PAppTable");
                            break;
                        }
                    case 2:
                        {
                            ob.GetProduct(con);
                            break;
                        }
                    case 3:
                        {
                            ob.GetAllProducts(con);
                            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                            adp.Update(ds, "PAppTable");
                            break;
                        }
                    case 4:
                        {
                            ob.UpdateProduct(con);
                            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                            adp.Update(ds,"PAppTable");
                            break;
                        }
                    case 5:
                        {
                            ob.DeleteProduct(con);
                            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                            adp.Update(ds,"PAppTable");
                            break;
                        }
                    
                    default:
                        {
                            Console.WriteLine("Invalid choice");
                            break;
                        }
                }
                Console.WriteLine("Do u want to continue[y/n]");
                s = Console.ReadLine();
            } while (s.ToLower() == "y");
        }       
    }  
}
