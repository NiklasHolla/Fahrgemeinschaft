using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

namespace DataBaseWrapper
{
    public class CommandParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public CommandParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
    public class DataBase
    {
        OdbcConnection connection;

        public DataBase(string connectionString)
        {
            connection = new OdbcConnection(connectionString);
        }

        public bool IsOpen
        {
            get
            {
                return connection.State == ConnectionState.Open;
                
            }
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public DataTable RunQuery(string sqlCmd)
        {
            DataTable dt = new DataTable();
            OdbcDataAdapter da = new OdbcDataAdapter(sqlCmd, connection);
            da.Fill(dt);
            return dt;

        }

        public DataTable RunQuery(string sqlCmd, params CommandParameter[] parameters  )
        {
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sqlCmd, connection);
            
            foreach (CommandParameter param in parameters)
            {
                cmd.Parameters.Add(param);
            }

            OdbcDataAdapter da = new OdbcDataAdapter(sqlCmd, connection);

            da.Fill(dt);
            return dt;
        }

        public object RunQueryScalar(string sqlCmd)
        {
            OdbcCommand cmd = new OdbcCommand(sqlCmd, connection);

            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                   
                    return cmd.ExecuteScalar();
                }
                catch(Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// Für Insert, Update und Delete Befehle
        /// auch: CREATE, Drop, GRANT
        /// verhalten wie Adapter
        /// Verwendet die ExecuteNonQuery Methode des Command Objekts
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <returns></returns>
        public int  RunNonQuery(string sqlCmd)
        {
            // todo 
            int rtrn;
            OdbcCommand cmd = new OdbcCommand(sqlCmd);

            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();

                    rtrn = cmd.ExecuteNonQuery();
                    return rtrn;

                }
                finally
                {
                    connection.Close();
                }
            }

            rtrn = cmd.ExecuteNonQuery();
            return rtrn;
        }
    }
}
