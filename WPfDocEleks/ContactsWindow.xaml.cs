using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPfDocEleks.ServiceDocEleks;

namespace WPfDocEleks
{
    /// <summary>
    /// Interaction logic for ContactsWindow.xaml
    /// </summary>
    public partial class ContactsWindow : Window
    {
        public short GetUserId { get; set; }
        public ContactsWindow(User user)
        {
            InitializeComponent();
            lblWelcome.Content = "Вітаємо вас " + user.UserName;
            GetUserId = user.UserId;
            DisplayContacts();
        }

        public void DisplayContacts()
        {
           // dataGridViewContacts.ItemsSource = null;
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
            List<Contact> listContacts = new List<Contact>();
            foreach (Contact contact in service.GetAllContacts(GetUserId))
            {
                listContacts.Add(contact);
            }

            dataGridViewContacts.ItemsSource = listContacts;
            

        }

        //Метод для оновлення DataGridView
        public void RefreshGrid()
        {
            this.dataGridViewContacts.ItemsSource = null;
            this.dataGridViewContacts.Items.Refresh();
        }

    
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void dataGridViewContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        private void btnDeleteContact_Click(object sender, RoutedEventArgs e)
        {
            //Видалення запису з БД
            Contact row = (Contact)dataGridViewContacts.SelectedItem;
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
            if (service.DeleteContact(row.ContactId) == 1)
            {
               
                MessageBox.Show("Запис успішно видалено!");
                //Очищуємо текстові поля від поточних даних 
                txtFirsN.Clear();
                txtLastN.Clear();
                txtMiddleN.Clear();
                txtPhoneNum.Clear();
                txtAdress.Clear();
              
              this.dataGridViewContacts.Items.Refresh();

            }
        }

        private void btnUpDateContact_Click(object sender, RoutedEventArgs e)
        {
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
            Contact row = (Contact)dataGridViewContacts.SelectedItem;

            Contact contact = new Contact();
            contact.FirstN = txtFirsN.Text;
            contact.LastN = txtLastN.Text;
            contact.MiddleN = txtMiddleN.Text;
            contact.PhoneNum = txtPhoneNum.Text;
            contact.Adress = txtAdress.Text;
            contact.UserId = GetUserId;
            contact.ContactId = row.ContactId;

            if (service.UpdateContact(contact) == 1)
            {
                this.dataGridViewContacts.Items.Refresh();
                MessageBox.Show("Запис успішно змінено!");
                
            }
        }

        private void txtAddContact_Click(object sender, RoutedEventArgs e)
        {
            AddContactWindow addContact = new AddContactWindow(this, GetUserId);
            addContact.Show();
        }

        private void dataGridViewContacts_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Contact row = (Contact)dataGridViewContacts.SelectedItem;
            txtFirsN.Text = row.FirstN;
            txtLastN.Text = row.LastN;
            txtMiddleN.Text = row.MiddleN;
            txtPhoneNum.Text = row.PhoneNum;
            txtAdress.Text = row.Adress;

            
        }
    }
}
