using Autofac;
using Ionthruster.Config;
using System;
using System.Linq;

namespace Ionthruster.Modules
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configLoader = new ConfigLoader();
            var configType = typeof(IConfig);
            var configTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => configType.IsAssignableFrom(t) && configType != t)
                .ToList();
            configTypes.ForEach(t => RegisterConfig(builder, t, configLoader));
        }

        private void RegisterConfig(ContainerBuilder builder, Type configType, ConfigLoader configLoader)
        {
            if (configLoader.Configs.ContainsKey(configType))
            {
                var configInstance = (object)configLoader.Configs[configType];

                builder.Register(_ => configInstance).As(configType);

                return;
            }

            builder.RegisterType(configType);
        }
    }
}