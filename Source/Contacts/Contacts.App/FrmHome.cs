using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contacts.App.Forms;
using Contacts.Core.Services;
using Contacts.Core.Services.Interfaces;
using Contacts.DataLayer.Enums;

namespace Contacts.App
{
    public partial class FrmHome : Form
    {
        private IContactService _contactService;
        public FrmHome()
        {
            InitializeComponent();
            NewObjects();
        }

        private void NewObjects()
        {
            try
            {
                _contactService = new ContactService();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(string p = null, ContactType? type = null)
        {
            dgvContacts.Rows.Clear();
            dgvContacts.AutoGenerateColumns = false;
            var results = _contactService.GetContacts(p, type);
            foreach (var item in results)
                dgvContacts.Rows.Add(item.Id, item.Name + " " + item.Family, item.Mobile, item.Phone, item.Type.ToString(), item.CreatedAt);
            lblCount.Text = results.Count().ToString();
        }
        private void FrmHome_Load(object sender, EventArgs e)
        {
            dgvContacts.BorderStyle = BorderStyle.FixedSingle;
            dgvContacts.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvContacts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            SetTypesToComboBox();
            BindGrid();
        }

        private void SetTypesToComboBox()
        {
            cboType.Items.Clear();
            cboType.Items.Add("Select Type");
            foreach (var item in Enum.GetNames(typeof(ContactType)))
                cboType.Items.Add(item.ToString().Trim());
            cboType.StartIndex = 0;

        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new FrmAddOrEdit();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                MessageBox.Show("Contact added successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Search();
            }
            else
                MessageBox.Show("Contact not added", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow != null)
            {
                int id = int.Parse(dgvContacts.CurrentRow.Cells[0].Value.ToString());
                var form = new FrmShow();
                form.ContactId = id;
                DialogResult dialogResult = form.ShowDialog();

            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            if (dgvContacts.CurrentRow != null)
            {
                int id = int.Parse(dgvContacts.CurrentRow.Cells[0].Value.ToString());
                var contact = _contactService.FindById(id);
                if (MessageBox.Show($"Are you sure to delete {contact.Name + "  " + contact.Family}", "Question", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    bool delete = _contactService.Delete(id);
                    if (delete)
                    {
                        MessageBox.Show("Contact delete successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Search();
                    }
                    else
                        MessageBox.Show("Contact not deleted", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow != null)
            {
                int id = int.Parse(dgvContacts.CurrentRow.Cells[0].Value.ToString());
                var form = new FrmAddOrEdit();
                form.ContactId = id;
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    MessageBox.Show("Contact Edited successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Search();
                }
                else
                    MessageBox.Show("Contact not edited", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void _Refresh()
        {
            txtSearch.Text = string.Empty;
            SetTypesToComboBox();
            BindGrid();
        }
        private void Search()
        {
            string parameter = txtSearch.Text;
            BindGrid(parameter, GetCboTypeValue());
        }
        private ContactType? GetCboTypeValue()
        {
            ContactType? type = null;
            if (cboType.SelectedIndex != 0)
                type = (ContactType)Enum.Parse(typeof(ContactType), cboType.Text.Trim());
            return type;
        }

        private void CboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void DgvContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnAboutUs_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }
    }
}
