﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Helper
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Student","false"),
            new Claim("Edit Student","false"),
            new Claim("Delete Student","false")
        };
    }
}
