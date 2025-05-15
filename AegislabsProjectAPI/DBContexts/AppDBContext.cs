using AegislabsProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AegislabsProjectAPI.DBContexts
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<MahasiswaModel> Mahasiswa { get; set; }
        public DbSet<JurusanModel> Jurusan { get; set; }
        public DbSet<MataKuliahModel> MataKuliah { get; set; }
        public DbSet<KRSModel> KRS { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
