using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_put_i_kurv_Click(object sender, EventArgs e)
    {
        // Liste kaldet "kurv" med plads til produkter
        List<ProduktIKurv> kurv = new List<ProduktIKurv>();

        // Henter "Session kurv" til "kurv"
        kurv = TagKurv(kurv);

        // Lægger produktet i kurven
        PutIKurv(kurv);

        // Vis kurv
        VisKurv(kurv);
    }

    private void VisKurv(List<ProduktIKurv> kurv)
    {
        // Vis kurv
        GridView_kurv.DataSource = kurv;
        GridView_kurv.DataBind();
    }

    private void PutIKurv(List<ProduktIKurv> kurv)
    {
        bool nytProdukt = true;

        nytProdukt = OpdaterProduktIKurv(kurv, nytProdukt);

        // er det et nyt produkt
        if (nytProdukt)
        {
            // Tilføj et produkt til listen
            kurv.Add(new ProduktIKurv(
                Convert.ToInt32(TextBox_id.Text),
                TextBox_navn.Text,
                Convert.ToDecimal(TextBox_pris.Text),
                Convert.ToInt32(TextBox_antal.Text)));
        }


    }

    private bool OpdaterProduktIKurv(List<ProduktIKurv> kurv, bool nytProdukt)
    {
        foreach (ProduktIKurv produkt in kurv)
        {
            // Hvis produktet er fundet
            if (produkt.Id == Convert.ToInt32(TextBox_id.Text))
            {
                // så opdater antal og samlet pris
                produkt.Antal += Convert.ToInt32(TextBox_antal.Text);
                produkt.SamletPris += produkt.Antal * produkt.Pris;

                // nu er det konstateret at det ikke er noget nyt produkt mere
                nytProdukt = false;
            }
        }
        return nytProdukt;
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