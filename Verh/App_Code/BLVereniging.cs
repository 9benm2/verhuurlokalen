using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLVereniging
/// </summary>
public class BLVereniging
{
    VerhuurDataContext db = new VerhuurDataContext();
	public BLVereniging()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertVereniging(Vereniging vereniging)
    {
        // naam mag niet leeg zijn, anders 0 returnen
        if (vereniging.Naam == "")
        {
            return 0;
        }
        else
        {
            db.Verenigings.InsertOnSubmit(vereniging);
            db.SubmitChanges();
            return vereniging.Id;
        }
    }
}