﻿using Application.Common.Abstractions.Messaging;
using Domain.Entities.Messages;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Messages.Queries.GetMessages;

public class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, MessagesResponse>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessagesResponse> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        if (!request.Sender.ChatId.HasValue)
            return new MessagesResponse(Result.Failure<IEnumerable<Message>>(
                new Error("Chat not exist")));

        return new MessagesResponse(await _messageRepository.GetMessagesAsync(
            request.Sender.ChatId.Value, cancellationToken));
    }
}