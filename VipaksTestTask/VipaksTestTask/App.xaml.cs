using System;
using System.Windows;
using System.Windows.Threading;
using Ninject;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Services;
using VipaksTestTask.ViewModels;

namespace VipaksTestTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message + Environment.NewLine + VipaksTestTask.Resources.AppWillBeClosed, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            //Точка компоновки
            var kernel = new StandardKernel();
            kernel.Bind<ITimerWrapper>().To<TimerWrapper>();
            kernel.Bind<ITimeManager>().To<TimeManager>().InSingletonScope();
            kernel.Bind<IScheduleProvider>().To<ScheduleProvider>();
            kernel.Bind<IPlaneCapacityProvider>().To<PlaneCapacityProvider>();
            kernel.Bind<AirportEngine>().ToSelf().InSingletonScope();
            kernel.Bind<ArrivalScoreboardViewModel>().ToSelf();
            kernel.Bind<DepartureScoreboardViewModel>().ToSelf();
            kernel.Bind<DiagramViewModel>().ToSelf();
            kernel.Bind<MainViewModel>().ToSelf();
            var mainWindow = new MainWindow{DataContext = kernel.Get<MainViewModel>()};
            mainWindow.Show();
        }

        
    }
}
