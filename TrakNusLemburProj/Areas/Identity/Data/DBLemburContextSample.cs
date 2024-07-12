using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrakNusLemburProj.Areas.Identity.Data;
using TrakNusLemburProj.Models;
using TrakNusLemburProj.Models.Builder;
using TrakNusLemburProj.Models.ViewModels;

namespace TrakNusLemburProj.Data;

public class DBLemburContextSample : IdentityDbContext<TrakNusLemburProjUser>
{
    public DBLemburContextSample(DbContextOptions<DBLemburContextSample> options)
        : base(options)
    {
    }

    public DbSet<DataLembur> DataLemburs => Set<DataLembur>();
    public DbSet<LemburReportViewModel> LemburReportViewModels => Set<LemburReportViewModel>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new DataLemburBuilder(this).Configure(builder.Entity<DataLembur>());
        new LemburReportViewModelBuilder(this).Configure(builder.Entity<LemburReportViewModel>());
    }
}
