namespace MoviesApi.Configurations
{
    public class MovieEntityTypeConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(
                m => m.Storeline).HasMaxLength(2500);

            builder.Property(
                m => m.Title).HasMaxLength(250);


        }
    }
}
