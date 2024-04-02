using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Services.CategoryService.Commands.CreateCategoryCommand;
using WhiskerHaven.Application.Services.CategoryService.Commands.DeleteCategoryCommand;
using WhiskerHaven.Application.Services.CategoryService.Commands.UpdateCategoryCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.CreateProductCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.DeleteProductCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.UpdateProductCommand;
using WhiskerHaven.Application.Services.UserService.SignIn;
using WhiskerHaven.Application.Services.UserService.SignUp;

namespace WhiskerHaven.Application
{
    /// <summary>
    /// Provides extension methods for configuring services in the IServiceCollection related to MediatR.
    /// </summary>
    public static class ServiceCollectionExtensions 
    {
        /// <summary>
        /// Registers MediatR handlers and services for different command and query types.
        /// </summary>
        /// <param name="services">The collection of services to add to.</param>
        /// <returns>The modified collection of services.</returns>
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            // Register MediatR handlers and services for category-related commands.
            #region Services Category
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateCategoryCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteCategoryCommandHandler).Assembly));
            #endregion

            // Register MediatR handlers and services for product-related commands.
            #region Services Product
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateProductCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteProductCommandHandler).Assembly));
            #endregion

            // Register MediatR handlers and services for user-related commands.
            #region Services User
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignUpCommandHandler).Assembly));
            #endregion
            return services;
        } 
    }
}
