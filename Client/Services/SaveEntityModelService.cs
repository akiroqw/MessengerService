﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Properties;
using Newtonsoft.Json;

namespace Client.Services;

public class SaveEntityModelService
{
    private static readonly object _messageLock = new();
    private static readonly object _contactLock = new();
    public static event EventHandler? MessagesSaved;

    public static void SaveEntity(MessageModel message)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            message.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{message.Id}.json");

        lock (_messageLock)
        {
            File.WriteAllText(filePath, json, encoding);
        }
    }

    public static void SaveEntity(ContactModel contact)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{contact.Id}.json");

        lock (_contactLock)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new StreamWriter(fileStream, encoding);
            writer.Write(json);
        }
    }

    public static async Task SaveEntityAsync(MessageModel message, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            message.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{message.Id}.json");


        await File.WriteAllTextAsync(filePath, json, encoding, cancellationToken);
    }

    public static async Task SaveEntityAsync(ContactModel contact, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{contact.Id}.json");

        await File.WriteAllTextAsync(filePath, json, encoding, cancellationToken);
    }

    public static async Task SaveMessagesAsync(IEnumerable<MessageModel> messages, CancellationToken cancellationToken)
    {
        foreach (var message in messages) await SaveEntityAsync(message, cancellationToken);

        OnMessagesSaved(EventArgs.Empty);
    }

    public static void SaveMessages(IEnumerable<MessageModel> messages)
    {
        foreach (var message in messages) SaveEntity(message);

        OnMessagesSaved(EventArgs.Empty);
    }


    protected static void OnMessagesSaved(EventArgs e)
    {
        MessagesSaved?.Invoke(null, e);
    }
}