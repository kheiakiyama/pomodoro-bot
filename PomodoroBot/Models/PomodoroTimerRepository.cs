﻿using Microsoft.WindowsAzure.Storage;
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
            var connectionString = Settings.Get("AzureWebJobsStorage");
            Trace.WriteLine(connectionString);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            m_Table = tableClient.GetTableReference(tableName);
            m_Table.CreateIfNotExists();
        }

        public async Task<PomodoroTimerEntity> Find(string key)
        {
            TableQuery<PomodoroTimerEntity> query = new TableQuery<PomodoroTimerEntity>()
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, key));
            var response = await m_Table.ExecuteQuerySegmentedAsync(query, null);
            return response.Results.Count == 0 ? null : response.Results[0];
        }

        private static readonly string tableName = "pomodorotimer";
        private CloudTable m_Table;

        public async Task Add(PomodoroTimerEntity entity)
        {
            await m_Table.ExecuteAsync(TableOperation.Insert(entity));
        }
    }
}
