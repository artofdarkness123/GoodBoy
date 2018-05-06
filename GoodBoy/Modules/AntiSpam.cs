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

        [Command("blacklist"), Summary("Add a word to the blacklist.")]
        public async Task AddWordToBlackListAsync(string add, [Remainder] string word)
        {
            if (add.Contains("add"))
            {
                AntiSpamService.AddWord(word);
                await ReplyAsync($"Added `{word}` to the blacklist.");
            }
            else
            {
                return;
            }
        }

        [Command("blacklist"), Summary("Remove a word from the blacklist.")]
        public async Task RemoveWordFromBlacklistAsync(string remove, [Remainder] string word)
        {
            if (remove.Contains("remove"))
            {
                AntiSpamService.RemoveWord(word);
                await ReplyAsync($"Removed `{word}` from the blacklist.");
            }
            else
            {
                return;
            }
        }

        [Command("blacklist"), Summary("Outputs a list of all the blacklisted words.")]
        public async Task ListWordsAsync()
        {
            var result = AntiSpamService.ListWords();
            await ReplyAsync(String.IsNullOrWhiteSpace(result) ? "<emptyset>" : result);
        }

        [Command("blacklist"), Summary("Clears the blacklist.")]
        public async Task ClearBlackListAsync(string clear)
        {
            if (clear.Contains("clear"))
            {
                AntiSpamService.ClearList();
                await ReplyAsync("Cleared the blacklist");
            }
            else
            {
                return;
            }
        }
    }
}
