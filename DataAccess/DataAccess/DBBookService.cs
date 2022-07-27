using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.DataAccess
{
    public class DBBookService : IRepositoryService<Book>
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlDataReader sqldataReader;
        
        public IEnumerable<Book> GetAllItems()
        {
            var BookList = new List<Book>();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Book", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    BookList.Add(new Book(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetString(2),
                                          sqldataReader.GetString(3), sqldataReader.GetString(4), sqldataReader.GetInt32(5),
                                          sqldataReader.GetString(6), sqldataReader.GetDateTime(7), sqldataReader.GetString(8),
                                          sqldataReader.GetString(9), sqldataReader.GetString(10), sqldataReader.GetInt32(11), sqldataReader.GetInt32(12)));
                }
                return BookList;
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
                return null;
            }
            finally
            {
                conn.Close();
            }
            
        }

        public Book GetById(int id)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Book WHERE ID = {id}", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Book(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetString(2),
                                                      sqldataReader.GetString(3), sqldataReader.GetString(4), sqldataReader.GetInt32(5),
                                                      sqldataReader.GetString(6), sqldataReader.GetDateTime(7), sqldataReader.GetString(8),
                                                      sqldataReader.GetString(9), sqldataReader.GetString(10), sqldataReader.GetInt32(11), sqldataReader.GetInt32(12));
                }
                else
                    return null;
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public Book GetByName(string name)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Book WHERE Title = '{name}'", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Book(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetString(2),
                                                      sqldataReader.GetString(3), sqldataReader.GetString(4), sqldataReader.GetInt32(5),
                                                      sqldataReader.GetString(6), sqldataReader.GetDateTime(7), sqldataReader.GetString(8),
                                                      sqldataReader.GetString(9), sqldataReader.GetString(10), sqldataReader.GetInt32(11), sqldataReader.GetInt32(12));
                }
                else
                    return null;
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int GetItemIDByName(string name)
        {
            throw new NotImplementedException();
        }
        public int GetItemIDByName(string name, int ID)
        {
            throw new NotImplementedException();
        }
        public Book GetItemByNameAndParentID(string name, int ID)
        {
            throw new NotImplementedException();
        }
        public void InsertItem(Book item)
        {
            string SqlQuery = $@"INSERT INTO Book Values('{item.ISBN_10}','{item.ISBN_13}','{item.Title}','{item.ImagePath}',
                                                         '{item.PageCount}','{item.Publisher}','{item.Published.ToString("yyyy-MM-dd")}','{item.Author}',
                                                         '{item.Language}','{item.Description}','{item.CategoryID}','{item.ShelfID}')";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateItem(Book item)
        {
            string SqlQuery = $@"UPDATE Book SET ISBN_10='{item.ISBN_10}', ISBN_13='{item.ISBN_13}', Title ='{item.Title}', ImagePath='{item.ImagePath}',
                                                         PageCount={item.PageCount}, Publisher='{item.Publisher}', Published ='{item.Published.ToString("yyyy-M-d")}', Author ='{item.Author}',
                                                         Language='{item.Language}', Description='{item.Description}', Category_ID={item.CategoryID}, Shelf_ID={item.ShelfID}
                                 WHERE ID={item.ID}";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateItem(int id, string DBProperty, object value)
        {
            string SqlQuery = $@"UPDATE Book SET {DBProperty}='{value}' WHERE ID={id}";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteItem(int id)
        {
            string SqlQuery = $@"DELETE FROM Book WHERE ID={id}";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        public Book GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        
    }
}
