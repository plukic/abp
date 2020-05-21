using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Volo.Abp.UI.Navigation
{
    public class MenuConfigurationContext : IMenuConfigurationContext
    {
        public IServiceProvider ServiceProvider { get; }
        private readonly object _serviceProviderLock = new object();

        private TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                lock (_serviceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                    }
                }
            }

            return reference;
        }

        public IAuthorizationService AuthorizationService => LazyGetRequiredService(typeof(IAuthorizationService), ref _authorizationService);
        private IAuthorizationService _authorizationService;

        private IStringLocalizerFactory _stringLocalizerFactory;
        public IStringLocalizerFactory StringLocalizerFactory => LazyGetRequiredService(typeof(IStringLocalizerFactory),ref _stringLocalizerFactory);

        public ApplicationMenu Menu { get; }

        public MenuConfigurationContext(ApplicationMenu menu, IServiceProvider serviceProvider)
        {
            Menu = menu;
            ServiceProvider = serviceProvider;
        }
    }
}
