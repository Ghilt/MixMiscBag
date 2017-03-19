using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandpiles3D
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
            Presenter presenter = new Presenter();
            Sandpiles3DRender f1 = new Sandpiles3DRender(presenter);
            presenter.SetView(f1);
            Application.Run(f1);
        }
    }
}
