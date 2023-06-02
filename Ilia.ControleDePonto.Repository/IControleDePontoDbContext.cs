using Ilia.ControleDePonto.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilia.ControleDePonto.Repository
{
    public interface IControleDePontoDbContext
    {
        DbSet<MomentoData> Momentos { get; }
        int SaveChanges();
    }
}
