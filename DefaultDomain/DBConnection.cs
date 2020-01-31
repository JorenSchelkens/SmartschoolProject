using System.Collections.Generic;
using MySql.Data.MySqlClient;
using InventarisDomain;

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
    }
}