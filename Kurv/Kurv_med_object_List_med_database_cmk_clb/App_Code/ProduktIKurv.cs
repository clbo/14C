using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProduktIKurv
/// </summary>
public class ProduktIKurv
{

    #region Felds

    private int id;
    private string navn;
    private decimal pris;
    private int antal;
    private decimal samletPris;

    #endregion

    #region Constructors
    
    public ProduktIKurv() 
    {

    }
    public ProduktIKurv(int id, string navn, decimal pris, int antal)
	{
        this.id = id;
        this.navn = navn;
        this.pris = pris;
        this.antal = antal;
        this.samletPris = pris * antal;
    }

    

    #endregion

    #region

    public int Id { get{return this.id;} set{this.id = value;} }
    public string Navn { get { return this.navn; } set { this.navn = value; } }
    public decimal Pris { get { return this.pris; } set { this.pris = value; } }
    public int Antal { get { return this.antal; } set { this.antal = value; } }
    public decimal SamletPris { get { return this.samletPris; } set { this.samletPris = value; } }

    #endregion
}