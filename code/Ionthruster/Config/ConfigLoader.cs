using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Ionthruster.Config
{
    public class ConfigLoader
    {
        private Lazy<Dictionary<Type, IConfig>> _configs;
        private Lazy<string> _configPath = new Lazy<string>(() =>
        {
            var configPath = ConfigurationManager.AppSettings["ConfigLoader.ConfigPath"];

            if (string.IsNullOrEmpty(configPath))
            {
                configPath = @"Config\";
            }

            return configPath;
        });

        private string ConfigPath => _configPath.Value;

        public Dictionary<Type, IConfig> Configs => _configs.Value;

        public ConfigLoader()
        {
             var configFullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, ConfigPath));

            _configs = new Lazy<Dictionary<Type, IConfig>>(() => LoadConfigs(configFullPath));
        }

        private Dictionary<Type, IConfig> LoadConfigs(string configPath)
        {
            var configJsons = LoadConfigJsons(configPath);
            var configType = typeof(IConfig);
            var configTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => configType.IsAssignableFrom(t));
            var configTypesWithJson = configTypes.Select(type =>
            {
                var jsonConfig = $"{type.Name}.json";
                var configJson = configJsons.FirstOrDefault(file => file.EndsWith($@"\{jsonConfig}"));

                return new KeyValuePair<Type, string>(type, configJson);
            })
            .Where(kv => !string.IsNullOrEmpty(kv.Value))
            .ToDictionary(kv => kv.Key, kv => kv.Value);

            return LoadConfigs(configTypesWithJson);
        }

        private Dictionary<Type, IConfig> LoadConfigs(Dictionary<Type, string> configTypesWithJson)
        {
            var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var configs = new Dictionary<Type, IConfig>();

            foreach (var key in configTypesWithJson.Keys)
            {
                var jsonContent = File.ReadAllText(configTypesWithJson[key]);

                configs.Add(key, JsonConvert.DeserializeObject(jsonContent, key) as IConfig);
            }

            return configs;
        }

        private List<string> LoadConfigJsons(string configPath)
        {
            return Directory.EnumerateFiles(configPath, "*.json").ToList();
        }
    }
}
