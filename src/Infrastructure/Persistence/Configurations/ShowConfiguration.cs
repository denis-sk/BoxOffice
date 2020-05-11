using System;
using System.Collections.Generic;
using System.Text;
using BoxOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoxOffice.Infrastructure.Persistence.Configurations
{
    public class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
