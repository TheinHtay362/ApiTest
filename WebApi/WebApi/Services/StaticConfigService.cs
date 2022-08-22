using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class StaticConfigService
    {
        public static IConfiguration configuration { get; set; }
    }
}
