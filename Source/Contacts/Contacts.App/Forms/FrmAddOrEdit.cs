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
using Contacts.Core.Tools;
using Contacts.DataLayer.Enums;

namespace Contacts.App.Forms
{
    public partial class FrmAddOrEdit : Form
    {
        public int ContactId = 0;
        private IContactService _contactService;
        public FrmAddOrEdit()
        {
            InitializeComponent();
            _contactService = new ContactService();
        }

        private void ResetLblError()
        {
            lblError.Text = string.Empty;
        }
        private void FrmAddOrEdit_Load(object sender, EventArgs e)
        {
            SetTypesToComboBox();
            FormLoad();
        }

        private void SetError(string message)
        {
            lblError.Text = message;
        }
        private void FormLoad()
        {
            if (ContactId == 0)
            {
                this.Text = "Add new Contact";
                btnAddOrEdit.Image = new Bitmap(Properties.Resources.plus);
                txtDate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                var contact = _contactService.FindById(ContactId);
                this.Text = $"Eidt {contact.Name + " " + contact.Family}";
                btnAddOrEdit.Image = new Bitmap(Properties.Resources.pen);
                txtName.Text = contact.Name;
                txtFamily.Text = contact.Family;
                txtMobile.Text = contact.Mobile;
                txtPhone.Text = contact.Phone;
                if (contact.Age != null)
                    txtAge.Text = contact.Age.ToString();
                txtDescription.Text = contact.Description;
                txtDate.Text = contact.CreatedAt.ToShortDateString();
                txtEmail.Text = contact.Email;
                cboType.SelectedIndex = cboType.FindString(contact.Type.ToString());
            }
        }



        private bool Validation()
        {
            ResetLblError();
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                SetError("Name is required ...");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFamily.Text))
            {
                SetError("Family is required ...");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtMobile.Text))
            {
                if (Tools.StringLength(txtMobile.Text) != 11)
                {
                    SetError("Mobile Length != 11 ...");
                    return false;
                }
                if (!Tools.NumberValidation(txtMobile.Text))
                {
                    SetError("Mobile not valid ...");
                    return false;
                }

            }
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                if (Tools.StringLength(txtPhone.Text) != 11)
                {
                    SetError("Phone Length != 11 ...");
                    return false;
                }
                if (!Tools.NumberValidation(txtPhone.Text))
                {
                    SetError("Phone not valid ...");
                    return false;
                }
            }
            if (!string.IsNullOrWhiteSpace(txtAge.Text))
            {

                if (!Tools.NumberValidation(txtAge.Text))
                {
                    SetError("Age not valid ...");
                    return false;
                }
            }
            return true;
        }
        private void SetTypesToComboBox()
        {
            cboType.Items.Clear();
            foreach (var item in Enum.GetNames(typeof(ContactType)))
                cboType.Items.Add(item.ToString().Trim());
            cboType.StartIndex = 0;

        }

        private void BtnAddOrEdit_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                int? age = null;
                if (!string.IsNullOrWhiteSpace(txtAge.Text) && Tools.NumberValidation(txtAge.Text))
                    age = int.Parse(txtAge.Text);
                if (ContactId == 0)
                {

                    bool create = _contactService.Create(txtName.Text, txtFamily.Text, txtMobile.Text,
                        txtPhone.Text, txtEmail.Text, txtDescription.Text, age, Tools.ParseEnum<ContactType>(cboType.Text));
                    if (create)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        DialogResult = DialogResult.No;
                        Close();
                    }
                }
                else
                {
                    bool update = _contactService.Update(ContactId, txtName.Text, txtFamily.Text, txtMobile.Text,
                        txtPhone.Text, txtEmail.Text, txtDescription.Text, age, Tools.ParseEnum<ContactType>(cboType.Text));
                    if (update)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        DialogResult = DialogResult.No;
                        Close();
                    }
                }

            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TxtMobile_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
