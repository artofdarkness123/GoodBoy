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

        public static ConfigHelper ConfigHelper;

        static void Main(string[] args)
        {
            var p = new Program();
            var myTask = p.StartAsync();
            var myAwaiter = myTask.GetAwaiter();
            myAwaiter.GetResult();
        }

        public async Task StartAsync()
        {
            ConfigHelper = new ConfigHelper();
            ConfigHelper.Init();

            if (String.IsNullOrWhiteSpace(ConfigHelper.Bot.Token))
                Console.WriteLine("You need a token in the config file for the bot to run.");

            if (String.IsNullOrWhiteSpace(ConfigHelper.Bot.Prefix))
                Console.WriteLine("You need a prefix for the bot to work!");

            if (!DBManager.DatabaseExists())
                DBManager.CreateDatabase();

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

            await _client.LoginAsync(TokenType.Bot, ConfigHelper.Bot.Token, true);
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
