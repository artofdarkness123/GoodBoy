using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace GoodBoy.Utilities
{
    class Emotes
    {
        private static Dictionary<string, string> emotes;

        static Emotes()
        {
            string json = File.ReadAllText("Utilities/Emotes.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            emotes = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetEmote(string key)
        {
            if (emotes.ContainsKey(key))
            {
                return emotes[key];
            }
            else
            {
                Console.WriteLine("No emote..");
                return "No emote.";
            }
        }
    }
}
