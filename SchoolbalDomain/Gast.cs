using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolbalDomain
{
    public class Gast
    {
        public string Naam { get; set; }
        public bool Confirmed { get; set; }

        public Gast(string naam)
        {
            this.Naam = naam;
            Confirmed = false;
        }
    }
}