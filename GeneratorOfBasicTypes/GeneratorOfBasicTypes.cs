using PluginBase;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GeneratorOfBasicTypes
{
    public class GeneratorOfBasicTypes : IGenerator
    {
        private Random fRandom = new Random();

        public List<Type> fSupportedTypes { get; }

        public GeneratorOfBasicTypes()
        {
            fSupportedTypes = new List<Type>();
            Type cls = typeof(GeneratorOfBasicTypes);
            MethodInfo[] mInfos = cls.GetMethods();
            foreach (MethodInfo mInfo in mInfos)
            {
                if (mInfo.Name.StartsWith("Generate"))
                    fSupportedTypes.Add(mInfo.ReturnType);
            }
        }

        public Boolean GenerateBoolean()
        {
            return fRandom.Next(0, 1) == 0;
        }
        
        public int GenerateInt()
        {
            return fRandom.Next();
        }

        public double GenerateDouble()
        {
            return fRandom.NextDouble();
        }

        public string GenerateString()
        {
            int size = fRandom.Next(3, 15);
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                int offset = fRandom.Next(0, 26);
                int code;
                char ch = 'a';
                code = ch;
                ch = (char)(code + offset);
                chars[i] = ch;
            }
            return new string(chars);
        }

        public long GenerateLong()
        {
            return (long)(fRandom.Next() << 32) + (fRandom.Next());
        }

        public float GenerateFloat()
        {
            double mantissa = (fRandom.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, fRandom.Next(-126, 128));
            return (float)(mantissa * exponent);
        }

        public object Next(Type type)
        {
            Type cls = typeof(GeneratorOfBasicTypes);
            MethodInfo[] mInfos = cls.GetMethods();
            foreach (MethodInfo mInfo in mInfos)
            {
                if (mInfo.ReturnType == type && mInfo.Name.StartsWith("Generate"))
                    return mInfo.Invoke(this, new object[] { });
            }
            return null;
        }
    }
}
