using BusinessLayer;
using DBAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class AdminForm : Form
    {
        HospitalManagementForm form1;
        AdminDataAccess ada = new AdminDataAccess();
        ReceptionistDataAccess rda = new ReceptionistDataAccess();
        public static string loginname = "";
        
        public AdminForm()
        {
            InitializeComponent();
        }
        public AdminForm(HospitalManagementForm f1)
        {
            this.form1 = f1;
            InitializeComponent();
        }

        private void homebutton_Click(object sender, EventArgs e)
        {
            admintabControl.SelectedTab = dashtab;
        }

        private void profilebutton_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin=ada.getProfile(loginname);
            nametextBox.Text = admin.Name;
            contacttextBox.Text = admin.Contact.ToString();
            adminaddresscomboBox.Items.Clear();
            adminaddresscomboBox.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                adminaddresscomboBox.Items.Add(address);
            }
            adminaddresscomboBox.SelectedItem = admin.Address;
            dateTimePicker.Text = admin.DateOfBirth.ToString();
            admintabControl.SelectedTab = profiletab;
        }

        private void changepasslinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = Changepasstab;
        }

        private void backlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = profiletab;
        }

        private void doctorbutton_Click(object sender, EventArgs e)
        {
            doctordataGridView.DataSource = null;
            doctordataGridView.Rows.Clear();
            List<Doctor> dlist = rda.doctordatagrid();
            foreach (var item in dlist)
            {
                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation,"details","edit","delete",item.Id);
            }
            int total = rda.getTotalDoctor();
            doctotallabel.Text = total.ToString();
            doctoraddresscombo.Items.Clear();
            doctoraddresscombo.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                doctoraddresscombo.Items.Add(address);
            }
            doctorsearchbycomboBox.Items.Clear();
            doctorsearchbycomboBox.Items.Add("Name");
            doctorsearchbycomboBox.Items.Add("Designation");
            doctorsearchbycomboBox.Text = "";
            doctorsearchtextBox.Text = "";
            admintabControl.SelectedTab = doctortab;
        }

        private void nursebutton_Click(object sender, EventArgs e)
        {
            nursedataGridView.DataSource = null;
            nursedataGridView.Rows.Clear();
            List<Nurse> nlist = rda.nursedatagrid();
            foreach (var item in nlist)
            {
                nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation,"details","edit","delete",item.Id);
            }
            int total = rda.getTotalNurse();
            nursetotallabel.Text = total.ToString();
            nurseaddrescomboBox1.Items.Clear();
            nurseaddrescomboBox1.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                nurseaddrescomboBox1.Items.Add(address);
            }
            nursesearchcomboBox.Items.Clear();
            nursesearchcomboBox.Items.Add("Name");
            nursesearchcomboBox.Items.Add("Designation");
            nursesearchcomboBox.Text = "";
            nursesearchtextBox.Text = "";
            admintabControl.SelectedTab = nursetab;
        }

        private void receptionistbutton_Click(object sender, EventArgs e)
        {
            recdataGridView.DataSource = null;
            recdataGridView.Rows.Clear();
            List<Receptionist> rlist = ada.getAllReceptionist();
            foreach (var item in rlist)
            {
                recdataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete",item.Id);
            }
            int total = rda.getTotalRec();
            rectotallabel.Text = total.ToString();
            receptionistaddresscomboBox1.Items.Clear();
            receptionistaddresscomboBox1.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                receptionistaddresscomboBox1.Items.Add(address);
            }
            recsearchcomboBox.Items.Clear();
            recsearchcomboBox.Items.Add("Name");
            recsearchcomboBox.Items.Add("Designation");
            recsearchcomboBox.Text = "";
            recsearchtextBox.Text = "";
            admintabControl.SelectedTab = receptionisttab;
        }

        private void roombutton_Click(object sender, EventArgs e)
        {
            roomdataGridView.DataSource = null;
            roomdataGridView.Rows.Clear();
            List<Room> rlist = rda.roomdatagrid();
            foreach (var item in rlist)
            {
                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus,"edit","delete");
            }
            int total = rda.getTotalRoom();
            roomtotallabel.Text = total.ToString();
            roomtypecomboBox.Items.Clear();
            roomtypecomboBox.Text = "";
            List<string> roomtypelist = new List<string>();
            roomtypelist = ada.roomtypecombo();
            foreach (var type in roomtypelist)
            {
                roomtypecomboBox.Items.Add(type);
            }
            roomsearchcomboBox.Items.Clear();
            roomsearchcomboBox.Items.Add("Roomno");
            roomsearchcomboBox.Items.Add("Roomtype");
            roomsearchcomboBox.Items.Add("Roomcost");
            roomsearchcomboBox.Items.Add("Roomstatus");
            roomsearchcomboBox.Text = "";
            roomsearchtextBox.Text = "";
            admintabControl.SelectedTab = roomtab;
        }

        private void logoutbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new HospitalManagementForm(this);
            form1.Show();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            loginname=HospitalManagementForm.loginName;
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void editprofilebutton_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin = ada.getProfile(loginname);

            if (nametextBox.Text == admin.Name && contacttextBox.Text == admin.Contact.ToString() && adminaddresscomboBox.SelectedItem.ToString() == admin.Address && Convert.ToDateTime(dateTimePicker.Text)==admin.DateOfBirth)
            {
                MessageBox.Show("No New Changes.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else 
            {
                if (string.IsNullOrEmpty(nametextBox.Text))
                {
                    MessageBox.Show("Name Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nametextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(contacttextBox.Text))
                {
                    MessageBox.Show("Contact Number Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    contacttextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(adminaddresscomboBox.Text))
                {
                    MessageBox.Show("Address Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    adminaddresscomboBox.Focus();
                    return;
                }
                else
                {
                    if (contacttextBox.Text.Length > 11)
                    {
                        MessageBox.Show("Invalid Contactno");
                    }
                    else
                    {
                        ada.changeProfile(loginname, nametextBox.Text, adminaddresscomboBox.Text, contacttextBox.Text, dateTimePicker.Text);
                        MessageBox.Show("Profile Updated Successfully.");
                    }
                }
            }
        }

        private void Submitbutton_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin = ada.checkOldPassword(loginname);

            if (string.IsNullOrEmpty(currentpasstextBox.Text))
            {
                MessageBox.Show("Please Enter Current Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                currentpasstextBox.Focus();
                return;
            }

            else if (string.IsNullOrEmpty(newtextBox.Text))
            {
                MessageBox.Show("Please Enter New Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                newtextBox.Focus();
                return;
            }

            else if (string.IsNullOrEmpty(retypetextBox.Text))
            {
                MessageBox.Show("Please Re-Enter New Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                retypetextBox.Focus();
                return;
            }

            else
            {
                if (currentpasstextBox.Text == admin.Password)
                {
                    if (newtextBox.Text != retypetextBox.Text)
                    {
                        MessageBox.Show("New Password & Re-typing New Password Does Not Match.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        retypetextBox.Focus();
                        return;
                    }
                    else
                    {
                        ada.changePassword(loginname, newtextBox.Text);
                        MessageBox.Show("Password changed successfully");
                        currentpasstextBox.Text = "";
                        newtextBox.Text = "";
                        retypetextBox.Text = "";
                    }
                }
                else if (currentpasstextBox.Text != admin.Password)
                {
                    MessageBox.Show("Current Password Does Not Match.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    currentpasstextBox.Focus();
                    return;
                }
            }
        }

        private void doctordataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = ((System.Windows.Forms.DataGridView)(sender)).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int docid = Convert.ToInt32(doctordataGridView.Rows[e.RowIndex].Cells[6].Value);
            if(str=="edit")
            {
                Doctor doctor = new Doctor();
                doctor = rda.getDoctorbyId(docid);
                editdocnametextBox.Text = doctor.Name;
                editdocaddresscomboBox.Items.Clear();
                editdocaddresscomboBox.Text = "";
                List<string> addresslist = new List<string>();
                addresslist = rda.addresscombo();
                foreach (var address in addresslist)
                {
                    editdocaddresscomboBox.Items.Add(address);
                }
                editdocaddresscomboBox.SelectedItem = doctor.Address;
                editdoccontacttextBox.Text = doctor.Contactno.ToString();
                editdocdateTimePicker.Text = doctor.JoiningDate.ToString();
                editdocsaltextBox.Text = doctor.Salary.ToString();
                editdocdestextBox.Text = doctor.Designation;
                editdocidlabel.Text = docid.ToString();
                admintabControl.SelectedTab = editdoctortab;
            }
            else if (str == "details")
            {
                Doctor doctor = new Doctor();
                doctor = rda.getDoctorbyId(docid);
                detaildocnamelabel.Text = doctor.Name;
                detaildocaddresslabel.Text = doctor.Address;
                detaildoccontactlabel.Text = doctor.Contactno.ToString();
                detaildocjoindatelabel.Text = doctor.JoiningDate.ToString();
                detaildocsalarylabel.Text = doctor.Salary.ToString();
                detaildocdeslabel.Text = doctor.Designation;
                detailsdocidlabel.Text = docid.ToString();
                admintabControl.SelectedTab = detailsdoctortab;
            }
            else if (str == "delete")
            {
                DialogResult dialogrslt = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.OKCancel);
                if (dialogrslt == DialogResult.OK)
                {
                    ada.deleteDoctor(docid);
                    doctordataGridView.DataSource = null;
                    doctordataGridView.Rows.Clear();
                    List<Doctor> dlist = rda.doctordatagrid();
                    foreach (var item in dlist)
                    {
                        doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                    }
                    int total = rda.getTotalDoctor();
                    doctotallabel.Text = total.ToString();
                }
            }
        }

        private void docreglinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = adddoctortab;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            doctordataGridView.DataSource = null;
            doctordataGridView.Rows.Clear();
            List<Doctor> dlist = rda.doctordatagrid();
            foreach (var item in dlist)
            {
                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete",item.Id);
            }
            int total = rda.getTotalDoctor();
            doctotallabel.Text = total.ToString();
            admintabControl.SelectedTab = doctortab;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            admintabControl.SelectedTab = doctortab;
        }

        private void detaildoctoreditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int docid = Convert.ToInt32(detailsdocidlabel.Text);
            Doctor doctor = new Doctor();
            doctor = rda.getDoctorbyId(docid);
            editdocnametextBox.Text = doctor.Name;
            editdocaddresscomboBox.Items.Clear();
            editdocaddresscomboBox.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                editdocaddresscomboBox.Items.Add(address);
            }
            editdocaddresscomboBox.SelectedItem = doctor.Address;
            editdoccontacttextBox.Text = doctor.Contactno.ToString();
            editdocdateTimePicker.Text = doctor.JoiningDate.ToString();
            editdocsaltextBox.Text = doctor.Salary.ToString();
            editdocdestextBox.Text = doctor.Designation;
            editdocidlabel.Text = docid.ToString();
            admintabControl.SelectedTab = editdoctortab;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            doctordataGridView.DataSource = null;
            doctordataGridView.Rows.Clear();
            List<Doctor> dlist = rda.doctordatagrid();
            foreach (var item in dlist)
            {
                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete",item.Id);
            }

            admintabControl.SelectedTab = doctortab;
        }

        private void addDoctorbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(doctextBox1.Text))
            {
                MessageBox.Show("Please enter doctor's name");
            }
            else if (doctoraddresscombo.SelectedItem == null)
            {
                MessageBox.Show("Please enter doctor's address");
            }
            else if (string.IsNullOrEmpty(docjoindateTimePicker1.Text))
            {
                MessageBox.Show("Please enter doctor's joining date");
            }
            else if (string.IsNullOrEmpty(doctextBox3.Text))
            {
                MessageBox.Show("Please enter doctor's contact number");
            }
            else if (string.IsNullOrEmpty(doctextBox5.Text))
            {
                MessageBox.Show("Please enter doctor's salary");
            }
            else if (string.IsNullOrEmpty(doctextBox6.Text))
            {
                MessageBox.Show("Please enter doctor's designation");
            }
            else
            {
                if (doctextBox3.Text.Length>11)
                {
                    MessageBox.Show("Invalid contact");
                }
                else
                {
                    ada.add_doctor(doctextBox1.Text, doctoraddresscombo.Text, doctextBox3.Text, docjoindateTimePicker1.Text, doctextBox5.Text, doctextBox6.Text);
                    MessageBox.Show("Doctor added successfully");
                    doctextBox1.Text = "";
                    doctextBox3.Text = "";
                    doctoraddresscombo.Text = "";
                    doctextBox5.Text = "";
                    doctextBox6.Text = "";
                }
            }
        }

        private void nurselinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = addnursetab;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nursedataGridView.DataSource = null;
            nursedataGridView.Rows.Clear();
            List<Nurse> nlist = rda.nursedatagrid();
            foreach (var item in nlist)
            {
                nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details","edit", "delete",item.Id);

            }
            int total = rda.getTotalNurse();
            nursetotallabel.Text = total.ToString();
            admintabControl.SelectedTab = nursetab;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nursetextBox1.Text))
            {
                MessageBox.Show("Please enter nurse's name");
            }
            else if (nurseaddrescomboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please enter nurse's address");
            }
            else if (string.IsNullOrEmpty(nursejoindateTimePicker1.Text))
            {
                MessageBox.Show("Please enter nurse's joining date");
            }
            else if (string.IsNullOrEmpty(nursetextBox5.Text))
            {
                MessageBox.Show("Please enter nurse's contact number");
            }
            else if (string.IsNullOrEmpty(nursetextBox16.Text))
            {
                MessageBox.Show("Please enter nurse's salary");
            }
            else if (string.IsNullOrEmpty(nursetextBox17.Text))
            {
                MessageBox.Show("Please enter nurse's designation");
            }
            else
            {
                if (nursetextBox5.Text.Length > 11)
                {
                    MessageBox.Show("Invalid Contact");
                }
                else
                {
                    ada.add_nurse(nursetextBox1.Text, nurseaddrescomboBox1.Text, nursetextBox5.Text, nursejoindateTimePicker1.Text, nursetextBox16.Text, nursetextBox17.Text);
                    MessageBox.Show("Nurse added successfully");
                    nursetextBox1.Text = "";
                    nursetextBox5.Text = "";
                    nurseaddrescomboBox1.Text = "";
                    nursetextBox16.Text = "";
                    nursetextBox17.Text = "";
                }
            }
        }

        private void reclinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = addreceptionisttab;
        }

        private void addrecbacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recdataGridView.DataSource = null;
            recdataGridView.Rows.Clear();
            List<Receptionist> rlist = ada.getAllReceptionist();
            foreach (var item in rlist)
            {
                recdataGridView.Rows.Add(item.Name, item.Contactno, item.Designation,"details", "edit", "delete",item.Id);
            }
            int total = rda.getTotalRec();
            rectotallabel.Text = total.ToString();
            admintabControl.SelectedTab = receptionisttab;
        }

        private void addrecbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(receptionisttextBox1.Text))
            {
                MessageBox.Show("Please enter nurse's name");
            }
            else if (string.IsNullOrEmpty(receptionisttextBox2.Text))
            {
                MessageBox.Show("Please enter nurse's email");
            }
            else if (receptionistaddresscomboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please enter nurse's address");
            }
            else if (string.IsNullOrEmpty(receptionistjoindateTimePicker1.Text))
            {
                MessageBox.Show("Please enter nurse's joining date");
            }
            else if (string.IsNullOrEmpty(receptionisttextBox3.Text))
            {
                MessageBox.Show("Please enter nurse's contact number");
            }
            else if (string.IsNullOrEmpty(receptionisttextBox4.Text))
            {
                MessageBox.Show("Please enter nurse's salary");
            }
            else if (string.IsNullOrEmpty(receptionisttextBox5.Text))
            {
                MessageBox.Show("Please enter nurse's designation");
            }
            else
            {
                ada.add_receptionist(receptionisttextBox1.Text, receptionisttextBox2.Text, receptionisttextBox3.Text, receptionistaddresscomboBox1.Text, receptionistjoindateTimePicker1.Text, receptionisttextBox4.Text, receptionisttextBox5.Text);
                MessageBox.Show("Receptionist added successfully");
                receptionisttextBox1.Text = "";
                receptionisttextBox2.Text = "";
                receptionistaddresscomboBox1.Text = "";
                receptionisttextBox3.Text = "";
                receptionisttextBox4.Text = "";
                receptionisttextBox5.Text = "";
            }
        }

        private void addroombacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            roomdataGridView.DataSource = null;
            roomdataGridView.Rows.Clear();
            List<Room> rlist = rda.roomdatagrid();
            foreach (var item in rlist)
            {
                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus, "edit", "delete");
            }
            int total = rda.getTotalRoom();
            roomtotallabel.Text = total.ToString();
            admintabControl.SelectedTab = roomtab;
        }

        private void roomlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = addroomtab;
        }

        private void addroombutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(roomcosttextBox.Text))
            {
                MessageBox.Show("Please enter room cost");
            }
            else if (roomtypecomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please enter room type");
            }
            else
            {
                ada.add_room(roomtypecomboBox.Text, roomcosttextBox.Text);
                MessageBox.Show("Room added successfully");
                roomcosttextBox.Text = "";
                roomtypecomboBox.Text = "";
            }
        }

        private void nursedataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = ((System.Windows.Forms.DataGridView)(sender)).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int nurseid = Convert.ToInt32(nursedataGridView.Rows[e.RowIndex].Cells[6].Value);
            if (str == "edit")
            {
                Nurse nurse = new Nurse();
                nurse = rda.getNursebyId(nurseid);
                editnursenametextBox.Text = nurse.Name;
                editnurseaddresscomboBox.Items.Clear();
                editnurseaddresscomboBox.Text = "";
                List<string> addresslist = new List<string>();
                addresslist = rda.addresscombo();
                foreach (var address in addresslist)
                {
                    editnurseaddresscomboBox.Items.Add(address);
                }
                editnurseaddresscomboBox.SelectedItem = nurse.Address;
                editnursecontacttextBox.Text = nurse.Contactno.ToString();
                editnursedateTimePicker.Text = nurse.JoiningDate.ToString();
                editnursesaltextBox.Text = nurse.Salary.ToString();
                editnursedestextBox.Text = nurse.Designation;
                editnurseidlabel.Text = nurseid.ToString();
                admintabControl.SelectedTab = editnursetab;
            }
            else if (str == "details")
            {
                Nurse nurse = new Nurse();
                nurse = rda.getNursebyId(nurseid);
                detailnursenamelabel.Text = nurse.Name;
                detailnurseaddresslabel.Text = nurse.Address;
                detailnursecontactlabel.Text = nurse.Contactno.ToString();
                detailnursejoindatelabel.Text = nurse.JoiningDate.ToString();
                detailnursesallabel.Text = nurse.Salary.ToString();
                detailnursedeslabel.Text = nurse.Designation;
                detailnurseidlabel.Text = nurseid.ToString();
                admintabControl.SelectedTab = detailsnursetab;
            }
            else if (str == "delete")
            {
                DialogResult dialogrslt = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.OKCancel);
                if (dialogrslt == DialogResult.OK)
                {
                    ada.deleteNurse(nurseid);
                    nursedataGridView.DataSource = null;
                    nursedataGridView.Rows.Clear();
                    List<Nurse> dlist = rda.nursedatagrid();
                    foreach (var item in dlist)
                    {
                        nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                    }
                    int total = rda.getTotalNurse();
                    nursetotallabel.Text = total.ToString();
                }
            }
        }

        private void recdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = ((System.Windows.Forms.DataGridView)(sender)).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            int recid = Convert.ToInt32(recdataGridView.Rows[e.RowIndex].Cells[6].Value);
            if (str == "edit")
            {
                Receptionist rec = new Receptionist();
                rec = ada.getReceptionistbyId(recid);
                editrecnametextBox.Text = rec.Name;
                editrecaddresscomboBox.Items.Clear();
                editrecaddresscomboBox.Text = "";
                List<string> addresslist = new List<string>();
                addresslist = rda.addresscombo();
                foreach (var address in addresslist)
                {
                    editrecaddresscomboBox.Items.Add(address);
                }
                editrecaddresscomboBox.SelectedItem = rec.Address;
                editrecemailtextBox.Text = rec.Email;
                editreccontacttextBox.Text = rec.Contactno.ToString();
                editrecdateTimePicker.Text = rec.JoiningDate.ToString();
                editrecsaltextBox.Text = rec.Salary.ToString();
                editrecdestextBox.Text = rec.Designation;
                editrecidlabel.Text = recid.ToString();
                admintabControl.SelectedTab = editreceptionisttab;
            }
            else if (str == "details")
            {
                Receptionist rec = new Receptionist();
                rec = ada.getReceptionistbyId(recid);
                detailrecnamelabel.Text = rec.Name;
                detailrecemaillabel.Text = rec.Email;
                detailrecaddresslabel.Text = rec.Address;
                detailreccontactlabel.Text = rec.Contactno.ToString();
                detailrecjoindatelabel.Text = rec.JoiningDate.ToString();
                detailrecsallabel.Text = rec.Salary.ToString();
                detailrecdeslabel.Text = rec.Designation;
                detailrecidlabel.Text = recid.ToString();
                admintabControl.SelectedTab = detailsreceptionisttab;
            }
            else if (str == "delete")
            {
                DialogResult dialogrslt = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.OKCancel);
                if (dialogrslt == DialogResult.OK)
                {
                    ada.deleteReceptionist(recid);
                    recdataGridView.DataSource = null;
                    recdataGridView.Rows.Clear();
                    List<Receptionist> rlist = ada.getAllReceptionist();
                    foreach (var item in rlist)
                    {
                        recdataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                    }
                    int total = rda.getTotalRec();
                    rectotallabel.Text = total.ToString();
                }
            }
        }

        private void roomdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = ((System.Windows.Forms.DataGridView)(sender)).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int roomid = Convert.ToInt32(roomdataGridView.Rows[e.RowIndex].Cells[0].Value);

            if (str == "edit")
            {
                Room room = new Room();
                room = ada.getRoomById(roomid);
                editroomtypecomboBox.Items.Clear();
                editroomtypecomboBox.Text = "";
                List<string> roomtypelist = new List<string>();
                roomtypelist = ada.roomtypecombo();
                foreach (var type in roomtypelist)
                {
                    editroomtypecomboBox.Items.Add(type);
                }
                editroomtypecomboBox.SelectedItem=room.RoomType;
                editroomcosttextBox.Text = room.RoomCost.ToString();
                editroomidlabel.Text = roomid.ToString();
                admintabControl.SelectedTab = editroomtab;
            }
            if (str == "delete")
            {
                DialogResult dialogrslt = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.OKCancel);
                if (dialogrslt == DialogResult.OK)
                {
                    ada.deleteRoom(roomid);
                    roomdataGridView.DataSource = null;
                    roomdataGridView.Rows.Clear();
                    List<Room> rlist = rda.roomdatagrid();
                    foreach (var item in rlist)
                    {
                        roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus, "edit", "delete");
                    }
                    int total = rda.getTotalRoom();
                    roomtotallabel.Text = total.ToString();
                }
            }

        }


        private void editnursebacklinklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = nursetab;
        }

        private void editroombacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            roomdataGridView.DataSource = null;
            roomdataGridView.Rows.Clear();
            List<Room> rlist = rda.roomdatagrid();
            foreach (var item in rlist)
            {
                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus, "edit", "delete");
            }
            admintabControl.SelectedTab = roomtab;
        }

        private void detailrecbacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recdataGridView.DataSource = null;
            recdataGridView.Rows.Clear();
            List<Receptionist> rlist = ada.getAllReceptionist();
            foreach (var item in rlist)
            {
                recdataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
            }

            admintabControl.SelectedTab = receptionisttab;
        }

        private void detailnursebacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = nursetab;
        }

        private void detailnurseeditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int nurseid = Convert.ToInt32(detailnurseidlabel.Text);
            Nurse nurse = new Nurse();
            nurse = rda.getNursebyId(nurseid);
            editnursenametextBox.Text = nurse.Name;
            editnurseaddresscomboBox.Items.Clear();
            editnurseaddresscomboBox.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                editnurseaddresscomboBox.Items.Add(address);
            }
            editnurseaddresscomboBox.SelectedItem = nurse.Address;
            editnursecontacttextBox.Text = nurse.Contactno.ToString();
            editnursedateTimePicker.Text = nurse.JoiningDate.ToString();
            editnursesaltextBox.Text = nurse.Salary.ToString();
            editnursedestextBox.Text = nurse.Designation;
            editnurseidlabel.Text = nurse.ToString();
            admintabControl.SelectedTab = editnursetab;
        }

        private void detailreceditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int recid = Convert.ToInt32(detailrecidlabel.Text);
            Receptionist rec = new Receptionist();
            rec = ada.getReceptionistbyId(recid);
            editrecnametextBox.Text = rec.Name;
            editrecaddresscomboBox.Items.Clear();
            editrecaddresscomboBox.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                editrecaddresscomboBox.Items.Add(address);
            }
            editrecaddresscomboBox.SelectedItem = rec.Address;
            editrecemailtextBox.Text = rec.Email;
            editreccontacttextBox.Text = rec.Contactno.ToString();
            editrecdateTimePicker.Text = rec.JoiningDate.ToString();
            editrecsaltextBox.Text = rec.Salary.ToString();
            editrecdestextBox.Text = rec.Designation;
            editrecidlabel.Text = recid.ToString();
            admintabControl.SelectedTab = editreceptionisttab;

        }

        private void editsavechangesbutton_Click(object sender, EventArgs e)
        {
            int docid = Convert.ToInt32(editdocidlabel.Text);
            Doctor doctor = new Doctor();
            doctor = rda.getDoctorbyId(docid);

            if (editdocnametextBox.Text == doctor.Name && editdocaddresscomboBox.SelectedItem.ToString() == doctor.Address && editdoccontacttextBox.Text == doctor.Contactno.ToString() && Convert.ToDateTime(editdocdateTimePicker.Text) == doctor.JoiningDate && editdocsaltextBox.Text==doctor.Salary.ToString() && editdocdestextBox.Text==doctor.Designation)
            {
                MessageBox.Show("No New Changes.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(editdocnametextBox.Text))
                {
                    MessageBox.Show("Name Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editdocnametextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editdoccontacttextBox.Text))
                {
                    MessageBox.Show("Contact Number Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editdoccontacttextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editdocaddresscomboBox.Text))
                {
                    MessageBox.Show("Address Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editdocaddresscomboBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editdocsaltextBox.Text))
                {
                    MessageBox.Show("Salary Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editdocsaltextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editdocdestextBox.Text))
                {
                    MessageBox.Show("Designation Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editdocdestextBox.Focus();
                    return;
                }

                else
                {
                    if (editdoccontacttextBox.Text.Length>11)
                    {
                        MessageBox.Show("Invalid Contact");
                    }
                    else{
                    ada.changeDoctorProfile(docid, editdocnametextBox.Text, editdocaddresscomboBox.SelectedItem.ToString(), editdoccontacttextBox.Text, editdocsaltextBox.Text, editdocdateTimePicker.Text, editdocdestextBox.Text);
                    MessageBox.Show("Profile Updated Successfully.");
                    }
                }
            }
        }

        private void editnursesavebutton_Click(object sender, EventArgs e)
        {
            int nurseid = Convert.ToInt32(editnurseidlabel.Text);
            Nurse nurse = new Nurse();
            nurse = rda.getNursebyId(nurseid);

            if (editnursenametextBox.Text == nurse.Name && editnurseaddresscomboBox.SelectedItem.ToString() == nurse.Address && editnursecontacttextBox.Text == nurse.Contactno.ToString() && Convert.ToDateTime(editnursedateTimePicker.Text) == nurse.JoiningDate && editnursesaltextBox.Text == nurse.Salary.ToString() && editnursedestextBox.Text == nurse.Designation)
            {
                MessageBox.Show("No New Changes.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(editnursenametextBox.Text))
                {
                    MessageBox.Show("Name Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editnursenametextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editnursecontacttextBox.Text))
                {
                    MessageBox.Show("Contact Number Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editnursecontacttextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editnurseaddresscomboBox.Text))
                {
                    MessageBox.Show("Address Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editnurseaddresscomboBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editnursesaltextBox.Text))
                {
                    MessageBox.Show("Salary Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editnursesaltextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editnursedestextBox.Text))
                {
                    MessageBox.Show("Designation Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editnursedestextBox.Focus();
                    return;
                }

                else
                {
                    if (editnursecontacttextBox.Text.Length > 11)
                    {
                        MessageBox.Show("Invalid Contactno");
                    }
                    else
                    {
                        ada.changeNurseProfile(nurseid, editnursenametextBox.Text, editnurseaddresscomboBox.SelectedItem.ToString(), editnursecontacttextBox.Text, editnursesaltextBox.Text, editnursedateTimePicker.Text, editnursedestextBox.Text);
                        MessageBox.Show("Profile Updated Successfully.");
                    }
                }
            }
        }

        private void editrecsavebutton_Click(object sender, EventArgs e)
        {
            int recid = Convert.ToInt32(editrecidlabel.Text);
            Receptionist rec = new Receptionist();
            rec = ada.getReceptionistbyId(recid);

            if (editrecnametextBox.Text == rec.Name && editrecaddresscomboBox.SelectedItem.ToString() == rec.Address && editreccontacttextBox.Text == rec.Contactno.ToString() && Convert.ToDateTime(editrecdateTimePicker.Text) == rec.JoiningDate && editrecsaltextBox.Text == rec.Salary.ToString() && editrecdestextBox.Text == rec.Designation && editrecemailtextBox.Text==rec.Email)
            {
                MessageBox.Show("No New Changes.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(editrecnametextBox.Text))
                {
                    MessageBox.Show("Name Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editrecnametextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editreccontacttextBox.Text))
                {
                    MessageBox.Show("Contact Number Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editreccontacttextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editrecaddresscomboBox.Text))
                {
                    MessageBox.Show("Address Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editrecaddresscomboBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editrecsaltextBox.Text))
                {
                    MessageBox.Show("Salary Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editrecsaltextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editrecdestextBox.Text))
                {
                    MessageBox.Show("Designation Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editrecdestextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editrecemailtextBox.Text))
                {
                    MessageBox.Show("Email Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editrecemailtextBox.Focus();
                    return;
                }
                else
                {
                    if (editreccontacttextBox.Text.Length > 11)
                    {
                        MessageBox.Show("Invalid Contact");
                    }
                    else
                    {
                        ada.changeReceptionistProfile(recid, editrecemailtextBox.Text, editrecnametextBox.Text, editrecaddresscomboBox.SelectedItem.ToString(), editreccontacttextBox.Text, editrecsaltextBox.Text, editrecdateTimePicker.Text, editrecdestextBox.Text);
                        MessageBox.Show("Profile Updated Successfully.");
                    }
                }
            }
        }

        private void editroomsavebutton_Click(object sender, EventArgs e)
        {
            int roomid = Convert.ToInt32(editroomidlabel.Text);
            Room room = new Room();
            room = ada.getRoomById(roomid);

            if (editroomtypecomboBox.SelectedItem.ToString() == room.RoomType && editroomcosttextBox.Text==room.RoomCost.ToString())
            {
                MessageBox.Show("No New Changes.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(editroomcosttextBox.Text))
                {
                    MessageBox.Show("Room Cost Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editroomcosttextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editroomtypecomboBox.Text))
                {
                    MessageBox.Show("Room Type Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editrecaddresscomboBox.Focus();
                    return;
                }
                else
                {
                    ada.editRoom(roomid,editroomcosttextBox.Text,editroomtypecomboBox.SelectedItem.ToString());
                    MessageBox.Show("Profile Updated Successfully.");
                }
            }
        }

        private void patientbutton_Click(object sender, EventArgs e)
        {
            patientdataGridView.DataSource = null;
            patientdataGridView.Rows.Clear();
            List<Patient> plist = ada.getAllPatient();
            foreach (var item in plist)
            {
                patientdataGridView.Rows.Add(item.Name, item.Address, item.Contactno,"details", "edit", "delete",item.id);
            }
            int total = rda.getTotalPatient();
            patienttotallabel.Text = total.ToString();
            patientsearchcomboBox.Items.Clear();
            patientsearchcomboBox.Items.Add("Name");
            patientsearchcomboBox.Items.Add("Address");
            patientsearchcomboBox.Text = "";
            patientsearchtextBox.Text = "";
            admintabControl.SelectedTab = patienttab;
        }

        private void patientdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = ((System.Windows.Forms.DataGridView)(sender)).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            int patientid = Convert.ToInt32(patientdataGridView.Rows[e.RowIndex].Cells[6].Value);
            if (str == "edit")
            {
                Patient patient = new Patient();
                patient = ada.getPatientbyId(patientid);
                editpatientnametextBox.Text = patient.Name;
                editpatientaddresscomboBox.Items.Clear();
                editpatientaddresscomboBox.Text = "";
                List<string> addresslist = new List<string>();
                addresslist = rda.addresscombo();
                foreach (var address in addresslist)
                {
                    editpatientaddresscomboBox.Items.Add(address);
                }
                editpatientaddresscomboBox.SelectedItem = patient.Address;
                editpatientcontacttextBox.Text = patient.Contactno.ToString();
                editpatientdobdateTimePicker.Text = patient.DOB.ToString();
                editpatientidlabel.Text = patientid.ToString();
                if (patient.Gender == "Male")
                {
                    editpatientmaleradioButton.Checked = true;
                }
                else if (patient.Gender == "Female")
                {
                    editpatientfemaleradioButton.Checked = true;
                }
                admintabControl.SelectedTab = editpatienttab;
            }
            else if (str == "details")
            {
                Patient patient = new Patient();
                int roomno = ada.getRoom(patientid);
                patient = ada.getPatientfromView(patientid);
                string rname = ada.getRec(patientid);
                detailpatientassistedlabeladmin.Text = rname;
                detailpatientidlabel.Text = patientid.ToString();
                detailpatientnamelabel.Text = patient.Name;
                detailpatientaddresslabel.Text = patient.Address;
                detailpatientgenderlabel.Text = patient.Gender;
                detailpatientdoblabel.Text = patient.DOB.ToString();
                detailpatientcontactlabel.Text = patient.Contactno.ToString();
                detailpatientstatuslabel.Text = patient.Status;
                detailpatientroomlabel.Text = roomno.ToString();
                detailpatientconditionlabel.Text = patient.Condition;
                detailpatientadmitdatelabel.Text = patient.Admitdate.ToString();
                detailpatientreleasedatedatelabel.Text = patient.Releasedate.ToString();
                admintabControl.SelectedTab = detailpatienttab;
            }
            else if (str == "delete")
            {
                DialogResult dialogrslt = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.OKCancel);
                if (dialogrslt == DialogResult.OK)
                {
                    ada.deletePatient(patientid);
                    patientdataGridView.DataSource = null;
                    patientdataGridView.Rows.Clear();
                    List<Patient> plist = ada.getAllPatient();
                    foreach (var item in plist)
                    {
                        patientdataGridView.Rows.Add(item.Name, item.Address, item.Contactno, "details", "edit", "delete", item.id);
                    }
                    int total = rda.getTotalPatient();
                    patienttotallabel.Text = total.ToString();
                }
            }
        }

        private void editpatientbacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            patientdataGridView.DataSource = null;
            patientdataGridView.Rows.Clear();
            List<Patient> plist = ada.getAllPatient();
            foreach (var item in plist)
            {
                patientdataGridView.Rows.Add(item.Name, item.Address, item.Contactno, "details", "edit", "delete", item.id);
            }
            admintabControl.SelectedTab = patienttab;
        }

        private void editpatientsavebutton_Click(object sender, EventArgs e)
        {
            int patientid = Convert.ToInt32(editpatientidlabel.Text);
            Patient patient = new Patient();
            patient = ada.getPatientbyId(patientid);
            string gender = "";

            if (editpatientnametextBox.Text == patient.Name && editpatientaddresscomboBox.SelectedItem.ToString() == patient.Address && editpatientcontacttextBox.Text == patient.Contactno.ToString() && Convert.ToDateTime(editpatientdobdateTimePicker.Text) == patient.DOB && (editpatientmaleradioButton.Text == patient.Gender || editpatientfemaleradioButton.Text == patient.Gender))
            {
                MessageBox.Show("No New Changes.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(editpatientnametextBox.Text))
                {
                    MessageBox.Show("Name Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editpatientnametextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editpatientcontacttextBox.Text))
                {
                    MessageBox.Show("Contact Number Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editpatientcontacttextBox.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(editpatientaddresscomboBox.Text))
                {
                    MessageBox.Show("Address Can Not Be Empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editpatientaddresscomboBox.Focus();
                    return;
                }
                else if (editpatientmaleradioButton.Checked == false && editpatientfemaleradioButton.Checked == false)
                {
                    MessageBox.Show("Select gender.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    editpatientmaleradioButton.Focus();
                    editpatientfemaleradioButton.Focus();
                    return;
                }
                else
                {
                    if (editpatientmaleradioButton.Checked == true)
                    {
                        gender = "Male";
                    }
                    else if (editpatientfemaleradioButton.Checked == true)
                    {
                        gender = "Female";
                    }
                    if (editpatientcontacttextBox.Text.Length > 11)
                    {
                        MessageBox.Show("Invalid Contactno");
                    }
                    else
                    {
                        ada.editPatient(patientid, editpatientnametextBox.Text, editpatientcontacttextBox.Text, editpatientaddresscomboBox.SelectedItem.ToString(), gender, editpatientdobdateTimePicker.Text);
                        MessageBox.Show("Profile Updated Successfully.");
                    }
                }
            }
        }

        private void detailpatientbacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            patientdataGridView.DataSource = null;
            patientdataGridView.Rows.Clear();
            List<Patient> plist = ada.getAllPatient();
            foreach (var item in plist)
            {
                patientdataGridView.Rows.Add(item.Name, item.Address, item.Contactno, "details", "edit", "delete", item.id);
            }
            admintabControl.SelectedTab = patienttab;
        }

        private void detailspatienteditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                int patientid=Convert.ToInt32(detailpatientidlabel.Text);
                Patient patient = new Patient();
                patient = ada.getPatientbyId(patientid);
                editpatientnametextBox.Text = patient.Name;
                editpatientaddresscomboBox.Items.Clear();
                editpatientaddresscomboBox.Text = "";
                List<string> addresslist = new List<string>();
                addresslist = rda.addresscombo();
                foreach (var address in addresslist)
                {
                    editpatientaddresscomboBox.Items.Add(address);
                }
                editpatientaddresscomboBox.SelectedItem = patient.Address;
                editpatientcontacttextBox.Text = patient.Contactno.ToString();
                editpatientdobdateTimePicker.Text = patient.DOB.ToString();
                editpatientidlabel.Text = patientid.ToString();
                if (patient.Gender == "Male")
                {
                    editpatientmaleradioButton.Checked = true;
                }
                else if (patient.Gender == "Female")
                {
                    editpatientfemaleradioButton.Checked = true;
                }
                admintabControl.SelectedTab = editpatienttab;
        }

        private void doctorsearchbutton_Click(object sender, EventArgs e)
        {
            if (doctorsearchbycomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select your search type");
            }
            else
            {
                if (doctorsearchtextBox.Text == "")
                {
                    MessageBox.Show("Please type your search");
                }
                else
                {
                    if (doctorsearchbycomboBox.SelectedItem.ToString() == "Name")
                    {
                        doctordataGridView.DataSource = null;
                        doctordataGridView.Rows.Clear();
                        List<Doctor> dlist = rda.getDoctorByName(doctorsearchtextBox.Text);
                        if (dlist.Count != 0)
                        {
                            foreach (var item in dlist)
                            {
                                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                    else if (doctorsearchbycomboBox.SelectedItem.ToString() == "Designation")
                    {
                        doctordataGridView.DataSource = null;
                        doctordataGridView.Rows.Clear();
                        List<Doctor> dlist = rda.getDoctorByDes(doctorsearchtextBox.Text);
                        if (dlist.Count != 0)
                        {
                            foreach (var item in dlist)
                            {
                                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                }
            }
        }

        private void nursesearchbutton_Click(object sender, EventArgs e)
        {
            if (nursesearchcomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select your search type");
            }
            else
            {
                if (nursesearchtextBox.Text == "")
                {
                    MessageBox.Show("Please type your search");
                }
                else
                {
                    if (nursesearchcomboBox.SelectedItem.ToString() == "Name")
                    {
                        nursedataGridView.DataSource = null;
                        nursedataGridView.Rows.Clear();
                        List<Nurse> nlist = rda.getNurseByName(nursesearchtextBox.Text);
                        if (nlist.Count != 0)
                        {
                            foreach (var item in nlist)
                            {
                                nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                    else if (nursesearchcomboBox.SelectedItem.ToString() == "Designation")
                    {
                        nursedataGridView.DataSource = null;
                        nursedataGridView.Rows.Clear();
                        List<Nurse> nlist = rda.getNurseByDes(nursesearchtextBox.Text);
                        if (nlist.Count != 0)
                        {
                            foreach (var item in nlist)
                            {
                                nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                }
            }
        }

        private void recsearchbutton_Click(object sender, EventArgs e)
        {
            if (recsearchcomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select your search type");
            }
            else
            {
                if (recsearchtextBox.Text == "")
                {
                    MessageBox.Show("Please type your search");
                }
                else
                {
                    if (recsearchcomboBox.SelectedItem.ToString() == "Name")
                    {
                        recdataGridView.DataSource = null;
                        recdataGridView.Rows.Clear();
                        List<Receptionist> rlist = ada.getReceptionistByName(recsearchtextBox.Text);
                        if (rlist.Count != 0)
                        {
                            foreach (var item in rlist)
                            {
                                recdataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                    else if (recsearchcomboBox.SelectedItem.ToString() == "Designation")
                    {
                        recdataGridView.DataSource = null;
                        recdataGridView.Rows.Clear();
                        List<Receptionist> rlist = ada.getReceptionistByDes(recsearchtextBox.Text);
                        if (rlist.Count != 0)
                        {
                            foreach (var item in rlist)
                            {
                                recdataGridView.Rows.Add(item.Name, item.Contactno, item.Designation, "details", "edit", "delete", item.Id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                }
            }
        }

        private void editrecbacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            admintabControl.SelectedTab = receptionisttab;
        }

        private void patientsearchbutton_Click(object sender, EventArgs e)
        {
            if (patientsearchcomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select your search type");
            }
            else
            {
                if (patientsearchtextBox.Text == "")
                {
                    MessageBox.Show("Please type your search");
                }
                else
                {
                    if (patientsearchcomboBox.SelectedItem.ToString() == "Name")
                    {
                        patientdataGridView.DataSource = null;
                        patientdataGridView.Rows.Clear();
                        List<Patient> plist = ada.getPatientByName(patientsearchtextBox.Text);
                        if (plist.Count != 0)
                        {
                            foreach (var item in plist)
                            {
                                patientdataGridView.Rows.Add(item.Name, item.Address, item.Contactno, "details", "edit", "delete", item.id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                    else if (patientsearchcomboBox.SelectedItem.ToString() == "Address")
                    {
                        patientdataGridView.DataSource = null;
                        patientdataGridView.Rows.Clear();
                        List<Patient> plist = ada.getPatientByaddress(patientsearchtextBox.Text);
                        if (plist.Count != 0)
                        {
                            foreach (var item in plist)
                            {
                                patientdataGridView.Rows.Add(item.Name, item.Address, item.Contactno, "details", "edit", "delete", item.id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                }
            }
        }

        private void roomsearchbutton_Click(object sender, EventArgs e)
        {
            if (roomsearchcomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select your search type");
            }
            else
            {
                if (roomsearchtextBox.Text == "")
                {
                    MessageBox.Show("Please type your search");
                }
                else
                {
                    if (roomsearchcomboBox.SelectedItem.ToString() == "Roomno")
                    {
                        roomdataGridView.DataSource = null;
                        roomdataGridView.Rows.Clear();
                        List<Room> rlist = rda.getRoomByRoomno(roomsearchtextBox.Text);
                        if (rlist.Count != 0)
                        {
                            foreach (var item in rlist)
                            {
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus, "edit", "delete");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                    else if (roomsearchcomboBox.SelectedItem.ToString() == "Roomtype")
                    {
                        roomdataGridView.DataSource = null;
                        roomdataGridView.Rows.Clear();
                        List<Room> rlist = rda.getRoomByRoomtype(roomsearchtextBox.Text);
                        if (rlist.Count != 0)
                        {
                            foreach (var item in rlist)
                            {
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus, "edit", "delete");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }

                    else if (roomsearchcomboBox.SelectedItem.ToString() == "Roomstatus")
                    {
                        roomdataGridView.DataSource = null;
                        roomdataGridView.Rows.Clear();
                        List<Room> rlist = rda.getRoomByRoomstatus(roomsearchtextBox.Text);
                        if (rlist.Count != 0)
                        {
                            foreach (var item in rlist)
                            {
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus, "edit", "delete");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }

                    else if (roomsearchcomboBox.SelectedItem.ToString() == "Roomcost")
                    {
                        roomdataGridView.DataSource = null;
                        roomdataGridView.Rows.Clear();
                        List<Room> rlist = rda.getRoomByRoomcost(roomsearchtextBox.Text);
                        if (rlist.Count != 0)
                        {
                            foreach (var item in rlist)
                            {
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus, "edit", "delete");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                }
            }
        }

        private void reportbutton_Click(object sender, EventArgs e)
        {
            admintabControl.SelectedTab = reporttab;
            activityreportpanel.Visible = true;
            activityrecdataGridView.DataSource = null;
            activityrecdataGridView.Rows.Clear();
            List<LoginInfo> list = new List<LoginInfo>();
            list=ada.getLoginInfoRec();
            foreach (var item in list)
            {
                activityrecdataGridView.Rows.Add(item.Name, item.loginDate);
            }
            adminreportdataGridView.DataSource = null;
            adminreportdataGridView.Rows.Clear();
            List<LoginInfo> alist = new List<LoginInfo>();
            alist = ada.getLoginInfoAdmin();
            foreach (var item in alist)
            {
                adminreportdataGridView.Rows.Add(item.Name, item.loginDate);
            }
        }

        private void activityreportlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            patientreportpanel.Visible = false;
            activityreportpanel.Visible = true;
            activityrecdataGridView.DataSource = null;
            activityrecdataGridView.Rows.Clear();
            List<LoginInfo> list = new List<LoginInfo>();
            list = ada.getLoginInfoRec();
            foreach (var item in list)
            {
                activityrecdataGridView.Rows.Add(item.Name, item.loginDate);
            }
            adminreportdataGridView.DataSource = null;
            adminreportdataGridView.Rows.Clear();
            List<LoginInfo> alist = new List<LoginInfo>();
            alist = ada.getLoginInfoAdmin();
            foreach (var item in alist)
            {
                adminreportdataGridView.Rows.Add(item.Name, item.loginDate);
            }
        }

        private void monthcomboBox_Click(object sender, EventArgs e)
        {
            List<string> mlist = new List<string>()
            {
                "JANUARY",
                "FEBRUARY",
                "MARCH",
                "APRIL",
                "MAY",
                "JUNE",
                "JULY",
                "AUGUST",
                "SEPTEMBER",
                "OCTOBER",
                "NOVEMBER",
                "DECEMBER"
            };
            monthcomboBox.Items.Clear();
            foreach (var item in mlist)
            {
                monthcomboBox.Items.Add(item);
            }
            
        }

        private void patientreportlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            patientreportpanel.Visible = true;
            monthcomboBox.Text = "";
            yearcomboBox.Text = "";
        }

        private void yearcomboBox_Click(object sender, EventArgs e)
        {
            yearcomboBox.Items.Clear();
            yearcomboBox.Items.Add(2012);
            yearcomboBox.Items.Add(2013);
            yearcomboBox.Items.Add(2014);
            yearcomboBox.Items.Add(2015);
            yearcomboBox.Items.Add(2016);
            yearcomboBox.Items.Add(2017);

        }

        private void patientreportsearchbutton_Click(object sender, EventArgs e)
        {
            if (monthcomboBox.SelectedItem == null ) 
            {
                MessageBox.Show("Please choose a month");
            }
            if(yearcomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please choose a year");
            }
            if (monthcomboBox.SelectedItem != null && yearcomboBox.SelectedItem != null)
            {
                List<Patient> plist = new List<Patient>();
                 plist=ada.getPatientreport(monthcomboBox.SelectedItem.ToString(), yearcomboBox.SelectedItem.ToString());
                 patientreportdataGridView.DataSource = null;
                 patientreportdataGridView.Rows.Clear();
                 if (plist.Count!=0)
                 {
                     foreach (var item in plist)
                     {
                         patientreportdataGridView.Rows.Add(item.Name, item.Address, item.Contactno);
                     }
                 }
                 else
                 {
                     MessageBox.Show("No Results Found");
                 }
                 totallabel.Text = plist.Count.ToString();
            }
        }
    }
}
