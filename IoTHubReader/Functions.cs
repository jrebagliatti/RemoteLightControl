﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace IoTHubReader
{
    public class Functions
    {
        [NoAutomaticTriggerAttribute]
        public static async Task ProcessMethod()
        {
            while (true)
            {
                try
                {
                    Console.Out.WriteLine("ProcessMethod call");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error occurred in processing pending altapay requests. Error : {0}", ex.Message);
                }
                await Task.Delay(TimeSpan.FromMinutes(10));
            }
        }
    }
}
