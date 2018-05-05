using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using GoodBoy.MessageHandlers;

namespace GoodBoy
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandHandler _chandler;
        private OnMessage _mhandler;

        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null)
            {
                Console.WriteLine("You need a token in the config file for the bot to run.");
            }

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 100
            });

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, Config.bot.token, true);
            await _client.StartAsync();
            await _client.SetGameAsync("with a ball.");

            //Command handler.
            _chandler = new CommandHandler();
            await _chandler.InitAsync(_client);

            //Message handler / antispam
            _mhandler = new OnMessage();
            await _mhandler.InitAsync(_client);

            await Task.Delay(-1);
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            await Task.CompletedTask;
        }
    }
}
