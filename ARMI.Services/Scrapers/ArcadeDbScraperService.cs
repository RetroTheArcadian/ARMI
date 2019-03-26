using ARMI.Data;
using ARMI.Dtos.ScraperDtos;
using ARMI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ARMI.Services.Scrapers
{
    public class ArcadeDbScraperService: BaseScraperService, IArcadeDbScraperService
    {
        // Source http://adb.arcadeitalia.net
        // Scraper instructions http://adb.arcadeitalia.net/service_scraper.php
        private static readonly string searchUrlPre = "http://adb.arcadeitalia.net/service_scraper.php?ajax=query_mame&lang=en&use_parent=1&game_name=";
        private static readonly HttpClient client = new HttpClient();
        private ApplicationDbContext db;

        public ArcadeDbScraperService(ApplicationDbContext context)
        {
            db = context;
        }
        private static async Task<ArcadeDbDto> GetDataFromListOfRoms(List<Rom> roms, string jobId)
        {
            var romsString = string.Join(";", roms.Select(x => x.Name).Distinct());
            var streamTask = client.GetStreamAsync(searchUrlPre + romsString);
            var serializer = new DataContractJsonSerializer(typeof(ArcadeDbDto));
            var arcadeDbDto = serializer.ReadObject(await streamTask) as ArcadeDbDto;
            return arcadeDbDto;
        }
        public async Task GetDataFromRomList(int romListId, string jobId)
        {
            //using (var db = new ApplicationDbContext())
            //{
                var roms = await db.Roms.Where(x => x.RomListId.Equals(romListId)).ToListAsync();
                var romData = await GetDataFromListOfRoms(roms, jobId);
            //}
        }
    }
}
