﻿using System.Collections.Generic;
using MySql.Data.MySqlClient;
using InventarisDomain;
using WinkelDomain;

namespace DefaultDomain
{
    public class DBConnection
    {
        private string ConnectionString { get; set; } = "Server=localhost;Port=6000;Database=smproject;Uid=root;Pwd=BAZandpoort1920;";
        private MySqlConnection MySqlConnection { get; set; }
        public string ErrorMessage { get; set; } = "";

        public DBConnection()
        {
            this.MySqlConnection = new MySqlConnection(this.ConnectionString);
        }
        private void ResetErrorMessage()
        {
            this.ErrorMessage = "";
        }

        #region Get
        public Lokaal GetLokaal(int lokaalnr)
        {
            //Reset error message zodat deze altijd van de opgeroepe methode is
            this.ResetErrorMessage();

            //Object of lijst van objecten klaarzetten
            Lokaal lokaal = new Lokaal();

            //SQL uitvoeren
            try
            {
                //Open connectie
                this.MySqlConnection.Open();

                //SQL code klaarzetten
                string sql = $"SELECT lokaalnr, lokaalverantwoordelijke FROM tbllokaal WHERE lokaalnr = {lokaalnr};";

                //SQL uitvoeren
                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                //Per rij de waarde(s) van de kolom(men) uitlezen en eventueel gebruiken
                while (reader.Read())
                {
                    /* 
                        De reader werkt door het datatype te vragen en dan de kolom nummer mee te geven
                        bv: reader.GetInt32(0);
                        dit zal de data van kolom 1 (index --> 0) als een int terug geven
                    */

                    lokaal.lokaalNr = reader.GetInt32(0);
                    lokaal.lokaalVerantwoordelijke = reader.GetString(1);
                }

                //Reader sluiten
                reader.Close();
            }
            catch (MySqlException ex)
            {
                //Bij een error word de ToString van die error op ErrorMessage gezet zodat dit gebruikt kan worden, voornamelijk tijdens het developen
                this.ErrorMessage = ex.ToString();
            }

            //Connectie sluiten
            this.MySqlConnection.Close();

            //Return object
            return lokaal;
        }

        public Winkel GetWinkel(int winkelnr)
        {
            this.ResetErrorMessage();

            Winkel winkel = new Winkel();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT winkelnr, naam, beheerder, actief FROM tblwinkel WHERE winkelnr = {winkelnr};";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {

                    winkel.winkelnr = reader.GetInt32(0);
                    winkel.naam = reader.GetString(1);
                    winkel.beheerder = reader.GetString(2);
                    winkel.actief = reader.GetBoolean(3);
                }

                //Reader sluiten
                reader.Close();
            }
            catch (MySqlException ex)
            {
                //Bij een error word de ToString van die error op ErrorMessage gezet zodat dit gebruikt kan worden, voornamelijk tijdens het developen
                this.ErrorMessage = ex.ToString();
            }

            //Connectie sluiten
            this.MySqlConnection.Close();

