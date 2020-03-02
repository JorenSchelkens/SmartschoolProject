using System.Collections.Generic;
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

        public Winkel GetWinkel(string winkelnaam)
        {
            this.ResetErrorMessage();

            Winkel winkel = new Winkel();

            try
            {
                this.MySqlConnection.Open();

                string sql = $"SELECT winkelnr, naam, beheerder, actief FROM tblwinkel WHERE naam = @naam;";

                MySqlCommand command = new MySqlCommand(sql, this.MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();
                command.Parameters.AddWithValue("@naam", winkelnaam);



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
                command.Parameters.AddWithValue("@actief", (artikel.actief)? 1: 0);
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
                    command.Parameters.AddWithValue("@goedgekeurd", 0);
                }
                else
                {
                    command.Parameters.AddWithValue("@goedgekeurd", 1);
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