// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clscs.EF
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "Person");
            builder.HasKey(p => p.BusinessEntityId);
            builder.Property(p => p.PersonType).IsRequired();
            builder.Property(p => p.NameStyle).IsRequired();
            builder.Property(p => p.Title).IsRequired(false);
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.MiddleName).IsRequired(false);
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.Suffix).IsRequired(false);
            builder.Property(p => p.EmailPromotion).IsRequired();
            builder.Property(p => p.AdditionalContactInfo).IsRequired(false);
            builder.Property(p => p.Demographics).IsRequired(false);
            builder.Property(p => p.rowguid).IsRequired(true);
            builder.Property(p => p.ModifiedDate).IsRequired();

            builder.HasOne(p => p.BusinessEntity).WithOne().HasForeignKey<Person>(p => p.BusinessEntityId);
        }
    }
}