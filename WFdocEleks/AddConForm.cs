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
    public partial class AddConForm : Form
    {

        public short GetUserId { get; set; }
        public ContactForm _contactForm;

        public AddConForm(ContactForm contactForm, short getUserID)
        {
            _contactForm = contactForm;
            InitializeComponent();
            GetUserId = getUserID;
            this.AcceptButton = btnAddCont;
        }

        //Обробник для додавання в базу нововго контакту
        private void btnAddCont_Click(object sender, EventArgs e)
        {
            ServiceDocEleksDBClient service = new ServiceDocEleksDBClient();
              //Провіряємо чи є в базі контакт з введеним номером, поточного користувача
                    if (service.FindPhoneNumber(GetUserId,txtPhoneNum.Text) ==1)
                    {
                        MessageBox.Show("Даний номер належить іншому контакту, введіть інший номер!", "Message", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        txtPhoneNum.Clear();
                        txtPhoneNum.Focus();
                        
                    }
                    else
                    {
                        Contact contact = new Contact();
                        contact.FirstN = txtFirstName.Text;
                        contact.LastN = txtLastName.Text;
                        contact.MiddleN = txtMiddleName.Text;
                        contact.PhoneNum = txtPhoneNum.Text;
                        contact.Adress = txtAdress.Text;
                        contact.UserId = GetUserId;

                        if (service.InsertContact(contact) == 1)
                        {
                            MessageBox.Show("Запис успішно додано!");
                            txtPhoneNum.Clear();
                            txtAdress.Clear();
                            txtFirstName.Clear();
                            txtLastName.Clear();
                            txtMiddleName.Clear();

                            _contactForm.RefreshGrid();
                            _contactForm.DisplayContacts();
                        }
                        
                    }
           
                  
                        
         
        } 

       
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

       

      
    }
}