﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2.Models.Entities;
using Garage2.Models.ViewModels;

namespace Garage2.Data
{
    public class Garage2Context : DbContext
    {
        public Garage2Context (DbContextOptions<Garage2Context> options)
            : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; } = default!;
        public DbSet<OverviewViewModel> OverviewViewModel { get; set; } = default!;

    }
}
