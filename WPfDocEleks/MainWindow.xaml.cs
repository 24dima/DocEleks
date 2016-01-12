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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPfDocEleks.ServiceDocEleks;
namespace WPfDocEleks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogIn_Copy1_Click(object sender, RoutedEventArgs e)
        {
            //Провіряємо чи користувач ввів логін 
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblStatusError.Content = "Будь-ласка введіть ваше ім'я!";
               txtName.Focus();
                return;
                
            }
            //Провіряємо чи користувач ввів пароль
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                lblStatusError.Content = "Будь-ласка введіть ваш пароль!";
                txtPass.Focus();
                return;

            }
            try
            {
                ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
                    //Провіряємо чи є користувач в базі з таким логіном і паролем

                User user = new User();
                user.UserName = txtName.Text;
                user.UserPass = txtPass.Text;
                
                    if (service.ValidUser(user) == 1)
                    {
                      user.UserId = service.GetUserId(user);
                      ContactsWindow contactForm = new ContactsWindow(user);
                      contactForm.Show();
                           
                    }
                   
                   else
                    {
                        //Провіряємо чи є в користувач з таким логіном, якщо є виводимо повідомлення що користувач ввів не дійсний пароль
                        if (service.ValidUserName(user) == 1)
                        {
                            lblStatusError.Content = "Ви ввели невірний пароль!";
                            txtPass.Focus();
                        }
                        else
                        {
                            lblStatusError.Content = "Користувача з таким іменем немає в базі!";
                            txtName.Focus();
                        }
                    
                    }
                    
                }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        }
    }

