﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    class BadCredentialsException : Exception
    {
        public BadCredentialsException(string message) : base(message)
        {
        }
    }
}
