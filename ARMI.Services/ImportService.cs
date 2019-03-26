using ARMI.Data;
using ARMI.Dtos;
using ARMI.Dtos.EmulationStation;
using ARMI.Models;
using ARMI.Services.Jobs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ARMI.Services
{
    public class ImportService : IImportService
    {
        private readonly IHubContext<JobProgressHub> hub;
        private readonly IConfiguration cfg;
        public ImportService(IConfiguration configuration, IHubContext<JobProgressHub> hubContext)
        {
            hub = hubContext;
            cfg = configuration;
        }

        public async Task ImportEmulators(Job job, CancellationTokenSource cancellationTokenSource)
        {
            using (var db = new ApplicationDbContext(cfg))
            {
                string jobId = job.JobId.ToString();
                string emulatorPath = "";
                emulatorPath = await db.Clients.Where(c => c.IsMaster == true).Select(x => x.AttractModeFolder).ToAsyncEnumerable().First() + "/emulators";
                string[] fileEntries = Directory.GetFiles(emulatorPath);
                var progress = new ProgressDto
                {
                    Done = 0,
                    Remaining = fileEntries.Length,
                    Total = fileEntries.Count(),
                    Percent = 0,
                    JobStatus = Enums.JobStatus.Running,
                    MsgLine1 = "Please wait...",
                    MsgLine2 = ""
                };
                await hub.Clients.Group(jobId).SendAsync("progress", progress);

                try
                {
                    List<Emulator> emulatorsExisting = await db.Emulators.ToListAsync();
                    List<Emulator> emulatorsNew = new List<Emulator>();
                    List<Models.System> systems = await db.Systems.ToListAsync();
                    for (var f = 0; f < fileEntries.Length; f++)
                    {
                        var fileName = fileEntries[f];
                        var emulatorName = Path.GetFileNameWithoutExtension(fileName);
                        var percent = (int)((double)f / fileEntries.Length * 90);
                        progress = new ProgressDto
                        {
                            Done = f,
                            Remaining = fileEntries.Length - f,
                            Total = fileEntries.Count(),
                            Percent = percent,
                            JobStatus = Enums.JobStatus.Running,
                            MsgLine1 = "Importing",
                            MsgLine2 = emulatorName
                        };
                        await hub.Clients.Group(jobId).SendAsync("progress", progress);
                        db.Update(job);
                        job.JobStatus = Enums.JobStatus.Running;
                        await db.SaveChangesAsync();
                        FileStream fileStream = new FileStream(fileName, FileMode.Open);
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            Emulator emulator = new Emulator();
                            emulator.ClientId = 1;
                            emulator.SystemId = 1;
                            string line;
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                List<string> words = line.Split("  ").ToList();
                                List<string> input = new List<string>();
                                for (var i = 0; i < words.Count; i++)
                                {
                                    var word = words[i].TrimStart().TrimEnd();
                                    if (!string.IsNullOrEmpty(word.Replace(" ", "").Replace("  ", "")))
                                    {
                                        input.Add(word);
                                    }
                                }
                                if (input.Count > 1)
                                {
                                    
                                    switch (input[0])
                                    {
                                        case "executable":
                                            emulator.Executable = input[1];
                                            break;
                                        case "args":
                                            emulator.Args = input[1];
                                            break;
                                        case "rompath":                                            
                                            emulator.Rompath = input[1];
                                            emulator.RomFolderName = input[1].Contains("/") ? input[1].TrimEnd('/').Split("/").Last() : input[1].TrimEnd('\\').Split("\\").Last();
                                            emulator.SystemId = systems.FirstOrDefault(x => x.RomFolder.Equals(emulator.RomFolderName))?.SystemId;
                                            break;
                                        case "romext":
                                            emulator.Romext = input[1];
                                            break;
                                        case "system":
                                            emulator.SystemId = emulator.SystemId ?? systems.FirstOrDefault(x => x.Name.Equals(input[1]))?.SystemId;
                                            emulator.EmulatorName = emulatorName;
                                            break;
                                        case "info_source":
                                            emulator.InfoSource = input[1];
                                            break;
                                        case "artwork":
                                            if (input.Count > 1)
                                            {
                                                switch (input[1])
                                                {
                                                    case "boxart":
                                                        emulator.Boxart = input[2];
                                                        break;
                                                    case "cartart":
                                                        emulator.Boxart = input[2];
                                                        break;
                                                    case "flyer":
                                                        emulator.Boxart = input[2];
                                                        break;
                                                    case "marquee":
                                                        emulator.Boxart = input[2];
                                                        break;
                                                    case "snap":
                                                        emulator.Boxart = input[2];
                                                        break;
                                                    case "wheel":
                                                        emulator.Boxart = input[2];
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            if (emulator != null && !string.IsNullOrEmpty(emulator.EmulatorName) && !emulatorsExisting.Exists(x => x.EmulatorName.Equals(emulator.EmulatorName)))
                            {
                                emulatorsNew.Add(emulator);
                            }
                        }
                    }
                    progress = new ProgressDto
                    {
                        Done = fileEntries.Length,
                        Remaining = 0,
                        Total = fileEntries.Count(),
                        Percent = 90,
                        JobStatus = Enums.JobStatus.Running,
                        MsgLine1 = "Read all",
                        MsgLine2 = "Saving to database..."
                    };
                    await hub.Clients.Group(jobId).SendAsync("progress", progress);
                    //context.Emulators.AddRange(emulatorsNew);
                    await db.AddRangeAsync(emulatorsNew);
                    progress = new ProgressDto
                    {
                        Done = fileEntries.Length,
                        Remaining = 0,
                        Total = fileEntries.Count(),
                        Percent = 100,
                        JobStatus = Enums.JobStatus.Done,
                        MsgLine1 = "Imported",
                        MsgLine2 = "All done!"
                    };
                    await hub.Clients.Group(jobId).SendAsync("progress", progress);
                    db.Update(job);
                    job.JobStatus = Enums.JobStatus.Done;
                    job.End = DateTime.Now;

                }
                catch (Exception e)
                {
                    progress = new ProgressDto
                    {
                        Done = fileEntries.Length,
                        Remaining = 0,
                        Total = fileEntries.Count(),
                        Percent = 100,
                        JobStatus = Enums.JobStatus.Failed,
                        MsgLine1 = "Failed",
                        MsgLine2 = e.Message
                    };
                    await hub.Clients.Group(jobId).SendAsync("progress", progress);
                    db.Update(job);
                    job.JobStatus = Enums.JobStatus.Failed;
                    job.End = DateTime.Now;
                }
                finally
                {
                    cancellationTokenSource.Cancel();
                    await db.SaveChangesAsync();
                }
            }
        }
        public async Task ImportRomLists(Job job)
        {
            using (var db = new ApplicationDbContext(cfg))
            {
                string jobId = job.JobId.ToString();
                var romListPath = await db.Clients.Where(c => c.IsMaster == true).Select(x => x.AttractModeFolder).ToAsyncEnumerable().First() + "/romlists";
                var fileEntries = Directory.GetFiles(romListPath).ToList().OrderBy(x => x);
                var progress = new ProgressDto
                {
                    Done = 0,
                    Remaining = fileEntries.Count(),
                    Total = fileEntries.Count(),
                    Percent = 0,
                    JobStatus = Enums.JobStatus.Running,
                    MsgLine1 = "Importing",
                    MsgLine2 = "Please wait..."
                };
                await hub.Clients.Group(jobId).SendAsync("progress", progress);
                try
                {
                    List<RomList> romListsExisting = await db.RomLists.ToListAsync();
                    List<RomList> romListsNew = new List<RomList>();
                    var f = 0;
                    foreach (var filename in fileEntries)
                    {
                        f++;
                        var romListName = Path.GetFileNameWithoutExtension(filename);
                        if (!romListsExisting.Exists(x => x.Title.Equals(romListName)))
                        {
                            RomList romList = new RomList
                            {
                                Title = romListName
                            };
                            romListsNew.Add(romList);
                        }
                    }
                    await db.RomLists.AddRangeAsync(romListsNew);
                    await db.SaveChangesAsync();

                    //reset for 2nd run
                    romListsExisting = await db.RomLists.ToListAsync();
                    var romListsExistingDictionary = new Dictionary<string, int>();
                    foreach (var romList in romListsExisting)
                    {
                        romListsExistingDictionary.Add(romList.Title, romList.RomListId);
                    }
                    List<Emulator> emulators = await db.Emulators.ToListAsync();
                    f = 0;
                    List<Rom> roms = new List<Rom>();
                    foreach (string fileName in fileEntries)
                    {
                        f++;
                        var romListName = Path.GetFileNameWithoutExtension(fileName);
                        var romList = romListsExisting.Find(x => x.Title.Equals(romListName));

                        FileStream fileStream = new FileStream(fileName, FileMode.Open);
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            string line;
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                List<string> words = line.Split(";").ToList();
                                //0#Name;1Title;2Emulator;3CloneOf;4Year;5Manufacturer;6Category;7Players;8Rotation;
                                //9Control;10Status;11DisplayCount;12DisplayType;13AltRomname;14AltTitle;15Extra;16Buttons
                                if (!string.IsNullOrEmpty(words[0]) && words[0] != "#Name") //skip header and unreadable
                                {
                                    var emulator = emulators.Find(x => x.EmulatorName.Equals(words[2]));
                                    var wc = words.Count();
                                    Rom rom = new Rom
                                    {
                                        Name = words[0].TrimStart('#'),
                                        Title = words[1],
                                        EmulatorNameOrg = words[2],
                                        CloneOf = words[3],
                                        Year = words[4],
                                        Manufacturer = words[5],
                                        Category = words[6],
                                        Players = words[7],
                                        Rotation = words[8],
                                        Control = words[9],
                                        Status = words[10],
                                        DisplayCount = words[11],
                                        DisplayType = words[12],
                                        AltRomname = words[13],
                                        AltTitle = words[14],
                                        Extra = wc > 15 ? words[15] : "",
                                        Buttons = wc > 16 ? words[16] : "",
                                        EmulatorId = emulator?.EmulatorId,
                                        RomListId = romList?.RomListId
                                    };
                                    roms.Add(rom);
                                }
                            }
                        }
                        var percent = (int)((double)f / fileEntries.Count() * 60);
                        progress = new ProgressDto
                        {
                            Done = f,
                            Remaining = fileEntries.Count() - f,
                            Percent = percent,
                            Total = fileEntries.Count(),
                            JobStatus = Enums.JobStatus.Running,
                            MsgLine1 = "Importing",
                            MsgLine2 = romListName
                        };
                        await hub.Clients.Group(jobId).SendAsync("progress", progress);
                    }
                    progress = new ProgressDto
                    {
                        Done = fileEntries.Count(),
                        Remaining = 0,
                        Total = fileEntries.Count(),
                        Percent = 60,
                        JobStatus = Enums.JobStatus.Running,
                        MsgLine1 = "Saving database",
                        MsgLine2 = "Please wait..."
                    };
                    await hub.Clients.Group(jobId).SendAsync("progress", progress);
                    //context.Roms.AddRange(roms);
                    await db.BulkInsertAsync(roms);
                    progress = new ProgressDto
                    {
                        Done = fileEntries.Count(),
                        Remaining = 0,
                        Total = fileEntries.Count(),
                        Percent = 95,
                        JobStatus = Enums.JobStatus.Running,
                        MsgLine1 = "FixParentKeys",
                        MsgLine2 = "Please wait..."
                    };
                    await hub.Clients.Group(jobId).SendAsync("progress", progress);
                    await FixParentKeys(job);
                    progress = new ProgressDto
                    {
                        Done = fileEntries.Count(),
                        Remaining = 0,
                        Total = fileEntries.Count(),
                        Percent = 100,
                        JobStatus = Enums.JobStatus.Done,
                        MsgLine1 = "Imported emulators ",
                        MsgLine2 = "All done!"
                    };
                    await hub.Clients.Group(jobId).SendAsync("progress", progress);
                }
                catch (Exception e)
                {
                    progress = new ProgressDto
                    {
                        Done = fileEntries.Count(),
                        Remaining = 0,
                        Total = fileEntries.Count(),
                        Percent = 100,
                        JobStatus = Enums.JobStatus.Failed,
                        MsgLine1 = "Failed",
                        MsgLine2 = e.Message
                    };
                    await hub.Clients.Group(jobId).SendAsync("progress", progress);
                    db.Update(job);
                    job.JobStatus = Enums.JobStatus.Failed;
                    job.End = DateTime.Now;

                }
                finally
                {

                }
            }
        }
        public async Task FixParentKeys(Job job)
        {
            using (var db = new ApplicationDbContext(cfg))
            {
                string jobId = job.JobId.ToString();
                var romLists = await db.RomLists.ToListAsync();
                //var romListsList = romLists.ToList();
                var roms = await db.Roms.Where(x => x.EmulatorNameOrg.Equals("@")).ToListAsync();
                foreach (var rom in roms)
                {
                    var romList = romLists.FirstOrDefault(x => x.Title.Equals(rom.Name));
                    if (romList != null)
                        romList.ParentRomListId = rom.RomListId;
                }
                db.UpdateRange(romLists);
                await db.SaveChangesAsync();
            }

        }
        public async Task ImportGameListXmlFromEmulationStation(Job job)
        {
            using (var db = new ApplicationDbContext(cfg))
            {
                var client = await db.Clients.SingleAsync(x => x.IsMaster.Equals(true));
                var gameLists = Directory.EnumerateFiles(client.RomFolder, "gamelist.xml", SearchOption.AllDirectories).ToList().OrderBy(x => x);
                List<EmulationStationGameListDto> emulationStationGameListDto = new List<EmulationStationGameListDto>();
                var progress = new ProgressDto
                {
                    Done = 0,
                    Remaining = gameLists.Count(),
                    Total = gameLists.Count(),
                    Percent = 0,
                    JobStatus = Enums.JobStatus.Running,
                    MsgLine1 = "Importing EmulationStation",
                    MsgLine2 = "Please wait..."
                };
                await hub.Clients.Group(job.JobId.ToString()).SendAsync("progress", progress);
                foreach (var gameList in gameLists)
                {
                    var emulator = Path.GetDirectoryName(gameList).Replace(client.RomFolder, "").Replace("\\", "").Replace("/", "");
                    var data = ReadXmlFromEmulationStation(gameList);
                    data.FolderName = emulator;
                    emulationStationGameListDto.Add(data);
                }
                //TODO: Merge information with the result from the gamelists
                var ff = emulationStationGameListDto;
                job.JobStatus = Enums.JobStatus.Done;
                job.End = DateTime.Now;
                db.Update(job);
                await db.SaveChangesAsync();
            }
        }
        private static EmulationStationGameListDto ReadXmlFromEmulationStation(string filename)
        {
            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new XmlSerializer(typeof(EmulationStationGameListDto));
            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            EmulationStationGameListDto data = new EmulationStationGameListDto();
            try
            {
                data = (EmulationStationGameListDto)serializer.Deserialize(reader);
                fs.Close();
            }
            catch (SystemException e)
            {
                //TODO: Log the error in the job table
                Console.WriteLine("Error reading " + filename + ": " + e.Message);
            }
            finally
            {
                fs.Close();
            }
            return data;
        }
    }
}
