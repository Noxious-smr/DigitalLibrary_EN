using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.DataAccess
{
    public class DBBorrowService : IRepositoryService<Borrow>
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private SqlDataReader sqldataReader;
        public IEnumerable<Borrow> GetAllItems()
        {
            var BorrowList = new List<Borrow>();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM BorrowCopy", conn);
            try
            {
                conn.Open();
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    BorrowList.Add(new Borrow(sqldataReader.GetInt32(0), sqldataReader.GetString(1), sqldataReader.GetInt32(2),
                        sqldataReader.GetString(3), sqldataReader.GetDateTime(4),
                        sqldataReader.GetDateTime(5), sqldataReader.GetDateTime(6),
                        sqldataReader.GetInt32(7),sqldataReader.GetString(8), sqldataReader.GetInt32(9), 
                        sqldataReader.GetString(10), sqldataReader.GetString(11)));
                }
                return BorrowList;
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
        public void InsertItem(Borrow item)
        {
            //int borrow_ID, string uniqueID, int copy_ID, string clientName, DateTime borrowDate, DateTime returnDate, 
            //    DateTime actualReturnDate, int givenByEmployee, string givenBy, int returnedByEmployee, string returnedBy, string copy_Identifier
            string SqlQuery = $@"INSERT INTO Borrow Values('{item.UniqueID}', '{item.Copy_ID}', '{item.ClientName}', '{item.BorrowDate:yyyy-MM-dd}', '{item.ReturnDate:yyyy-MM-dd}',
                                '{item.ActualReturnDate:yyyy-MM-dd}', '{item.GivenByEmployee}', '{item.ReturnedByEmployee}')";
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

        public void UpdateItem(Borrow item)
        {
            string SqlQuery = $@"UPDATE Borrow SET ActualReturnDate='{DateTime.Now:yyyy-MM-dd}', ReturnedByEmployee = '{item.ReturnedByEmployee}' 
                                WHERE Borrow_ID={item.Borrow_ID}";
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
        public Borrow GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Borrow GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Borrow GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Borrow GetItemByNameAndParentID(string name, int ID)
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

        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
