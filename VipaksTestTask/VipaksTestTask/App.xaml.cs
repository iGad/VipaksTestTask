﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Ninject;
using Ninject.Components;
using Ninject.Modules;
using VipaksTestTask.Services;
using VipaksTestTask.ViewModels;

namespace VipaksTestTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            MessageBox.Show(dispatcherUnhandledExceptionEventArgs.Exception.Message, "Ошибка", MessageBoxButton.OK);
            Shutdown();
        }
        
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var kernel = new StandardKernel();
            kernel.Bind<ITimerWrapper>().To<TimerWrapper>();
            kernel.Bind<ITimeManager>().To<TimeManager>().InSingletonScope();
            kernel.Bind<IScheduleProvider>().To<ScheduleProvider>();
            kernel.Bind<IPlaneCapacityProvider>().To<PlaneCapacityProvider>();
            kernel.Bind<AirportEngine>().ToSelf().InSingletonScope();
            kernel.Bind<ArrivalScoreboardViewModel>().ToSelf();
            kernel.Bind<DepartureScoreboardViewModel>().ToSelf();
            kernel.Bind<MainViewModel>().ToSelf();
            var mainWindow = new MainWindow{DataContext = kernel.Get<MainViewModel>()};
            mainWindow.Show();
        }
    }
}
