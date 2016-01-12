using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFdocEleks.DocEleksService;

namespace WFdocEleks
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            lblStatusError.Text = "";
            //Задаємо параметр AcceptButton, щоб при натиску на Enter виконувалася подія btnLogin
            this.AcceptButton = btnLogin;
        }

        //Обробник входу в систему
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Провіряємо чи користувач ввів логін 
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                lblStatusError.Text = "Будь-ласка введіть ваше ім'я!";
               txtUserName.Focus();
                return;
                
            }
            //Провіряємо чи користувач ввів пароль
            if (string.IsNullOrEmpty(txtUserPass.Text))
            {
                lblStatusError.Text = "Будь-ласка введіть ваш пароль!";
                txtUserPass.Focus();
                return;

            }
            try
            {
                ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
                    //Провіряємо чи є користувач в базі з таким логіном і паролем

                User user = new User();
                user.UserName = txtUserName.Text;
                user.UserPass = txtUserPass.Text;
                
                    if (service.ValidUser(user) == 1)
                    {
                      user.UserId = service.GetUserId(user);
                      ContactForm contactForm = new ContactForm(user);
                      contactForm.Show();
                           
                    }
                   
                   else
                    {
                        //Провіряємо чи є в користувач з таким логіном, якщо є виводимо повідомлення що користувач ввів не дійсний пароль
                        if (service.ValidUserName(user) == 1)
                        {
                            lblStatusError.Text = "Ви ввели невірний пароль!";
                            txtUserPass.Focus();
                        }
                        else
                        {
                            lblStatusError.Text = "Користувача з таким іменем немає в базі!";
                            txtUserName.Focus();
                        }
                    
                    }
                    
                }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)20)
            {
                txtUserName.Focus();
            }
        }

        private void txtUserPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)16)
            {
                btnLogin.PerformClick();
            }
        }

       
    }
}

