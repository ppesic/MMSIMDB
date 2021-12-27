using Microsoft.EntityFrameworkCore;
using MMSIMDB.Application.Interfaces;
using MMSIMDB.Domain.Entities;
using MMSIMDB.Mapping;

namespace MMSIMDB.Persistence.Contexts
{
    public class MMSIMDBDBContext : DbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public MMSIMDBDBContext(DbContextOptions<MMSIMDBDBContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieType> MovieType { get; set; }
        public DbSet<MovieUserRating> MovieUserRating { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MovieMap).Assembly);
            base.OnModelCreating(builder);

            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(el => el.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
