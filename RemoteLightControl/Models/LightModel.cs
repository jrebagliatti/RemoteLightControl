using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RemoteLightControl.Models
{
    public class LightModel
    {
        public int Id { get; set; }

        public String DeviceId { get; set; }

        public String Description { get; set; }

        public float Value { get; set; }
    }
}