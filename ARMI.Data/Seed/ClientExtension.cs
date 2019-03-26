using ARMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARMI.Data.Seed
{
    public static class ClientExtension
    {
        public static void EnsureSeedClientData(this ApplicationDbContext context)
        {
            if (context.AllMigrationsApplied())
            {
                if (!context.Clients.Any())
                {
                    context.AddRange(ClientList);
                    context.SaveChanges();
                }
            }
        }

        public static readonly IEnumerable<Client> ClientList =
            new List<Client>
            {
                new Client
                {
                    AttractModeFolder = @"C:\arcade\attract",
                    ClientHostType = Enums.ClientHostType.Local,
                    Host = "localhost",
                    IsMaster = true,
                    Password ="",
                    Port = -1,
                    RomFolder = @"C:\arcade\roms",
                    Title = "Master",
                    UserName = ""
                },
                new Client
                {
                    AttractModeFolder = "/opt/retropie/configs/all/attractmode",
                    ClientHostType = Enums.ClientHostType.SFtp,
                    Host = "RetroPieX",
                    IsMaster = false,
                    Password ="pi",
                    Port = 22,
                    RomFolder = "/home/pi/RetroPie/roms",
                    Title = "RetroPieX",
                    UserName = "pi",
                }
            };
    }
}
