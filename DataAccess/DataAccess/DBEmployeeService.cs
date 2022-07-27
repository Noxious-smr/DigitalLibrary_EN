using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.DataAccess
{
    public class DBEmployeeService : IRepositoryService<Employee>
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlDataReader sqldataReader;
        public IEnumerable<Employee> GetAllItems()
        {
            var EmployeesList = new List<Employee>();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Employee WHERE ID != 6", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    EmployeesList.Add(new Employee(sqldataReader.GetInt32(0), sqldataReader.GetString(1), 
                                                        sqldataReader.GetString(2), sqldataReader.GetString(3), sqldataReader.GetDateTime(4)));
                }
                return EmployeesList;
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
        public void DeleteItem(int id)
        {
            string SqlQuery = $@"DELETE FROM Employee WHERE ID={id}";
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

        public void InsertItem(Employee item)
        {
            string SqlQuery = $@"INSERT INTO Employee Values('{item.UniqueID}', '{item.FirstName}', '{item.LastName}', '{item.Birthdate.ToString("yyyy-MM-dd")}')";
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

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Employee GetByUniqueID(string uniqueId)
        {
            SqlCommand cmd = new SqlCommand(@$"SELECT * FROM Employee WHERE UniqueID = '{uniqueId}'", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                if (sqldataReader.Read())
                {
                    return new Employee(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetString(2),
                                                      sqldataReader.GetString(3), sqldataReader.GetDateTime(4));
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
            };
        }
        public void UpdateItem(Employee item)
        {
            string SqlQuery = $@"UPDATE Employee SET UniqueID = '{item.UniqueID}', FirstName = '{item.FirstName}', 
                                LastName = '{item.LastName}', Birthdate = '{item.Birthdate.ToString("yyyy-MM-dd")}'
                                WHERE ID = {item.ID}";
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

        public Employee GetItemByNameAndParentID(string name, int ID)
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


        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
