using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public interface IMediaTypeService
    {
        IEnumerable<MediaType> GetMediaTypes();
        MediaType GetMediaType(int mediaTypeId);
    }
}
