﻿using Client.Interfaces;
using Client.Services;
using Client.ViewModels;

namespace Client.Commands.Navigation;

internal class NavigateCommand : CommandBase
{
    private readonly INavigationService _navigationService;

    public NavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}