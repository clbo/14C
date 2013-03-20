using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BL;

/// <summary>
/// Summary description for DAL
/// </summary>
/// 
namespace DAL
{
    #region Abstract connection
    public abstract class DALConnection
    {
        protected string StoredProcedureProperty { get; set; }
        protected DataTable DataTableProperty { get; set; }

        protected SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
        protected SqlCommand cmd = new SqlCommand();

        protected void Connection()
        {
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedureProperty;

        }

        protected void TilfoejParameters()
        {
            if (DataTableProperty != null)
            {
                DataRow[] result = DataTableProperty.Select();
                foreach (DataRow row in result)
                {
                    for (int i = 1; i <= row.ItemArray.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + i.ToString(), row[i - 1]);
                    }

                }
            }
        }

        protected void ExNoQuery()
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
    #endregion

    #region Create, Update, Delete
    /// <summary>
    /// <para>Tager sig af alle Insert, Update og Delete forespørgelser</para>
    /// <para>Parametre: StoredProcedure og DataTable indeholdende parametre</para>
    /// </summary>
    public class DALCreateUpdateDelete : DALConnection
    {
        /// <summary>
        /// Constructor
        /// <para>Sætter StoredProcedure og Parametre</para>
        /// </summary>
        /// <param name="storedprocedure">Navnet på StoredProcedure (string)</param>
        /// <param name="datatable">Parametre der skal sendes med forespørgelsen (DataTable)</param>
        public DALCreateUpdateDelete(string storedprocedure, DataTable datatable)
        {
            StoredProcedureProperty = storedprocedure;
            DataTableProperty = datatable;
        }

        public void CreateUpdateDelete()
        {
            Connection();
            TilfoejParameters();
            ExNoQuery();
        }

    }
    #endregion

    #region Read (SELECT)
    /// <summary>
    /// <para>Select fra database.</para> 
    /// <para>Parametre: StoredProcedure og evt. DataTable indeholdende parametre</para>
    /// <para>Returnerer en Datatable med udtrækket</para>
    /// </summary>

    public class DALRead : DALConnection
    {
        /// <summary>
        /// The <paramref name="storedprocedure"/> parameter takes a string.
        /// </summary>
        /// <param name="storedprocedure">Navnet på din StoredProcedure</param>
        public DALRead(string storedprocedure)
        {
            StoredProcedureProperty = storedprocedure;
        }
        /// <summary>
        /// The The <paramref name="storedprocedure"/> parameter takes a string.
        /// </summary>
        /// <param name="storedprocedure"></param>
        /// <param name="datatable"></param>
        public DALRead(string storedprocedure, DataTable datatable)
        {
            StoredProcedureProperty = storedprocedure;
            DataTableProperty = datatable;
        }
        /// <summary>
        /// Read main method når der skal selectes
        /// </summary>
        /// <returns>Returns a DataTable with the query</returns>
        public DataTable Read()
        {
            Connection();
            TilfoejParameters();
            return Reader();
        }

        #region Private Methods
        private DataTable Reader()
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Tæl kolonnerne i udtrækket fra databasen
            int kolonner = reader.FieldCount;

            // Opret DataTable med X antal Kolonner  - Kalder statisk metode i BL namespace, CrateDataTable Class
            DataTable table = CreateDataTable.CreateDataTabelMethod(kolonner);

            while (reader.Read())
            {
                AddRowsToDatatableFromReader(reader, kolonner, table);
            }
            conn.Close();
            return table;
        }

        private static void AddRowsToDatatableFromReader(SqlDataReader reader, int kolonner, DataTable table)
        {
            object[] objarray = new object[kolonner];
            for (int i = 0; i < kolonner; i++)
            {
                objarray[i] = reader[i];
            }

            table.Rows.Add(objarray);
        } 
        #endregion
    }
    #endregion
}