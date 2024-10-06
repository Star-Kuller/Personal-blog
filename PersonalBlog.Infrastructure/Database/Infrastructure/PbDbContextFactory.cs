using Microsoft.EntityFrameworkCore;

namespace PersonalBlog.Infrastructure.Database.Infrastructure;

public class PbDbContextFactory :  DesignTimeDbContextFactoryBase<PbDbContext>
{
    protected override PbDbContext CreateNewInstance(DbContextOptions<PbDbContext> options)
    { 
        return new PbDbContext(options);
    }
}