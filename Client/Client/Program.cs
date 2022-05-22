/**************************************************************************
 *                                                                        *
 *  File:        Program.cs                                               *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The main entry point for the client application.         *
 *                                                                        *
 **************************************************************************/

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
