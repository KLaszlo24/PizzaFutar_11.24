using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaFutar
{
    internal class Adatbazis
    {
        private readonly string connectionString;

        public Adatbazis()
        {
            this.connectionString = "server=localhost;database=pizzafutár;user=root;password='';";
        }
        public List<Rendeles> Feltolt()
        {
            List< Rendeles > lista = new List<Rendeles>();
            Dictionary<int, Pizza> pizzak = new Dictionary<int, Pizza>();
            Dictionary<int, Cim> cimek = new Dictionary<int, Cim>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Az adatbázis felépült");
                    string query = "SELECT * FROM pizza";
                    using(MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                string nev = reader.GetString("nev");
                                int meret = reader.GetInt32("meret");
                                int ar = reader.GetInt32("ar");
                                Pizza pizza = new Pizza(id, nev, meret, ar);
                                pizzak.Add(id, pizza);
                            }
                        }
                    }
                    query = "SELECT * FROM cim";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                string nev = reader.GetString("nev");
                                string utca = reader.GetString("utca");
                                string hsz = reader.GetString("hsz");
                                Cim cim = new Cim(id, nev, utca, hsz);
                                cimek.Add(id, cim);
                            }
                        }
                    }
                    query = "SELECT * FROM rendeles";
                    using(MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                int pizzaId = reader.GetInt32("pizzaid");
                                int db = reader.GetInt32("darab");
                                int cimId = reader.GetInt32("cimid");
                                TimeSpan ido = reader.GetTimeSpan("szallitas");
                                Pizza pizza = pizzak[pizzaId];
                                Cim cim = cimek[cimId];
                                Rendeles rendeles = new Rendeles(id, pizza, db, cim, ido);
                                lista.Add(rendeles);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba az adatbázis kapcsolódásban" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba az adatbázis kapcsolódásban" + ex);
            }
            return lista;
        }
    }
}
