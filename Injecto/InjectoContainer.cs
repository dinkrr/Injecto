using System;
using System.Collections.Generic;
using System.Linq;

namespace Injecto
{
    public class InjectoContainer
    {
        private readonly List<Type> _dependencies = new List<Type>();
        
        public void AddDependency<T>()
        {
            _dependencies.Add(typeof(T));
        }

        public Type GetDependency(Type type)
        {
            return _dependencies.First(x => x.Name == type.Name);
        }
    }
}