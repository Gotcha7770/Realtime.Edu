using System;

namespace Realtime.Edu.WPF
{
    public class BootStrap
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();

            app.Run();
        }
    }
}