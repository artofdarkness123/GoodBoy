using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace GoodBoy.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        
        public Help(CommandService service)
        {
            _service = service;
        }

        [Command("help"), Summary("Get a list of all of the commands.")]
        public async Task HelpAsync()
        {
            string prefix = Program._configHelper.Bot.Prefix;
            var embed = new EmbedBuilder()
            {
                Color = Color.Green,
                Title = $"For more info on a command, use {prefix}help (Command Name)"
                //Description = $"For more info on a command, use {prefix}help <Command Name>"
            };

            foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"`{prefix}{cmd.Name}` - {cmd.Summary}\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    embed.AddField(x =>
                    {
                        x.Name = $"# {module.Name}";
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync("", false, embed.Build());
        }

        [Command("help"), Summary("Get info about a certain command.")]
        public async Task HelpAsync(string command)
        {
            var result = _service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command named **{command}**.");
                return;
            }

            string prefix = Program._configHelper.Bot.Prefix;
            var embed = new EmbedBuilder()
            {
                Color = Color.Green
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;
                string parameters = null;

                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    parameters += $"**<{cmd.Parameters[i]}>** ";
                }

                if (cmd.Parameters.Count == 0)
                {
                    embed.AddField(x =>
                    {
                        x.Name = $"Command: {cmd.Name}";
                        x.Value = $"`Summary:` {cmd.Summary}\n" +
                        $"`Usage:` {prefix}{cmd.Name}\n";
                        x.IsInline = false;
                    });
                }
                else if (cmd.Parameters.Count == 1)
                {
                    embed.AddField(x =>
                    {
                        x.Name = $"Command: {cmd.Name}";
                        x.Value = $"`Summary:` {cmd.Summary}\n" +
                        $"`Usage:` {prefix}{cmd.Name} <{string.Join("", cmd.Parameters)}>\n";
                        x.IsInline = false;
                    });
                }
                else
                {
                    embed.AddField(x =>
                    {
                        x.Name = $"Command: {cmd.Name}";
                        x.Value = $"`Summary:` {cmd.Summary}\n" +
                        $"`Usage:` {prefix}{cmd.Name} {parameters}";
                    });
                }
            }

            await ReplyAsync("", false, embed);
        }
    }
}
