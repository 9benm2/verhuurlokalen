using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLContractpersoon
/// </summary>
public class BLContractPersoon
{
    VerhuurDataContext db = new VerhuurDataContext();

    public BLContractPersoon()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<ContractPersoon> GetContractPersonenByContractId(int contractId)
    {
        List<ContractPersoon> contractPersonen = db.GetContractPersonenByContractId(contractId).ToList<ContractPersoon>();
        return contractPersonen;
    }
    public List<ContractPersoon> GetContractPersonenByPersoonId(int persoonId)
    {
        List<ContractPersoon> contractPersonen = db.GetContractPersonenByPersoonId(persoonId).ToList<ContractPersoon>();
        return contractPersonen;
    }
    public int InsertContractpersoon(ContractPersoon contractPersoon, int rolId)
    {
        if (1 > 2)//tijdelijk nog geen controle
        {
            return 0;
        }
        else
        {
            contractPersoon.RolId = rolId;
            db.ContractPersoons.InsertOnSubmit(contractPersoon);
            db.SubmitChanges();
            return contractPersoon.Id;
        }
    }

}