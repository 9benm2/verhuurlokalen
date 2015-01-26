using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLStatusBetaling
/// </summary>
public class BLStatusBetaling
{
    VerhuurDataContext db = new VerhuurDataContext();
	public BLStatusBetaling()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<StatusBetaling> GetBetalingStatussen()
    {
        List<StatusBetaling> statussen = (from item in db.StatusBetalings select item).ToList<StatusBetaling>();
        return statussen;
    }
}