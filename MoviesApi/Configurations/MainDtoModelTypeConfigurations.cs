namespace MoviesApi.Configurations
{
    public class MainDtoModelTypeConfigurations : IEntityTypeConfiguration<MainDto>
    {
        public void Configure(EntityTypeBuilder<MainDto> builder)
        {
            builder.Property
                (d => d.Name).HasMaxLength(100);


        }
    }
}
