namespace MoviesApi.Configurations
{
    public class GenreEntityTypeConfigurations : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property
                (g => g.Name).HasMaxLength(100);

            builder.Property
                (g => g.Id).ValueGeneratedOnAdd();

        }
    }
}
