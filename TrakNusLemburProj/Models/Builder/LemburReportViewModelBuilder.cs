using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrakNusLemburProj.Data;
using TrakNusLemburProj.Models.ViewModels;

namespace TrakNusLemburProj.Models.Builder
{
    public class LemburReportViewModelBuilder : IEntityTypeConfiguration<LemburReportViewModel>
    {
        private readonly DBLemburContextSample _dBContextSample;
        public LemburReportViewModelBuilder(DBLemburContextSample dbContextSample)
        {
            _dBContextSample = dbContextSample;
        }
        public void Configure(EntityTypeBuilder<LemburReportViewModel> builder)
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
                .Property(p => p.Bulan)
                .HasColumnType("int")
                .HasPrecision(10);

            builder
                .Property(p => p.Tahun)
                .HasColumnType("int")
                .HasPrecision(10);

            builder
                .Property(p => p.TotalJamLembur)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
        }
    }
}
