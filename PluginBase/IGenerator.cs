using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    public interface IGenerator
    {
        object Next(Type type);
        List<Type> fSupportedTypes { get; }
    }
}
