using System;
using System.Windows.Forms;

namespace SNTN
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
            //костыль пиздец, но мне ща не до этого
            try
            {
                Application.Run(new LoginForm());
            }
            catch (ObjectDisposedException)
            {
                
            }
        }
    }
}
