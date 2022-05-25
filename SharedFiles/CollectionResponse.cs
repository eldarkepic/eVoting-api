﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.SharedFiles
{
    public class CollectionResponse<T> : BaseResponse
    {
        public IEnumerable<T> Records { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? PagesCount { get; set; }
    }
}
