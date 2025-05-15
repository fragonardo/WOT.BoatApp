using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoatApp.Infrastructure.Persistence.EntityTypeConfiguration;

public class BoatEntityTypeConfiguration : IEntityTypeConfiguration<BoatApp.Domain.Models.Boat>
{
    public void Configure(EntityTypeBuilder<BoatApp.Domain.Models.Boat> builder)
    {
        builder.ToTable("Boat");

        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Id)
            .IsUnique();

        builder.Property(b => b.Id);
            //.ValueGeneratedOnAdd();
            //.HasDefaultValueSql("NEWID()");

        builder.Property(b => b.Name)
            .HasMaxLength(100);

        builder.Property(b => b.SerialNumber)
            .HasMaxLength(25)
            .IsRequired();

        builder.Property(b => b.Owner)
            .HasMaxLength(100);

        builder.Property(b => b.LaunchingDate)
            .IsRequired();

        builder.Property(b => b.Type)
            .HasConversion<int>();


        builder.HasData(
            new Domain.Models.Boat(Guid.NewGuid(),"645-BC-400", Domain.Models.BoatType.Catamaran, new DateTime(2018, 01, 01), "Donald Duck", "Sea-Duck", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-004", Domain.Models.BoatType.CabinCruiser, new DateTime(2018, 10, 01), "Archibald Haddock", "Karaboudjan", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-001", Domain.Models.BoatType.Log, new DateTime(2018, 10, 01), "Corto Maltese", "Le faucon", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-002", Domain.Models.BoatType.Deck, new DateTime(2045, 10, 01), "Kernok", "Pampoul", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "645-BC-403", Domain.Models.BoatType.Catamaran, new DateTime(2018, 01, 01), "Donald Duck", "Sea-Duck", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-008", Domain.Models.BoatType.CabinCruiser, new DateTime(2018, 10, 01), "Archibald Haddock", "Karaboudjan", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-021", Domain.Models.BoatType.Log, new DateTime(2018, 10, 01), "Corto Maltese", "Le faucon", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-031", Domain.Models.BoatType.Deck, new DateTime(2045, 10, 01), "Kernok", "Pampoul", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-008", Domain.Models.BoatType.CabinCruiser, new DateTime(2018, 10, 01), "Archibald Haddock", "Karaboudjan", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-025", Domain.Models.BoatType.Log, new DateTime(2018, 10, 01), "Corto Maltese", "Le faucon", 1),
            new Domain.Models.Boat(Guid.NewGuid(), "675-BC-035", Domain.Models.BoatType.Deck, new DateTime(2045, 10, 01), "Kernok", "Pampoul", 1)
        );
    }
}
