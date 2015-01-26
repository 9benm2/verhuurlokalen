using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLTypeBetaling
/// </summary>
public class BLTypeBetaling
{
    VerhuurDataContext db = new VerhuurDataContext();
	public BLTypeBetaling()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<TypeBetaling> GetBetalingTypes()
    {
        List<TypeBetaling> types = (from item in db.TypeBetalings select item).ToList<TypeBetaling>();
        return types;
    }
}