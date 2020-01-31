using System;

namespace WinkelDomain
{
    public class Transactie
    {
        public int transactienr;
        public DateTime datum;
        public double bedrag;
        public string gebruikersnaam;
        public bool omschrijving;
        public Transactie() { }
    }
}