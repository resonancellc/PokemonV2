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
                    case 1:
                        return Properties.Resources._001_bulbasaur_back;
                        
                    case 2:
                        return Properties.Resources._002_ivysaur_back;

                    case 4:
                        return Properties.Resources._004_charmander_back;

                    case 7:
                        return Properties.Resources._007_squirtle_back;

                    case 8:
                        return Properties.Resources._008_wartortle_back;

                    case 13:
                        return Properties.Resources._013_weedle_back;

                    case 14:
                        return Properties.Resources._014_kakuna_back;

                    case 74:
                        return Properties.Resources._074_geodude_back;


                    default:
                        return null;
                }
            }
            else
            {
                switch (id)
                {
                    case 1:
                        return Properties.Resources._001_bulbasaur_front;

                    case 2:
                    //return Properties.Resources._002;

                    case 4:
                        return Properties.Resources._004_charmander_front;

                    case 7:
                        return Properties.Resources._007_squirtle_front;

                    case 8:
                        return Properties.Resources._008_wartortle_front;

                    case 13:
                        return Properties.Resources._013_weedle_front;

                    case 14:
                        return Properties.Resources._014_kakuna_front;

                    case 74:
                        return Properties.Resources._074_geodude_front;


                    default:
                        return null;
                }
            }
        }
    }
}
