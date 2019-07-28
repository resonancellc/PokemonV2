using System;
using System.Text;

namespace Pokemon
{
    public static class BattleLog
    {
        public static string Log { get; set; }

        public static StringBuilder StringBuilder = new StringBuilder();

        public static void AppendText(string text)
        {
            StringBuilder.AppendLine(text);
        }

        public static void ClearText()
        {
            StringBuilder.Clear();
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
