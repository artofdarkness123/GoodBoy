using System;
using System.IO;
using Newtonsoft.Json;

namespace GoodBoy
{
    class Config
    {
        private const string configFolder = "Resources";
        private const string configFile = "config.json";
        private const string fileAndFolder = "Resources/config.json";

        public static BotConfig bot;

        static Config()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            if (!File.Exists(fileAndFolder))
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(fileAndFolder, json);
            }
            else
            {
                string json = File.ReadAllText(fileAndFolder);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }

        public struct BotConfig
        {
            public string token;
            public string prefix;
        }
    }
}
