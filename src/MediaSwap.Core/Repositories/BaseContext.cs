using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaSwap.Core.Repositories
{
    public class BaseContext
    {
        public MediaSwapContext GetContext()
        {
            return new MediaSwapContext();
        }
    }
}
