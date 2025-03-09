﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
