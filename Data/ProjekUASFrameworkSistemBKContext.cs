using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjekUASFrameworkSistemBK.Models;

namespace ProjekUASFrameworkSistemBK.Data
{
    public class ProjekUASFrameworkSistemBKContext : DbContext
    {
        public ProjekUASFrameworkSistemBKContext(DbContextOptions<ProjekUASFrameworkSistemBKContext> options)
            : base(options)
        {
        }

        public DbSet<SiswaViewModel> Siswa { get; set; }
        public DbSet<Konseling> Konseling { get; set; }
        public DbSet<Registrasi> Registrasi { get; set; } // Tambahkan ini
    }
}