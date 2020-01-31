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
                    tempVoorwerp.naamItem = reader.GetString(1);

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

                string sql = $"SELECT winkelnr, naam, beheerder, actief FROM [tblwinkel];";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    winkel.winkelnr = reader.GetInt32(0);
                    winkel.naam = reader.GetString(1);
                    winkel.beheerder = reader.GetString(2);
                    winkel.actief = (reader.GetInt32(3) == 1) ? true : false;
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
            Bestelling bestelling= new Bestelling();

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

        public List<Rekening> GetallRekeningen()
        {
            this.ResetErrorMessage();

            List<Rekening> rekeningen = new List<Rekening>();
            Rekening rekening= new Rekening();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT gebruikersnaam, krediet FROM tblrekening;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    rekening.gebruikersnaam= reader.GetString(0);
                    rekening.krediet= reader.GetInt32(1);

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
        #endregion

        #region Add
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
        #endregion

        #region Delete
        #endregion



    }
}