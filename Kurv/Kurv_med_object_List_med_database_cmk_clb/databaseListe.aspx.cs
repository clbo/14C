using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class databaseListe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Repeater_mobler_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        List<ProduktIKurv> kurv = new List<ProduktIKurv>();

        // Henter "Session kurv" til "kurv"
        kurv = TagKurv(kurv);

        // Lægger produktet i kurven
        PutIKurv(e, kurv);

        // Vis kurv
        VisKurv(kurv);

    }

    private static void PutIKurv(RepeaterCommandEventArgs e, List<ProduktIKurv> kurv)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Mobler WHERE M_id = @id";
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = e.CommandArgument;

        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            bool nytProdukt = true;

            //nytProdukt = OpdaterProduktIKurv(kurv, nytProdukt);

            foreach (ProduktIKurv produkt in kurv)
            {
                // Hvis produktet er fundet
                if (produkt.Id == Convert.ToInt32(e.CommandArgument.ToString()))
                {
                    // så opdater antal og samlet pris
                    produkt.Antal += 1;
                    produkt.SamletPris += produkt.Antal * produkt.Pris;

                    // nu er det konstateret at det ikke er noget nyt produkt mere
                    nytProdukt = false;
                }
            }


            // er det et nyt produkt
            if (nytProdukt)
            {
                // Tilføj et produkt til listen
                kurv.Add(new ProduktIKurv(
                    Convert.ToInt32(reader["M_id"]),
                    reader["m_Navn"].ToString(),
                    Convert.ToDecimal(reader["M_Pris"].ToString()),
                    1));
            }
        }
    }

    private void VisKurv(List<ProduktIKurv> kurv)
    {
        // Vis kurv
        GridView_kurv.DataSource = kurv;
        GridView_kurv.DataBind();
    }

    private List<ProduktIKurv> TagKurv(List<ProduktIKurv> kurv)
    {
        // hvis ikke vi har en session kaldet kurv
        if (Session["kurv"] == null)
        {
            // så opret en "Session["kurv"]" med værdien "List kurv"
            // Session.Add("kurv", kurv);

            // eller sådan her
            Session["kurv"] = kurv;
        }

        // sæt "list kurv" lig med "session kurv"
        kurv = (List<ProduktIKurv>)Session["kurv"];

        return kurv;
    }
}