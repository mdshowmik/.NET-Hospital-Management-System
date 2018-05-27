using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class Connection
    {
        static string strConnectionString = "Data Source=XE;User Id=scott;Password=tiger;";
        OracleConnection objConn;

        public OracleConnection DB()
        {
            if (objConn == null)
            {
                objConn = new OracleConnection(strConnectionString);
            }
            return objConn;
        }
    }
}
