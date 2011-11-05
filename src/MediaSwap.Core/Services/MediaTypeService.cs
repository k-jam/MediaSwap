using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;

namespace MediaSwap.Core.Services
{
    public class MediaTypeService : BaseContext, IMediaTypeService
    {
        public IEnumerable<Models.MediaType> GetMediaTypes()
        {
            using (var context = GetContext())
            {
                return context.MediaType.ToList();
            }
        }

        public Models.MediaType GetMediaType(int mediaTypeId)
        {
            using (var context = GetContext())
            {
                return context.MediaType.FirstOrDefault(m => m.MediaTypeId == mediaTypeId);
            }
        }
    }
}
