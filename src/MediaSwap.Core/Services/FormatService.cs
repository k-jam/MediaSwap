using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;

namespace MediaSwap.Core.Services
{
    public class FormatService : BaseContext , IFormatService
    {
        public IEnumerable<Models.Format> GetFormats()
        {
            using (var context = GetContext())
            {
                return context.Format.ToList();
            }
        }

        public Models.Format GetFormat(int formatTypeId)
        {
            using (var context = GetContext())
            {
                return context.Format.FirstOrDefault(p => p.FormatTypeId == formatTypeId);
            }
        }
    }
}
