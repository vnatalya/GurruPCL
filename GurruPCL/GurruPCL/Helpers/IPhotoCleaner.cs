using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GurruPCL.Helpers
{
    public interface IPhotoCleaner
    {
        void DeletePhoto(string path);
    }
}
