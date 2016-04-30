using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroBot.Models
{
    public class PomodoroTimerRepository
    {
        public PomodoroTimerRepository()
        {
            var connectionString = Settings.Get("StorageConnectionString");
            Trace.WriteLine(connectionString);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            m_Table = tableClient.GetTableReference(tableName);
            m_Table.CreateIfNotExists();
        }

        private static readonly string tableName = "pomodorotimer";
        private CloudTable m_Table;

        public async Task Add(PomodoroTimerEntity entity)
        {
            await m_Table.ExecuteAsync(TableOperation.Insert(entity));
        }
    }
}
