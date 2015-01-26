using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NieuwePeriode : System.Web.UI.Page
{
    BLStatus blStatus = new BLStatus();
    BLType blType = new BLType();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Status> statussen = blStatus.GetStatussen();
            List<Type> types = blType.GetTypes();
            foreach (Status status in statussen)
            {
                ListItem l = new ListItem(status.Naam, status.Id.ToString());
                DropDownListStatus.Items.Add(l);
            }
            foreach (Type type in types)
            {
                ListItem l = new ListItem(type.Naam, type.Id.ToString());
                DropDownListType.Items.Add(l);
            }
        }
    }
    protected void ButtonPeriode_Click(object sender, EventArgs e)
    {
        BLPeriode blPeriode = new BLPeriode();
        Periode periode = new Periode();
        periode.BeginPeriode = Convert.ToDateTime(TextBoxBegin.Text);
        periode.EindePeriode = Convert.ToDateTime(TextBoxEinde.Text);
        periode.TypeId = Convert.ToInt32(DropDownListType.SelectedValue);
        periode.StatusId = Convert.ToInt32(DropDownListStatus.SelectedValue);
        int periodeId = blPeriode.InsertPeriode(periode);
        if (periodeId == 0)
        {
            Session["melding"] = "Begin moet voor einde liggen en er mag geen periode overlappen.";
        }
        else
        {
            Session["melding"] = "Periode aangemaakt. <a href='NieuwePeriode.aspx'> Nog een periode aanmaken</a>.";
        }
        Response.Redirect("Default.aspx");
    }
}