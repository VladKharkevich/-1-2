using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Faker
{
    public class Faker
    {
        private readonly TypeGenerator fTypeGenerator = new TypeGenerator();

        private Stack<Type> fDtoInProgress = new Stack<Type>();

        private object GenerateWithFaker(Type type)
        {
            if (fTypeGenerator.CanGenerate(type))
            {
                return fTypeGenerator.Generate(type);
            }

            if (!fDtoInProgress.Contains(type))
            {
                MethodInfo create = typeof(Faker).GetMethod("Create").MakeGenericMethod(type);
                return create.Invoke(this, null);
            }
            if (fDtoInProgress.Contains(type))
            {
                return null;
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var listType = type.GetGenericTypeDefinition();
                    var genericType = type.GetGenericArguments()[0];
                    var constructedList = listType.MakeGenericType(genericType);
                    var random = new Random();
                    byte length = (byte)random.Next();
                    object[] parameters = { length };
                    var instance = Activator.CreateInstance(constructedList, parameters);
                    for (int i = 0; i < length; i++)
                    {
                        instance.GetType().GetMethod("Add")
                            .Invoke(instance, new[] { GenerateWithFaker(genericType) });
                    }

                    return instance;
                }
            }
            return null;
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            FieldInfo[] fields = type.GetFields();
            Type[] types = fields.Select(x => x.GetType()).ToArray();
            ConstructorInfo constructor = type.GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null, types, null);
            if (constructor == null)
            {
                if (type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length != 0)
                    constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)[0];
            }
            return constructor;
        }

        private object[] GenerateParameters(ConstructorInfo constructor)
        {
            List<object> parameters = new List<object>(
                constructor.GetParameters().Length);
            ParameterInfo[] parameterInfos = constructor.GetParameters();
            foreach (ParameterInfo pInfo in parameterInfos)
            {
                Type tempType = pInfo.ParameterType;
                parameters.Add(GenerateWithFaker(tempType));
            }
            return parameters.ToArray();
        }

        private void SetFieldsAndProperties(object obj)
        {
            obj.GetType().GetFields().ToList().ForEach(f => f.SetValue(obj, GenerateWithFaker(f.FieldType)));
            obj.GetType().GetProperties().ToList().ForEach(p => p.SetValue(obj, GenerateWithFaker(p.PropertyType)));
        }


        public T Create<T>()
        {
            Type type = typeof(T);
            fDtoInProgress.Push(type);
            ConstructorInfo constructor = GetConstructor(type);
            if (constructor == null)
            {
                return default(T);
            }
            object[] parameters = GenerateParameters(constructor);
            object obj = constructor.Invoke(parameters);
            SetFieldsAndProperties(obj);
            fDtoInProgress.Pop();
            return (T)obj;
        }
    }
}
