using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLContract
/// </summary>
public class BLContract
{
    VerhuurDataContext db = new VerhuurDataContext();

    public BLContract()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Contract GetContractById(int id)
    {
        Contract contract = (from item in db.Contracts where item.Id == id select item).Single<Contract>();
        return contract;
    }

    public int InsertContract(Contract contract)
    {
        db.Contracts.InsertOnSubmit(contract);
        db.SubmitChanges();
        return contract.Id;
    }
    public Contract GetContractIdByPeriodeId(int periodeId)
    {
        Contract contract = (from item in db.Contracts where periodeId == item.PeriodeId select item).Single<Contract>();
        return contract;
    }
}