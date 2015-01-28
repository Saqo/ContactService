using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace CService
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Patronymic { get; set; }

        [DataMember]
        public string Adress { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }
    }
}