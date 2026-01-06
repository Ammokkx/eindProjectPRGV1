using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Exceptions;
using System.IO;

namespace ClientSimTests
{
    public class UnitTestStreets
    {

        [Theory]
        [InlineData("somestreet")]
        [InlineData("Unknown")]

        public void StreetName_Valid(string d)
        {
            Street street = new Street("meow", "mew");
            street.Name = d;
            Assert.Equal(d, street.Name);

        }

        [Theory]
        [InlineData("somestreet")]
        [InlineData("Unknown")]
        public void Municipality_Valid(string d)
        {
            Street street = new Street("meow", "mew");
            street.Municipality = d;
            Assert.Equal(d, street.Municipality);

        }
        [Theory]
        [InlineData("")]
        [InlineData("(unknown)")]
        public void Municipality_Invalid(string d)
        {
            Street street = new Street("meow", "mew");
            Assert.Throws<DataReadingException>(() => street.Municipality = d);

        }

    }
}
