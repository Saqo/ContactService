using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace CService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ContactsService : IContactsService
    {
        public Contact[] GetContacts()
        {
            List<Contact> allContacts = new List<Contact>();
            LinqContactDataContext dataContext = new LinqContactDataContext();
            bool isClone;
            Contact contactForChanging = null;
            var contacts = from c in dataContext.Contacts join t in dataContext.Telephones on c.ContactTelephoneID equals t.TelephoneID select new { c.ContactFirstName, c.ContactLastName, c.ContactPatronymic, c.ContactAdress, c.Telephones.TelephoneNumber };
            foreach (var contact in contacts)
            {
                isClone = false;

                if (allContacts.Count != 0)
                {
                    foreach (Contact c in allContacts)
                    {
                        if (c.Adress == contact.ContactAdress &&
                           c.FirstName == contact.ContactFirstName &&
                           c.LastName == contact.ContactLastName &&
                           c.Patronymic == contact.ContactPatronymic &&
                           c.TelephoneNumber == contact.TelephoneNumber)
                        {
                            isClone = true;
                            break;
                        }
                        else
                        {
                            if (c.Adress == contact.ContactAdress &&
                               c.FirstName == contact.ContactFirstName &&
                               c.LastName == contact.ContactLastName &&
                               c.Patronymic == contact.ContactPatronymic)
                            {
                                isClone = true;
                                contactForChanging = c;
                                break;
                            }
                        }
                    }
                    if (!isClone)
                    {
                        AddContact(allContacts, contact.ContactFirstName, contact.ContactLastName, contact.ContactPatronymic, contact.ContactAdress, contact.TelephoneNumber);
                    }
                    else
                    {
                        if (contactForChanging != null) contactForChanging.TelephoneNumber += ", " + contact.TelephoneNumber;
                    }
                }
                else
                {
                    AddContact(allContacts, contact.ContactFirstName, contact.ContactLastName, contact.ContactPatronymic, contact.ContactAdress, contact.TelephoneNumber);
                }
            }
            return allContacts.ToArray();
        }

        protected void AddContact(List<Contact> allContacts, string contactFirstName, string contactLastName, string contactPatronymic, string contactAdress, string contactTelephoneNumber)
        {
            allContacts.Add(new Contact
            {
                FirstName = contactFirstName,
                LastName = contactLastName,
                Patronymic = contactPatronymic,
                Adress = contactAdress,
                TelephoneNumber = contactTelephoneNumber
            });
        }
    }
}
