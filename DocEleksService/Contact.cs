using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DocEleksService
{
     [DataContract]
    public class Contact
    {
         [DataMember]
         public short ContactId { get; set; }
          [DataMember]
         public short UserId { get; set; }

          [DataMember]
          public string FirstN { get; set; }
           [DataMember]
          public string LastN { get; set; }
           [DataMember]
           public string MiddleN { get; set; }
           [DataMember]
           public string PhoneNum { get; set; }
           [DataMember]
           public string Adress { get; set; }


    }
}