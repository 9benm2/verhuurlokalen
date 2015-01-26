using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLType
/// </summary>
public class BLType
{
    VerhuurDataContext db = new VerhuurDataContext();
	public BLType()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<Type> GetTypes()
    {
        List<Type> types = (from item in db.Types select item).ToList<Type>();
        return types;
    }
}