﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime LastUpdateOn { get; set; }
        public int LastUpdatedBy { get; set; }

    }
}
