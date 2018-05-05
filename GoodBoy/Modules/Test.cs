using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using GoodBoy.Utilities;

namespace GoodBoy.Modules
{
    public class Test : ModuleBase <SocketCommandContext>
    {
        [Command("testcommand"), Summary("This is the summary for a test command.")]
        public async Task TestAsync(string firstParam, string secondParam)
        {
            await ReplyAsync($"{firstParam} + {secondParam}");
        }

        [Command("dmuser"), Summary("Test command to DM a user.")]
        public async Task DmAsync(SocketGuildUser user, [Remainder] string message)
        {
            await user.SendMessageAsync(message);
        }

        [Command("testmessage"), Summary("Just a message for testing...")]
        public async Task TestMessageAsync()
        {
            await ReplyAsync($"{Emotes.GetEmote("redmark")} Placeholder");
        }
    }
}
