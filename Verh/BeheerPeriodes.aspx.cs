using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class BeheerPeriodes : System.Web.UI.Page
{
    BLPeriode blPeriode = new BLPeriode();
    BLStatus blStatus = new BLStatus();
    BLType blType = new BLType();
    BLStatusBetaling blStatusBetaling = new BLStatusBetaling();

    protected void Page_Load(object sender, EventArgs e)
    {
        //admincheck
        if (Session["persoon"] != null)
        {
            if (((Persoon)Session["persoon"]).Type != "admin")
            {
                GeenAdminFout();
            }
        }
        else
        {
            GeenAdminFout();
        }
        if (!IsPostBack)
        {
            LeesPeriodes();
            VulStatusEnTypeDropDown();
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
    protected void GeenAdminFout()
    {
        Session["melding"] = "Meld u eerst aan als admin om deze pagina te kunnen bezoeken.";
        Response.Redirect("Default.aspx");
    }
    protected void LeesPeriodes()
    {
        List<Periode> periodes = blPeriode.GetPeriodes();
        GridViewPeriodes.DataSource = periodes;
        GridViewPeriodes.DataBind();
    }
    protected void GridViewPeriodes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewPeriodes.PageIndex = e.NewPageIndex;
        LeesPeriodes();
    }
    protected void VulStatusEnTypeDropDown()
    {
        //periode dropdowns
        List<Type> types = blType.GetTypes();
        List<Status> statussen = blStatus.GetStatussen();
        foreach (Type type in types)
        {
            ListItem l = new ListItem(type.Naam, type.Id.ToString());
            DropDownListType.Items.Add(l);
        }
        foreach (Status status in statussen)
        {
            ListItem l = new ListItem(status.Naam, status.Id.ToString());
            DropDownListStatus.Items.Add(l);
        }
        //betaling dropdowns
        List<StatusBetaling> statusBetalingen = blStatusBetaling.GetBetalingStatussen();
        foreach (StatusBetaling statusBetaling in statusBetalingen)
        {
            ListItem l = new ListItem(statusBetaling.Naam, statusBetaling.Id.ToString());
            DropDownListWaarborgStatus.Items.Add(l);
            DropDownListVoorschotStatus.Items.Add(l);
            DropDownListRestbedragStatus.Items.Add(l);
        }
    }
    protected void GridViewPeriodes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Periode periode = blPeriode.GetPeriode(int.Parse(GridViewPeriodes.SelectedDataKey["Id"].ToString()));
        Session["periode"] = periode;
        TextBoxBegin.Text = Convert.ToDateTime(periode.BeginPeriode).ToShortDateString();
        TextBoxEinde.Text = Convert.ToDateTime(periode.EindePeriode).ToShortDateString();
        DropDownListStatus.SelectedValue = periode.StatusId.ToString();
        DropDownListType.SelectedValue = periode.TypeId.ToString();
        foreach (Contract contract in periode.Contracts)
        {
            //contract in sessie steken voor updates
            Session["contract"] = contract;
            foreach (ContractPersoon contractPersoon in contract.ContractPersoons)
            {
                TextBoxVereniging.Text = contractPersoon.Persoon.Vereniging.Naam;
                //kamporganisator gegevens ophalen
                if (contractPersoon.RolId == 1)
                {
                    TextBoxEmail.Text = contractPersoon.Persoon.Email;
                    TextBoxVoornaam.Text = contractPersoon.Persoon.Voornaam;
                    TextBoxNaam.Text = contractPersoon.Persoon.Naam;
                    TextBoxAdres.Text = contractPersoon.Persoon.Adres;
                    TextBoxPostcode.Text = contractPersoon.Persoon.Postcode;
                    TextBoxPlaats.Text = contractPersoon.Persoon.Plaats;
                    TextBoxTelefoonnummer.Text = contractPersoon.Persoon.Telefoonnummer;
                }

            }
            foreach (Betaling betaling in contract.Betalings)
            {
                if (betaling.TypeBetalingId == 1)
                {
                    TextBoxWaarborg.Text = betaling.Bedrag.ToString();
                    DropDownListWaarborgStatus.SelectedValue = betaling.StatusBetalingId.ToString();
                }
                if (betaling.TypeBetalingId == 2)
                {
                    TextBoxVoorschot.Text = betaling.Bedrag.ToString();
                    DropDownListVoorschotStatus.SelectedValue = betaling.StatusBetalingId.ToString();
                }
                if (betaling.TypeBetalingId == 3)
                {
                    TextBoxRestbedrag.Text = betaling.Bedrag.ToString();
                    DropDownListRestbedragStatus.SelectedValue = betaling.StatusBetalingId.ToString();
                }
            }
            TextBoxPersonen.Text = contract.Kamp.AantalPersonen.ToString();
            TextBoxTenten.Text = contract.Kamp.AantalTenten.ToString();
            TextBoxAankomst.Text = contract.Kamp.TijdstipAankomst;
            TextBoxVertrek.Text = contract.Kamp.TijdstipVertrek;
            TextBoxOpmerkingen.Text = contract.Kamp.Opmerkingen;
        }


        Panel1.Visible = true;
    }
    protected void ButtonWijzigen_Click(object sender, EventArgs e)
    {
        //contract uit sessie halen
        Contract contract = (Contract)Session["contract"];

        //betalingen updaten
        BLBetaling blBetaling = new BLBetaling();
        List<Betaling> betalingen = blBetaling.GetBetalingenByContractId(contract.Id);
        foreach (Betaling betaling in betalingen)
        {
            if (betaling.TypeBetalingId == 1)
            {
                betaling.StatusBetalingId = Convert.ToInt32(DropDownListWaarborgStatus.SelectedValue);
                betaling.Bedrag = Convert.ToInt32(TextBoxWaarborg.Text);
                blBetaling.UpdateBetaling(betaling);
            }
            if (betaling.TypeBetalingId == 2)
            {
                betaling.StatusBetalingId = Convert.ToInt32(DropDownListVoorschotStatus.SelectedValue);
                betaling.Bedrag = Convert.ToInt32(TextBoxVoorschot.Text);
                blBetaling.UpdateBetaling(betaling);
            }
            if (betaling.TypeBetalingId == 3)
            {
                betaling.StatusBetalingId = Convert.ToInt32(DropDownListRestbedragStatus.SelectedValue);
                betaling.Bedrag = Convert.ToInt32(TextBoxRestbedrag.Text);
                blBetaling.UpdateBetaling(betaling);
            }
        }

        //kamp updaten
        BLKamp blKamp = new BLKamp();
        Kamp kamp = contract.Kamp;
        kamp.AantalPersonen = Convert.ToInt32(TextBoxPersonen.Text);
        kamp.AantalTenten = Convert.ToInt32(TextBoxTenten.Text);
        kamp.TijdstipAankomst = TextBoxAankomst.Text;
        kamp.TijdstipVertrek = TextBoxVertrek.Text;
        kamp.Opmerkingen = TextBoxOpmerkingen.Text;
        blKamp.UpdateKamp(kamp);
        
        //periode updaten
        Periode periode = contract.Periode;
        periode.BeginPeriode = Convert.ToDateTime(TextBoxBegin.Text);
        periode.EindePeriode = Convert.ToDateTime(TextBoxEinde.Text);
        periode.TypeId = Convert.ToInt32(DropDownListType.SelectedValue);
        periode.StatusId = Convert.ToInt32(DropDownListStatus.SelectedValue);
        blPeriode.UpdatePeriode(periode);


        Session["melding"] = "Wijzigingen opgeslagen.";
        Response.Redirect("BeheerPeriodes.aspx");
    }
    protected void ButtonVerwijderen_Click(object sender, EventArgs e)
    {
        blPeriode.DeletePeriode(((Periode)Session["periode"]).Id);
        Session["melding"] = "Periode en de bijbehorende betalingen/kampgegevens verwijderd.";
        Response.Redirect("BeheerPeriodes.aspx");
    }
}