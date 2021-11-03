﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string TestProp { get; set; }
    }
}
