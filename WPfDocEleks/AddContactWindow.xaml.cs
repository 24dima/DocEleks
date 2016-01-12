using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for AddContactWindow.xaml
    /// </summary>
    public partial class AddContactWindow : Window
    {
        public short GetUserId { get; set; }
        public ContactsWindow _contactForm;
        public AddContactWindow()
        {
            InitializeComponent();
        }

        public AddContactWindow(ContactsWindow contactsWindow, short getUserID)
        {
            _contactForm = contactsWindow;
            InitializeComponent();
            GetUserId = getUserID;
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
            //Провіряємо чи є в базі контакт з введеним номером, поточного користувача
            if (service.FindPhoneNumber(GetUserId, txtPhoneNum.Text) == 1)
            {
                MessageBox.Show("Даний номер належить іншому контакту, введіть інший номер!", "Message", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                txtPhoneNum.Clear();
                txtPhoneNum.Focus();

            }
            else
            {
                Contact contact = new Contact();
                contact.FirstN = txtFirsN.Text;
                contact.LastN = txtLastN.Text;
                contact.MiddleN = txtMiddleN.Text;
                contact.PhoneNum = txtPhoneNum.Text;
                contact.Adress = txtAdress.Text;
                contact.UserId = GetUserId;

                if (service.InsertContact(contact) == 1)
                {
                    MessageBox.Show("Запис успішно додано!");
                    txtPhoneNum.Clear();
                    txtAdress.Clear();
                    txtFirsN.Clear();
                    txtLastN.Clear();
                    txtAdress.Clear();
                    txtMiddleN.Clear();

                    _contactForm.RefreshGrid();
                    _contactForm.DisplayContacts();
                }

            }
           
        }
    }
}
