﻿using Dotnettency.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Dotnettency
{
    public static class MultitenancyServicesExtensions
    {
        public static MultitenancyOptionsBuilder<TTenant> AddAspNetCore<TTenant>(this MultitenancyOptionsBuilder<TTenant> builder)
           where TTenant : class
        {
            var httpContextAccesser = builder.Services.FindServiceInstance<IHttpContextAccessor>();
            if (httpContextAccesser == null)
            {
                httpContextAccesser = new HttpContextAccessor();
                builder.Services.AddSingleton<IHttpContextAccessor>(httpContextAccesser);
            }

            builder.SetGenericOptionsProvider(typeof(OptionsMonitorOptionsProvider<>));
            var provider = new HttpContextProvider(httpContextAccesser);
            builder.SetHttpContextProvider(provider);
            return builder;
        }
    }
}