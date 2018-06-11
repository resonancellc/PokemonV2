using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class ImageHelper
    {
        public static Bitmap GetImageById(bool isPlayer, int id)
        {
            if (isPlayer)
            {
                switch (id)
                {
                    case 1: return Properties.Resources._001_bulbasaur_back;
                    case 2: return Properties.Resources._002_ivysaur_back;
                    case 3: return Properties.Resources._003_venusaur_back;
                    case 4: return Properties.Resources._004_charmander_back;
                    case 5: return Properties.Resources._005_charmeleon_back;
                    case 6: return Properties.Resources._006_charizard_back;
                    case 7: return Properties.Resources._007_squirtle_back;
                    case 8: return Properties.Resources._008_wartortle_back;
                    case 9: return Properties.Resources._009_blastoise_back;
                    case 13: return Properties.Resources._013_weedle_back;
                    case 14: return Properties.Resources._014_kakuna_back;
                    case 74: return Properties.Resources._074_geodude_back;

                    default:
                        return null;
                }
            }
            else
            {
                switch (id)
                {
                    case 1: return Properties.Resources._001_bulbasaur_front;
                    case 2: return Properties.Resources._002_ivysaur_front;
                    case 3: return Properties.Resources._003_venusaur_front;
                    case 4: return Properties.Resources._004_charmander_front;
                    case 5: return Properties.Resources._005_charmeleon_front;
                    case 6: return Properties.Resources._006_charizard_front;
                    case 7: return Properties.Resources._007_squirtle_front;
                    case 8: return Properties.Resources._008_wartortle_front;
                    case 9: return Properties.Resources._009_blastoise_front;
                    case 13: return Properties.Resources._013_weedle_front;
                    case 14: return Properties.Resources._014_kakuna_front;
                    case 74: return Properties.Resources._074_geodude_front;

                    default:
                        return null;
                }
            }
        }
    }
}
