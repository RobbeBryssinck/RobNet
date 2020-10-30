﻿using System;
using System.Threading;
using System.Threading.Tasks;
using BotnetJobAPI.Domain.Entities;

namespace BotnetJobAPI.Data.Repository.v1
{
    public interface IBotnetJobRepository
    {
        Task<BotnetJob> GetBotnetJobByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}