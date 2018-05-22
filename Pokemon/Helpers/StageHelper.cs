using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class StageHelper
    {
        /// <summary>
        /// Converts stage to multipler
        /// </summary>
        /// <param name="stage"></param>
        /// <returns>Multipler</returns>
        public static float StageToMultipler(int stage)
        {
            float value = 1;
            switch (stage)
            {
                case -6:
                    value = 0.25f;
                    break;
                case -5:
                    value = 0.285f;
                    break;
                case -4:
                    value = 0.33f;
                    break;
                case -3:
                    value = 0.4f;
                    break;
                case -2:
                    value = 0.5f;
                    break;
                case -1:
                    value = 0.66f;
                    break;
                case 0:
                    value = 1;
                    break;
                case 1:
                    value = 1.5f;
                    break;
                case 2:
                    value = 2f;
                    break;
                case 3:
                    value = 2.5f;
                    break;
                case 4:
                    value = 3f;
                    break;
                case 5:
                    value = 3.5f;
                    break;
                case 6:
                    value = 4f;
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}
