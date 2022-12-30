namespace MoviesApi.Configurations
{
    public class MoviesDtoModelTypeConfiguration : IEntityTypeConfiguration<MoviesDto>
    {
        public void Configure(EntityTypeBuilder<MoviesDto> builder)
        {
            builder.Property(
                m => m.Storeline).HasMaxLength(2500);

            builder.Property(
                m => m.Title).HasMaxLength(250);
        }
    }
}
