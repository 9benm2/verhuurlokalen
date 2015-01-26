using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelAanmeldMelding.Text = "";
        buttonEnMeldingAanpassen();
        Persoon persoon = (Persoon)(Session["persoon"]);
        if (persoon != null)
        {
            if (persoon.Type == "admin")
            {
                MenuAdmin.Visible = true;
            }
            else
            {
                MenuAdmin.Visible = false;
            }
            
        }
        else
        {
            MenuAdmin.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["persoon"] == null)
        {
            BLPersoon blPersoon = new BLPersoon();
            Persoon persoon = blPersoon.LoginOK(TextboxEmail.Text, TextboxWachtwoord.Text);
            if (persoon == null)
            {
                MenuAdmin.Visible = false;
                LabelAanmeldMelding.ForeColor = System.Drawing.Color.Red;
                LabelAanmeldMelding.Text = "Combinatie e-mail en wachtwoord fout";
            }
            else
            {
                Session["persoon"] = persoon;
                Button1.Text = "Afmelden";
                AanmeldPanel.Visible = false;
                //als de gebruiker al een periode selecteerde, gaat hij automatisch naar periode aanvragen pagina
                if (Session["periodeaanmaken"] != null)
                {
                    if (Session["periodeaanmaken"].ToString() == "1")
                    {
                        Session["melding"] = "U kan nu verder gaan met het aanvragen van uw periode";
                        Response.Redirect("PeriodeAanvragen.aspx");
                    }
                }
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            //als iemand aangemeld is, de persoon afmelden
            Session["persoon"] = null;
            LabelAanmeldMelding.ForeColor = System.Drawing.Color.Red;
            LabelAanmeldMelding.Text = "Afgemeld";
            Button1.Text = "Aanmelden";
            AanmeldPanel.Visible = true;
            Response.Redirect("Default.aspx");
        }
    }
    protected void buttonEnMeldingAanpassen()
    {
        if (Session["persoon"] == null)
        {
            AanmeldPanel.Visible = true;
        }
        else
        { //gebruiker als ingelogd weergeven en van aanmeldknop een afmeldknop maken als wel aangemeld is
            Persoon persoon = (Persoon)Session["persoon"];
            Button1.Text = "Afmelden";
            AanmeldPanel.Visible = false;
            LabelAanmeldMelding.ForeColor = System.Drawing.Color.Green;
            LabelAanmeldMelding.Text = "Welkom, " + persoon.Voornaam;
        }

    }
}
