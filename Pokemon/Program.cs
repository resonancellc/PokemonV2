using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StaticSQL.SetConnectionString("Server=DESKTOP-6CLE20J\\SQLEXPRESS;Database=Pokemon;Trusted_Connection=true;");

            PokemonList.FillPokemonList();

            //StaticTypes.FillPokemonList();
            //StaticTypes.FillPokemonStatsList();
            //StaticTypes.FillAttackList();
            StaticTypes.FillItemList();

            Application.Run(new StartForm());
        }
    }
}
