using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public static class StaticMain
    {
        public static ICollection<Form> openedForms = new List<Form>();

        public static void FormOpened(Form form)
        {
            openedForms.Add(form);
        }
        public static void FormClosed(Form form)
        {
            openedForms.Remove(form);
        }

    }
}
