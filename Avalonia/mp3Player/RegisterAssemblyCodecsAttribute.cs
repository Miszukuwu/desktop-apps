using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp3Player
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public abstract class RegisterAssemblyCodecsAttribute : Attribute
    {
        public abstract void RegisterAssemblyCodecs();
    }
}