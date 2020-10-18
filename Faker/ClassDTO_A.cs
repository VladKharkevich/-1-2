using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public class ClassDTO_A
    {
        public int FieldInt;
        public uint FieldUInt;
        public char FieldChar;
        public string FieldString;
        public double FieldDouble;
        public float FieldFloat;
        public ClassDTO_B classDTO_B;

        public ClassDTO_A(int fieldInt, double fieldDouble, string fieldString)
        {
            FieldInt = fieldInt;
            FieldDouble = fieldDouble;
            FieldString = fieldString;
        }
    }
}
