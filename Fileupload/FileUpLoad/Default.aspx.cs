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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region simpel upload til mappe og database

    protected void Button_upload_Click(object sender, EventArgs e)
    {
        FileUpload_img.SaveAs(Server.MapPath("~/Images/upload/") + FileUpload_img.FileName);

        if (File.Exists(Server.MapPath("~/Images/upload/") + FileUpload_img.FileName))
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Media (ImageFileName) VALUES (@ImageFileName)";
            cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar).Value = FileUpload_img.FileName;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Besked om at billedet er gemt
            Label_besked.Text = "Billedet blev gemt: ";

        }

        else
        {
            Label_besked.Text = "Billedet blev <b>ikke</b> gemt: ";
        }
    }

    #endregion

    #region upload med GUID filnavn

    protected void Button_Dynamisk_filenavn_Click(object sender, EventArgs e)
    {
        // Opret en tilfældig tekst streng 
        Guid TilfealdigtFilNavn = Guid.NewGuid();

        #region find filtypenavnet

        // Få fat i filtypenavnet
        // Gem hele filnavnet i en streng
        string fileTypeNavn = FileUpload_img.FileName;

        // opdel strengen i 2 dele (navnet og filtypeendelsen (jog, gif, png)) og gem det i en liste
        // Split('.') klipper strengen over der hvor der er et punktum
        List<string> endelsen = new List<string>(fileTypeNavn.Split('.'));

        // find filtypeendelsen
        string filTypeEndelse = endelsen[endelsen.Count - 1];

        #endregion

        FileUpload_img.SaveAs(Server.MapPath("~/Images/upload/") + TilfealdigtFilNavn + "." + filTypeEndelse);

        if (File.Exists(Server.MapPath("~/Images/upload/") + TilfealdigtFilNavn + "." + filTypeEndelse))
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Media (ImageFileName) VALUES (@ImageFileName)";
            cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar).Value = TilfealdigtFilNavn + "." + filTypeEndelse; // denne linie er blevet ændret
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Besked om at billedet er gemt
            Label_besked.Text = "Billedet blev gemt: ";

        }

        else
        {
            Label_besked.Text = "Billedet blev <b>ikke</b> gemt: ";
        }
    }

    #endregion

    #region  upload med DATO filnavn
    protected void Button_upload_dato_Click(object sender, EventArgs e)
    {
        // Find dato og tid lige nu og gem det i en string
        string dato = DateTime.Now.ToString();
        // Skift mellemrum og kolon ud med streg og undescore
        dato = dato.Replace(" ", "-");
        dato = dato.Replace(":", "_");

        #region find filtypenavnet

        // Få fat i filtypenavnet
        // Gem hele filnavnet i en streng
        string fileTypeNavn = FileUpload_img.FileName;

        // opdel strengen i 2 dele (navnet og filtypeendelsen (jog, gif, png)) og gem det i en liste
        // Split('.') klipper strengen over der hvor der er et punktum
        List<string> endelsen = new List<string>(fileTypeNavn.Split('.'));

        // find filtypeendelsen
        string filTypeEndelse = endelsen[endelsen.Count - 1];

        #endregion

        FileUpload_img.SaveAs(Server.MapPath("~/Images/upload/") + dato + "." + filTypeEndelse);

        if (File.Exists(Server.MapPath("~/Images/upload/") + dato + "." + filTypeEndelse))
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Media (ImageFileName) VALUES (@ImageFileName)";
            cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar).Value = dato + "." + filTypeEndelse; // denne linie er blevet ændret
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Besked om at billedet er gemt
            Label_besked.Text = "Billedet blev gemt: ";
        }

        else
        {
            Label_besked.Text = "Billedet blev <b>ikke</b> gemt: ";
        }
    }

    #endregion
}