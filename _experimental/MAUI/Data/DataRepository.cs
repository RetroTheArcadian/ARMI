using SQLite;
using System.Threading.Tasks;
using ARMI.Data.Models;

namespace ARMI.Data;

public class DataRepository
{
    string _dbPath;
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection conn;

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        //Ensure tables are created and if they are change CreateTableAsync will attempts to update the db schema
        await conn.CreateTableAsync<GameDevice>();    
    }

    public DataRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public async Task AddDevice(string name){
        int result = 0;
        try{
            await Init();

            if(string.IsNullOrEmpty(name))
                throw new Exception("Valid devicename required");

            result = await conn.InsertAsync(new GameDevice{ Name = name});

            StatusMessage = string.Format("{0} device record(s) added [Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add device {0}. Error: {1}", name, ex.Message);
        }
    }
    public async Task<List<GameDevice>> GetAllDevices()
    {
        try
        {
            await Init();
            return await conn.Table<GameDevice>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve device data. {0}", ex.Message);
        }

        return new List<GameDevice>();
    }
}