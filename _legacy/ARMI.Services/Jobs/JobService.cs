using ARMI.Data;
using ARMI.Enums;
using ARMI.Models;
using Coravel.Queuing.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ARMI.Services.Jobs
{
    public class JobService:IJobService
    {
        private ApplicationDbContext _db;
        private CancellationToken _token;
        private static ConcurrentDictionary<Guid, CancellationTokenSource> _tokens = new ConcurrentDictionary<Guid, CancellationTokenSource>();

        private readonly IImportService _importService;
        private readonly IQueue _queue;


        public JobService(
            ApplicationDbContext context, 
            IQueue queue,
            IImportService importService
        )
        {
            _db = context;
            _queue = queue;
            _importService = importService;
        }
        private void SetToken(CancellationToken token) => _token = token;

        public async Task<List<Job>> GetJobs()
        {
            return await _db.Jobs.OrderBy(x => x.Start).ToListAsync();
        }

        public async Task<List<Job>> GetJobs(string search, int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IAsyncEnumerable<Job> query;
            switch (sortColumn)
            {
                case "Start":
                    if (sortByAscending)
                        query = _db.Jobs
                            .Where(x => string.IsNullOrEmpty(search) ? 1 == 1 : x.Title.Contains(search))
                            .OrderBy(x => x.Start)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .ToAsyncEnumerable();
                    else
                        query = _db.Jobs
                            .Where(x => string.IsNullOrEmpty(search) ? 1 == 1 : x.Title.Contains(search))
                            .OrderByDescending(x => x.Start)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .ToAsyncEnumerable();
                    break;
                case "JobStatus":
                    if (sortByAscending)
                        query = _db.Jobs
                            .Where(x => string.IsNullOrEmpty(search) ? 1 == 1 : x.Title.Contains(search))
                            .OrderBy(x => x.JobStatus).ThenBy(x=>x.Start)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .ToAsyncEnumerable();
                    else
                        query = _db.Jobs
                            .Where(x => string.IsNullOrEmpty(search) ? 1 == 1 : x.Title.Contains(search))
                            .OrderByDescending(x => x.Start).ThenByDescending(x=>x.Start)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .ToAsyncEnumerable();
                    break;
                case "Title":
                default:
                    if (sortByAscending)
                        query = _db.Jobs
                            .Where(x => string.IsNullOrEmpty(search) ? 1 == 1 : x.Title.Contains(search))
                            .OrderBy(x => x.Title).ThenBy(x => x.Start)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .ToAsyncEnumerable();
                    else
                        query = _db.Jobs
                            .Where(x => string.IsNullOrEmpty(search) ? 1 == 1 : x.Title.Contains(search))
                            .OrderByDescending(x => x.Title).ThenByDescending(x => x.Start)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .ToAsyncEnumerable();
                    break;
            }
            return await query.ToList();
        }

        public async Task<List<Job>> GetRunningJobs()
        {
            return await _db.Jobs.Where(x=>x.JobStatus.Equals(JobStatus.Running)).OrderBy(y => y.Start).ToListAsync();
        }

        private Job CreateJob(string title)
        {
            Guid jobId = Guid.NewGuid();
            Job job = new Job
            {
                JobIdGuid = jobId.ToString(),
                Title = title,
                JobStatus = JobStatus.Queued,
                Start = DateTime.Now
            };
            _db.Add(job);
            _db.SaveChanges();
            return job;
        }

        public string StartImportEmulators()
        {
            Job job = CreateJob("Import Emulators");
            CancellationTokenSource newToken = new CancellationTokenSource();
            _tokens.TryAdd(new Guid(job.JobIdGuid), newToken);
            _queue.QueueTask(async () => {
                SetToken(newToken.Token);
                while (!_token.IsCancellationRequested)
                {
                    await _importService.ImportEmulators(job, newToken);
                }                
            });
            return job.JobId.ToString();
        }

        public string StartImportRomLists()
        {
            Job job = CreateJob("ImportRomLists");
            CancellationTokenSource newToken = new CancellationTokenSource();
            _tokens.TryAdd(new Guid(job.JobIdGuid), newToken);
            _queue.QueueAsyncTask(async () => {
                SetToken(newToken.Token);
                while (!_token.IsCancellationRequested)
                {
                    await _importService.ImportRomLists(job);
                }
            });
            return job.JobId.ToString();
        }

        public string StartImportGameListXmlFromEmulationStation()
        {
            Job job = CreateJob("Import ES GameListXml");
            CancellationTokenSource newToken = new CancellationTokenSource();
            _tokens.TryAdd(new Guid(job.JobIdGuid), newToken);
            _queue.QueueAsyncTask(async () => {
                SetToken(newToken.Token);
                while (!_token.IsCancellationRequested)
                {
                    await _importService.ImportGameListXmlFromEmulationStation(job);
                }
            });
            return job.JobId.ToString();
        }

        public string CancelJob(Guid guid)
        {
            // Make sure we remove and dispose tokens to avoid leaking...
            _tokens.TryRemove(guid, out var token);
            token.Cancel();
            token.Dispose();
            return "Job was cancelled";
        }        
    }
}
