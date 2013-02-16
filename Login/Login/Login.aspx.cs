using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Login normal
    protected void Button_login_Click(object sender, EventArgs e)
    {
        // opret forbindelsen til databasen
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        // opret et SqlCommand object
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Sql sætningen
        cmd.CommandText = "SELECT * FROM BRUGER WHERE Bruger_email = @brugeremail AND Bruger_password = @password";

        // Parametrene @brugernavn og @password i sql sætningen ovenover til føjes
        cmd.Parameters.Add("@brugeremail", SqlDbType.NVarChar).Value = TextBox_email.Text;
        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = TextBox_pass.Text;

        // åben forbindelsen til databasen
        conn.Open();

        // opret en SqlDataReader og navngiv den "reader"
        SqlDataReader reader = cmd.ExecuteReader();

        // hvis brugeren findes i databasen - hvis der er noget i "reader"
        if (reader.Read())
        {
            // opret din session som er "beviset" på at du er logget ind
            Session["bruger_id"] = reader["id"].ToString();
            // Send brugeren videre til administrationssiden
            Response.Redirect("Admin/Default.aspx");
        }
        else // hvis brugeren ikke findes i databasen
        {
            // meddelelse til brugeren om at han ikke er logget ind
            Label_error_msg.Text = "Kombinationen af din email og password findes ikke i databasen";

            //Response.Redirect("Default.aspx");
        }

        // luk forbindelsen til databasen
        conn.Close();


    }

    #endregion

    #region login med Sha1 kryptering
    protected void Button_Kryptering_Click(object sender, EventArgs e)
    {
        // Det er selvfølgelig nødvendigt at brugerens password i databasen er krypteret for at dette vil virke.
        // Derfor skal der også laves en kryptering af passwordet når man opretter brugeren (på siden Admin/OpretBruger.aspx)
        // Du kan også kryptere paswordet på en side som "http://www.sha1-online.com/" og herefter ligge det i databasen 
        // (hvis du ikke har en side som OpretBruger.aspx)
        // En Sha1 kryptering af passwordet "1234" er 7110eda4d09e062aa5e4a390b0a572ac0d2c0220

        //Lav det indtastede password om til SHA1 og gem det i variabel
        string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox_pass.Text, "SHA1");


        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM BRUGER WHERE Bruger_email = @brugeremail AND Bruger_Password = @password";
        cmd.Parameters.Add("@brugeremail", SqlDbType.NVarChar).Value = TextBox_email.Text;
        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = hashedPassword;
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Session["bruger_id"] = reader["id"].ToString();
            Response.Redirect("Admin/Default.aspx");
        }
        else
        {
            Label_error_msg.Text = "Kombinationen af din email og password findes ikke i databasen";
        }
        conn.Close();
    }

    #endregion
    
    // login med bruger og rolleinfo
    // på samme måde som ovenover, men med flere sessions som indeholder flere bruger informationer hver især
    // eller med en session som indeholder en Datatable eller en liste med bruger informationer


}