using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLPeriode
/// </summary>
public class BLPeriode
{
    VerhuurDataContext db = new VerhuurDataContext();

    public BLPeriode()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Periode GetPeriodeById(int id)
    {
        Periode periode = (from item in db.Periodes where item.Id == id select item).Single<Periode>();
        return periode;
    }

    public int InsertPeriode(Periode periode)
    {
        List<Periode> periodes = this.GetPeriodesBetween(Convert.ToDateTime(periode.BeginPeriode), Convert.ToDateTime(periode.EindePeriode));
        // begin periode moet voor einde periode liggen en er mag geen periode overlappen, anders 0 returnen
        if (periode.BeginPeriode > periode.EindePeriode || periodes.Count != 0)
        {
            return 0;
        }
        else
        {
            db.Periodes.InsertOnSubmit(periode);
            db.SubmitChanges();
            return periode.Id;
        }
    }
    public void DeletePeriode(int periodeId)
    {
        BLContract blContract = new BLContract();
        Contract contract = blContract.GetContractIdByPeriodeId(periodeId);
        int contractId = contract.Id;
        //als periode wordt gedelete ook wissen:
        //1) betalingen
        List<Betaling> betalingen = (from item in db.GetBetalingenByContractId(contractId) select item).ToList<Betaling>();
        db.Betalings.DeleteAllOnSubmit(betalingen);        
        //2)contractpersonen
        List<ContractPersoon> contractPersonen = (from item in db.GetContractPersonenByContractId(contractId) select item).ToList<ContractPersoon>();
        db.ContractPersoons.DeleteAllOnSubmit(contractPersonen);
        //3)kamp
        Kamp kamp = (from item in db.Kamps where item.Id == contract.KampId select item).Single<Kamp>();
        db.Kamps.DeleteOnSubmit(kamp);
        //4) contract
        Contract contract1 = (from item in db.Contracts where item.Id == contractId select item).Single<Contract>();
        db.Contracts.DeleteOnSubmit(contract1);
        //5) periode zelf
        Periode periode = (from item in db.Periodes where item.Id == periodeId select item).Single<Periode>();
        db.Periodes.DeleteOnSubmit(periode);
        db.SubmitChanges();
    }
    public List<Periode> GetPeriodes()
    {
        List<Periode> periodes = (from item in db.Periodes orderby item.BeginPeriode select item).ToList<Periode>();
        return periodes;
    }
    public Periode GetPeriode(int id)
    {
        var periodes = from item in db.Periodes where item.Id == id select item;
        if (periodes.Count() == 1)
        {
            return periodes.Single<Periode>();
        }
        else
        {
            return null;
        }
    }
    public List<Periode> GetPeriodesByDate(DateTime datum)
    {
        List<Periode> periodes = (List<Periode>)(db.GetPeriodesOpDatum(datum).ToList<Periode>());
        return periodes;
    }
    public List<Periode> GetPeriodesBetween(DateTime begin, DateTime einde)
    {
        List<Periode> periodes = (List<Periode>)(db.GetPeriodesTussen(begin, einde).ToList<Periode>());
        return periodes;
    }

    public void UpdatePeriode(Periode periode)
    {
        Periode oldPeriode = this.GetPeriodeById(periode.Id);
        oldPeriode.BeginPeriode = periode.BeginPeriode;
        oldPeriode.EindePeriode = periode.EindePeriode;
        oldPeriode.TypeId = periode.TypeId;
        oldPeriode.StatusId = periode.StatusId;
        db.SubmitChanges();
    }
}
