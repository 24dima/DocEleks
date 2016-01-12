using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DocEleksService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceDocEleksDB" in both code and config file together.
    [ServiceContract]
    public interface IServiceDocEleksDB
    {
        [OperationContract]
        int InsertUser(User user);
        
        [OperationContract]
        int ValidUser(User user);

        [OperationContract]
        int ValidUserName(User user);

        [OperationContract]
        int InsertContact(Contact contact);
               [OperationContract]
        int DeleteContact(short contactId);
               [OperationContract]
        int UpdateContact(Contact contact);

               [OperationContract]

        List<Contact> GetAllContacts(short userId);

               [OperationContract]
        DataTable FindContacts(short userId, string phoneNum);


        [OperationContract]
        short GetUserId(User user);

        [OperationContract]
        //Провіряємо чи є в БД контакт з введеним номером
        int FindPhoneNumber(short userId, string phoneNumer);
    }
}
