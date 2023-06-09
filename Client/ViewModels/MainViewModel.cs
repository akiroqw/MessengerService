﻿using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly NavigationStore _navigationStore;
    private readonly UserStore _userStore;

    public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore,
        UserStore userStore)
    {
        _navigationStore = navigationStore;
        _modalNavigationStore = modalNavigationStore;
        _userStore = userStore;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
    }

    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public bool IsOpen => _modalNavigationStore.IsOpen;

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnCurrentModalViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsOpen));
    }

    public void OnWindowClosing()
    {
        UserStoreSettingsService.SaveUserStore(_userStore);
    }
}