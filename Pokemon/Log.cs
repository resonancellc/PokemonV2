using Pokemon.Views;
using System;
using System.Text;

namespace Pokemon
{
    public class BattleLogController : IBattleLogController
    {
        public StringBuilder StringBuilder = new StringBuilder();
        private readonly IBattleView _battleView;

        public BattleLogController(IBattleView battleView)
        {
            _battleView = battleView;
        }

        public void SetText(string text)
        {
            ClearText();
            StringBuilder.AppendLine(text);
            _battleView.RefreshBattleLog();
        }

        public void ClearText()
        {
            StringBuilder.Clear();
        }
    }
}
