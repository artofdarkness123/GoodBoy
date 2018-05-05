using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using GoodBoy.Utilities;

namespace GoodBoy.Modules
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        [Command("kick"), Summary("Kick a user from the discord.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickAsync(SocketGuildUser user = null, [Remainder] string reason = null)
        {
            //TODO: Permission checking and some other stuff to make sure shit doesnt break...
            if (user == null)
            {
                await ReplyAsync($"{Emotes.GetEmote("redmark")}Who would you like me to kick?");
                return;
            }

            if (user == Context.User)
            {
                await ReplyAsync($"{Emotes.GetEmote("redmark")} You cant kick yourself!");
                return;
            }

            if (string.IsNullOrWhiteSpace(reason))
            {
                await ReplyAsync($"{Emotes.GetEmote("redmark")} Please provide a reason to kick the user."); //TODO: Emoji
                return;
            }

            try
            {
                await user.SendMessageAsync($"You have been kicked from the server `{Context.Guild.Name.ToString()}` for the reason `{reason}`");
                await ReplyAsync($"{Emotes.GetEmote("greenmark")} {user.Mention} has been kicked.");
                await user.KickAsync(reason);
            }
            catch
            {
                await ReplyAsync($"{Emotes.GetEmote("redmark")} An error occured. Are you trying to kick someone with a higher rank then the bot? If not please contact @temporis#6402 on discord."); //TODO: Emoji
            }
        }

        [Command("clear"), Summary("Clear a given amount of messages from a channel.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task ClearAsync(int amount = 0)
        {
            if (amount == 0)
            {
                await ReplyAsync($"{Emotes.GetEmote("redmark")} Please specify how many messages you want to be cleared");
                return;
            }

            var msgs = await Context.Channel.GetMessagesAsync(amount + 1).Flatten(); //Get messages

            try
            {
                await Context.Channel.DeleteMessagesAsync(msgs); //Delete messages.

                if (amount == 1)
                {
                    await ReplyAsync($"{Emotes.GetEmote("greenmark")} {amount} message has been cleared");
                }
                else
                {
                    await ReplyAsync($"{Emotes.GetEmote("greenmark")} {amount} messages have been cleared.");
                }
            }
            catch
            {
                await ReplyAsync($"{Emotes.GetEmote("redmark")} An error has occurred. Please contact @temporis#6402 on discord.");
            }
        }
    }
}
