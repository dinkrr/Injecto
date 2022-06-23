using System;
using System.Linq;

namespace Injecto
{
    public class InjectoResolver
    {
        InjectoContainer _container;

        public InjectoResolver(InjectoContainer container)
        {
            _container = container;
        }

        public T GetService<T>() => (T) GetService(typeof(T));

        private object GetService(Type type)
        {
            var dependency = _container.GetDependency(type);

            var constructor = dependency.GetConstructors().Single();
            var parameters = constructor.GetParameters().ToArray();

            if (parameters.Length > 0)
            {
                object[] parameterImplementations = GetParameterImplementations(parameters);
                return Activator.CreateInstance(dependency, parameterImplementations);
            }

            return Activator.CreateInstance(dependency);
        }

        private object[] GetParameterImplementations(System.Reflection.ParameterInfo[] parameters)
        {
            var parameterImplementations = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterImplementations[i] = GetService(parameters[i].ParameterType);
            }

            return parameterImplementations;
        }
    }
}