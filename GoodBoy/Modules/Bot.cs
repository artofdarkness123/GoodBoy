using System;
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
        //TODO: Deserialize and reserialize / find a way to fix this so it sets the actual bot info in config.json...
        [Command("setprefix"), Summary("Set the prefix for the bot.")]
        [RequireOwner]
        public async Task SetPrefixAsync(string prefix)
        {
            Program._configHelper.Bot.Prefix = prefix;
            await ReplyAsync($"Successfully set the prefix to `{prefix}`");
            Program._configHelper.Save();
        }

        [Command("prefix"), Summary("Gets the current bot prefix.")]
        public async Task GetPrefixAsync()
        {
            await ReplyAsync($"The current bot prefix is `{Program._configHelper.Bot.Prefix}`");
        }

        [Command("ping"), Summary("Pings the bot.")]
        [RequireOwner]
        public async Task PingAsync()
        {
            await ReplyAsync($"Pong! The current latency is `{Context.Client.Latency}ms`.");
        }

        [Command("stop"), Summary("Shut down the bot.")]
        [RequireOwner]
        public async Task StopAsync()
        {
            await ReplyAsync("Goodbye for now!");
            await Context.Client.StopAsync();
        }
    }
}
