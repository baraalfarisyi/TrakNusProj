using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrakNusLemburProj.Data;

namespace TrakNusLemburProj.Models.Builder
{
    public class DataLemburBuilder : IEntityTypeConfiguration<DataLembur>
    {
        private readonly DBLemburContextSample _dBContextSample;
        public DataLemburBuilder(DBLemburContextSample dbContextSample)
        {
            _dBContextSample = dbContextSample;
        }
        public void Configure(EntityTypeBuilder<DataLembur> builder)
        {
            builder
                .Property(c => c.Id)
                .HasColumnType("UNIQUEIDENTIFIER");

            builder
                .Property(c => c.NRP)
                .HasMaxLength(7);

            builder
                .Property(c => c.Name)
                .HasMaxLength(50);

            builder
                .Property(c => c.Divisi)
                .HasMaxLength(50);

            builder
                .Property(c => c.Department)
                .HasMaxLength(50);

            builder
                .Property(p => p.TglLembur)
                .HasDefaultValue("1900-01-01");

            builder
                .Property(p => p.MulaiLembur)
                .HasDefaultValue("1900-01-01");

            builder
                .Property(p => p.AkhirLembur)
                .HasDefaultValue("1900-01-01");
        }
    }
}
