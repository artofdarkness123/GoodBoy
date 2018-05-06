using System;
using System.IO;
using Newtonsoft.Json;

namespace DiscordHelpers
{
    public class ConfigHelper
    {
        private const string configFolder = "Resources";
        private const string configFile = "config.json";
        private const string fileAndFolder = "Resources/config.json";
        
        public BotConfig Bot { get; set; }

        public void Init()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            if (!File.Exists(fileAndFolder))
            {
                this.Bot = new BotConfig();
                string json = JsonConvert.SerializeObject(this.Bot, Formatting.Indented);
                File.WriteAllText(fileAndFolder, json);
            }
            else
            {
                string json = File.ReadAllText(fileAndFolder);
                this.Bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this.Bot, Formatting.Indented);
            File.WriteAllText(fileAndFolder, json);
        }
    }
}
