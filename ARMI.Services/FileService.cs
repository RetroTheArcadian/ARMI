using ARMI.Data;
using ARMI.Dtos;
using ARMI.Helpers;
using ARMI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ARMI.Services
{
    public class FileService : IFileService
    {
        private ApplicationDbContext db;

        public FileService(ApplicationDbContext context)
        {
            db = context;
        }
        public List<RomDto> RomsWithFileInfo(List<int> romListIds)
        {
            List<RomDto> roms = db.Roms.Where(x => romListIds.Contains((int)x.RomListId)).ToDtos();
            var emulatorIds = roms.Select(x => x.EmulatorId).Distinct().ToList();
            var emulators = db.Emulators.Where(x => emulatorIds.Contains(x.EmulatorId)).ToList();
            if (db.RomLists.Select(x => x.Title).Contains("Arcade")) //arcadefolder is special since it does not have just one emulator attached
            {
                emulators.Add(new Emulator
                {
                    RomFolderName = "arcade"
                });
            }
            foreach(var emulator in emulators)
            {
                var romPath = db.Clients.First(c => c.IsMaster == true).RomFolder + "/" + emulator.RomFolderName; //could use emulator.Rompath if prefered
                var attractModePath = db.Clients.First(c => c.IsMaster == true).AttractModeFolder + "/menu-art";
                string[] romExtensions = string.IsNullOrEmpty(emulator.Romext) == true ? new string[1] { "*.*" } : emulator.Romext.Split(";");
                string[] romExtensionsWithStar = new string[romExtensions.Length];
                for (var i = 0; i < romExtensions.Length; i++)
                {
                    romExtensionsWithStar[i] = "*" + romExtensions[i];
                }
                string[] artImageExtension = { "*.png", "*.jpg", "jpeg", "*.gif", "*.bmp", "*.tga" };
                string[] artVideoExtension = { "*.mp4", "*.m4a", "*.m4v", "*.f4v", "*.f4a", "*.m4b", "*.m4r", "*.f4b", "*.mov", "*.gp", "*.3gp2", "*.3g2", "*.3gpp", "*.3gpp2", "*.ogg", "*.oga", "*.ogv", "*.ogx", "*.wmv", "*.wma", "*.asf", "*.webm", "*.avi", "*.flv", "*.ts" };
                var romFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath, romExtensionsWithStar, SearchOption.TopDirectoryOnly).Distinct().ToList();
                var boxartFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath + "/boxart", artImageExtension, SearchOption.TopDirectoryOnly).Union(DirectoryWithMultipleFileExtensionFilters.GetFiles(attractModePath + "/boxart", artImageExtension, SearchOption.TopDirectoryOnly)).Distinct().ToList();
                var cartartFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath + "/cartart", artImageExtension, SearchOption.TopDirectoryOnly).Union(DirectoryWithMultipleFileExtensionFilters.GetFiles(attractModePath + "/cartart", artImageExtension, SearchOption.TopDirectoryOnly)).Distinct().ToList();
                var fanartFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath + "/fanart", artImageExtension, SearchOption.TopDirectoryOnly).Union(DirectoryWithMultipleFileExtensionFilters.GetFiles(attractModePath + "/fanart", artImageExtension, SearchOption.TopDirectoryOnly)).Distinct().ToList();
                var flyyerFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath + "/flyer", artImageExtension, SearchOption.TopDirectoryOnly).Union(DirectoryWithMultipleFileExtensionFilters.GetFiles(attractModePath + "/flyer", artImageExtension, SearchOption.TopDirectoryOnly)).Distinct().ToList();
                var marqueeFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath + "/marquee", artImageExtension, SearchOption.TopDirectoryOnly).Union(DirectoryWithMultipleFileExtensionFilters.GetFiles(attractModePath + "/marquee", artImageExtension, SearchOption.TopDirectoryOnly)).Distinct().ToList();
                var snapFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath + "/snap", artVideoExtension, SearchOption.TopDirectoryOnly).Union(DirectoryWithMultipleFileExtensionFilters.GetFiles(attractModePath + "/snap", artImageExtension, SearchOption.TopDirectoryOnly)).Distinct().ToList();
                var wheelFiles = DirectoryWithMultipleFileExtensionFilters.GetFiles(romPath + "/wheel", artImageExtension, SearchOption.TopDirectoryOnly).Union(DirectoryWithMultipleFileExtensionFilters.GetFiles(attractModePath + "/wheel", artImageExtension, SearchOption.TopDirectoryOnly)).Distinct().ToList();

                //Some systems like Sega Megadrive have multiple emulators defined because of brandname is different i regions of the world so assume if one is found true property remains
                foreach (var rom in roms)                     
                {
                    rom.HaveRomFile = rom.HaveRomFile ? true : romFiles.Exists(x => x.Equals(rom.Name));
                    rom.HaveBoxartFile = rom.HaveBoxartFile ? true : boxartFiles.Exists(x => x.Equals(rom.Name));
                    rom.HaveCartartFile = rom.HaveCartartFile ? true : cartartFiles.Exists(x => x.Equals(rom.Name));
                    rom.HaveFanartFile = rom.HaveFanartFile ? true : fanartFiles.Exists(x => x.Equals(rom.Name));
                    rom.HaveFlyerFile = rom.HaveFlyerFile ? true : flyyerFiles.Exists(x => x.Equals(rom.Name));
                    rom.HaveMarqueeFile = rom.HaveMarqueeFile ? true : marqueeFiles.Exists(x => x.Equals(rom.Name));
                    rom.HaveSnapFile = rom.HaveSnapFile ? true : snapFiles.Exists(x => x.Equals(rom.Name));
                    rom.HaveWheelFile = rom.HaveWheelFile ? true : wheelFiles.Exists(x => x.Equals(rom.Name));
                };
            }
            return roms;
        }
    }
}
