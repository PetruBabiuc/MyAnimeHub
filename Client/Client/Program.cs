using System;
using Interfaces;
using Views;
using System.Windows.Forms;

namespace Client
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
            IView view = new GuiView();
            view.Start();
        }
    }
}
