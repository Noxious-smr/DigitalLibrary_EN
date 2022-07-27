using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.DataAccess
{
    public class DBCopyService : IRepositoryService<Copy>
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlDataReader sqldataReader;

        public IEnumerable<Copy> GetAllItems()
        {
            var CopyList = new List<Copy>();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Copy", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    CopyList.Add(new Copy(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetInt32(2), sqldataReader.GetBoolean(3), sqldataReader.GetString(4)));
                }
                return CopyList;
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

        public Copy GetById(int id)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Copy WHERE Copy_ID = {id}", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Copy(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetInt32(2), sqldataReader.GetBoolean(3), sqldataReader.GetString(4));
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
        public Copy GetByUniqueID(string uniqueId)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Copy WHERE UniqueID = '{uniqueId}'", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Copy(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetInt32(2), sqldataReader.GetBoolean(3), sqldataReader.GetString(4));
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
        public void InsertItem(Copy item)
        {
            var isRented = item.IsRented ? 1 : 0;
            string SqlQuery = $@"INSERT INTO Copy Values('{item.UniqueID}', {item.Book_ID}, {isRented}, '{item.State}')";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show($"Database Error Copy : {e.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateItem(Copy item)
        {
            string SqlQuery = $@"UPDATE Copy SET Book_ID='{item.Book_ID}', IsRented = '{item.IsRented}', State='{item.State}' WHERE Copy_ID={item.ID}";
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
            string SqlQuery = $@"UPDATE Copy SET {DBProperty}='{value}' WHERE Copy_ID={id}";
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
            string SqlQuery = $@"DELETE FROM Copy WHERE Copy_ID={id}";
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

        public Copy GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int GetItemIDByName(string name)
        {
            throw new NotImplementedException();
        }

        public int GetItemIDByName(string name, int ID)
        {
            throw new NotImplementedException();
        }

        public Copy GetItemByNameAndParentID(string name, int ID)
        {
            throw new NotImplementedException();
        }        
    }
}
