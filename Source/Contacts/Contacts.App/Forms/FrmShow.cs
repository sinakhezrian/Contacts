using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contacts.Core.Services;
using Contacts.Core.Services.Interfaces;

namespace Contacts.App.Forms
{
    public partial class FrmShow : Form
    {
        public int ContactId = 0;
        private IContactService _contactService;
        public FrmShow()
        {
            InitializeComponent();
            _contactService = new ContactService();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmShow_Load(object sender, EventArgs e)
        {
            if (ContactId != 0)
            {
                var contact = _contactService.FindById(ContactId);
                lblName.Text = contact.Name;
                lblFamily.Text = contact.Family;
                lblMobile.Text = contact.Mobile;
                lblPhone.Text = contact.Phone;
                lblEmail.Text = contact.Email;
                lblAge.Text = contact.Age.ToString();
                lblType.Text = contact.Type.ToString();
                lblDesc.Text = contact.Description;
                lblDateTime.Text = contact.CreatedAt.ToLongDateString();

            }
        }
    }
}
