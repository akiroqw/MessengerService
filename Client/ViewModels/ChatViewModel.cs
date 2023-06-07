﻿using System;
using System.Collections.Generic;
using Client.Models;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using System.Collections.ObjectModel;
using System.Windows;
using Client.Commands.Messages;
using MediatR;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private ContactModel _currentContact;
    public ContactModel CurrentContact
    {
        get => _currentContact;
        set
        {
            _currentContact = value;
            OnPropertyChanged(nameof(CurrentContact));
        }
    }

    private ObservableCollection<MessageModel> _messages = new();
    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    private string _messageText;
    public string MessageText
    {
        get => _messageText;
        set
        {
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }
    public ICommand SendMessageCommand { get; }

    public ChatViewModel(ContactModel currentContact, HttpClient httpClient)
    {
        _currentContact = currentContact;

        SendMessageCommand = new SendMessageCommand(CurrentContact, httpClient, this);
    }

}