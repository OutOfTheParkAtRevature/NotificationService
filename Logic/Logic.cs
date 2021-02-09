using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository;
using System.Threading;
using System.Threading.Tasks;
using Model;
using System;

namespace Logic
{
    public class Logic
    {
        public Logic() { }
        public Logic(Repo repo, ILogger<Repo> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        private readonly Repo _repo;
        private readonly ILogger<Repo> _logger;
       
    }
}
