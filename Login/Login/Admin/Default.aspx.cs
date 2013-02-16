using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
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
}