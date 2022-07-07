using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Core.Services.Interfaces;
using Contacts.DataLayer.Context;
using Contacts.DataLayer.Entities;
using Contacts.DataLayer.Enums;

namespace Contacts.Core.Services
{
    public class ContactService : IContactService
    {
        public bool Create(string name, string family, string mobile, string phone, string email, string description, int? age, ContactType type)
        {
            try
            {
                int _age = 0;
                if (age != null)
                    _age = age.Value;
                using (var context = new DatabaseContext())
                {
                    Contact contact = new Contact()
                    {
                        Name = name,
                        Family = family,
                        Mobile = mobile,
                        Phone =   phone,
                        Email = email,
                        Age = _age,
                        Type =  type,
                        Description = description,
                        CreatedAt = DateTime.Now
                    };
                    context.Contacts.Add(contact);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var contact = FindById(id);
                    context.Entry(contact).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Contact FindById(int id)
        {
            using (var context = new DatabaseContext())
                return context.Contacts.Find(id);
        }

        public IEnumerable<Contact> GetContacts(string p = null, ContactType? type = null)
        {

            using (var context = new DatabaseContext())
            {
                IEnumerable<Contact> contacts = context.Contacts;
                if (!string.IsNullOrEmpty(p))
                    contacts = contacts.Where(w => w.Name.Contains(p)
                                                   || w.Family.Contains(p)
                                                                      || w.Mobile.Contains(p) || w.Phone.Contains(p) ||
                                                                      w.Email.Contains(p));
                if (type != null)
                    contacts = contacts.Where(w => w.Type == type);
                return contacts.OrderByDescending(o => o.CreatedAt).ToList();
            }

        }

        public bool Update(int id, string name, string family, string mobile, string phone, string email, string description, int? age, ContactType type)
        {
            try
            {
                int _age = 0;
                if (age != null)
                    _age = age.Value;
                using (var context = new DatabaseContext())
                {
                    Contact contact = FindById(id);
                    contact.Name = name;
                    contact.Family = family;
                    contact.Mobile = mobile;
                    contact.Phone = phone;
                    contact.Email = email;
                    contact.Age = _age;
                    contact.Type = type;
                    contact.Description = description;
                    contact.CreatedAt = DateTime.Now;
                    context.Entry(contact).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
