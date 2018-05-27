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
    public class ReceptionistDataAccess
    {
        public string Value = "";
        public string result = "";
        Connection dbcon = new Connection();
        OracleConnection conn = new OracleConnection();

        public LogIn login(string email, string password)
        {
            conn = dbcon.DB();
            conn.Open();
            LogIn log = new LogIn();
            OracleCommand objCmd = new OracleCommand("SELECT U_EMAIL,U_PASSWORD,U_TYPE FROM USER_TYPE where U_EMAIL='" + email + "'and U_PASSWORD='" + password + "'", conn);
            using (OracleDataReader reader = objCmd.ExecuteReader())
            {
                {
                    while (reader.Read())
                    {
                        log.Email = reader.GetValue(reader.GetOrdinal("U_EMAIL")).ToString();
                        log.Password = reader.GetValue(reader.GetOrdinal("U_PASSWORD")).ToString();
                        log.Type = reader.GetValue(reader.GetOrdinal("U_TYPE")).ToString();
                    }
                    reader.Close();
                    conn.Close();
                }
                return log;
            }
        }

        public Receptionist getProfile(string loginname)
        {
            conn = dbcon.DB();
            conn.Open();
            Receptionist receptionist = new Receptionist();
            OracleCommand objCmd = new OracleCommand("Select * from RECEPTIONIST where R_Email='" + loginname + "'", conn);
            using (OracleDataReader reader = objCmd.ExecuteReader())
            {
                {
                    while (reader.Read())
                    {
                        receptionist.Name = reader.GetValue(reader.GetOrdinal("R_NAME")).ToString();
                        receptionist.Address = reader.GetValue(reader.GetOrdinal("R_Address")).ToString();
                        receptionist.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("R_CONTACT_NO")));
                        receptionist.JoiningDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("R_JOINING_DATE")));
                        receptionist.Salary = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("R_SALARY")));
                        receptionist.Designation = reader.GetValue(reader.GetOrdinal("R_DESIGNATION")).ToString();
                    }
                    reader.Close();
                }
                conn.Close();
                return receptionist;
            }  
        }

        public List<Patient> patientdatagrid()
        {
            conn = dbcon.DB();
            conn.Open();
            List<Patient> plist = new List<Patient>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT P_NAME,P_CONTACT_NO,P_STATUS,P_ID,ROOM_NO FROM PATIENT_ROOM_VIEW", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Patient p = new Patient();
                        p.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                        p.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                        p.Status = reader.GetValue(reader.GetOrdinal("P_STATUS")).ToString();
                        p.id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_ID")));
                        p.RoomNo = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                        plist.Add(p);
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

        public List<Doctor> doctordatagrid() 
        {
            conn = dbcon.DB();
            conn.Open();
            List<Doctor> dlist = new List<Doctor>();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from DOCTOR", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Doctor doctor = new Doctor();
                        doctor.Name = reader.GetValue(reader.GetOrdinal("D_NAME")).ToString();
                        doctor.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("D_CONTACT_NO")));
                        doctor.Designation = reader.GetValue(reader.GetOrdinal("D_DESIGNATION")).ToString();
                        doctor.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("D_ID")));
                        dlist.Add(doctor);
                    }
                    reader.Close();  
                    conn.Close();
                    }
                }
            catch (Exception ex)
            {

            }
            return dlist;       
        }

        public int getTotalDoctor()
        {
            conn = dbcon.DB();
            conn.Open();
            int total = 0 ;
            try
            {
                OracleCommand cmd = new OracleCommand("select COUNT(*) as total from DOCTOR", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        total = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("total")));
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return total;
        }

        public int getTotalNurse()
        {
            conn = dbcon.DB();
            conn.Open();
            int total = 0;
            try
            {
                OracleCommand cmd = new OracleCommand("select COUNT(*) as total from NURSE", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        total = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("total")));
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return total;
        }

        public int getTotalRec()
        {
            conn = dbcon.DB();
            conn.Open();
            int total = 0;
            try
            {
                OracleCommand cmd = new OracleCommand("select COUNT(*) as total from RECEPTIONIST", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        total = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("total")));
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return total;
        }

        public int getTotalPatient()
        {
            conn = dbcon.DB();
            conn.Open();
            int total = 0;
            try
            {
                OracleCommand cmd = new OracleCommand("select COUNT(*) as total from PATIENT", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        total = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("total")));
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return total;
        }

        public int getTotalRoom()
        {
            conn = dbcon.DB();
            conn.Open();
            int total = 0;
            try
            {
                OracleCommand cmd = new OracleCommand("select COUNT(*) as total from ROOM", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        total = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("total")));
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return total;
        }


        public Doctor getDoctorbyId(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            Doctor doctor = new Doctor();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from DOCTOR where D_ID='"+id+"'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {                       
                        doctor.Name = reader.GetValue(reader.GetOrdinal("D_NAME")).ToString();
                        doctor.Address = reader.GetValue(reader.GetOrdinal("D_ADDRESS")).ToString();
                        doctor.JoiningDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("D_JOINING_DATE")));
                        doctor.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("D_CONTACT_NO")));
                        doctor.Salary = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("D_SALARY")));
                        doctor.Designation = reader.GetValue(reader.GetOrdinal("D_DESIGNATION")).ToString();
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return doctor;
        }

        public List<Nurse> nursedatagrid()
        {
            conn = dbcon.DB();
            conn.Open();
            List<Nurse> nlist = new List<Nurse>();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from NURSE", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Nurse nurse = new Nurse();
                        nurse.Name = reader.GetValue(reader.GetOrdinal("N_NAME")).ToString();
                        nurse.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("N_CONTACT_NO")));
                        nurse.Designation = reader.GetValue(reader.GetOrdinal("N_DESIGNATION")).ToString();
                        nurse.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("N_ID")));
                        nlist.Add(nurse);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return nlist;
        }

        public Nurse getNursebyId(int id)
        {
            conn = dbcon.DB();
            conn.Open();
            Nurse nurse = new Nurse();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from NURSE where N_ID='" + id + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nurse.Name = reader.GetValue(reader.GetOrdinal("N_NAME")).ToString();
                        nurse.Address = reader.GetValue(reader.GetOrdinal("N_ADDRESS")).ToString();
                        nurse.JoiningDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("N_JOINING_DATE")));
                        nurse.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("N_CONTACT_NO")));
                        nurse.Salary = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("N_SALARY")));
                        nurse.Designation = reader.GetValue(reader.GetOrdinal("N_DESIGNATION")).ToString();
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return nurse;
        }

        public List<Room> roomdatagrid()
        {
            conn = dbcon.DB();
            conn.Open();
            List<Room> rlist = new List<Room>();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from ROOM", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Room room = new Room();
                        room.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                        room.RoomType = reader.GetValue(reader.GetOrdinal("ROOM_TYPE")).ToString();
                        room.RoomCost = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("ROOM_COST")));
                        room.RoomStatus = reader.GetValue(reader.GetOrdinal("ROOM_STATUS")).ToString();
                        rlist.Add(room);
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

        public List<int> roomnocombo()
        {
            conn = dbcon.DB();
            conn.Open();
            List<int> roomnolist = new List<int>();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from ROOM where ROOM_STATUS='empty'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomnolist.Add(Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO"))));
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return roomnolist;
        }

        public List<string> addresscombo()
        {
            conn = dbcon.DB();
            conn.Open();
            List<string> addresslist = new List<string>();
            try
            {
                OracleCommand cmd = new OracleCommand("select * from ADDRESS", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        addresslist.Add(reader.GetValue(reader.GetOrdinal("ADDRESS_NAME")).ToString());
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return addresslist;
        }
        

        public void patient_admit(string name,string address,string gender,string dob,string contact,string status,string room,string loginname)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("PATIENT_ADMIT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pname", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add("paddress", OracleDbType.NVarchar2).Value = address;
                cmd.Parameters.Add("pgender", OracleDbType.NVarchar2).Value = gender;
                cmd.Parameters.Add("pdob", OracleDbType.Date).Value = Convert.ToDateTime(dob);
                cmd.Parameters.Add("pcontact", OracleDbType.Int32).Value = Convert.ToInt32(contact);
                cmd.Parameters.Add("pstatus", OracleDbType.NVarchar2).Value = status;
                cmd.Parameters.Add("loginname", OracleDbType.Varchar2).Value = loginname;
                if (room != "")
                {
                    cmd.Parameters.Add("roomno", OracleDbType.Int32).Value = Convert.ToInt32(room);
                }
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void patient_checkout(int id)
        {
            int pid = Convert.ToInt32(id);
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("PATIENT_CHECKOUT ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PATIENTID", OracleDbType.Int32).Value = pid;
                cmd.ExecuteNonQuery();
                conn.Close();
            }           
            catch (Exception ex)
            {
            }
        }

        public Receptionist checkOldPassword(string loginname)
        {
            conn = dbcon.DB();
            conn.Open();
            Receptionist rec = new Receptionist();
            OracleCommand objCmd = new OracleCommand("Select U_PASSWORD from USER_TYPE where U_EMAIL='" + loginname + "'", conn);
            using (OracleDataReader reader = objCmd.ExecuteReader())
            {
                {
                    while (reader.Read())
                    {
                        rec.Password = reader.GetValue(reader.GetOrdinal("U_PASSWORD")).ToString();
                    }
                    reader.Close();
                }
                conn.Close();
                return rec;
            }
        }

        public void changePassword(string loginname, string newPassword)
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

        public List<Doctor> getDoctorByName(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Doctor> dlist = new List<Doctor>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM DOCTOR WHERE D_NAME='"+name+"'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Doctor doctor = new Doctor();
                            doctor.Name = reader.GetValue(reader.GetOrdinal("D_NAME")).ToString();
                            doctor.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("D_CONTACT_NO")));
                            doctor.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("D_ID")));
                            doctor.Designation = reader.GetValue(reader.GetOrdinal("D_DESIGNATION")).ToString();
                            dlist.Add(doctor);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dlist;
        }

        public List<Doctor> getDoctorByDes(string des)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Doctor> dlist = new List<Doctor>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM DOCTOR WHERE D_DESIGNATION='" + des + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Doctor doctor = new Doctor();
                            doctor.Name = reader.GetValue(reader.GetOrdinal("D_NAME")).ToString();
                            doctor.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("D_CONTACT_NO")));
                            doctor.Designation = reader.GetValue(reader.GetOrdinal("D_DESIGNATION")).ToString();
                            doctor.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("D_ID")));
                            dlist.Add(doctor);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dlist;
        }

        public List<Patient> getPatientByName(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Patient> plist = new List<Patient>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT P.*,A.ROOM_NO FROM PATIENT P,ASSIGNED_ROOM A WHERE P.P_ID=A.P_ID AND P_CONDITION='Not Released' AND P_NAME='" + name + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient();
                            patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                            patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                            patient.Status = reader.GetValue(reader.GetOrdinal("P_STATUS")).ToString();
                            patient.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
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

        public List<Patient> getPatientByroom(string roomno)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Patient> plist = new List<Patient>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT P.*,A.ROOM_NO FROM PATIENT P,ASSIGNED_ROOM A WHERE P.P_ID=A.P_ID AND P_CONDITION='Not Released' AND A.ROOM_NO='" + roomno + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient();
                            patient.Name = reader.GetValue(reader.GetOrdinal("P_NAME")).ToString();
                            patient.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("P_CONTACT_NO")));
                            patient.Status = reader.GetValue(reader.GetOrdinal("P_STATUS")).ToString();
                            patient.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
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

        public List<Nurse> getNurseByName(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Nurse> nlist = new List<Nurse>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM NURSE WHERE N_NAME='" + name + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Nurse nurse = new Nurse();
                            nurse.Name = reader.GetValue(reader.GetOrdinal("N_NAME")).ToString();
                            nurse.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("N_CONTACT_NO")));
                            nurse.Designation = reader.GetValue(reader.GetOrdinal("N_DESIGNATION")).ToString();
                            nurse.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("N_ID")));
                            nlist.Add(nurse);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return nlist;
        }

        public List<Nurse> getNurseByDes(string name)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Nurse> nlist = new List<Nurse>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM NURSE WHERE N_DESIGNATION='" + name + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Nurse nurse = new Nurse();
                            nurse.Name = reader.GetValue(reader.GetOrdinal("N_NAME")).ToString();
                            nurse.Contactno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("N_CONTACT_NO")));
                            nurse.Designation = reader.GetValue(reader.GetOrdinal("N_DESIGNATION")).ToString();
                            nurse.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("N_ID")));
                            nlist.Add(nurse);
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return nlist;
        }

        public List<Room> getRoomByRoomno(string roomno)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Room> rlist = new List<Room>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM ROOM WHERE ROOM_NO='" + roomno + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Room room = new Room();
                            room.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                            room.RoomType = reader.GetValue(reader.GetOrdinal("ROOM_TYPE")).ToString();
                            room.RoomStatus = reader.GetValue(reader.GetOrdinal("ROOM_STATUS")).ToString();
                            room.RoomCost= Convert.ToDouble(reader.GetValue(reader.GetOrdinal("ROOM_COST")));
                            rlist.Add(room);
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

        public List<Room> getRoomByRoomtype(string type)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Room> rlist = new List<Room>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM ROOM WHERE ROOM_TYPE='" + type + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Room room = new Room();
                            room.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                            room.RoomType = reader.GetValue(reader.GetOrdinal("ROOM_TYPE")).ToString();
                            room.RoomStatus = reader.GetValue(reader.GetOrdinal("ROOM_STATUS")).ToString();
                            room.RoomCost = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("ROOM_COST")));
                            rlist.Add(room);
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

        public List<Room> getRoomByRoomstatus(string status)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Room> rlist = new List<Room>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM ROOM WHERE ROOM_STATUS='" + status + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Room room = new Room();
                            room.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                            room.RoomType = reader.GetValue(reader.GetOrdinal("ROOM_TYPE")).ToString();
                            room.RoomStatus = reader.GetValue(reader.GetOrdinal("ROOM_STATUS")).ToString();
                            room.RoomCost = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("ROOM_COST")));
                            rlist.Add(room);
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

        public List<Room> getRoomByRoomcost(string cost)
        {
            conn = dbcon.DB();
            conn.Open();
            List<Room> rlist = new List<Room>();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM ROOM WHERE ROOM_COST='" + cost + "'", conn);
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            Room room = new Room();
                            room.Roomno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ROOM_NO")));
                            room.RoomType = reader.GetValue(reader.GetOrdinal("ROOM_TYPE")).ToString();
                            room.RoomStatus = reader.GetValue(reader.GetOrdinal("ROOM_STATUS")).ToString();
                            room.RoomCost = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("ROOM_COST")));
                            rlist.Add(room);
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

        public void setLogInfo(string email)
        {
            conn = dbcon.DB();
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("LOGIN_INFO_PROCEDURE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("email", OracleDbType.NVarchar2).Value = email;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
