using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registreer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        meldingWeergevenEnWissen();
        if (LabelMelding.Text == "")
        {
            LabelMelding.Visible = false;
        }
    }
    protected void meldingWeergevenEnWissen()
    {
        if (Session["melding"] != null)
        {
            LabelMelding.Text = Session["melding"].ToString();
            Session["melding"] = null;
        }
        else
        {
            LabelMelding.Text = "";
        }
    }
    protected void ButtonRegistreer_Click(object sender, EventArgs e)
    {
        BLPersoon blPersoon = new BLPersoon();
        Persoon persoon = new Persoon();
        persoon.Voornaam = TextBoxVoornaam.Text;
        persoon.Naam = TextBoxNaam.Text;
        persoon.Email = TextBoxEmail.Text;
        persoon.Wachtwoord = TextBoxWachtwoord.Text;
        persoon.Telefoonnummer = TextBoxTelefoonnummer.Text;
        persoon.Adres = TextBoxAdres.Text;
        persoon.Postcode = TextBoxPostcode.Text;
        persoon.Plaats = TextBoxPlaats.Text;
        int persoonId = blPersoon.InsertPersoon(persoon);
        if (persoonId == 0)
        {
            Session["melding"] = "Het e-mail adres waarmee u probeerde te registreren heeft al een account. <a href=\"#\" onclick=\"goBack()\">Terug</a>";
        }
        else
        {
            Session["melding"] = "Bedankt voor uw registratie. U kan zich nu aanmelden met uw e-mail adres en wachtwoord.";
        }
        Response.Redirect("Default.aspx");
    }
}