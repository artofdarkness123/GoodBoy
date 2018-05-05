using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace GoodBoy.MessageHandlers
{
    class OnMessage
    {
        DiscordSocketClient _client;

        public async Task InitAsync(DiscordSocketClient client)
        {
            _client = client;
            _client.MessageReceived += HandleMessages;
            await Task.CompletedTask;
        }

        private async Task HandleMessages(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            var context = new SocketCommandContext(_client, msg);

            if (context.Message.Author == _client.CurrentUser) return;
            if (string.IsNullOrWhiteSpace(msg.Content.ToString())) return;

            //Implement antispam...

            await Task.CompletedTask;
        }
    }
}
