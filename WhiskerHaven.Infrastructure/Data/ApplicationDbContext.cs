using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Infrastructure.Data
{

    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {

        private readonly ILogger<ApplicationDbContext> _logger;

        /// <summary>
        /// Constructor for the ApplicationDbContext class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        /// <param name="logger">Instance of ILogger to log information.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets or sets the DbSet for managing users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for managing roles.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for managing products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for managing categories.
        /// </summary>
        public DbSet<Category> Categories { get; set; }


        /// <summary>
        /// Override method for saving changes to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges()
        {
            try
            {
                AddAuditoryAttributes();
                var response = base.SaveChanges();
                _logger.LogInformation($"Cambios guardados correctamente en la base de datos: {response}");

                return response;
            }
            catch (Exception ex)
            {
                this.ChangeTracker.Clear();
                _logger.LogWarning($"Algo salio mal: {ex}");
                
                throw ex;
            }
        }

        /// <summary>
        /// Adds auditory attributes such as created date and status to entities being added to the database.
        /// </summary>
        private void AddAuditoryAttributes()
        {
            // Detects changes in the entities being tracked by the DbContext.
            this.ChangeTracker.DetectChanges();

            // Retrieves entities that are in the Added state.
            var added = this.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added)
                        .Select(e => e.Entity)
                        .ToArray();

            // Iterates through added entities and sets auditory attributes if applicable.
            foreach (var entity in added)
            {
                // Checks if the entity is derived from BaseEntity.
                if (entity is BaseEntity)
                {

                    var track = entity as BaseEntity;
                    track.CreatedDate = DateTime.Now;
                    track.Status = true;
                }
            }
            // Retrieves entities that are in the Modified state.
            var modified = this.ChangeTracker.Entries()
                            .Where(e => e.State == EntityState.Modified)
                            .Select(e => e.Entity)
                            .ToArray();

            // Iterates through modified entities and updates auditory attributes if applicable.
            foreach (var entity in modified)
            {
                // Checks if the entity is derived from BaseEntity.
                if (entity is BaseEntity)
                {
                    // Casts the entity to BaseEntity to access its properties.
                    var track = entity as BaseEntity;
                    track.UpdatedDate = DateTime.UtcNow;
                    track.Status = true;
                }
            }
            
        }

    }
}
