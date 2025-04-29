// SGLinearSessionMonitor/Models/ConfigSettings.cs
using Newtonsoft.Json;
using System;
using System.IO;

namespace SGLinearSessionMonitor.Models
{
    public class ConfigSettings
    {
        public int MaxInstancesPerUser { get; set; }
        public int MonitorIntervalSeconds { get; set; }

        private static string configFilePath = "config.json";

        public static ConfigSettings Load()
        {
            if (!File.Exists(configFilePath))
            {
                return new ConfigSettings
                {
                    MaxInstancesPerUser = 3,   // Limite de 3 instâncias por usuário
                    MonitorIntervalSeconds = 10 // Intervalo de monitoramento de 10 segundos
                };
            }

            var json = File.ReadAllText(configFilePath);
            return JsonConvert.DeserializeObject<ConfigSettings>(json);
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(configFilePath, json);
        }
    }
}
