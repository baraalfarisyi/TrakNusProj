using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrakNusProj.Areas.Identity.Data;
using TrakNusProj.Models;
using TrakNusProj.Models.Builder;

namespace TrakNusProj.Data;

public class DBContextSample : IdentityDbContext<TrakNusProjUser>
{
    public DBContextSample(DbContextOptions<DBContextSample> options)
        : base(options)
    {
    }
    public DbSet<DataKaryawan> DataKaryawans => Set<DataKaryawan>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new DataKaryawanBuilder(this).Configure(builder.Entity<DataKaryawan>());
    }
}
