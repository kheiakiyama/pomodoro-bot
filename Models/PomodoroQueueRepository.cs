using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroBot.Models
{
    public class PomodoroQueueRepository
    {
        public PomodoroQueueRepository()
        {
            InitializeStorage();
        }

        private CloudQueue m_Queue;

        private void InitializeStorage()
        {
            // Open storage account using credentials from .cscfg file.
            var storageAccount = CloudStorageAccount.Parse(Settings.Get("AzureWebJobsStorage"));

            // Get context object for working with queues, and 
            // set a default retry policy appropriate for a web user interface.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            //queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            // Get a reference to the queue.
            m_Queue = queueClient.GetQueueReference(PomodoroNotification.QueueName);
            m_Queue.CreateIfNotExists();
        }

        public void Add(PomodoroNotification info)
        {
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(info));
            m_Queue.AddMessage(queueMessage, timeToLive: TimeSpan.FromHours(12));
        }
    }
}