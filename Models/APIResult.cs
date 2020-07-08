using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeAPI.Models
{
    public class APIResult
    {
        public bool IsSucceed { get; set; }

        public string ErrorMsg { get; set; }

        public object Data { get; set; }
    }
}
