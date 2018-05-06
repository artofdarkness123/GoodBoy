using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GoodBoy.Services;

namespace GoodBoy.Modules
{
    public class AntiSpam : ModuleBase<SocketCommandContext>
    {
        //TODO: Implement
        [Command("addword"), Summary("Add a word to the blacklist.")]
        public async Task BlackListAsync([Remainder] string word)
        {
            AntiSpamService.AddWord(word);

            await ReplyAsync($"Added `{word}` to the blacklist.");
        }

        //TODO: Implement
        [Command("removeword"), Summary("Remove a word from the blacklist.")]
        public async Task UnBlackListAsync([Remainder] string word)
        {
            AntiSpamService.RemoveWord(word);

            await ReplyAsync($"Removed `{word}` from the blacklist.");
        }

        [Command("blacklist"), Summary("Outputs a list of all the blacklisted words.")]
        public async Task ListWordsAsync()
        {
            await ReplyAsync(AntiSpamService.ListWords());
        }
    }
}
