using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DocEleksService
{
    [DataContract]
    public class User
    {
       [DataMember]

        public short UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserPass { get; set; }
    }
}