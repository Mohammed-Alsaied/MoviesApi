using MoviesApi.Configurations;

namespace MoviesApi.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        //Singleton
        private static readonly Lazy<ApplicationDbContext> _context
           = new Lazy<ApplicationDbContext>(() => new ApplicationDbContext());

        public static ApplicationDbContext Context

        {
            get
            {
                return _context.Value;
            }
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new MovieEntityTypeConfigurations().Configure(modelBuilder.Entity<Movie>());
            new GenreEntityTypeConfigurations().Configure(modelBuilder.Entity<Genre>());

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
