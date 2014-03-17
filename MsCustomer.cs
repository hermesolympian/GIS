using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace GIS.Master
{
    public partial class MsCustomer : Form
    {
        string conString;
        List<string> listCustName;
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader reader;

        string state;

        public MsCustomer()
        {
            InitializeComponent();

            loadDataGrid();
            loadAutoComplete();

            resetForm();
            state = "save";
            configForm();
        }

        private void loadDataGrid()
        {
            conString = ConfigurationManager.ConnectionStrings["localConString"].ConnectionString;
            con = new MySqlConnection(conString);

            try
            {
                con.Open();

                listCustName = new List<string>();
                cmd = new MySqlCommand();
                cmd.CommandText = "select customerid, customername, OrderLIMIT, phone, mobile, email, address, placeofbirth, dateofbirth from mscustomer";
                cmd.Connection = con;
                reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                DataRow dr = new DataTable().NewRow();

                dt.Columns.Add(new DataColumn("customerid", Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("Customer Name", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Order Limit", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Phone", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Mobile", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Email", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Address", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Place - Date Of Birth", Type.GetType("System.String")));

                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["customerid"] = reader.GetInt32(0);
                    dr["Customer Name"] = reader.GetString(1);
                    dr["Order Limit"] = reader.GetValue(2) == DBNull.Value ? "0" : reader.GetString(2);
                    dr["Phone"] = reader.GetString(3);
                    dr["Mobile"] = reader.GetString(4);
                    dr["Email"] = reader.GetString(5);
                    dr["Address"] = reader.GetString(6);
                    dr["Place - Date Of Birth"] = reader.GetString(7) + " - " + reader.GetDateTime(8).ToString("dd MMMM yyyy");

                    dt.Rows.Add(dr);

                    listCustName.Add(reader.GetString(1));
                }
                DataGridView1.DataSource = dt;

                DataGridView1.Columns["customerid"].Visible = false;
                DataGridView1.Columns["Place - Date Of Birth"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                reader.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void loadAutoComplete()
        {
            string[] custName = new string[listCustName.Count];
            var source = new AutoCompleteStringCollection();
            int i = 0;

            foreach (string temp in listCustName)
            {
                custName[i] = temp;
                i++;
            }
            source.AddRange(custName);

            txtCustName.AutoCompleteCustomSource = source;
            txtCustName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCustName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            loadCustomer(DataGridView1.CurrentRow.Cells[0].Value.ToString());

            state = "edit";
            configForm();
        }

        private void loadCustomer(string customerid)
        {
            conString = ConfigurationManager.ConnectionStrings["localConString"].ConnectionString;
            con = new MySqlConnection(conString);

            try
            {
                con.Open();

                cmd = new MySqlCommand();
                cmd.CommandText = "select customerid, customername, OrderLIMIT, phone, mobile, email, address, placeofbirth, dateofbirth from mscustomer where customerid = " + customerid;
                cmd.Connection = con;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    hidCustomerID.Text = reader.GetInt32(0).ToString();
                    txtCustName.Text = reader.GetString(1);
                    txtLimit.Text = reader.GetValue(2) == DBNull.Value ? "0" : reader.GetString(2);
                    txtPhone.Text = reader.GetString(3);
                    txtMobile.Text = reader.GetString(4);
                    txtEmail.Text = reader.GetString(5);
                    txtAddress.Text = reader.GetString(6);
                    txtPOB.Text = reader.GetString(7);
                    dtpDOB.Value = reader.GetDateTime(8);
                }

                reader.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (state == "save")
            {
                if (validateForm())
                {
                    conString = ConfigurationManager.ConnectionStrings["localConString"].ConnectionString;
                    con = new MySqlConnection(conString);

                    try
                    {
                        con.Open();

                        cmd = new MySqlCommand();
                        cmd.CommandText = "insert into mscustomer (customername, phone, mobile, email, address, dateofbirth, placeofbirth, datein, userin, OrderLIMIT) values ('" + txtCustName.Text + "', '" + txtPhone.Text + "', '" + txtMobile.Text + "', '" + txtEmail.Text + "', '" + txtAddress.Text + "', '" + dtpDOB.Value.ToString("yyyy-MM-dd") + "','" + txtPOB.Text + "', now(), " + MySession.UserID + ", " + txtLimit.Text + ")";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                        con.Close();

                        MessageBox.Show("Success Save New Customer !!");

                        resetForm();
                        state = "save";
                        configForm();

                        loadDataGrid();
                    }
                    catch (MySqlException ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (state == "edit")
            {
                state = "update";
                configForm();
            }
            else if (state == "update")
            {
                if (validateForm())
                {
                    conString = ConfigurationManager.ConnectionStrings["localConString"].ConnectionString;
                    con = new MySqlConnection(conString);

                    try
                    {
                        con.Open();

                        cmd = new MySqlCommand();
                        cmd.CommandText = "insert into hscustomer (customername, phone, mobile, email, address, dateofbirth, placeofbirth, datein, userin, dateup, userup, OrderLimit) select customername, phone, mobile, email, address, dateofbirth, placeofbirth, datein, userin, now(), " + MySession.UserID + ", OrderLIMIT from mscustomer where customerid = " + hidCustomerID.Text + "; update mscustomer set customername = '" + txtCustName.Text + "', phone = '" + txtPhone.Text + "', mobile = '" + txtMobile.Text + "', email = '" + txtEmail.Text + "', address = '" + txtAddress.Text + "', placeofbirth = '" + txtPOB.Text + "', dateofbirth = '" + dtpDOB.Value.ToString("yyyy-MM-dd") + "', OrderLIMIT=" + txtLimit.Text.Trim() + " where customerid = " + hidCustomerID.Text;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                        con.Close();

                        MessageBox.Show("Success Update Customer !!");

                        resetForm();
                        state = "save";
                        configForm();

                        loadDataGrid();
                    }
                    catch (MySqlException ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetForm();
            state = "save";
            configForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (hidCustomerID.Text != "")
            {
                if (MessageBox.Show("Are you sure want to delete this item?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    conString = ConfigurationManager.ConnectionStrings["localConString"].ConnectionString;
                    con = new MySqlConnection(conString);

                    try
                    {
                        con.Open();

                        cmd = new MySqlCommand();
                        cmd.CommandText = "insert into hscustomer (customername, phone, mobile, email, address, dateofbirth, placeofbirth, datein, userin, dateup, userup) select customername, phone, mobile, email, address, dateofbirth, placeofbirth, datein, userin, now(), " + MySession.UserID + " from mscustomer where customerid = " + hidCustomerID.Text + "; delete from mscustomer where customerid = " + hidCustomerID.Text;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                        con.Close();

                        MessageBox.Show("Success Delete Customer !!");

                        resetForm();
                        state = "save";
                        configForm();

                        loadDataGrid();
                    }
                    catch (MySqlException ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Customer Chosen !!");
            }
        }

        private bool validateForm()
        {
            if (string.IsNullOrEmpty(txtCustName.Text))
            {
                MessageBox.Show("Please Fill Customer Name !!");
            }
            else if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Please Fill Phone !!");
            }
            else if (!isNumVCS(txtPhone.Text) || (isNumVCS(txtPhone.Text) && txtPhone.Text.Contains('-')))
            {
                MessageBox.Show("Please Fill Phone With Numeric Value [0-9] !!");
            }
            else if (string.IsNullOrEmpty(txtMobile.Text))
            {
                MessageBox.Show("Please Fill Mobile !!");
            }
            else if (!isNumVCS(txtMobile.Text) || (isNumVCS(txtMobile.Text) && txtMobile.Text.Contains('-')))
            {
                MessageBox.Show("Please Fill Mobile With Numeric Value [0-9] !!");
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please Fill Email !!");
            }
            else if (!isValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please Fill Email With Valid Email !!");
            }
            else if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Please Fill Address !!");
            }
            else if (string.IsNullOrEmpty(txtPOB.Text))
            {
                MessageBox.Show("Please Fill Place Of Birth !!");
            }
            else if (string.IsNullOrEmpty(txtLimit.Text))
            {
                MessageBox.Show("Please Fill Limit Transaction !!");
            }
            else if (!isNumVCS(txtLimit.Text) || (isNumVCS(txtLimit.Text) && txtLimit.Text.Contains('-')))
            {
                MessageBox.Show("Please Fill Limit Transaction With Numeric Value [0-9] !!");
            }
            else
            {
                return true;
            }

            return false;
        }

        private void resetForm()
        {
            txtCustName.Text = "";
            txtPhone.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPOB.Text = "";
            dtpDOB.Value = DateTime.Now;

            hidCustomerID.Text = "";
            txtLimit.Text = "";
        }

        private void configForm()
        {
            if (state == "save")
            {
                txtCustName.Enabled = true;
                txtPhone.Enabled = true;
                txtMobile.Enabled = true;
                txtEmail.Enabled = true;
                txtAddress.Enabled = true;
                txtPOB.Enabled = true;
                txtLimit.Enabled = true;
                dtpDOB.Enabled = true;

                btnSave.Text = "Save";
            }
            else if (state == "edit")
            {
                txtCustName.Enabled = false;
                txtPhone.Enabled = false;
                txtMobile.Enabled = false;
                txtEmail.Enabled = false;
                txtAddress.Enabled = false;
                txtPOB.Enabled = false;
                txtLimit.Enabled = false;
                dtpDOB.Enabled = false;

                btnSave.Text = "Edit";
            }
            else if (state == "update")
            {
                txtCustName.Enabled = true;
                txtPhone.Enabled = true;
                txtMobile.Enabled = true;
                txtEmail.Enabled = true;
                txtAddress.Enabled = true;
                txtPOB.Enabled = true;
                txtLimit.Enabled = true;
                dtpDOB.Enabled = true;

                btnSave.Text = "Update";
            }
        }

        private bool isNumVCS(string inputNum)
        {
            Int64 dt = 0;
            return Int64.TryParse(inputNum, out dt);
        }

        private bool isValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void MsCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure want to close this form?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                this.Activate();
            }
        }

        private void txtCustName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (DataGridViewRow row in DataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().ToUpper() == txtCustName.Text.ToUpper())
                    {
                        DataGridView1.CurrentCell = DataGridView1[1, row.Index];
                        loadCustomer(DataGridView1.CurrentRow.Cells[0].Value.ToString());

                        state = "edit";
                        configForm();
                    }
                }
            }
        }

        private void Digit_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }
    }
}