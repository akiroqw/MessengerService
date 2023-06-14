﻿using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Messages;
using Client.Models;
using Client.Queries;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private ContactModel _currentContact;

    private ObservableCollection<MessageModel> _messages = new();

    private string _messageText;

    public ChatViewModel(UserStore userStore, ContactModel currentContact, HttpClient httpClient)
    {
        _currentContact = currentContact;
        UserStore = userStore;

        SendMessageCommand = new SendFileCommand(this, CurrentContact, httpClient);
        SendFileCommand = new SendFileCommand(this, CurrentContact, httpClient);
        GetMessagesQuery = new LoadMessagesCommand(this, httpClient);
        GetMessagesQuery.Execute(null);

        SaveEntityModelService.MessagesSaved += ((sender, args) =>
        {
            GetMessagesQuery.Execute(null);
        });
    }

    public UserStore UserStore { get; }
    public ContactModel CurrentContact
    {
        get => _currentContact;
        set
        {
            _currentContact = value;
            OnPropertyChanged(nameof(CurrentContact));
        }
    }
    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }
    public string MessageText
    {
        get => _messageText;
        set
        {
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }

    public ICommand GetMessagesQuery { get; }
    public ICommand SendMessageCommand { get; }
    public ICommand SendFileCommand { get; }
}