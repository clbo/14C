using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Soeg_Mobler : System.Web.UI.Page
{

    #region Beskrivelse
    
    
    // Oprettelse af Avanceret søge sql streng
    // Opgaven er løst ved brug af søgeformularen i CMK-Møbler opgaven
    // 1. Opret først din søgestreng, som den skal se ud hvis den ikke er dynamisk.
    //      Lav den som håndkode, eller brug SQL Query Builder
    // 2. søgestrengen kommer til at se ud som noget ala dette
    //
    //  SELECT * FROM Mobler
    //  WHERE     (FK_Kategori = @kategori) AND (FK_Design = @designer) AND 
    //  (M_Design_aar BETWEEN @design_arr_min AND @design_arr_max) AND (M_Pris BETWEEN 
    //  @Design_pris_min AND @Design_pris_max) 
    //  OR 
    //  (FK_Kategori = @kategori1) AND (FK_Design = @designer) AND 
    //  (M_Design_aar BETWEEN @design_arr_min AND @design_arr_max) AND (M_Pris BETWEEN 
    //  @Design_pris_min AND @Design_pris_max)
    
    // Brug evt. også inner join

    // Det der ændres dynamisk er hvad der står efter "WHERE"
    // parametrerne bliver kun tilføjet hvis der er skrevet noget i tekstfelterne, ellers spriges der videre til næste tekstfelt.
    // Mellem hvert nyt parameter tilføjes der et 'AND' til søgestrengen, og herefter selve sql strengen
    // Der tilføjes nyt til variablen 'sqlText' (som indeholder vores sql streng (SELECT * FROM Mobler WHERE )) ved hjælp af 
    // += ""; (sqlText += " (FK_Kategori = @kategori)" ) 

    // -----------------------------------------------------------------------------

    #endregion

    #region Private Fields (variabler der bruges i sidens metoder)

    private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private string sqlText = "SELECT * FROM Mobler WHERE";
    private int count = 0;

    #endregion

    #region pageload

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // Tilføjelse af Tomt felt til Dropdownlistens top
    //protected void DropDownList_designer_DataBound(object sender, EventArgs e)
    //{
    //    DropDownList_designer.Items.Insert(0, new ListItem("- Alle -", "0"));
    //}

    #endregion

    #region Søgning på varenummer med brug af LIKE


    protected void Button_varenummer_Click(object sender, EventArgs e)
    {
        this.cmd.Connection = this.conn;
        string nummer = TextBox_varenummer.Text;
        this.cmd.CommandText = "SELECT * FROM Mobler WHERE (M_nummer LIKE '%" + @nummer + "%')";
        cmd.Parameters.Add("@nummer", SqlDbType.NVarChar).Value = TextBox_varenummer.Text;

        conn.Open();
        Repeater_mobler_soeg_resultat.DataSource = cmd.ExecuteReader();
        Repeater_mobler_soeg_resultat.DataBind();
        conn.Close();
    }

    #endregion

    #region Avanceret Søgning


    protected void Button_main_soeg_Click(object sender, EventArgs e)
    {
        this.cmd.Connection = conn;

        //CheckBoxList - Kategorier
        if (CheckBoxList_kategori.SelectedIndex != -1)
        {
            // For hver checkbox der er afkrydset køres loopet
            for (int i = 0; i < CheckBoxList_kategori.Items.Count; i++)
            {
                // Checkbokse
                if (CheckBoxList_kategori.Items[i].Selected && this.count == 0)
                {
                    this.count = this.count + 1;
                    this.cmd.CommandText = sqlText += " (FK_Kategori = @kategori)";
                    cmd.Parameters.Add("@kategori", SqlDbType.Int).Value = CheckBoxList_kategori.Items[i].Value;

                    //DropDownList - Designer
                    DesignerMethod();
                    // Design år
                    DesignAarMethod();
                    // Pris
                    PrisMethod();
                }
                else if (CheckBoxList_kategori.Items[i].Selected)
                {
                    this.cmd.CommandText = sqlText += " OR (FK_Kategori = @kategori" + i + ")";
                    cmd.Parameters.Add("@kategori" + i, SqlDbType.Int).Value = CheckBoxList_kategori.Items[i].Value;

                    //DropDownList - Designer
                    DesignerMethod();
                    // Design år
                    DesignAarMethod();
                    // Pris
                    PrisMethod();
                }
            }

        }
        else
        {
            //DropDownList - Designer
            DesignerMethod();
            // Design år
            DesignAarMethod();
            // Pris
            PrisMethod();
        }

        // Default søg, hvis ingen felter er udfyldt
        DefaultMethod();

        conn.Open();
        Repeater_mobler_soeg_resultat.DataSource = cmd.ExecuteReader();
        Repeater_mobler_soeg_resultat.DataBind();
        conn.Close();
    }

    #endregion

    #region Private Metoder
    // Private Metoder

    private void DesignerMethod()
    {
        if (DropDownList_designer.SelectedIndex != 0)
        {
            SqlAndMethod();

            this.count = this.count + 1;
            this.cmd.CommandText = sqlText += " (FK_Design = @designer" + this.count + ")";
            cmd.Parameters.Add("@designer" + this.count, SqlDbType.Int).Value = DropDownList_designer.SelectedValue;

        }
    }

    private void DesignAarMethod()
    {
        if (TextBox_designer_min.Text != "" && TextBox_designer_max.Text != "")
        {
            SqlAndMethod();

            this.count = this.count + 1;
            this.cmd.CommandText = sqlText += " (M_Design_aar BETWEEN @design_arr_min" + this.count + " AND @design_arr_max" + this.count + ")";
            cmd.Parameters.Add("@design_arr_min" + this.count, SqlDbType.Int).Value = TextBox_designer_min.Text;
            cmd.Parameters.Add("@design_arr_max" + this.count, SqlDbType.Int).Value = TextBox_designer_max.Text;

        }
    }

    private void PrisMethod()
    {
        if (TextBox_pris_min.Text != "" && TextBox_pris_max.Text != "")
        {
            SqlAndMethod();

            this.count = this.count + 1;
            this.cmd.CommandText = sqlText += " (M_Pris BETWEEN @Design_pris_min" + this.count + " AND @Design_pris_max" + this.count + ")";
            cmd.Parameters.Add("@Design_pris_min" + this.count, SqlDbType.Int).Value = TextBox_pris_min.Text;
            cmd.Parameters.Add("@Design_pris_max" + this.count, SqlDbType.Int).Value = TextBox_pris_max.Text;

        }
    }

    private void SqlAndMethod()
    {
        if (this.count != 0)
        {
            this.cmd.CommandText = sqlText += " AND";

        }
    }

    private void DefaultMethod()
    {
        if (CheckBoxList_kategori.SelectedIndex == -1 && DropDownList_designer.SelectedIndex == 0
            && TextBox_designer_min.Text == "" && TextBox_designer_max.Text == ""
            && TextBox_pris_min.Text == "" && TextBox_pris_max.Text == "")
        {
            this.cmd.CommandText = "SELECT * FROM Mobler";

        }
    }

    #endregion


}
