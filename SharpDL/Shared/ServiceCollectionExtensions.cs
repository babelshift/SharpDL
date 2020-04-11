using System;
using Microsoft.Extensions.DependencyInjection;
using SharpDL.Graphics;

namespace SharpDL.Shared
{
    public static class SharpGameServiceCollectionExtensions
    {
        /// <summary>Registers the base game services with the specified specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services">Collection of services to register base game.</param>
        /// <typeparam name="T">Type of main game class that inherits from SharpDL.Game to register with container.</typeparam>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddSharpGame<T>(this IServiceCollection services)
            where T : Game
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<T>();
            services.AddSingleton<IWindowFactory, WindowFactory>();
            services.AddSingleton<IRendererFactory, RendererFactory>();

            return services;
        }
    }
}