using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackScreenPopup
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Overlay form = new Overlay();
            form.Icon = new Icon("C:/Users/BeanZ/Documents/BlackScreenPopup/BlackScreenPopup/favicon.ico");
            Application.Run(form);

        }
    }
}
