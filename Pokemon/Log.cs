using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Log
    {

    }

    public static class BattleLog
    {
        public static string Log { get; set; }

        public static void AppendText(string text)
        {
            Log += Environment.NewLine;
            Log += text;
        }

        public static void ClearText()
        {
            Log = "";
        }
    }

    public static class DebugLog
    {
        public static string Log { get; set; }

        public static void AppendText(string text)
        {
            Log += text + Environment.NewLine;
        }
    }
}
