using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;

namespace MediaSwap.Core.Services
{
    public class PlatformService : BaseContext , IPlatformService
    {
        public IEnumerable<Models.Platform> GetPlatforms()
        {
            using (var context = GetContext())
            {
                return context.Platform.ToList();
            }
        }

        public Models.Platform GetPlatform(int platformId)
        {
            using (var context = GetContext())
            {
                return context.Platform.FirstOrDefault(p => p.PlatformTypeId == platformId);
            }
        }
    }
}
