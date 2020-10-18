using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Faker.Tests
{
    [TestClass]
    public class FakerTests
    {
        ClassDTO_A testDTO_A;
        [TestInitialize]
        public void TestSetUp()
        {
            Faker faker = new Faker();
            testDTO_A = faker.Create<ClassDTO_A>();

        }

        [TestMethod]
        public void TestCycleDependence()
        {
            testDTO_A.classDTO_B.classDTO_C.classDTO_A.Should().Be(null);
        }

        [TestMethod]
        public void TestRandomInt()
        {
            testDTO_A.FieldInt.Should().NotBe(0); 
        }

        [TestMethod]
        public void TestUnknownBasicUINTType()
        {
            testDTO_A.FieldUInt.Should().Be(0);
        }

        [TestMethod]
        public void TestDatetime()
        {
            testDTO_A.classDTO_B.dateTime.Year.Should().BeInRange(1945, 2077);
            testDTO_A.classDTO_B.dateTime.Month.Should().BeInRange(1, 12);
            testDTO_A.classDTO_B.dateTime.Day.Should().BeInRange(1, 31);
        }
        [TestMethod]
        public void TestStringLength()
        {
            testDTO_A.FieldString.Length.Should().BeInRange(3, 15);
        }
    }
}
