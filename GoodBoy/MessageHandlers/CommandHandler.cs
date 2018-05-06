using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using GoodBoy.Utilities;

namespace GoodBoy.MessageHandlers
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;

        public async Task InitAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;

            if (context.Message.Author == _client.CurrentUser) return;
            if (msg.Content == null) return;

            if (msg.HasStringPrefix(Program.ConfigHelper.Bot.Prefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);

                if (result.Error == CommandError.UnknownCommand)
                {
                    Console.WriteLine($"Unknown command sent in #{context.Channel.ToString()} in the server {context.Guild.ToString()}");
                    await context.Channel.SendMessageAsync($"{Emotes.GetEmote("redmark")} Unknown command. Do `{Program.ConfigHelper.Bot.Prefix}help` for a list of commands :)");
                }

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
