using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLKamp
/// </summary>
public class BLKamp
{
    VerhuurDataContext db = new VerhuurDataContext();
    public BLKamp()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Kamp GetKampById(int id)
    {
        Kamp kamp = (from item in db.Kamps where item.Id == id select item).Single<Kamp>();
        return kamp;
    }

    public int InsertKamp(Kamp kamp)
    {
        db.Kamps.InsertOnSubmit(kamp);
        db.SubmitChanges();
        return kamp.Id;
    }
    public void UpdateKamp(Kamp kamp)
    {
        Kamp oldKamp = this.GetKampById(kamp.Id);
        oldKamp.AantalPersonen = kamp.AantalPersonen;
        oldKamp.AantalTenten = kamp.AantalTenten;
        oldKamp.TijdstipAankomst = kamp.TijdstipAankomst;
        oldKamp.TijdstipVertrek = kamp.TijdstipVertrek;
        oldKamp.Opmerkingen = kamp.Opmerkingen;
        db.SubmitChanges();
    }

}