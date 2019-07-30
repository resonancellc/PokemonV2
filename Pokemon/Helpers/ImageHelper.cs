using System.Drawing;

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
                    case 10: return Properties.Resources._010_caterpie_back;
                    case 11: return Properties.Resources._011_metapod_back;
                    case 12: return Properties.Resources._012_butterfree_back;
                    case 13: return Properties.Resources._013_weedle_back;
                    case 14: return Properties.Resources._014_kakuna_back;
                    case 15: return Properties.Resources._015_beedrill_back;
                    case 16: return Properties.Resources._016_pidgey_back;
                    case 17: return Properties.Resources._017_pidgeotto_back;
                    case 18: return Properties.Resources._018_pidgeot_back;
                    case 19: return Properties.Resources._019_rattata_back;
                    case 20: return Properties.Resources._020_raticate_back;
                    case 21: return Properties.Resources._021_spearow_back;
                    case 37: return Properties.Resources._037_vulpix_back;
                    case 69: return Properties.Resources._069_bellsprout_back;
                    case 74: return Properties.Resources._074_geodude_back;
                    case 77: return Properties.Resources._077_ponyta_back;
                    case 81: return Properties.Resources._081_magnemite_back;
                    case 131: return Properties.Resources._131_lapras_back;
                    case 147: return Properties.Resources._147_dratini_back;

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
                    case 10: return Properties.Resources._010_caterpie_front;
                    case 11: return Properties.Resources._011_metapod_front;
                    case 12: return Properties.Resources._012_butterfree_front;
                    case 13: return Properties.Resources._013_weedle_front;
                    case 14: return Properties.Resources._014_kakuna_front;
                    case 15: return Properties.Resources._015_beedrill_front;
                    case 16: return Properties.Resources._016_pidgey_front;
                    case 17: return Properties.Resources._017_pidgeotto_front;
                    case 18: return Properties.Resources._018_pidgeot_front;
                    case 19: return Properties.Resources._019_rattata_front;
                    case 20: return Properties.Resources._020_raticate_front;
                    case 21: return Properties.Resources._021_spearow_front;
                    case 37: return Properties.Resources._037_vulpix_front;
                    case 69: return Properties.Resources._069_bellsprout_front;
                    case 74: return Properties.Resources._074_geodude_front;
                    case 77: return Properties.Resources._077_ponyta_front;
                    case 81: return Properties.Resources._081_magnemite_front;
                    case 131: return Properties.Resources._131_lapras_front;
                    case 147: return Properties.Resources._147_dratini_front;

                    default:
                        return null;
                }
            }
        }

        public static Bitmap GetItemImageById(int id)
        {
            switch (id)
            {
                case 1: return Properties.Resources.item_potion;
                case 2: return Properties.Resources.item_fullHeal;
                case 3: return Properties.Resources.item_superPotion;
                case 4: return Properties.Resources.item_xAttack;
                default:
                    return null;
            }
        }
    }
}
