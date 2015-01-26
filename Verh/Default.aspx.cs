using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BLPeriode blPeriode = new BLPeriode();

    VerhuurDataContext db = new VerhuurDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LabelCalendar.ForeColor = System.Drawing.Color.Green;
            LabelCalendar.Text = "Leg de begin-en einddatum van uw reservatie vast met behulp van de kalender en de knoppen";
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
        if (Session["begin"] != null)
        {
            LabelBegin.Text = Session["begin"].ToString();
        }
        if (Session["einde"] != null)
        {
            LabelEinde.Text = Session["einde"].ToString();
        }
        ButtonBevestigenAanpassen();
    }
    protected void ButtonBevestigenAanpassen()
    {
        if (LabelBegin.Text != "" && LabelEinde.Text != "")
        {
            ButtonBevestigen.Visible = true;
            ButtonBevestigen.ForeColor = System.Drawing.Color.Green;
            List<Periode> periodes = blPeriode.GetPeriodesBetween(Convert.ToDateTime(LabelBegin.Text), Convert.ToDateTime(LabelEinde.Text));
            if (periodes.Count == 0)
            {
                ButtonBevestigen.ForeColor = System.Drawing.Color.Green;
                ButtonBevestigen.Enabled = true;
                ButtonBevestigen.Text = LabelBegin.Text + " - " + LabelEinde.Text + " aanvragen";
            }
            else
            {
                ButtonBevestigen.Enabled = false;
                ButtonBevestigen.ForeColor = System.Drawing.Color.Red;
                ButtonBevestigen.Text = "Overlappende periode";
            }
        }
        else
        {
            ButtonBevestigen.Visible = false;
        }
    }
    protected void Calendar_SelectionChanged(object sender, EventArgs e)
    {
        List<Periode> periodes = blPeriode.GetPeriodesByDate(Calendar.SelectedDate);
        if (periodes.Count == 0)
        {
            if (Calendar.SelectedDate.Date < DateTime.Now)
            {
                LabelCalendar.ForeColor = System.Drawing.Color.Red;
                LabelCalendar.Text = "Deze dag ligt in het verleden.";
                ButtonBegin.Enabled = false;
                ButtonEinde.Enabled = false;
            }
            else
            {
                LabelCalendar.ForeColor = System.Drawing.Color.Green;
                LabelCalendar.Text = "Hier kan u een periode aanvragen. Leg de begin-en einddatum vast met de knoppen.";
                ButtonBegin.Enabled = true;
                ButtonEinde.Enabled = true;
            }
        }
        else if (periodes.Count == 1)
        {
            foreach (Periode periode in periodes)
            {
                if (periode.StatusId == 2)
                {
                    LabelCalendar.ForeColor = System.Drawing.Color.Red;
                    LabelCalendar.Text = "Op deze periode is momenteel al een optie genomen.";
                    ButtonBegin.Enabled = false;
                    ButtonEinde.Enabled = false;
                }
                else if (periode.StatusId == 3)
                {
                    LabelCalendar.ForeColor = System.Drawing.Color.Red;
                    LabelCalendar.Text = "Deze periode is definitief bezet.";
                    ButtonEinde.Enabled = false;
                    ButtonEinde.Enabled = false;
                }
            }
        }
        LabelCalendar.Visible = true;
    }
    protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
    {
        //calendar aanpassen naargelang welke nog beschikbaar zijn
        var periodes = from item in db.Periodes select item;
        foreach (Periode periode in periodes)
        {
            DateTime begin = (DateTime)(periode.BeginPeriode);
            DateTime einde = (DateTime)(periode.EindePeriode);
            TimeSpan difference = einde.Date - begin.Date;

            VoegPeriodeToe(begin, difference, e, periode.Status.Id);
        }
    }
    protected void VoegPeriodeToe(DateTime begin, TimeSpan difference, DayRenderEventArgs e, int status)
    {
        for (int i = 0; i <= difference.Days; i++)
        {
            if (e.Day.Date == begin.AddDays(i))
            {
                if (status == 1)
                {
                    e.Cell.BackColor = System.Drawing.Color.LightGreen;
                }
                if (status == 2)
                {
                    e.Cell.BackColor = System.Drawing.Color.Orange;
                }
                if (status == 3)
                {
                    e.Cell.BackColor = System.Drawing.Color.Salmon;
                }
            }
        }
    }
    protected void ButtonBevestigen_Click(object sender, EventArgs e)
    {
        if (LabelBegin.Text != "" && LabelEinde.Text != "")
        {
            Session["begin"] = LabelBegin.Text;
            Session["einde"] = LabelEinde.Text;
        }
        Response.Redirect("PeriodeAanvragen.aspx");

    }
    protected void ButtonBegin_Click(object sender, EventArgs e)
    {
        //controleren of begin voor einde ligt
        if (LabelEinde.Text != "")
        {
            if (Convert.ToDateTime(LabelEinde.Text) > Calendar.SelectedDate)
            {
                if (Calendar.SelectedDate.Date < DateTime.Now)
                {
                    //als begin voor vandaag ligt, fout geven
                    LabelCalendar.ForeColor = System.Drawing.Color.Red;
                    LabelCalendar.Text = "Begindatum moet na vandaag liggen!";
                    LabelCalendar.Visible = true;
                }
                else
                {
                    BeginOpslaan();
                }
            }
            else
            {
                LabelCalendar.ForeColor = System.Drawing.Color.Red;
                LabelCalendar.Text = "Begindatum moet voor einddatum liggen!";
                LabelCalendar.Visible = true;
            }
            ButtonBevestigenAanpassen();
        }
        else
        {//als er nog niets in LabelEinde zit, geen controle nodig
            BeginOpslaan();
        }

    }
    protected void BeginOpslaan()
    {
        LabelBegin.Text = Calendar.SelectedDate.ToShortDateString();
        Session["begin"] = LabelBegin.Text;

    }
    protected void ButtonEinde_Click(object sender, EventArgs e)
    {
        //controleren of einde na begin ligt
        if (LabelBegin.Text != "")
        {
            if (Convert.ToDateTime(LabelBegin.Text) < Calendar.SelectedDate)
            {
                EindeOpslaan();
            }
            else
            {
                LabelCalendar.ForeColor = System.Drawing.Color.Red;
                LabelCalendar.Text = "Einddatum moet na begindatum liggen!";
                LabelCalendar.Visible = true;
            }
            ButtonBevestigenAanpassen();
        }
        else
        {//als begin nog niet opgeslagen is, niet controleren
            LabelEinde.Text = Calendar.SelectedDate.ToShortDateString();
            Session["einde"] = LabelEinde.Text;
        }
    }
    protected void EindeOpslaan()
    {
        LabelEinde.Text = Calendar.SelectedDate.ToShortDateString();
        Session["einde"] = LabelEinde.Text;
    }
}