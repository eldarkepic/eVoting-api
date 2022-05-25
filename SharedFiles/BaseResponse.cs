using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.SharedFiles
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
