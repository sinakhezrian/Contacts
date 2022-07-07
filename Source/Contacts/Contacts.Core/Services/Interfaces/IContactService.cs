using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.DataLayer.Entities;

namespace Contacts.Core.Services.Interfaces
{
    public interface IContactService
    {
        bool Create(string name, string family, string mobile, string phone, string email, string description, int? age,
            DataLayer.Enums.ContactType type);
        bool Update(int id, string name, string family, string mobile, string phone, string email, string description, int? age,
            DataLayer.Enums.ContactType type);
        bool Delete(int id);
        IEnumerable<Contact> GetContacts(string p = null, DataLayer.Enums.ContactType? type = null);
        Contact FindById(int id);
    }
}
