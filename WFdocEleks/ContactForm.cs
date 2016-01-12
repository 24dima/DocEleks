using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using WFdocEleks.DocEleksService;

namespace WFdocEleks
{
    public partial class ContactForm : Form
    {
        //Властивіть для отримання Id поточного користувача
      public short GetUserId { get; set; }

        public ContactForm()
        {
            InitializeComponent();
        }

        //Конструктор з параметром
        public ContactForm(User user)
        {
            GetUserId = user.UserId;
            InitializeComponent();
            lblWelcome.Text = "Вітаємо вас " + user.UserName + "!";
           DisplayContacts();
            
        }
        //Метод для відображення  контактів поточного користувача
        public void DisplayContacts()
        {
            dataGridViewContacts.DataSource = null;
            dataGridViewContacts.Refresh();
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
            List<Contact> listContacts = new List<Contact>();
            foreach (Contact contact in service.GetAllContacts(GetUserId))
            {
                listContacts.Add(contact);
            }

            dataGridViewContacts.DataSource = listContacts;

        }

       
        //Метод для оновлення DataGridView
       public void RefreshGrid()
        {
           this.dataGridViewContacts.DataSource = null;
           this.dataGridViewContacts.Refresh();
        }

      
        //Пошук контакту за номером телефону
        private void txtFind_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //ініціалізація текстових полів значеннями з dataGridViewContacts
        private void dataGridViewContacts_MouseClick_1(object sender, MouseEventArgs e)
        {
            txtFirstName.Text = dataGridViewContacts.SelectedRows[0].Cells[0].Value.ToString();
            txtLastName.Text = dataGridViewContacts.SelectedRows[0].Cells[1].Value.ToString();
            txtMiddleName.Text = dataGridViewContacts.SelectedRows[0].Cells[2].Value.ToString();
            txtPhoneNum.Text = dataGridViewContacts.SelectedRows[0].Cells[3].Value.ToString();
            txtAdress.Text = dataGridViewContacts.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            //Видалення запису з БД
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
            if (service.DeleteContact((short)dataGridViewContacts.SelectedRows[0].Cells[5].Value) == 1)
            {
                dataGridViewContacts.Refresh();
                MessageBox.Show("Запис успішно видалено!");
                //Очищуємо текстові поля від поточних даних 
                txtFirstName.Clear();
                txtLastName.Clear();
                txtMiddleName.Clear();
                txtPhoneNum.Clear();
                txtAdress.Clear();
                DisplayContacts();
                
            }

        }

        private void btnAddContact_Click_1(object sender, EventArgs e)
        {
            //Додати новий запис в БД
            AddConForm addCF = new AddConForm(this, GetUserId);
            addCF.Show();
        }

        //Подія оновлення запису в БД
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
           
         
                Contact contact = new Contact();
                contact.FirstN = txtFirstName.Text;
                contact.LastN = txtLastName.Text;
                contact.MiddleN = txtMiddleName.Text;
                contact.PhoneNum = txtPhoneNum.Text;
                contact.Adress = txtAdress.Text;
                contact.UserId = GetUserId;
            contact.ContactId = (short) dataGridViewContacts.SelectedRows[0].Cells[5].Value;

                if (service.UpdateContact(contact) == 1)
                {
                    MessageBox.Show("Запис успішно змінено!");

                    RefreshGrid();
                    DisplayContacts();
                }

            
           
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
          
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
            List<Contact> listContacts = new List<Contact>();
            string numberPhone= "%" + txtFind.Text + "%";
            dataGridViewContacts.DataSource = service.FindContacts(GetUserId, numberPhone);
          
        }

       
    }
}
