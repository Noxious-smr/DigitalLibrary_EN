using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.DataAccess
{
    public class DBCategoryService : IRepositoryService<Category>
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlDataReader sqldataReader;

        public IEnumerable<Category> GetAllItems()
        {
            var CategoryList = new List<Category>();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Category", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    CategoryList.Add(new Category(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetString(2)));
                }
                return CategoryList;
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

        public Category GetById(int id)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Category WHERE Category_ID = {id}", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Category(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetString(2));
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
        public Category GetByName(string name)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Category WHERE Category_Name = '{name}'", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Category(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetString(2));
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

        public Category GetItemByNameAndParentID(string name, int ID)
        {
            throw new NotImplementedException();
        }
        public void InsertItem(Category item)
        {
            string SqlQuery = $@"INSERT INTO Category Values('{item.Category_Name}', '{item.ImagePath}')";
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
        public void UpdateItem(Category item)
        {
            string SqlQuery = $@"UPDATE Category SET Category_Name='{item.Category_Name}', Image_Path = '{item.ImagePath}' WHERE Category_ID={item.Category_ID}";
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
            string SqlQuery = $@"UPDATE Category SET {DBProperty}='{value}' WHERE Category_ID={id}";
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
            string SqlQuery = $@"DELETE FROM Category WHERE Category_ID={id}";
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

        public Category GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }        
    }
}
