using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.SharedFiles
{
    public class LoginResponse : BaseResponse
    {
        public string AccessToken { get; set; }
        public DateTime? ExpiryDate { get; set; }
        
    }
}
