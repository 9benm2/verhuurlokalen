using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PeriodeAanvragen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["persoon"] == null)
        {
            //na aanmeldenbutton checken of periodeaanmaken 1 is en dan redirecten naar PeriodeAanvragen redirecten zodat de persoon meteen kan voortgaan met de periode te reserveren
            Session["periodeaanmaken"] = 1;
            Session["melding"] = "Om een periode aan te vragen moet u eerst ingelogd zijn. Hier kan u een nieuwe account aanmaken.";
            Response.Redirect("Registreren.aspx");
        }
        if (Session["melding"] == null)
        {
            LabelMelding.Visible = false;
        }
        else
        {
            LabelMelding.Visible = true;
            LabelMelding.Text = Session["melding"].ToString();
            Session["melding"] = null;
        }
        if (Session["begin"] != null && Session["einde"] != null)
        {
            TextBoxBegin.Text = Session["begin"].ToString();
            TextBoxBegin.Enabled = false;
            TextBoxEinde.Text = Session["einde"].ToString();
            TextBoxEinde.Enabled = false;
        }
        else
        {
            Session["melding"] = "Kies eerst een begin en einde via de kalender.";
            Response.Redirect("Default.aspx");

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BLPeriode blPeriode = new BLPeriode();
        BLKamp blKamp = new BLKamp();
        BLContract blContract = new BLContract();
        BLContractPersoon blContractPersoon = new BLContractPersoon();
        BLVereniging blVereniging = new BLVereniging();
        BLPersoon blPersoon = new BLPersoon();
        BLBetaling blBetaling = new BLBetaling();
        Vereniging vereniging = new Vereniging();
        Periode periode = new Periode();
        Kamp kamp = new Kamp();
        Contract contract = new Contract();
        ContractPersoon contractpersoon = new ContractPersoon();

        periode.BeginPeriode = Convert.ToDateTime(TextBoxBegin.Text);
        periode.EindePeriode = Convert.ToDateTime(TextBoxEinde.Text);
        if (CheckBoxDefinitief.Checked)
        {
            //optie toegekend
            periode.StatusId = 3;
        }
        else
        {
            //optie in aanvraag
            periode.StatusId = 2;
        }
        //type standaard
        periode.TypeId = 4;
        int periodeId = blPeriode.InsertPeriode(periode);
        kamp.AantalPersonen = Convert.ToInt32(TextBoxPersonen.Text);
        kamp.AantalTenten = Convert.ToInt32(TextBoxTenten.Text);
        kamp.TijdstipAankomst = TextBoxAankomst.Text;
        kamp.TijdstipVertrek = TextBoxVertrek.Text;
        kamp.Opmerkingen = TextBoxOpmerkingen.Text;
        int kampId = blKamp.InsertKamp(kamp);
        vereniging.Naam = TextBoxVereniging.Text;
        int verenigingId = blVereniging.InsertVereniging(vereniging);
        int persoonId = ((Persoon)Session["persoon"]).Id;
        contract.KampId = kampId;
        contract.PeriodeId = periodeId;
        int contractId = blContract.InsertContract(contract);
        contractpersoon.ContractId = contractId;
        contractpersoon.PersoonId = persoonId;
        int contractPersoonId = blContractPersoon.InsertContractpersoon(contractpersoon, 1); //1 voor kamporganisator
        blPersoon.UpdatePersoonVerenigingId(persoonId, verenigingId);
        int aantalNachten = (int)Convert.ToDateTime(TextBoxEinde.Text).Subtract(Convert.ToDateTime(TextBoxBegin.Text)).TotalDays;
        blBetaling.InsertBetalingenBijContract(aantalNachten, Convert.ToInt32(TextBoxPersonen.Text), Convert.ToInt32(TextBoxTenten.Text), contractId);

        //==================================================mail sturen====================================================
        if (periode.StatusId == 3)
        {
            Session["melding"] = "De periode is voorlopig gereserveerd. Vergeet ze niet binnen de 7 dagen definitief te maken! In uw mailbox zit een e-mail met het overzicht van uw aanvraag.";
        }
        else
        {
            Session["melding"] = "De periode is definitief gereserveerd. In uw mailbox zit een e-mail met het overzicht van uw aanvraag. Gelieve het voorschot en de waarborg spoedig te betalen.";
        }
        //sessie met begin en einddatum leegmaken
        Session["begin"] = null;
        Session["einde"] = null;
        Response.Redirect("Default.aspx");
    }
    protected void TextBoxPersonen_TextChanged(object sender, EventArgs e)
    {
        int result;
        if (int.TryParse(TextBoxPersonen.Text, out result))
        {
            double bedrag = 0;
            //==========!!!!!!!!!ook in pagina Periodeaanvragen.aspx.cs aanpassingen over prijzen doen bij textchanged personen textbox
            //bedrag uitrekenen
            int aantalNachten = (int)Convert.ToDateTime(TextBoxEinde.Text).Subtract(Convert.ToDateTime(TextBoxBegin.Text)).TotalDays;
            double berekendBedrag = aantalNachten * result * 3.5;
            //minimumbedrag sowieso > 2500, anders nachten*personen*3.5
            if (berekendBedrag < 2500)
            {
                bedrag = 2500;
            }
            else
            {
                bedrag = berekendBedrag;
            }
            TextBoxKostprijs.Text = bedrag.ToString();
            TextBoxWaarborg.Text = "500";
        }
        else
        {
            TextBoxKostprijs.Text = "Geef getal bij personen in";
        }
    }
}