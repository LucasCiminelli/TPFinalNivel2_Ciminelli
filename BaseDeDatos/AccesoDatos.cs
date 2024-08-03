using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BaseDeDatos
{
    public class AccesoDatos
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public SqlDataReader Reader
        {
            get { return reader; }
        }

        public AccesoDatos()
        {
            this.connection = new SqlConnection("server=.;database=CATALOGO_DB; integrated security=true");
            this.command = new SqlCommand();
        }


        public void setQuery(string query)
        {
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
        }

        public void executeReader()
        {

            command.Connection = connection;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void executeAction()
        {
            command.Connection = connection;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void setParameter(string name, object value)
        {
            command.Parameters.AddWithValue(name, value);
        }

        public void closeConection()
        {
            if(reader!= null)
            {
                reader.Close();
                connection.Close();
            }
        }
    }
}
