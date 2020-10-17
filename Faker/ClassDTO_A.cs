using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public class ClassDTO_A
    {
        public int FieldInt;
        public string FieldString;
        public double FieldFloat;
        public ClassDTO_B classDTO_B;

        public ClassDTO_A(int fieldInt, double fieldFloat, string fieldString)
        {
            FieldInt = fieldInt;
            FieldFloat = fieldFloat;
            FieldString = fieldString;
        }
    }
}