            //Return object
            return winkel;
        }

        #endregion

        #region GetAll
        public List<Voorwerp> GetAllVoorwerpen()
        {
            this.ResetErrorMessage();

            List<Voorwerp> voorwerpen = new List<Voorwerp>();
            Voorwerp tempVoorwerp = new Voorwerp();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT voorwerpnr, item FROM tblvoorwerp;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tempVoorwerp.voorwerpNr = reader.GetInt32(0);
                    tempVoorwerp.voorwerpNaam = reader.GetString(1);

                    voorwerpen.Add(tempVoorwerp);
                    tempVoorwerp = new Voorwerp();
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return voorwerpen;
        }
        public List<Lokaal> GetAllLokalen()
        {
            this.ResetErrorMessage();

            List<Lokaal> lokalen = new List<Lokaal>();
            Lokaal tempLokaal = new Lokaal();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT lokaalnr, lokaalverantwoordelijke FROM tbllokaal;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tempLokaal.lokaalNr = reader.GetInt32(0);
                    tempLokaal.lokaalVerantwoordelijke = reader.GetString(1);

                    lokalen.Add(tempLokaal);
                    tempLokaal = new Lokaal();
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return lokalen;
        }
        public List<Winkel> GetAllWinkels()
        {
            this.ResetErrorMessage();

            List<Winkel> winkels = new List<Winkel>();
            Winkel winkel = new Winkel();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT winkelnr, naam, beheerder, actief FROM tblwinkel;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    winkel.winkelnr = reader.GetInt32(0);
                    winkel.naam = reader.GetString(1);
                    winkel.beheerder = reader.GetString(2);
                    winkel.actief = (reader.GetInt32(3) == 1) ? true : false;

                    winkels.Add(winkel);
                    winkel = new Winkel();
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return winkels;
        }
        public List<Artikel> GetAllArtikels()
        {
            this.ResetErrorMessage();

            List<Artikel> artikels = new List<Artikel>();
            Artikel artikel = new Artikel();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT productnr, productnaam, prijs, stock, winkelnr, korting, actief FROM tblartikel;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    artikel.productnr = reader.GetInt32(0);
                    artikel.productnaam = reader.GetString(1);
                    artikel.prijs = reader.GetDouble(2);
                    artikel.stock = reader.GetInt32(3);
                    artikel.winkelnr = reader.GetInt32(4);
                    artikel.korting = reader.GetInt32(5);
                    artikel.actief = reader.GetBoolean(6);

                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return artikels;
        }
        public List<Bestelling> GetAllBestellingen()
        {
            this.ResetErrorMessage();

            List<Bestelling> bestellingen = new List<Bestelling>();
            Bestelling bestelling = new Bestelling();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT bestelnr, datum, gebruikersnaam, prijs FROM tblbestlling;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bestelling.bestelnr = reader.GetInt32(0);
                    bestelling.datum = reader.GetDateTime(1);
                    bestelling.gebruikersnaam = reader.GetString(2);
                    bestelling.prijs = reader.GetDouble(3);

                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return bestellingen;
        }
        public List<Rekening> GetAllRekeningen()
        {
            this.ResetErrorMessage();

            List<Rekening> rekeningen = new List<Rekening>();
            Rekening rekening = new Rekening();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT gebruikersnaam, krediet FROM tblrekening;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    rekening.gebruikersnaam = reader.GetString(0);
                    rekening.krediet = reader.GetInt32(1);

                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return rekeningen;
        }
        public List<Transactie> GetAllTransacties()
        {
            this.ResetErrorMessage();

            List<Transactie> transacties = new List<Transactie>();
            Transactie transactie = new Transactie();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT transactienr, datum, bedrag, gebruikersnaam, beschrijving FROM tbltransacties;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    transactie.transactienr = reader.GetInt32(0);
                    transactie.datum = reader.GetDateTime(1);
                    transactie.bedrag = reader.GetDouble(2);
                    transactie.gebruikersnaam = reader.GetString(3);
                    transactie.beschrijving = reader.GetString(4);
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return transacties;
        }

        #endregion

        #region Add KLAAR

        public bool AddVoorwerp(Voorwerp voorwerp)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblvoorwerp(voorwerpnaam) VALUES(@voorwerpNaam);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@voorwerpNaam", voorwerp.voorwerpNaam);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddLokaal(Lokaal lokaal)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tbllokaal(lokaalnr, lokaalverantwoordelijke) VALUES(@lokaalnr, @lokaalverantwoordelijke);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@lokaalnr", lokaal.lokaalNr);
                command.Parameters.AddWithValue("@lokaalverantwoordelijke", lokaal.lokaalVerantwoordelijke);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddItemsInLokaal(ItemsInLokaal itemsInLokaal)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblitemsinlokalen(lokaalnr, voorwerpnr, aantal) VALUES(@lokaalnr, @voorwerpnr, @aantal);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@lokaalnr", itemsInLokaal.lokaalNr);
                command.Parameters.AddWithValue("@voorwerpnr", itemsInLokaal.voorwerpNr);
                command.Parameters.AddWithValue("@aantal", itemsInLokaal.aantal);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddArtikel(Artikel artikel)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblartikel(productnaam, prijs, stock, winkelnr, korting, actief) VALUES(@productnaam, @prijs, @stock, @winkelnr, @korting, @actief);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@productnaam", artikel.productnaam);
                command.Parameters.AddWithValue("@prijs", artikel.prijs);
                command.Parameters.AddWithValue("@stock", artikel.stock);
                command.Parameters.AddWithValue("@winkelnr", artikel.winkelnr);
                command.Parameters.AddWithValue("@korting", artikel.korting);
                command.Parameters.AddWithValue("@actief", artikel.actief);


                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddWinkel(Winkel winkel)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblwinkel(naam, beheerder, actief) VALUES(@naam, @beheerder, @actief);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@naam", winkel.naam);
                command.Parameters.AddWithValue("@beheerder", winkel.beheerder);
                command.Parameters.AddWithValue("@actief", (winkel.actief) ? 1 : 0);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddBestelling(Bestelling bestelling)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblbestelling(datum, gebruikersnaam, prijs) VALUES(@datum, @gebruikersnaam, @prijs);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@datum", bestelling.datum);
                command.Parameters.AddWithValue("@gebruikersnaam", bestelling.gebruikersnaam);
                command.Parameters.AddWithValue("@prijs", bestelling.prijs);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddBesteldeArtikels(BesteldArtikel besteldArtikel)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblbesteldeartikels(bestelnr, productnr, prijs) VALUES(@bestelnr, @productnr, @prijs);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@bestelnr", besteldArtikel.bestelnr);
                command.Parameters.AddWithValue("@productnr", besteldArtikel.productnr);
                command.Parameters.AddWithValue("@prijs", besteldArtikel.prijs);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddRekening(Rekening rekening)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblrekening(gebruikersnaam, krediet) VALUES(@gebruikersnaam, @krediet);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@gebruikersnaam", rekening.gebruikersnaam);
                command.Parameters.AddWithValue("@krediet", rekening.krediet);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        public bool AddTransacties(Transactie transactie)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tbktransacties(datum, bedrag, gebruikersnaam, beschrijving) VALUES(@datum, @bedrag, @gebruikersnaam, @beschrijving);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@datum", transactie.datum);
                command.Parameters.AddWithValue("@bedrag", transactie.bedrag);
                command.Parameters.AddWithValue("@gebruikersnaam", transactie.gebruikersnaam);
                command.Parameters.AddWithValue("@beschrijving", transactie.beschrijving);

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;
        }
        #endregion

        #region Active/Non Active KLAAR
        public bool VeranderStatusArtikel(Artikel artikel)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"UPDATE tblartikels SET actief = @actief WHERE productnr = {artikel.productnr}";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                if (artikel.actief)
                {
                    command.Parameters.AddWithValue("@actief", 0);
                }
                else
                {
                    command.Parameters.AddWithValue("@actief", 1);
                }

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;

        }
        public bool VeranderStatusWinkel(Winkel winkel)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"UPDATE tblwinkel SET actief = @actief WHERE winkelnr = {winkel.winkelnr}";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                if (winkel.actief)
                {
                    command.Parameters.AddWithValue("@actief", 0);
                }
                else
                {
                    command.Parameters.AddWithValue("@actief", 1);
                }

                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
                succes = false;
            }

            this.MySqlConnection.Close();

            return succes;

        }
        #endregion

        #region GetAll DATA

        public Lokaal GetAllDataLokaal(int lokaalnr)
        {
            this.ResetErrorMessage();

            Lokaal lokaal = new Lokaal();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT lokaalnr, lokaalverantwoordelijke FROM tbllokaal WHERE lokaalnr = {lokaalnr};";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lokaal.lokaalNr = reader.GetInt32(0);
                    lokaal.lokaalVerantwoordelijke = reader.GetString(1);
                }

                lokaal = GetAllItemsInLokaal(lokaal);

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return lokaal;
        }

        private Lokaal GetAllItemsInLokaal(Lokaal lokaal)
        {
            this.ResetErrorMessage();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int temp = reader.GetInt32(1);
                    string naam = GetVoorwerpNaam(temp).ToLower();

                    switch (naam)
                    {
                        case "stoel":
                            lokaal.aantalStoelen = reader.GetInt32(2);
                            break;

                        case "bank":
                            lokaal.aantalBanken = reader.GetInt32(2);
                            break;

                        case "beamers":
                            lokaal.aantalBeamers = reader.GetInt32(2);
                            break;

                        case "computers":
                            lokaal.aantalComputers= reader.GetInt32(2);
                            break;

                        case "schermen":
                            lokaal.aantalStoelen = reader.GetInt32(2);
                            break;
                    }
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return lokaal;
        }

        private string GetVoorwerpNaam(int nr)
        {
            this.ResetErrorMessage();

            string itemnaam = "";

            try
            {
                this.MySqlConnection.Open();

                string sql = $"";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    itemnaam = reader.GetString(1);

                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.ToString();
            }

            this.MySqlConnection.Close();

            return itemnaam;
        }

        #endregion



    }
}