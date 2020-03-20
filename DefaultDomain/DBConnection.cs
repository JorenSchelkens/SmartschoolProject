using System.Collections.Generic;
using MySql.Data.MySqlClient;
using InventarisDomain;
using WinkelDomain;
using SchoolbalDomain;

namespace DefaultDomain
{
    public class DBConnection
    {
        private string ConnectionString { get; set; } = "Server=localhost;Port=6000;Database=smproject;Uid=root;Pwd=BAZandpoort1920;";
        private MySqlConnection MySqlConnection { get; set; }
        public string ErrorMessage { get; set; } = "";

        //TODO: Bestelling Methode + Winkel met artikels in 1 keer terug geven

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

        public Winkel GetWinkel(string winkelnaam)
        {
            this.ResetErrorMessage();

            Winkel winkel = new Winkel();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT winkelnr, naam, beheerder, actief, goedgekeurd FROM tblwinkel WHERE naam = @naam;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                command.Parameters.AddWithValue("@naam", winkelnaam);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    winkel.winkelnr = reader.GetInt32(0);
                    winkel.naam = reader.GetString(1);
                    winkel.beheerder = reader.GetString(2);
                    winkel.actief = reader.GetBoolean(3);
                    winkel.goedgekeurd = reader.GetBoolean(4);
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

        public Inschrijving GetInschrijving(Gast gastheer, string klas)
        {
            this.ResetErrorMessage();

            Inschrijving inschrijving = new Inschrijving(gastheer, klas);
            Gast gast1 = new Gast("");
            Gast gast2 = new Gast("");

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT klas, gast1, gast2, bevestigdGastheer, secret FROM tblinschrijvingen WHERE naam = $gastheer";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                command.Parameters.AddWithValue("@naam", gastheer.Naam);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    inschrijving.klas = reader.GetString(0);
                    gast1.Naam = reader.GetString(1);
                    gast2.Naam = reader.GetString(2);
                    gastheer.Confirmed = (reader.GetInt32(3) == 1) ? true : false;
                    inschrijving.secret = reader.GetString(4);
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

            inschrijving.gast1 = gast1;
            inschrijving.gast1 = gast2;
            inschrijving.gastheer = gastheer;

            //Return object
            return inschrijving;
        }

        public Artikel GetArtikel(string productnaam)
        {
            this.ResetErrorMessage();

            Artikel artikel = new Artikel();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT productnr, productnaam, prijs, stock, winkelnr, korting, actief FROM tblartikel WHERE productnaam = @productnaam;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@productnaam", productnaam);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    artikel.productnr = reader.GetInt32(0);
                    artikel.productnaam = reader.GetString(1);
                    artikel.standaardPrijs = reader.GetDouble(2);
                    artikel.stock = reader.GetInt32(3);
                    artikel.winkelnr = reader.GetInt32(4);
                    artikel.korting = reader.GetInt32(5);
                    artikel.actief = (reader.GetInt32(6) == 1) ? true : false;
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
            return artikel;
        }

        #endregion

        #region GetAll
        public List<Winkel> GetAllWinkels()
        {
            this.ResetErrorMessage();

            List<Winkel> winkels = new List<Winkel>();
            Winkel winkel = new Winkel();
            List<Artikel> artikels = new List<Artikel>();
            Artikel artikel = new Artikel();
            try
            {
                artikels = GetAllArtikels();
                this.MySqlConnection.Open();
                string sql = $"SELECT winkelnr, naam, beheerder, actief, goedgekeurd FROM tblwinkel;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    winkel.winkelnr = reader.GetInt32(0);
                    winkel.naam = reader.GetString(1);
                    winkel.beheerder = reader.GetString(2);
                    winkel.actief = (reader.GetInt32(3) == 1) ? true : false;
                    winkel.goedgekeurd = (reader.GetInt32(4) == 1) ? true : false;

                    for (int i = 0; i < artikels.Count; i++) 
                    {
                        if (artikels[i].winkelnr == winkel.winkelnr) 
                        {
                            winkel.artikels.Add(artikels[i]);
                        }
                    }

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
                    artikel.standaardPrijs = reader.GetDouble(2);
                    artikel.stock = reader.GetInt32(3);
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

        #endregion

        #region Add KLAAR
        public bool AddArtikel(Artikel artikel)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblartikel(productnaam, prijs, stock, korting, actief, winkelnr) VALUES(@productnaam, @prijs, @stock, @korting, @actief, @winkelnr);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@productnaam", artikel.productnaam);
                command.Parameters.AddWithValue("@prijs", artikel.standaardPrijs);
                command.Parameters.AddWithValue("@stock", artikel.stock);
                command.Parameters.AddWithValue("@korting", artikel.korting);
                command.Parameters.AddWithValue("@actief", (artikel.actief) ? 1 : 0);
                command.Parameters.AddWithValue("@winkelnr", artikel.winkelnr);


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

        public bool AddInschrijving(Inschrijving inschrijving)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"INSERT INTO tblinschrijvingen(naam,klas,gast1, gast2, bevestigdGastheer, secret) VALUES(@naam, @klas , @gast1, @gast2, @bevigdGastheer, @secret);";


                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@naam", inschrijving.gastheer.Naam);
                command.Parameters.AddWithValue("@klas", inschrijving.klas);
                command.Parameters.AddWithValue("@gast1", inschrijving.gast1.Naam);
                command.Parameters.AddWithValue("@gast2", inschrijving.gast2.Naam);
                command.Parameters.AddWithValue("@bevestigdGastheer", (inschrijving.gastheer.Confirmed) ? 1 : 0);
                command.Parameters.AddWithValue("@secret", inschrijving.secret);

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

                string sql = $"INSERT INTO tblwinkel(naam, beheerder, actief,goedgekeurd) VALUES(@naam, @beheerder, @actief,@goedgekeurd);";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@naam", winkel.naam);
                command.Parameters.AddWithValue("@beheerder", winkel.beheerder);
                command.Parameters.AddWithValue("@actief", (winkel.actief) ? 1 : 0);
                command.Parameters.AddWithValue("@goedgekeurd", winkel.goedgekeurd);

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

                string sql = $"UPDATE tblartikel SET actief = @actief WHERE productnr = {artikel.productnr}";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                if (artikel.actief)
                {
                    command.Parameters.AddWithValue("@actief", 0);
                }
                else
                {
                    command.Parameters.AddWithValue("@actief", 1);
                }

                var temp = command.ExecuteNonQuery();

                if (temp > 0)
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
                    command.Parameters.AddWithValue("@actief", 1);
                }
                else
                {
                    command.Parameters.AddWithValue("@actief", 0);
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

        public bool VeranderGoedgekeurdWinkel(Winkel winkel)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"UPDATE tblwinkel SET goedgekeurd = @goedgekeurd WHERE winkelnr = {winkel.winkelnr}";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                if (winkel.goedgekeurd)
                {
                    command.Parameters.AddWithValue("@goedgekeurd", 1);
                }
                else
                {
                    command.Parameters.AddWithValue("@goedgekeurd", 0);
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

        #region Bevestigingen
        public bool VeranderInschrijvingConfirmatie(Inschrijving inschrijving)
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"UPDATE tblinschrijvingen SET bevestigdGastheer = @goedgekeurd WHERE naam = {inschrijving.gastheer.Naam}";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);

                command.Parameters.AddWithValue("@goedgekeurd", 1);

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

        public bool DeleteRowsFromsmProject()
        {
            this.ResetErrorMessage();

            bool succes = false;

            try
            {
                this.MySqlConnection.Open();

                string sql = $"DELETE from tblartikel where productnaam !=''";
                string sql1 = $"DELETE from tblwinkel where winkelnr !=''";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlCommand command1 = new MySqlCommand(sql1, this.MySqlConnection);
                if (command.ExecuteNonQuery() > 0)
                {
                    succes = true;
                }
                if (command1.ExecuteNonQuery() > 0)
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
    }
}