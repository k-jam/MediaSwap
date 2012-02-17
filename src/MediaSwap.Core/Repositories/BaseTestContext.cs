using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaSwap.Core.Repositories
{
    public class BaseTestContext
    {
        public MediaSwapTestContext GetContext()
        {
            return new MediaSwapTestContext();
        }
    }
}
