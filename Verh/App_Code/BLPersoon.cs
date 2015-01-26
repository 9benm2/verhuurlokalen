using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLPersoon
/// </summary>
public class BLPersoon
{
    VerhuurDataContext db = new VerhuurDataContext();
    public BLPersoon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Persoon LoginOK(string email, string wachtwoord)
    {
        var personen = from item in db.Persoons where item.Email == email && item.Wachtwoord == wachtwoord select item;
        if (personen.Count() == 1)
        {
            return personen.Single<Persoon>();
        }
        else
        {
            return null;
        }
    }
    public int InsertPersoon(Persoon persoon)
    {
        // elke persoon unieke email
        var personen =
            from item in db.Persoons where item.Email == persoon.Email select item;

        if (personen.Count() > 0)
        {
            return 0;
        }
        else
        {
            //elk aangemaakte persoon is van type normal en geen verenigingId toegekend
            persoon.Type = "normal";
            db.Persoons.InsertOnSubmit(persoon);
            db.SubmitChanges();
            return persoon.Id;
        }
    }
    public void UpdatePersoonVerenigingId(int persoonId, int verenigingId)
    {
        Persoon persoon = (from item in db.Persoons where item.Id == persoonId select item).Single<Persoon>(); 
        persoon.VerenigingId = verenigingId;
        db.SubmitChanges();
    }
}