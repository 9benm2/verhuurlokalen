using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLBetaling
/// </summary>
public class BLBetaling
{
    VerhuurDataContext db = new VerhuurDataContext();
    public BLBetaling()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Betaling GetBetalingById(int id)
    {
        Betaling betaling = (from item in db.Betalings where item.Id == id select item).Single<Betaling>();
        return betaling;
    }
    public List<Betaling> GetBetalingenByContractId(int contractId)
    {
        List<Betaling> betalingen = db.GetBetalingenByContractId(contractId).ToList<Betaling>();
        return betalingen;
    }
    public void InsertBetalingenBijContract(int aantalNachten, int aantalPersonen, int aantalTenten, int contractId)
    {
        Betaling betalingWaarborg = new Betaling();
        Betaling betalingVoorschot = new Betaling();
        Betaling betalingRest= new Betaling();
        double bedrag = 0;
        //==========!!!!!!!!!ook in pagina Periodeaanvragen.aspx.cs aanpassingen over prijzen doen bij textchanged personen textbox
        double voorschot = 250;
        //bedrag uitrekenen
        double berekendBedrag = aantalNachten * aantalPersonen * 3.5;
        //minimumbedrag sowieso > 2500, anders nachten*personen*3.5
        if (berekendBedrag < 2500)
        {
            bedrag = 2500;
        }
        else
        {
            bedrag = berekendBedrag;
        }

        //----------waarborg------------
        betalingWaarborg.Bedrag = 500;
        betalingWaarborg.ContractId = contractId;
        betalingWaarborg.StatusBetalingId = 1;//onbetaald default
        betalingWaarborg.TypeBetalingId = 1; //waarborg
        db.Betalings.InsertOnSubmit(betalingWaarborg);

        //----------voorschot------------
        betalingVoorschot.Bedrag = (int)(Math.Ceiling(voorschot));
        betalingVoorschot.ContractId = contractId;
        betalingVoorschot.StatusBetalingId = 1;//onbetaald default
        betalingVoorschot.TypeBetalingId = 2; //voorschot
        db.Betalings.InsertOnSubmit(betalingVoorschot);

        //----------restbedrag = bedrag-voorschot------------
        betalingRest.Bedrag = (int)(Math.Ceiling(bedrag - voorschot));
        betalingRest.ContractId = contractId;
        betalingRest.StatusBetalingId = 1;//onbetaald default
        betalingRest.TypeBetalingId = 3; //restbedrag
        db.Betalings.InsertOnSubmit(betalingRest);

        db.SubmitChanges();
    }
    public int InsertExtraBetalingBijContract(int bedrag, int contractId)
    {
        Betaling betaling = new Betaling();
        betaling.Bedrag = bedrag;
        betaling.ContractId = contractId;
        betaling.StatusBetalingId = 1;//onbetaald default
        betaling.TypeBetalingId = 4; //extra default

        db.Betalings.InsertOnSubmit(betaling);
        db.SubmitChanges();
        return betaling.Id;
    }
    public void UpdateBetaling(Betaling betaling){
        Betaling oldBetaling = this.GetBetalingById(betaling.Id);
        oldBetaling.Bedrag = betaling.Bedrag;
        oldBetaling.StatusBetalingId = betaling.StatusBetalingId;
        db.SubmitChanges();
    }
}