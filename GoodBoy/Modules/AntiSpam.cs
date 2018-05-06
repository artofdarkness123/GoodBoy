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
        [Command("blacklist"), Summary("Outputs a list of all the blacklisted words.")]
        public async Task ListWordsAsync()
        {
            await ReplyAsync(AntiSpamService.ListWords());
        }

        [Command("blacklist"), Summary("Add / Remove a word to the blacklist")]
        public async Task AddWordToBlackListAsync(string AddOrRemove, [Remainder] string word)
        {
            if (AddOrRemove.Contains("add"))
            {
                AntiSpamService.AddWord(word);
                await ReplyAsync($"Added `{word}` to the blacklist.");
            }
            else if (AddOrRemove.Contains("remove"))
            {
                AntiSpamService.RemoveWord(word);
                await ReplyAsync($"Removed `{word}` from the blacklist");
            }
            else
            {
                return;
            }
        }

        //[Command("blacklist"), Summary("Remove a word from the blacklist.")]
        //public async Task RemoveWordFromBlackListAsync(string remove, [Remainder] string word)
        //{
        //    AntiSpamService.RemoveWord(word);

        //    await ReplyAsync($"Removed `{word}` from the blacklist");
        //}
    }
}
