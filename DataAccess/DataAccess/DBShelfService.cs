using DomainLayer.Abstractions;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.DataAccess
{
    public class DBShelfService : IRepositoryService<Shelf>
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlDataReader sqldataReader;

        public IEnumerable<Shelf> GetAllItems()
        {
            var ShelfList = new List<Shelf>();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Shelf", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    ShelfList.Add(new Shelf(sqldataReader.GetInt32(0), sqldataReader.GetInt32(1), sqldataReader.GetString(2)));
                }
                return ShelfList;
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

        public Shelf GetById(int id)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Shelf WHERE Shelf_ID = {id}", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Shelf(sqldataReader.GetInt32(0), sqldataReader.GetInt32(1), sqldataReader.GetString(2));
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
        public Shelf GetByName(string name)
        {
            return null;
        }

        public int GetItemIDByName(string name)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT Shelf_ID FROM Shelf WHERE Shelf_Nr = '{name}'", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return sqldataReader.GetInt32(0);
                }
                else
                    return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }
        public int GetItemIDByName(string name, int ID)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT Shelf_ID FROM Shelf WHERE Shelf_Nr = '{name}' and Cabinet_ID = {ID}", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return sqldataReader.GetInt32(0);
                }
                else
                    return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error: {e.Message}");
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }
        public Shelf GetItemByNameAndParentID(string name, int ID)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Shelf WHERE Shelf_Nr = '{name}' and Cabinet_ID = {ID}", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Shelf(sqldataReader.GetInt32(0),  sqldataReader.GetInt32(1),sqldataReader.GetString(2));
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
        public void InsertItem(Shelf item)
        {
            string SqlQuery = $@"INSERT INTO Shelf Values({item.Cabinet_ID}, '{item.Shelf_Nr}')";
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
        public void UpdateItem(Shelf item)
        {
            string SqlQuery = $@"UPDATE Shelf SET Shelf_Nr = '{item.Shelf_Nr}' WHERE Shelf_ID={item.ID}";
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
            string SqlQuery = $@"DELETE FROM Shelf WHERE SHelf_ID={id}";
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

        public Shelf GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
