using Xunit;
using DefaultDomain;
using DefaultDomain.Classes;
using WinkelDomain;
using System;
using InventarisDomain;
using System.Collections.Generic;

namespace DefaultDomainTests
{
    public class DefaultDomainTests
    {
        public Artikel artikel { get; set; }
        public Winkel winkel { get; set; }
        private Lokaal lokaal { get; set; }

        public DefaultDomainTests()
        {
            artikel = new Artikel("malk");
            winkel = new Winkel("drekke","melkboeren");
            
        }

        [Fact]
        public void DBConnectionAddArtikelTest()
        {
            artikel.actief = true;

            DBConnection dBConnection = new DBConnection();

            bool succes = dBConnection.AddArtikel(artikel);

            Assert.True(succes);
            dBConnection.DeleteRowsFromsmProject();
        }

        [Fact]
        public void DBConnectionAddWinkelTest() 
        {
            Winkel winkel = new Winkel("dries.leyers", "test");
            winkel.naam = "dries.pannenkoek";

            DBConnection dBConnection = new DBConnection();

            bool succes = dBConnection.AddWinkel(winkel);

            Assert.True(succes);
            dBConnection.DeleteRowsFromsmProject();
        }

        [Fact]
        public void DBConnectionVeranderStatusArtikelTest()
        {
            artikel.actief = false;
            DBConnection dBConnection = new DBConnection();

            var temp = dBConnection.AddArtikel(artikel);

            artikel = dBConnection.GetArtikel(artikel.productnaam);
            bool succes = dBConnection.VeranderStatusArtikel(artikel);
            

            Assert.True(succes);
            dBConnection.DeleteRowsFromsmProject();
        }

        [Fact]
        public void DBConnectionVeranderStatusWinkelTest()
        {
            winkel.actief = false;
            DBConnection dBConnection = new DBConnection();

            var temp = dBConnection.AddWinkel(winkel);
            winkel = dBConnection.GetWinkel(winkel.naam);

            winkel.actief = true;

            Assert.True(dBConnection.VeranderStatusWinkel(winkel));
            dBConnection.DeleteRowsFromsmProject();
        }

        [Fact]
        public void DBConnectionVeranderGoedgekeurdwinkelTest()
        {
            winkel.goedgekeurd = false;
            DBConnection dBConnection = new DBConnection();

            var temp = dBConnection.AddWinkel(winkel);
            winkel = dBConnection.GetWinkel(winkel.naam);

            winkel.goedgekeurd = true;

            Assert.True(dBConnection.VeranderGoedgekeurdWinkel(winkel));
            var temp2 = 0;
            dBConnection.DeleteRowsFromsmProject();
        }

        [Fact]
        public void DBConnectionGetLokaalTest()
        {
            DBConnection dBConnection = new DBConnection();
            DBConnectionMongoDB mongoDB = new DBConnectionMongoDB();
            var temp = mongoDB.SaveLokaal(lokaal);
            lokaal = dBConnection.GetLokaal(216);
            Assert.NotNull(lokaal);
        }

        [Fact]
        public void DBConnectionGetWinkelTest()
        {
            DBConnection dBConnection = new DBConnection();
            var temp = dBConnection.AddWinkel(winkel);
            winkel = dBConnection.GetWinkel(winkel.naam);
            Assert.NotNull(winkel.artikels);
            dBConnection.DeleteRowsFromsmProject();
        }


        [Fact]
        public void DBConnectionGetAllWinkelTest()
        {
            DBConnection dBConnection = new DBConnection();
            var temp = dBConnection.AddWinkel(winkel);
            List<Winkel> winkels = new List<Winkel>();

            winkels = dBConnection.GetAllWinkels();
            Assert.NotNull(winkels);
            dBConnection.DeleteRowsFromsmProject();
        }

        [Fact]
        public void DBConnectionGetArtikelTest()
        {
            DBConnection dBConnection = new DBConnection();
            var temp = dBConnection.AddArtikel(artikel);
            artikel = dBConnection.GetArtikel("malk");
            Assert.NotNull(artikel.productnr);
        }

        [Fact]
        public void MongoDBConnectionSaveLokaalTest() 
        {
            DBConnectionMongoDB mongoDB = new DBConnectionMongoDB();
        }
    }
}