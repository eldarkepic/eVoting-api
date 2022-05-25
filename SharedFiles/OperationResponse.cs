using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.SharedFiles
{
    public class OperationResponse<T> : BaseResponse
    { 
        public T Data { get; set; }

    }
}
