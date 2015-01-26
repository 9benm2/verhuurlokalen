using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLStatus
/// </summary>
public class BLStatus
{
    VerhuurDataContext db = new VerhuurDataContext();
	public BLStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<Status> GetStatussen()
    {
        List<Status> statussen = (from item in db.Status select item).ToList<Status>();
        return statussen;
    }
}