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
    public partial class HospitalManagementForm : Form
    {
        AdminForm adminform;
        ReceptionistDataAccess rda = new ReceptionistDataAccess();
        AdminDataAccess ada = new AdminDataAccess();

        public static string loginName = "";
        public static string email = "";
        public static string password = "";


        public HospitalManagementForm()
        {
            InitializeComponent();
        }

        public HospitalManagementForm(AdminForm aform)
        {
            this.adminform = aform;
            InitializeComponent();
        }

        private void submitbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lnametextbox.Text))
            {
                MessageBox.Show("Please Enter Email.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lnametextbox.Focus();
                return;
            }

            else if (string.IsNullOrEmpty(lpasswordtextbox.Text))
            {
                MessageBox.Show("Please Enter Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lpasswordtextbox.Focus();
                return;
            }

            try
            {
                LogIn log = new LogIn();
                email = lnametextbox.Text;
                password = lpasswordtextbox.Text;
                log = (LogIn)rda.login(email, password);

                if (lnametextbox.Text == log.Email && lpasswordtextbox.Text == log.Password )
                {
                    rda.setLogInfo(log.Email);
                    if (log.Type == "RECEPTIONIST")
                    {
                        loginName = log.Email;
                        logintabControl.SelectedTab = receptionisttab;
                    }
                    else if (log.Type == "ADMIN")
                    {
                        loginName = log.Email;
                        this.Hide();
                        adminform=new AdminForm(this);
                        adminform.Show();
                        lnametextbox.Text = "";
                        lpasswordtextbox.Text = "";
                    }
                }

                else
                {
                    MessageBox.Show("Invalid UserName Or Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lpasswordtextbox.Focus();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rhomebutton_Click(object sender, EventArgs e)
        {
            rtabControl.SelectedTab = rdashboardtab;
        }

        private void rprofilebutton_Click(object sender, EventArgs e)
        {
            Receptionist rec = new Receptionist();
            rec = rda.getProfile(loginName);
            profilenamelabel.Text = rec.Name;
            profileaddresslabel.Text = rec.Address;
            profilecontactlabel.Text = rec.Contactno.ToString();
            profilejoinlabel.Text = rec.JoiningDate.ToString();
            profiledeslabel.Text = rec.Designation.ToString();
            rtabControl.SelectedTab = rprofiletab;
        }

        private void rpatientbutton_Click(object sender, EventArgs e)
        {
            pstatuscombobox.Items.Clear();
            pstatuscombobox.Text="";
            pstatuscombobox.Items.Add("In Door");
            pstatuscombobox.Items.Add("Out Door");

            paddresscomboBox.Items.Clear();
            paddresscomboBox.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                paddresscomboBox.Items.Add(address);
            }
            patientsearchcomboBox.Items.Clear();
            patientsearchcomboBox.Items.Add("Name");
            patientsearchcomboBox.Items.Add("Roomno");
            patientsearchcomboBox.Text = "";
            patientsearchtextBox.Text = "";
            rtabControl.SelectedTab = rpatienttab;
            patientregpanel.Visible = true;
        }

        private void patientreglinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pstatuscombobox.Items.Clear();
            pstatuscombobox.Text = "";
            pstatuscombobox.Items.Add("In Door");
            pstatuscombobox.Items.Add("Out Door");

            paddresscomboBox.Items.Clear();
            paddresscomboBox.Text = "";
            List<string> addresslist = new List<string>();
            addresslist = rda.addresscombo();
            foreach (var address in addresslist)
            {
                paddresscomboBox.Items.Add(address);
            }

            pinfopanel.Visible = false;
            patientregpanel.Visible = true;
        }

        private void PatientinfolinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pdataGridView.DataSource = null;
            pdataGridView.Rows.Clear();
            List<Patient> plist = rda.patientdatagrid();
            foreach (var item in plist)
            {
                pdataGridView.Rows.Add(item.Name, item.Contactno, item.Status, item.RoomNo ,"details","edit", "checkout",item.id);
               
            }
            pinfopanel.Visible = true;
        }

        private void rdoctorbutton_Click(object sender, EventArgs e)
        {
            doctordataGridView.DataSource = null;
            doctordataGridView.Rows.Clear();
            List<Doctor> dlist = rda.doctordatagrid();
            foreach (var item in dlist)
            {
                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation);
            }
            doctorsearchbycomboBox.Items.Clear();
            doctorsearchbycomboBox.Items.Add("Name");
            doctorsearchbycomboBox.Items.Add("Designation");
            doctorsearchbycomboBox.Text = "";
            doctorsearchtextBox.Text = "";
            rtabControl.SelectedTab = rdoctortab;
        }

        private void rnursebutton_Click(object sender, EventArgs e)
        {
            nursedataGridView.DataSource = null;
            nursedataGridView.Rows.Clear();
            List<Nurse> nlist = rda.nursedatagrid();
            foreach (var item in nlist)
            {
                nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation);
            }
            nursesearchcomboBox.Items.Clear();
            nursesearchcomboBox.Items.Add("Name");
            nursesearchcomboBox.Items.Add("Designation");
            nursesearchcomboBox.Text = "";
            nursesearchtextBox.Text = "";
            rtabControl.SelectedTab = rnursetab;
        }

        private void rroombutton_Click(object sender, EventArgs e)
        {
            roomdataGridView.DataSource = null;
            roomdataGridView.Rows.Clear();
            List<Room> rlist = rda.roomdatagrid();
            foreach (var item in rlist)
            {
                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost,item.RoomStatus);
            }
            roomsearchcomboBox.Items.Clear();
            roomsearchcomboBox.Items.Add("Roomno");
            roomsearchcomboBox.Items.Add("Roomtype");
            roomsearchcomboBox.Items.Add("Roomcost");
            roomsearchcomboBox.Items.Add("Roomstatus");
            roomsearchcomboBox.Text = "";
            roomsearchtextBox.Text = "";
            rtabControl.SelectedTab = rroomtab;
        }

        private void pdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = ((System.Windows.Forms.DataGridView)(sender)).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int patientid = Convert.ToInt32(pdataGridView.Rows[e.RowIndex].Cells[7].Value);

            if (str == "checkout")
            {
                DialogResult dialogrslt = MessageBox.Show("Are you sure to checkout?", "Checkout", MessageBoxButtons.OKCancel);
                if (dialogrslt == DialogResult.OK)
                {
                    rda.patient_checkout(patientid);
                    pdataGridView.DataSource = null;
                    pdataGridView.Rows.Clear();
                    List<Patient> plist = rda.patientdatagrid();
                    foreach (var item in plist)
                    {
                        pdataGridView.Rows.Add(item.Name, item.Contactno, item.Status, item.RoomNo,"details","edit", "checkout", item.id);
                    }
                }
                else if (dialogrslt == DialogResult.Cancel)
                {
                }
            }
            if (str == "details")
            {
                Patient patient = new Patient();
                int roomno = ada.getRoom(patientid);
                patient = ada.getPatientfromView(patientid);
                string rname = ada.getRec(patientid);
                detailpatientassistedlabel.Text = rname;
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
                rtabControl.SelectedTab = patientdetailtab;
            }
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
                rtabControl.SelectedTab = patientedittab;
            }
        }

        private void rlogoutbutton_Click(object sender, EventArgs e)
        {
            lnametextbox.Text = "";
            lpasswordtextbox.Text = "";
            rtabControl.SelectedTab = rdashboardtab;
            logintabControl.SelectedTab = Logintab;
        }

        private void patientregbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pnametextBox.Text))
            {
                MessageBox.Show("Please enter patient's name");
            }
            else if (paddresscomboBox.SelectedItem==null)
            {
                MessageBox.Show("Please enter patient's address");
            }
            else if (!pgenderradioButton.Checked && !pfemaleradioButton.Checked)
            {
                MessageBox.Show("Please select patient's gender");
            }
            else if (string.IsNullOrEmpty(pdobdateTimePicker.Text))
            {
                MessageBox.Show("Please enter patient's dateofbirth");
            }
            else if (string.IsNullOrEmpty(pcontacttextBox.Text))
            {
                MessageBox.Show("Please enter patient's contact number");
            }
            else if (pstatuscombobox.SelectedItem==null)
            {
                MessageBox.Show("Please enter patient's status");
            }
            
            else
            {
                string gender = "";
                if (pgenderradioButton.Checked == true)
                {
                    gender = pgenderradioButton.Text;
                }
                else if (pfemaleradioButton.Checked == true)
                {
                    gender = pfemaleradioButton.Text;
                }
                if (pcontacttextBox.Text.Length < 11)
                {
                    MessageBox.Show("Invalid Contactno");
                    if (pstatuscombobox.SelectedItem.ToString() == "In Door")
                    {
                        if (proomcomboBox.SelectedItem == null)
                        {
                            MessageBox.Show("Please enter patient's roomno");
                        }
                    }
                }
                else
                {
                    rda.patient_admit(pnametextBox.Text, paddresscomboBox.Text, gender, pdobdateTimePicker.Text, pcontacttextBox.Text, pstatuscombobox.Text, proomcomboBox.Text,loginName);
                    MessageBox.Show("Patient admitted successfully");
                    pnametextBox.Text = "";
                    pcontacttextBox.Text = "";
                    paddresscomboBox.Text = "";
                    pstatuscombobox.Text = "";
                    proomcomboBox.Text = "";
                    pgenderradioButton.Checked = false;
                    pfemaleradioButton.Checked = false;
                }
            }
       }

        private void pstatuscombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((pstatuscombobox.SelectedItem).ToString() == "Out Door")
            {
                proomcomboBox.Enabled = false;
            }
            if ((pstatuscombobox.SelectedItem).ToString() == "In Door")
            {
                proomcomboBox.Enabled = true;
            }
        }

        private void proomcomboBox_Click(object sender, EventArgs e)
        {
            proomcomboBox.Items.Clear();
            proomcomboBox.Text = "";
            List<int> roomnolist = new List<int>();
            roomnolist = rda.roomnocombo();
            foreach (var roomno in roomnolist)
            {
                proomcomboBox.Items.Add(roomno);
            }
        }

        private void HospitalManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void changepassbutton_Click(object sender, EventArgs e)
        {
            Receptionist rec = new Receptionist();
            rec = rda.checkOldPassword(loginName);

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
                if (currentpasstextBox.Text == rec.Password)
                {
                    if (newtextBox.Text != retypetextBox.Text)
                    {
                        MessageBox.Show("New Password & Re-typing New Password Does Not Match.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        retypetextBox.Focus();
                        return;
                    }
                    else
                    {
                        rda.changePassword(loginName, newtextBox.Text);
                        MessageBox.Show("Password changed successfully");
                        currentpasstextBox.Text = "";
                        newtextBox.Text = "";
                        retypetextBox.Text = "";
                    }
                }
                else if (currentpasstextBox.Text != rec.Password)
                {
                    MessageBox.Show("Current Password Does Not Match.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    currentpasstextBox.Focus();
                    return;
                }
            }
        }

        private void changepasslinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtabControl.SelectedTab = ChangePasswordtab;
        }

        private void backlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtabControl.SelectedTab = rprofiletab;
        }

        private void lpasswordtextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submitbutton.PerformClick();
            }
        }

        private void detailpatientbacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtabControl.SelectedTab = rpatienttab;
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
            rtabControl.SelectedTab = patientedittab;
        }

        private void editpatientbacklinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pdataGridView.DataSource = null;
            pdataGridView.Rows.Clear();
            List<Patient> plist = rda.patientdatagrid();
            foreach (var item in plist)
            {
                pdataGridView.Rows.Add(item.Name, item.Contactno, item.Status, item.RoomNo, "details", "edit", "checkout", item.id);
            }
            rtabControl.SelectedTab = rpatienttab;
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
                else if (editpatientaddresscomboBox.SelectedItem==null)
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
                        MessageBox.Show("Patient Updated Successfully.");
                    }
                }
            }
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
                                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation);
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
                                doctordataGridView.Rows.Add(item.Name, item.Contactno, item.Designation);
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
                        pdataGridView.DataSource = null;
                        pdataGridView.Rows.Clear();
                        List<Patient> plist = rda.getPatientByName(patientsearchtextBox.Text);
                        if (plist.Count != 0)
                        {
                            foreach (var item in plist)
                            {
                                pdataGridView.Rows.Add(item.Name, item.Contactno, item.Status, item.Roomno, "details", "edit", "checkout", item.id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No results found");
                        }
                    }
                    else if (patientsearchcomboBox.SelectedItem.ToString() == "Roomno")
                    {
                        pdataGridView.DataSource = null;
                        pdataGridView.Rows.Clear();
                        List<Patient> plist = rda.getPatientByroom(patientsearchtextBox.Text);
                        if (plist.Count != 0)
                        {
                            foreach (var item in plist)
                            {
                                pdataGridView.Rows.Add(item.Name, item.Contactno, item.Status, item.Roomno, "details", "edit", "checkout", item.id);
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
                                nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation);
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
                                nursedataGridView.Rows.Add(item.Name, item.Contactno, item.Designation);
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
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus);
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
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus);
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
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus);
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
                                roomdataGridView.Rows.Add(item.Roomno, item.RoomType, item.RoomCost, item.RoomStatus);
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
    }
}
