﻿using System;
using System.Windows;
using Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public partial class App
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        _serviceProvider = Client.Startup.Configure();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var navigation = _serviceProvider.GetRequiredService<INavigationService>();
        navigation.Navigate();

        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}