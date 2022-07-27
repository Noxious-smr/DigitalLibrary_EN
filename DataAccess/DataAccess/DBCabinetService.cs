using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.DataAccess
{
    public class DBCabinetService : IRepositoryService<Cabinet>
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlDataReader sqldataReader;

        public IEnumerable<Cabinet> GetAllItems()
        {
            var CabinetList = new List<Cabinet>();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Cabinet", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    CabinetList.Add(new Cabinet(sqldataReader.GetInt32(0), sqldataReader.GetString(1)));
                }
                return CabinetList;
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

        public Cabinet GetById(int id)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Cabinet WHERE Cabinet_ID = {id}", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Cabinet(sqldataReader.GetInt32(0), sqldataReader.GetString(1));
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
        public Cabinet GetByName(string name)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Cabinet WHERE Cabinet_Name = '{name}'", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Cabinet(sqldataReader.GetInt32(0),sqldataReader.GetString(1));
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
            SqlCommand cmd = new SqlCommand(@$"SELECT Cabinet_ID FROM Cabinet WHERE Cabinet_Name = '{name}'", conn);
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
        public void InsertItem(Cabinet item)
        {
            string SqlQuery = $@"INSERT INTO Cabinet Values('{item.Name}')";
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
        public void UpdateItem(Cabinet item)
        {
            string SqlQuery = $@"UPDATE Cabinet SET Cabinet_Name='{item.Name}' WHERE Cabinet_ID={item.ID}";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void DeleteItem(int id)
        {
            string SqlQuery = $@"DELETE FROM Cabinet WHERE Cabinet_ID={id}";
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

        public int GetItemIDByName(string name, int ID)
        {
            throw new NotImplementedException();
        }

        public Cabinet GetItemByNameAndParentID(string name, int ID)
        {
            throw new NotImplementedException();
        }

        public Cabinet GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
