﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response.Base
{
    public class SuccessResponse
    {
        public string Title { get; set; }
        public int StatusCode{ get; set; }
        public string SuccessMessage { get; set; }
        public object Data { get; set; }
    }
}
