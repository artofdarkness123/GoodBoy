using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordHelpers;
using GoodBoy.MessageHandlers;
using GoodBoy.Utilities;

namespace GoodBoy
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandHandler _chandler;
        private OnMessage _mhandler;

        public static ConfigHelper _configHelper;

        static void Main(string[] args)
        {
            var p = new Program();
            var myTask = p.StartAsync();
            var myAwaiter = myTask.GetAwaiter();
            myAwaiter.GetResult();
        }

        public async Task StartAsync()
        {
            _configHelper = new ConfigHelper();
            _configHelper.Init();
            if (String.IsNullOrWhiteSpace(_configHelper.Bot.Token))
            {
                Console.WriteLine("You need a token in the config file for the bot to run.");
            }

            if (!DBManager.DatabaseExists())
            {
                DBManager.CreateDatabase();
            }

            if (!DBManager.TableExists("BadWords")) //TODO: Change to something else.
            {
                DBManager.BuildTables();
            }

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 100
            });

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, _configHelper.Bot.Token, true);
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
