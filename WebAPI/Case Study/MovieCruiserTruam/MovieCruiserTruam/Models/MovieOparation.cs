using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace MovieCruiserTruam.Models
{
    public class MovieOparation
    {
        public static string sqlDataSource = "Server=(localdb)\\mssqllocaldb;Database=Movie;Trusted_Connection=True;MultipleActiveResultSets=true;";
        private static SqlDataReader reader;

        public static IEnumerable<Movie> GetMovieLIst(string constr)
        {
            List<Movie> menuList = new List<Movie>();

            SqlConnection sqlConnection = new SqlConnection(constr);

            string querry = "select * from Movie";

            SqlCommand cmd = new SqlCommand(querry, sqlConnection);

            try
            {
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                Movie movieItem = null;
                while (reader.Read())
                {
                    movieItem = new Movie();
                    movieItem.Id = reader.GetInt32(0);
                    movieItem.Title = reader.GetString(1);
                    movieItem.BoxOffice = reader.GetString(2);
                    movieItem.Active=reader.GetBoolean(3);
                    movieItem.DateOfLaunch=reader.GetDateTime(4);
                    movieItem.HasTeaser=reader.GetBoolean(5);
                    movieItem.GenreId=reader.GetInt32(6);
                    movieItem.GenreType=reader.GetString(7);



                    menuList.Add(movieItem);


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

       

        public static bool Update(int id, Movie movie)
        {
            var list = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                SqlCommand cmd = new SqlCommand("Update Movie set Id=@Id,Title=@Title,BoxOffice=@BoxOffice,Active=@Active,DateOfLaunch=@DateOfLaunch,HasTeaser=@HasTeaser,GenreId=@GenreId,GenreType=@GenreType, where Id=id", con);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@BoxOffice", movie.BoxOffice);
                cmd.Parameters.AddWithValue("@Active", movie.Active);
                cmd.Parameters.AddWithValue("@DateOfLaunch", movie.DateOfLaunch);
                cmd.Parameters.AddWithValue("@HasTeaser", movie.HasTeaser);
                cmd.Parameters.AddWithValue("@GenreId", movie.GenreId);
                cmd.Parameters.AddWithValue("@GenreType", movie.GenreType);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
        }
        public static void Insert(UserDetails user)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO UserDetails(Id,UserName,FirstName,LastName,Password,ConfirmPassword) Values (@Id,@UserName,@FirstName,@LastName,@Password,@ConfirmPassword)";
            sqlCmd.Connection = con;


            sqlCmd.Parameters.AddWithValue("@Id", user.Id);
            sqlCmd.Parameters.AddWithValue("@UserName", user.UserName);
            sqlCmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", user.LastName);
            sqlCmd.Parameters.AddWithValue("@Password", user.Password);
            sqlCmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();

        }
        public static List<UserDetails> UserList()
        {
            List<UserDetails> users = new List<UserDetails>();

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from UserDetails";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        users.Add(new UserDetails
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            UserName = rd["UserName"].ToString(),
                            FirstName = rd["FirstName"].ToString(),
                            LastName = rd["LastName"].ToString(),
                            Password = rd["Password"].ToString(),
                            ConfirmPassword = rd["ConfirmPassword"].ToString()

                        });
                    }
                    con.Close();
                }

            }
            return users;


        }
        public static void InsertIntoFavorites(List<Favarite> favorite)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Favorites(Id,MovieListId,UserId) Values (@Id,@MovieListId,@UserId)";
            sqlCmd.Connection = con;
            con.Open();
            foreach (var i in favorite)
            {
                sqlCmd.Parameters.AddWithValue("@Id", i.Id);
                sqlCmd.Parameters.AddWithValue("@MovieListId", i.MovieListId);
                sqlCmd.Parameters.AddWithValue("@UserId", i.UserId);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Parameters.Clear();
            }

            con.Close();
        }
        public static List<Movie> FavoriteList(int userid, ref int rowCount)
        {

            List<Movie> Items = new List<Movie>();
            List<int> list = new List<int>();

            var l = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };


            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select Movie from Favorites where Userid = @userid";
                    cmd.Parameters.AddWithValue("@userid", userid);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        list.Add(Convert.ToInt32(rd["MovieListId"]));
                    }
                    rd.Close();
                    foreach (var i in list)
                    {

                        cmd.CommandText = "select * from MovieList where Id = @i";
                        cmd.Parameters.AddWithValue("@i", i);
                        SqlDataReader rd1 = cmd.ExecuteReader();
                        while (rd1.Read())
                        {
                            Items.Add(new Movie
                            {
                                Id = Convert.ToInt32(rd1["Id"]),
                                Title = rd1["Title"].ToString(),
                                BoxOffice = rd1["BoxOffice"].ToString(),
                                Active = Convert.ToBoolean(rd1["Active"]),
                                DateOfLaunch = Convert.ToDateTime(rd1["DateOfLaunch"]),
                                HasTeaser = Convert.ToBoolean(rd1["HasTeaser"]),
                                GenreId = Convert.ToInt32(rd1["GenreId"]),
                                GenreType = rd1["GenreType"].ToString()
                            });
                            rowCount += Convert.ToInt32(rd1["Id"]);

                        }
                        cmd.Parameters.Clear();
                        rd1.Close();
                    }
                    con.Close();

                }

            }
            return Items;

        }
        public static string Delete(int favId)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from Favorites where Id =@favId";
            sqlCmd.Connection = con;
            con.Open();
            sqlCmd.Parameters.AddWithValue("@favId", favId);
            int i = sqlCmd.ExecuteNonQuery();
            if (i >= 1)
                return "record deleted";
            else
                return "no record";

        }
    }
}