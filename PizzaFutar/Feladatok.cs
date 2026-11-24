using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaFutar
{
    internal class Feladatok
    {
        List<Rendeles> lista;

        public Feladatok()
        {
            Adatbazis adatbazis = new Adatbazis();
            lista = adatbazis.Feltolt();
            Kiir();
        }
        private void Kiir()
        {
            foreach (var item in lista)
            {
                Console.WriteLine($" {item.Cim.Nev} - {item.Pizza.Nev} - {item.Db}");
            }
        }
    }
}
