using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reservaties : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["persoon"] == null)
        {
            Session["melding"] = "Meld u eerst aan om uw reservaties te bekijken";
        }
        else
        {
            //reservaties ophalen
            BLContractPersoon blContractPersoon = new BLContractPersoon();
            List<ContractPersoon> contractPersonen = blContractPersoon.GetContractPersonenByPersoonId(((Persoon)Session["persoon"]).Id);
            if (contractPersonen.Count == 0)
            {
                Session["melding"] = "U heeft nog geen reservaties.";
            }
            else
            {
                Panel1.Controls.Add(new LiteralControl("<h1>Totaal:" + contractPersonen.Count + " periodes </h1>"));
                int i = 0;
                foreach (ContractPersoon contractPersoon in contractPersonen)
                {
                    i++;
                    //rol + periode
                    Panel1.Controls.Add(new LiteralControl("<h2>" + i + ") " + contractPersoon.Rol.Naam + " van periode:</h2>"));
                    Label labelPeriode = new Label();
                    labelPeriode.Text = Convert.ToDateTime(contractPersoon.Contract.Periode.BeginPeriode).ToShortDateString() + " - " + Convert.ToDateTime(contractPersoon.Contract.Periode.EindePeriode).ToShortDateString();
                    AddAndNewLine(labelPeriode);
                    Label("Status:", 100);
                    Label(contractPersoon.Contract.Periode.Status.Naam, 300);

                    //betalingen
                    Panel1.Controls.Add(new LiteralControl("<h2>Betalingen</h2>"));
                    foreach (Betaling betaling in contractPersoon.Contract.Betalings)
                    {
                        Label l1 = new Label();
                        l1.Width = 150;
                        l1.Text = betaling.TypeBetaling.Naam;
                        AddControl(l1);

                        Label l2 = new Label();
                        l2.Width = 80;
                        l2.Text = betaling.Bedrag.ToString();
                        AddControl(l2);

                        Label l3 = new Label();
                        l3.Width = 180;
                        l3.Text = betaling.StatusBetaling.Naam;
                        AddAndNewLine(l3);
                    }

                    //informatie kamp en vereniging
                    Panel1.Controls.Add(new LiteralControl("<h2>Informatie kamp</h2>"));
                    if (contractPersoon.Persoon.Vereniging != null)
                    {
                        Label("Vereniging:", 150);
                        Label(contractPersoon.Persoon.Vereniging.Naam.ToString(), 300);
                    }
                    NewLine();
                    Label("Aantal personen:", 150);
                    Label(contractPersoon.Contract.Kamp.AantalPersonen.ToString(), 300);
                    NewLine();
                    Label("Aantal tenten:", 150);
                    Label(contractPersoon.Contract.Kamp.AantalTenten.ToString(), 300);
                    NewLine();
                    Label("Uur aankomst:", 150);
                    Label(contractPersoon.Contract.Kamp.TijdstipAankomst, 300);
                    NewLine();
                    Label("Uur vertrek:", 150);
                    Label(contractPersoon.Contract.Kamp.TijdstipVertrek, 300);
                    NewLine();
                    Label("Opmerkingen:", 150);
                    Label(contractPersoon.Contract.Kamp.Opmerkingen, 300);
                }
            }

        }
        if (Session["melding"] != null)
        {
            LabelMelding.Visible = true;
            LabelMelding.Text = Session["melding"].ToString();
            Session["melding"] = null;
        }
        else
        {
            LabelMelding.Visible = false;
        }
    }
    protected void AddControl(Control control)
    {
        Panel1.Controls.Add(control);
    }
    protected void NewLine()
    {
        Panel1.Controls.Add(new LiteralControl("<br />"));
    }
    protected void AddAndNewLine(Control control)
    {
        Panel1.Controls.Add(control);
        NewLine();
    }
    protected void Label(String text, int width)
    {
        Label label = new Label();
        label.Width = width;
        label.Text = text;
        AddControl(label);
    }
}