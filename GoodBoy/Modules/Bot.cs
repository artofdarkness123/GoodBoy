﻿using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace GoodBoy.Modules
{
    public class Bot : ModuleBase<SocketCommandContext>
    {
        [Command("setprefix"), Summary("Set the prefix for the bot.")]
        [RequireOwner]
        public async Task SetPrefixAsync(string prefix)
        {
            Config.bot.prefix = prefix;
            await ReplyAsync($"Successfully set the prefix to `{prefix}`");
        }

        [Command("prefix"), Summary("Gets the current bot prefix.")]
        public async Task GetPrefixAsync()
        {
            await ReplyAsync($"The current bot prefix is `{Config.bot.prefix}`");
        }

        [Command("ping"), Summary("Pings the bot.")]
        [RequireOwner]
        public async Task PingAsync()
        {
            await ReplyAsync($"Pong! The current latency is `{Context.Client.Latency}ms`.");
        }
    }
}