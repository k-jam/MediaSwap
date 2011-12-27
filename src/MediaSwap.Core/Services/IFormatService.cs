using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public interface IFormatService
    {
        IEnumerable<Format> GetFormats();
        Format GetFormat(int formatTypeId);
    }
}
