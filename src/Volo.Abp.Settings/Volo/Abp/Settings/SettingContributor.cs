﻿using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Settings
{
    public abstract class SettingContributor : ISettingContributor, ISingletonDependency
    {
        public abstract string EntityType { get; }

        protected ISettingStore SettingStore { get; }

        protected SettingContributor(ISettingStore settingStore)
        {
            SettingStore = settingStore;
        }

        public abstract Task<string> GetOrNullAsync(string name, bool fallback);

        public abstract Task<string> GetOrNullAsync(string name, string entityId, bool fallback = true);
    }
}