﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public interface IUserService
    {   
        User GetUser { get; set; }
        User SaveUser { get; set; }
        User ValidateUser(string token);
    }
}
