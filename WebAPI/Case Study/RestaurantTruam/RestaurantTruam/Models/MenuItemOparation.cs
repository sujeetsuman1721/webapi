using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RestaurantTruam.Models
{
    public class MenuItemOparation
    {
        
            private SqlDataReader reader;

            // ado.net to read the data and fetch the data from databse 

            public List<MenuItem> GetMenuItemData(string con)
            {
                List<MenuItem> menuList = new List<MenuItem>();

                SqlConnection sqlConnection = new SqlConnection(con);

                string querry = "select * from MenuItem";

                SqlCommand cmd = new SqlCommand(querry, sqlConnection);

                try
                {
                    sqlConnection.Open();

                    reader = cmd.ExecuteReader();
                    MenuItem menuItem = null;
                    while (reader.Read())
                    {
                        menuItem = new MenuItem();
                        menuItem.Id = reader.GetInt32(0);
                        menuItem.Name = reader.GetString(1);
                        menuItem.Price = (decimal)reader.GetValue(2);

                        menuList.Add(menuItem);


                    }


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sqlConnection.Close();
                }
                return menuList;

            }

        internal List<MenuItem> GetMenuItemByID(string con, int id)
        {
            List<MenuItem> menuList = new List<MenuItem>();

            SqlConnection sqlConnection = new SqlConnection(con);

            string querry = "select * from MenuItem where Id="+id;

            SqlCommand cmd = new SqlCommand(querry, sqlConnection);

            try
            {
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                MenuItem menuItem = null;
                while (reader.Read())
                {
                    menuItem = new MenuItem();
                    menuItem.Id = reader.GetInt32(0);
                    menuItem.Name = reader.GetString(1);
                    menuItem.Price = (decimal)reader.GetValue(2);

                    menuList.Add(menuItem);


                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }
            return menuList;

        }

        public bool UpdateMenu(string con, int id, MenuItem menuItem)
            {
                using (SqlConnection connection = new SqlConnection(con))
                {

                    string sql = $"Update MenuItem SET Name='{menuItem.Name}', Price='{menuItem.Price}' Where Id='{id}'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }

                return true;
            }


        }
    }

