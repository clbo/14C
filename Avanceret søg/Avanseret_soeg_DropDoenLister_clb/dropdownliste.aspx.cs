using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class dropdownliste : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // tilføjeslse af første felt i dropdownlister
    protected void DropDownList_farve_DataBound(object sender, EventArgs e)
    {
        DropDownList_farve.Items.Insert(0, new ListItem("- Vælg farver -", "0"));
    }
    protected void DropDownList_styrke_DataBound(object sender, EventArgs e)
    {
        DropDownList_styrke.Items.Insert(0, new ListItem("- Alle styrker -", "0"));
    }
    protected void DropDownList_surhed_DataBound(object sender, EventArgs e)
    {
        DropDownList_surhed.Items.Insert(0, new ListItem("- Alle surheder -", "0"));
    }
    protected void DropDownList_type_DataBound(object sender, EventArgs e)
    {
        DropDownList_type.Items.Insert(0, new ListItem("- Alle typer -", "0"));
    }


    // Knappen søg
    protected void Button_søg_Click(object sender, EventArgs e)
    {


        // SQL strengen (første del)
        string sqlText = "SELECT * FROM bolcher WHERE";

        // Tæller der bruges i koden nedenunder til at holde styr på hvor mange gange et for loop har kørt
        int count = 0;

        // Forbind til databasen
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        // hvis intet er valgt i farvedropdown vil SelectedIndex være lig med 0
        // Så hvis noget er valgt køres koden 
        if (DropDownList_farve.SelectedIndex != 0)
        {
            // tilføjer et "AND" til sqlstrengen hvis det ikke er første gang noget tilføjes

            if (count != 0)
            {
                cmd.CommandText = sqlText += " AND";
            }

            // forøger counter med 1
            count = count + 1;

            // tilføj tekst til SQL strengen
            cmd.CommandText = sqlText += " (FK_faveID = @farve" + count + ")";
            cmd.Parameters.Add("@farve" + count, SqlDbType.Int).Value = DropDownList_farve.SelectedValue;

        }

        // hvis intet er valgt i styrkedropdown vil SelectedIndex være lig med 0
        // Så hvis noget er valgt køres koden 
        if (DropDownList_styrke.SelectedIndex != 0)
        {
            // kalder (kører) metoden SqlMethod(), som tilføjer et "AND" til sqlstrengen
            if (count != 0)
            {
                cmd.CommandText = sqlText += " AND";
            }

            // forøger counter med 1
            count = count + 1;

            // tilføj tekst til SQL strengen
            cmd.CommandText = sqlText += " (FK_styrkeID = @styrke" + count + ")";
            cmd.Parameters.Add("@styrke" + count, SqlDbType.Int).Value = DropDownList_styrke.SelectedValue;

        }

        // hvis intet er valgt i surheddropdown vil SelectedIndex være lig med 0
        // Så hvis noget er valgt køres koden 
        if (DropDownList_surhed.SelectedIndex != 0)
        {
            // kalder (kører) metoden SqlMethod(), som tilføjer et "AND" til sqlstrengen
            if (count != 0)
            {
                cmd.CommandText = sqlText += " AND";
            }

            // forøger counter med 1
            count = count + 1;

            // tilføj tekst til SQL strengen
            cmd.CommandText = sqlText += " (FK_surhedID = @surhed" + count + ")";
            cmd.Parameters.Add("@surhed" + count, SqlDbType.Int).Value = DropDownList_surhed.SelectedValue;

        }

        // hvis intet er valgt i typedropdown vil SelectedIndex være lig med 0
        // Så hvis noget er valgt køres koden 
        if (DropDownList_type.SelectedIndex != 0)
        {
            // kalder (kører) metoden SqlMethod(), som tilføjer et "AND" til sqlstrengen
            if (count != 0)
            {
                cmd.CommandText = sqlText += " AND";
            }

            // forøger counter med 1
            count = count + 1;

            // tilføj tekst til SQL strengen
            cmd.CommandText = sqlText += " (FK_typeID = @type" + count + ")";
            cmd.Parameters.Add("@type" + count, SqlDbType.Int).Value = DropDownList_type.SelectedValue;

        }


        // Default, hvis ingen søgekriterier er valgt
        if (DropDownList_farve.SelectedIndex == 0 && DropDownList_styrke.SelectedIndex == 0
           && DropDownList_surhed.SelectedIndex == 0 && DropDownList_type.SelectedIndex == 0)
        {
            // sql sætninger der med sikkerhed ikke returnerer noget resultat
            // Altså hvis man ikke har søgt på noget, kommer der ikke noget resultat
            cmd.CommandText = "SELECT * FROM bolcher WHERE bolcheID = -1";

        }

        conn.Open();
        Repeater_resultat.DataSource = cmd.ExecuteReader();
        Repeater_resultat.DataBind();
        conn.Close();


    }
}