using PluginBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorOfDateTimes
{
    public class GeneratorOfDateTimes : IGenerator
    {
        public List<Type> fSupportedTypes { get; }

        public GeneratorOfDateTimes()
        {
            fSupportedTypes = new List<Type>();
            fSupportedTypes.Add(typeof(DateTime));
        }

        public DateTime GenerateDateTime()
        {
            Random random = new Random();
            int month = random.Next(1, 12);
            int year = random.Next(1945, 2077);
            int day = 1;
            if (month == 2)
            {
                if (year % 4 == 0)
                    day = random.Next(1, 29);
                else
                    day = random.Next(1, 28);
            }
            else if ((month == 4) || (month == 6) || (month == 9) || (month == 11))
                day = random.Next(1, 30);
            else
                day = random.Next(1, 31);

            return new DateTime(year, month, day);
        }

        public object Next(Type type)
        {
            if (typeof(DateTime) == type)
                return GenerateDateTime();
            return null;
        }
    }
}
