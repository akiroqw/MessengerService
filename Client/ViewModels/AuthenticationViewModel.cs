﻿using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using Client.Stores;
using Domain.Entities.Users;

namespace Client.ViewModels;

public sealed class AuthenticationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }
    public string Email
    {
        get => ((User)_userStore.User).Email!;
        set
        {
            ((User)_userStore.User).Email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    public string Password
    {
        get => _userStore.User.Password!;
        set
        {
            _userStore.User.Password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    public ICommand AuthenticationCommand { get; }
    public ICommand NavigateCommand { get; }
    public AuthenticationViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        _userStore = userStore;

        NavigateCommand = new NavigateCommand<RegistrationViewModel>(
            new NavigationService<RegistrationViewModel>(navigationStore,
                () => new RegistrationViewModel(httpClient, userStore, navigationStore)));

        var navigationService = new NavigationService<HomeViewModel>(
            navigationStore,
            () => new HomeViewModel(userStore, httpClient));

        AuthenticationCommand = new AuthenticationCommand(this, httpClient, userStore, navigationService);
    }
}