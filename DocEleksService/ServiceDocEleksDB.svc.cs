using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DocEleksService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceDocEleksDB" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceDocEleksDB.svc or ServiceDocEleksDB.svc.cs at the Solution Explorer and start debugging.

    public class ServiceDocEleksDB : IServiceDocEleksDB
    {
        public ServiceDocEleksDB()
        {
            ConnectToDocEleksDB();
        }
         
         SqlCommand comm;
      
         SqlConnection conn;

        //Метод для підключеня до БД
        void ConnectToDocEleksDB()
        {
         
            conn = new SqlConnection("Data Source=24DIMA-PC;Initial Catalog=DocEleksDB;Integrated Security=True");
            comm = conn.CreateCommand();
        }

   
        //Провіряємо чи є користувач в базі з введенним іменем
        public int ValidUserName(User user)
        {
            try
            {
                comm.CommandText = "SELECT * FROM Users WHERE UserName = @UserName";
                comm.Parameters.AddWithValue("UserName", user.UserName);
                comm.Parameters.AddWithValue("UserPass", user.UserPass);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    if (user.UserName == reader[1].ToString())
                    {
                        return 1;
                    }

                }

                return 0;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        //Додаємо новий контакт в БД
        public int InsertContact(Contact contact)
        {
          try
            {
                comm.CommandText = "INSERT INTO Contacts(UserId,FirstN,LastN,MiddleN,PhoneNum,Adress) VALUES(@UserId,@FirstN,@LastN,@MiddleN,@PhoneNum,@Adress)";
                comm.Parameters.AddWithValue("UserId", contact.UserId);
                comm.Parameters.AddWithValue("FirstN", contact.FirstN);
                comm.Parameters.AddWithValue("LastN", contact.LastN);
                comm.Parameters.AddWithValue("MiddleN", contact.MiddleN);
                comm.Parameters.AddWithValue("PhoneNum", contact.PhoneNum);
                comm.Parameters.AddWithValue("Adress", contact.Adress);
                comm.CommandType = CommandType.Text;
                conn.Open();
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        
        }

        //Видаляємо контакт з БД
        public int DeleteContact(short contactId)
        {
            try
            {
                comm.CommandText = "DELETE Contacts WHERE ContactId = @ContactId";
                comm.Parameters.AddWithValue("ContactId", contactId);
                comm.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                return 1;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        //Метод для зміни записів в БД
        public int UpdateContact(Contact contact)
        {
            try
            {
                comm.CommandText = "UPDATE Contacts SET UserId = @UserId,FirstN = @FirstN,LastN = @LastN, MiddleN = @MiddleN,PhoneNum = @PhoneNum, Adress = Adress WHERE ContactId = @ContactId";
                comm.Parameters.AddWithValue("ContactId", contact.ContactId);
                comm.Parameters.AddWithValue("UserId", contact.UserId);
                comm.Parameters.AddWithValue("FirstN", contact.FirstN);
                comm.Parameters.AddWithValue("LastN", contact.LastN);
                comm.Parameters.AddWithValue("MiddleN", contact.MiddleN);
                comm.Parameters.AddWithValue("PhoneNum", contact.PhoneNum);
                comm.Parameters.AddWithValue("Adress", contact.Adress);
                comm.CommandType = CommandType.Text;
                conn.Open();
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //Метод для отримання всіх контактів поточного користувача
        public List<Contact> GetAllContacts(short userId)
        {
            List<Contact> userContacts = new List<Contact>();
            
            try
            {
                comm.CommandText = "SELECT * FROM Contacts  WHERE UserId = @UserId";
                comm.Parameters.AddWithValue("UserId", userId);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    Contact contact = new Contact();
                    contact.ContactId = (short) reader[0];
                    contact.UserId = (short)reader[1];
                    contact.FirstN = reader[2].ToString();
                    contact.LastN = reader[3].ToString();
                    contact.MiddleN = reader[4].ToString();
                    contact.PhoneNum = reader[5].ToString();
                    contact.Adress = reader[6].ToString();

                    userContacts.Add(contact);
                    
                }
                

                return userContacts;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        //Метод для отримання ID користувача
        public short GetUserId(User user)
        {
            try
            {
                comm.CommandText = "SELECT * FROM Users WHERE UserName = @UserName AND UserPass = @UserPass";
                comm.Parameters.AddWithValue("UserName", user.UserName);
                comm.Parameters.AddWithValue("UserPass", user.UserPass);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    if (user.UserName == reader[1].ToString() && user.UserPass == reader[2].ToString())
                    {
                        return user.UserId = (short)reader["UserId"];
                    }

                }

                return 0;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        //Провіряємо чи є в БД користувач з введеним іменем і паролем
        public int ValidUser(User user)
        {
            try
            {
                comm.CommandText = "SELECT * FROM Users WHERE UserName = @UserName AND UserPass = @UserPass";
                comm.Parameters.AddWithValue("UserName", user.UserName);
                comm.Parameters.AddWithValue("UserPass", user.UserPass);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    if (user.UserName == reader[1].ToString() && user.UserPass == reader[2].ToString())
                    {
                        return 1;
                    }
                   
                }

                return 0;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        //Метод для вставки нового користувача
        public int InsertUser(User user)
        {
            try
            {
                comm.CommandText = "INSERT INTO Users(UserName,UserPass) VALUES(@UserName, @UserPass)";
                comm.Parameters.AddWithValue("UserName", user.UserName);
                comm.Parameters.AddWithValue("UserPass", user.UserPass);
                comm.CommandType = CommandType.Text;
                conn.Open();
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        //Провіряємо чи є в базі контакт з введеним номером
        public int FindPhoneNumber(short userId, string phoneNumer)
        {
            try
            {
                comm.CommandText = "SELECT * FROM Contacts WHERE UserId = 2 AND PhoneNum = @PhoneNum";
                comm.Parameters.AddWithValue("UserId", userId);
                comm.Parameters.AddWithValue("PhoneNum", phoneNumer);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    if (userId.ToString() == reader[1].ToString() && phoneNumer == reader[5].ToString())
                    {
                        return 1;
                    }

                }

                return 0;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        //Пошук контакту за номером
        public DataTable FindContacts(short userId, string phoneNum)
        {
            
            try
            {
                conn.Open();
                
                SqlCommand selectContact = new SqlCommand("SELECT * FROM Contacts WHERE UserId =" + userId + " AND PhoneNum LIKE'%" + phoneNum + "%'", conn);
               
                DataSet docEleksDB = new DataSet("DocEleksDB");

                SqlDataAdapter adapterDocEleks = new SqlDataAdapter();
                adapterDocEleks.SelectCommand = selectContact;
                adapterDocEleks.Fill(docEleksDB, "Contacts");
                return docEleksDB.Tables["Contacts"];
                
                

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }
    }
}
