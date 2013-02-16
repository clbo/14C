using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class SletBilleder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton_slet_Command(object sender, CommandEventArgs e)
    {
        try
        {
            // slet filen i image/upload mappen
            File.Delete(Server.MapPath("~/Images/upload/" + e.CommandArgument));

            // slet filen i databasen
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Media WHERE ImageFileName = @ImageFileName";
            cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar).Value = e.CommandArgument;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            //Reload af siden så billederne opdateres
            Response.Redirect("SletBilleder.aspx");
        }
        catch (Exception)
        {
            Label_slet_msg.Text = "Billedet blev desværre <b>ikke</b> slettet";

        }

    }
}