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

public partial class Admin_OpretBruger : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Tjek om man er logget ind
        // Tjek om man er logget ind - Skal gøres på alle sider der skal passwordbeskyttes
        // Kan evt. gøres på en admin/Masterpage, så virker det på alle sider der gør brug af denne master page

        if (Session["Bruger_id"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            // Kode der køres hvis man er logget ind
        }
        #endregion
    }

    #region logud
    protected void Button_logud_Click(object sender, EventArgs e)
    {
        Session.Abandon(); // sletter alle Sessions
        // Eller hvis du kun vil lukke en bestemt session kan du gøre sådan:
        // Session["Bruger_id"] = null;
        Response.Redirect("../Login.aspx");
    }
    #endregion

    #region Opret bruger normalt
    protected void Button_opret_normal_Click(object sender, EventArgs e)
    {
        // brug try/catch til at tjekke om alt gik som det skulle, og undgå de gule fejlsider
        // try betyder prøv, eller forsøg - så forsøg at køre denne kode.
        // hvis det går godt er alt ok, hvis det ikke går godt sendes der en "Exception" (det i normalt ser på den gule fejlside)
        // og den fanges (engelsk catch) og den kode der står i catch delen udføres
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Bruger (Bruger_navn, Bruger_password, Bruger_email) VALUES (@brugernavn, @password, @email)";
            cmd.Parameters.Add("@brugernavn", SqlDbType.NVarChar).Value = TextBox_Brugernavn.Text;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = TextBox_Password.Text;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBox_Email.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Label_bruger_msg.Text = "Brugeren blev oprettet";
        }
        catch (Exception exp)
        {
            //Label_bruger_msg.Text = exp.ToString(); // kan bruges hvis du vil udskrive fejlmeddelelsen i dit label
            Label_bruger_msg.Text = "Brugeren blev <b>ikke</b> oprettet";
        }

    }

    #endregion

    #region Opret bruger med krypteret password
    protected void krypteret_password_Click(object sender, EventArgs e)
    {
        try
        {
            //Lav det indtastede password om til SHA1 og gem det i variabel
            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox_Password.Text, "SHA1");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Bruger (Bruger_navn, Bruger_password, Bruger_email) VALUES (@brugernavn, @password, @email)";
            cmd.Parameters.Add("@brugernavn", SqlDbType.NVarChar).Value = TextBox_Brugernavn.Text;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = hashedPassword; // Her er blevet ændret
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBox_Email.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Label_bruger_msg.Text = "Brugeren blev oprettet";
        }
        catch (Exception exp)
        {
            //Label_bruger_msg.Text = exp.ToString(); // kan bruges hvis du vil udskrive fejlmeddelelsen i dit label
            Label_bruger_msg.Text = "Brugeren blev <b>ikke</b> oprettet";
        }

    }
    #endregion
}