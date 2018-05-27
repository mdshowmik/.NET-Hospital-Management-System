using BusinessLayer;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class AdminDataAccess
    {
        Connection dbcon = new Connection();
        OracleConnection conn = new OracleConnection();

        public Admin getProfile(string loginname)
        {
            conn = dbcon.DB();
            conn.Open();
            Admin admin = new Admin();
            OracleCommand objCmd = new OracleCommand("Select * from ADMIN where ADMIN_EMAIL='" + loginname + "'", conn);
            using (OracleDataReader reader = objCmd.ExecuteReader())
            {
                {
                    while (reader.Read())
                    {
                        admin.Name = reader.GetValue(reader.GetOrdinal("ADMIN_NAME")).ToString();
                        admin.Address = reader.GetValue(reader.GetOrdinal("ADMIN_ADDRESS")).ToString();
                        admin.Contact = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ADMIN_CONTACT_NO")));
                        admin.DateOfBirth = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("DATE_OF_BIRTH")));
                    }
                    reader.Close();
                }
                conn.Close();
                return admin;
            }

        }

        public void changeProfile(string loginname, string name, string address, string contact, string dob)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("ADMIN_PROFILE_UPDATE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UEMAIL", OracleDbType.Varchar2).Value = loginname;
                cmd.Parameters.Add("ANAME", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add("AADDRESS", OracleDbType.Varchar2).Value = address;
                cmd.Parameters.Add("ACONTACT", OracleDbType.Int32).Value = Convert.ToInt32(contact);
                cmd.Parameters.Add("DOB", OracleDbType.Date).Value = Convert.ToDateTime(dob);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void changeDoctorProfile(int id, string name, string address, string contact, string salary, string joindate, string designation)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("EDIT_PACKAGE.EDIT_DOCTOR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value =id;
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
                cmd.Parameters.Add("contact", OracleDbType.Int32).Value = Convert.ToInt32(contact);
                cmd.Parameters.Add("salary", OracleDbType.Double).Value = Convert.ToDouble(salary);
                cmd.Parameters.Add("dob", OracleDbType.Date).Value = Convert.ToDateTime(joindate);
                cmd.Parameters.Add("designation", OracleDbType.Varchar2).Value = designation;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void deleteDoctor(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE_PACKAGE.DELETE_DOCTOR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value =id;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public Admin checkOldPassword(string loginname)
        {
            conn = dbcon.DB();
            conn.Open();
            Admin admin = new Admin();
            OracleCommand objCmd = new OracleCommand("Select U_PASSWORD from USER_TYPE where U_EMAIL='" + loginname + "'", conn);
            using (OracleDataReader reader = objCmd.ExecuteReader())
            {
                {
                    while (reader.Read())
                    {
                        admin.Password = reader.GetValue(reader.GetOrdinal("U_PASSWORD")).ToString();
                    }
                    reader.Close();
                }
                conn.Close();
                return admin;
            }
        }

        public void changePassword(string loginname,string newPassword)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("ADMIN_CHANGE_PASSWORD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UPASSWORD", OracleDbType.NVarchar2).Value = newPassword;
                cmd.Parameters.Add("UEMAIL", OracleDbType.NVarchar2).Value = loginname;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public List<Receptionist> getAllReceptionist()
        {
            conn = dbcon.DB();
            conn.Open();
            List<Receptionist> rlist = new List<Receptionist>();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from RECEPTIONIST", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Receptionist rec = new Receptionist();
                        rec.Name = reader.GetValue(reader.GetOrdinal("R_NAME")).ToString();
                        rec.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_CONTACT_NO")));
                        rec.Designation = reader.GetValue(reader.GetOrdinal("R_DESIGNATION")).ToString();
                        rec.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_ID")));
                        rlist.Add(rec);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return rlist;
        }

        public Receptionist getReceptionistbyId(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            Receptionist rec = new Receptionist();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from RECEPTIONIST where R_ID='" + id + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rec.Name = reader.GetValue(reader.GetOrdinal("R_NAME")).ToString();
                        rec.Email = reader.GetValue(reader.GetOrdinal("R_EMAIL")).ToString();
                        rec.Address = reader.GetValue(reader.GetOrdinal("R_ADDRESS")).ToString();
                        rec.JoiningDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("R_JOINING_DATE")));
                        rec.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_CONTACT_NO")));
                        rec.Salary = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("R_SALARY")));
                        rec.Designation = reader.GetValue(reader.GetOrdinal("R_DESIGNATION")).ToString();
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return rec;
        }

        public void add_doctor(string name, string address, string contact, string joindate, string salary, string designation)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("ADD_DOCTOR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("dname", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add("daddress", OracleDbType.NVarchar2).Value = address;
                cmd.Parameters.Add("dcontact", OracleDbType.NVarchar2).Value = contact;
                cmd.Parameters.Add("djoindate", OracleDbType.Date).Value = Convert.ToDateTime(joindate);
                cmd.Parameters.Add("dsalary", OracleDbType.NVarchar2).Value = salary;
                cmd.Parameters.Add("ddesignation", OracleDbType.NVarchar2).Value = designation;

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void add_nurse(string name, string address, string contact, string joindate, string salary, string designation)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("ADD_NURSE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nname", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add("naddress", OracleDbType.NVarchar2).Value = address;
                cmd.Parameters.Add("ncontact", OracleDbType.NVarchar2).Value = contact;
                cmd.Parameters.Add("njoindate", OracleDbType.Date).Value = Convert.ToDateTime(joindate);
                cmd.Parameters.Add("nsalary", OracleDbType.NVarchar2).Value = salary;
                cmd.Parameters.Add("ndesignation", OracleDbType.NVarchar2).Value = designation;

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void changeNurseProfile(int id, string name, string address, string contact, string salary, string joindate, string designation)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("EDIT_PACKAGE.EDIT_NURSE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
                cmd.Parameters.Add("contact", OracleDbType.Int32).Value = Convert.ToInt32(contact);
                cmd.Parameters.Add("salary", OracleDbType.Double).Value = Convert.ToDouble(salary);
                cmd.Parameters.Add("dob", OracleDbType.Date).Value = Convert.ToDateTime(joindate);
                cmd.Parameters.Add("designation", OracleDbType.Varchar2).Value = designation;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void deleteNurse(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE_PACKAGE.DELETE_NURSE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void add_receptionist(string name, string email, string contact, string address, string joindate, string salary, string designation)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("ADD_RECEPTIONIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("rname", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add("remail", OracleDbType.NVarchar2).Value = email;
                cmd.Parameters.Add("raddress", OracleDbType.NVarchar2).Value = address;
                cmd.Parameters.Add("rcontact", OracleDbType.NVarchar2).Value = contact;
                cmd.Parameters.Add("rjoindate", OracleDbType.Date).Value = Convert.ToDateTime(joindate);
                cmd.Parameters.Add("rsalary", OracleDbType.NVarchar2).Value = salary;
                cmd.Parameters.Add("rdesignation", OracleDbType.NVarchar2).Value = designation;

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void changeReceptionistProfile(int id,string email, string name, string address, string contact, string salary, string joindate, string designation)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("EDIT_PACKAGE.EDIT_RECEPTIONIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = email;
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
                cmd.Parameters.Add("contact", OracleDbType.Int32).Value = Convert.ToInt32(contact);
                cmd.Parameters.Add("salary", OracleDbType.Double).Value = Convert.ToDouble(salary);
                cmd.Parameters.Add("dob", OracleDbType.Date).Value = Convert.ToDateTime(joindate);
                cmd.Parameters.Add("designation", OracleDbType.Varchar2).Value = designation;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void deleteReceptionist(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE_PACKAGE.DELETE_RECEPTIONIST", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public List<string> roomtypecombo()
        {
            conn = dbcon.DB();
            conn.Open();
            List<string> roomtypelist = new List<string>();
            try
            {
                OracleCommand cmd = new OracleCommand("select distinct ROOM_TYPE from ROOM", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomtypelist.Add(reader.GetValue(reader.GetOrdinal("ROOM_TYPE")).ToString());
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return roomtypelist;
        }

        public void add_room(string roomtype, string roomcost)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("ADD_ROOM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("roomtype", OracleDbType.NVarchar2).Value = roomtype;
                cmd.Parameters.Add("roomcost", OracleDbType.NVarchar2).Value = roomcost;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public Room getRoomById(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            Room room = new Room();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from ROOM where ROOM_NO='" + id + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        room.Roomno =Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                        room.RoomType = reader.GetValue(reader.GetOrdinal("ROOM_TYPE")).ToString();
                        room.RoomCost =Convert.ToDouble(reader.GetValue(reader.GetOrdinal("ROOM_COST")));
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return room;
        }

        public void editRoom(int id, string roomcost, string roomtype)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("EDIT_PACKAGE.EDIT_ROOM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("roomid", OracleDbType.Int32).Value = id;
                cmd.Parameters.Add("roomtype", OracleDbType.Varchar2).Value = roomtype;
                cmd.Parameters.Add("roomcost", OracleDbType.Varchar2).Value = roomcost;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }

        }

        public void deleteRoom(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE_PACKAGE.DELETE_ROOM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public List<Patient> getAllPatient()
        {
            conn = dbcon.DB();
            conn.Open();
            List<Patient> plist = new List<Patient>();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from PATIENT", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Patient patient = new Patient();
                        patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                        patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                        patient.Address = reader.GetValue(reader.GetOrdinal("P_ADDRESS")).ToString();
                        patient.id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_ID")));
                        plist.Add(patient);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return plist;
        }

        public Patient getPatientbyId(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            Patient patient = new Patient();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from PATIENT where P_ID='" + id + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                        patient.Address = reader.GetValue(reader.GetOrdinal("P_ADDRESS")).ToString();
                        patient.DOB = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("P_DOB")));
                        patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                        patient.Gender = reader.GetValue(reader.GetOrdinal("P_GENDER")).ToString();
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return patient;
        }

        public void editPatient(int id, string name, string contact,string address,string gender,string dob)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("EDIT_PACKAGE.EDIT_PATIENT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("patinetid", OracleDbType.Int32).Value = id;
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add("contact", OracleDbType.Int32).Value = Convert.ToInt32(contact);
                cmd.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
                cmd.Parameters.Add("gender", OracleDbType.Varchar2).Value = gender;
                cmd.Parameters.Add("dob", OracleDbType.Date).Value = Convert.ToDateTime(dob);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }

        }

        public Patient getPatientfromView(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            Patient patient = new Patient();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from PATIENT_VIEW where P_ID='"+id+"'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patient.id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_ID")));
                        patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                        patient.Address = reader.GetValue(reader.GetOrdinal("P_ADDRESS")).ToString();
                        patient.Gender = reader.GetValue(reader.GetOrdinal("P_GENDER")).ToString();
                        patient.DOB = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("P_DOB")));
                        patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                        patient.Status = reader.GetValue(reader.GetOrdinal("P_STATUS")).ToString();
                        patient.Condition = reader.GetValue(reader.GetOrdinal("P_CONDITION")).ToString();
                        //patient.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                        patient.Admitdate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("ADMIT_DATE")));
                        patient.Releasedate = reader.GetValue(reader.GetOrdinal("RELEASE_DATE")) as DateTime?;
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return patient;
        }

        public string getRec(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            string  name = "";
            try
            {
                OracleCommand cmd = new OracleCommand("select * from ASSIST_PATIENT_VIEW where P_ID='" + id + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetValue(reader.GetOrdinal("R_NAME")).ToString();

                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return name;
        }
        public int getRoom(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            int room=0 ;
            try
            {
                OracleCommand cmd = new OracleCommand("select ROOM_NO from ASSIGNED_ROOM where P_ID='" + id + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        room = (reader.GetValue(reader.GetOrdinal("ROOM_NO")) as int?) ?? 0;
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return room;

        }

        public void deletePatient(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE_PACKAGE.DELETE_PATIENT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public List<Receptionist> getReceptionistByName(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Receptionist> rlist = new List<Receptionist>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM RECEPTIONIST WHERE R_NAME='" + name + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Receptionist rec = new Receptionist();
                            rec.Name = reader.GetValue(reader.GetOrdinal("R_NAME")).ToString();
                            rec.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_CONTACT_NO")));
                            rec.Designation = reader.GetValue(reader.GetOrdinal("R_DESIGNATION")).ToString();
                            rec.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_ID")));
                            rlist.Add(rec);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return rlist;
        }

        public List<Receptionist> getReceptionistByDes(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Receptionist> rlist = new List<Receptionist>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM RECEPTIONIST WHERE R_DESIGNATION='" + name + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Receptionist rec = new Receptionist();
                            rec.Name = reader.GetValue(reader.GetOrdinal("R_NAME")).ToString();
                            rec.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_CONTACT_NO")));
                            rec.Designation = reader.GetValue(reader.GetOrdinal("R_DESIGNATION")).ToString();
                            rec.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_ID")));
                            rlist.Add(rec);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return rlist;
        }

        public List<Patient> getPatientByName(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Patient> plist = new List<Patient>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * from PATIENT WHERE P_NAME='" + name + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient();
                            patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                            patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                            patient.Status = reader.GetValue(reader.GetOrdinal("P_STATUS")).ToString();
                            patient.id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_ID")));
                            plist.Add(patient);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return plist;
        }

        public List<Patient> getPatientByaddress(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Patient> plist = new List<Patient>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * from PATIENT where P_ADDRESS='" + name + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient();
                            patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                            patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                            patient.Address = reader.GetValue(reader.GetOrdinal("P_ADDRESS")).ToString();
                            patient.id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_ID")));
                            plist.Add(patient);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return plist;
        }

        public List<LoginInfo> getLoginInfoRec()
        {
            conn = dbcon.DB();
            conn.Open();
            List<LoginInfo> list = new List<LoginInfo>();
            try
            {
              OracleCommand cmd = new OracleCommand("SELECT * FROM LOGIN_VIEW_REC", conn);
              using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            LoginInfo li = new LoginInfo();
                            li.Name = reader.GetValue(reader.GetOrdinal("R_NAME")).ToString();
                            li.loginDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("LOGIN_DATE")));
                            list.Add(li);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<LoginInfo> getLoginInfoAdmin()
        {
            conn = dbcon.DB();
            conn.Open();
            List<LoginInfo> list = new List<LoginInfo>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM LOGIN_VIEW_ADMIN", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            LoginInfo li = new LoginInfo();
                            li.Name = reader.GetValue(reader.GetOrdinal("ADMIN_NAME")).ToString();
                            li.loginDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("LOGIN_DATE")));
                            list.Add(li);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Patient> getPatientreport(string month, string year)
        {

            conn = dbcon.DB();
            conn.Open();
            string mon = month.Substring(0, 3);
            int yr = Convert.ToInt32(year.Substring(1,3));
            List<Patient> plist = new List<Patient>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT P.P_NAME,P.P_ADDRESS,P.P_CONTACT_NO from PATIENT P,LOG_PATIENT L where L.PATIENT_ID=P.P_ID AND TO_CHAR(ADMIT_DATE,'MON')='" + mon + "'AND TO_CHAR(ADMIT_DATE,'YY')='" + yr + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient();
                            patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                            patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                            patient.Address = reader.GetValue(reader.GetOrdinal("P_ADDRESS")).ToString();
                            plist.Add(patient);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return plist;
        }

    }


}
