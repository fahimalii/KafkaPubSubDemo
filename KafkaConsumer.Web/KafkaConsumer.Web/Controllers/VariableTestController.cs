﻿using KafkaConsumer.Web.GlobalVariables;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace KafkaConsumer.Web.Controllers
{
    public class VariableModel
    {
        public string Message { get; set; }
        public List<string> MessageList { get; set; }
        public int ProcessId { get; set; }
        public string ConsumerGroupId { get; set; }

        public VariableModel()
        {
            MessageList = SharedVariables.MessagesList;
        }
    }
    public class VariableTestController : Controller
    {
        // GET: VariableTest
        public ActionResult Index()
        {
            //Debug.WriteLine($"Starting {nameof(StatusUpdateWorker)} for consuming messages");

            //HostingEnvironment.QueueBackgroundWorkItem(
            //    cancellationToken => new StatusUpdateWorker().StartProcessing(cancellationToken));

            var model = new VariableModel
            {
                Message = SharedVariables.Message,
                ProcessId = Process.GetCurrentProcess().Id,
                ConsumerGroupId = SharedVariables.ConsumerGroupID
            };

            return View(model);
        }

        public ActionResult Index2()
        {
            //Debug.WriteLine($"Starting {nameof(StatusUpdateWorker)} for consuming messages");

            //HostingEnvironment.QueueBackgroundWorkItem(
            //    cancellationToken => new StatusUpdateWorker().StartProcessing(cancellationToken));

            var model = new VariableModel
            {
                Message = SharedVariables.TimeBasedMessage,
                ProcessId = Process.GetCurrentProcess().Id,
                ConsumerGroupId = SharedVariables.TimeBasedConsumerGroupID
            };

            return View(model);
        }
    }
}