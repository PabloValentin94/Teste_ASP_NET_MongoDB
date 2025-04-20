using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revisao_ASP_NET_MongoDB.Models;

public class PseudoContextMongoDB : DbContext
{
    public PseudoContextMongoDB (DbContextOptions<PseudoContextMongoDB> options) : base(options) {  }

    public DbSet<Carro> Carros { get; set; } = default!;

    public DbSet<Avaliacao> Avaliacoes { get; set; } = default!;
}
