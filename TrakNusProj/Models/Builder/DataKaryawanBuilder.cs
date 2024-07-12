using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrakNusProj.Data;

namespace TrakNusProj.Models.Builder
{
    public class DataKaryawanBuilder : IEntityTypeConfiguration<DataKaryawan>
    {
        private readonly DBContextSample _dBContextSample;
        public DataKaryawanBuilder(DBContextSample dbContextSample)
        {
            _dBContextSample = dbContextSample;
        }
        public void Configure(EntityTypeBuilder<DataKaryawan> builder)
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
        }
    }
}
