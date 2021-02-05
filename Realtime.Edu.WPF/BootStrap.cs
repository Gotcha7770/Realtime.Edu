using System;
using Autofac;
using LiteDB.Realtime;
using ReactiveUI;
using Realtime.Edu.Core.ViewModels;
using Splat;
using Splat.Autofac;

namespace Realtime.Edu.WPF
{
    public class BootStrap
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();
            
            // Build a new Autofac container.
            var container = new ContainerBuilder();
            container.RegisterType<MainWindow>().As<IViewFor<MainViewModel>>();
            
            // Use Autofac for ReactiveUI dependency resolution.
            // After we call the method below, Locator.Current and
            // Locator.CurrentMutable start using Autofac locator.
            container.UseAutofacDependencyResolver();
            
            // These .InitializeX() methods will add ReactiveUI platform 
            // registrations to your container. They MUST be present if
            // you *override* the default Locator.
            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();

            app.Run();
        }
    }
}