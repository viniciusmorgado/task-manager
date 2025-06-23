using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Tests.ValueObjects.Tests
{
    public class DescriptionTests
    {
        [Fact]
        public void Constructor_Allows_Null_Or_Whitespace_And_Trims()
        {
            var desc1 = new Description(null);
            Assert.Equal(string.Empty, desc1.Value);

            var desc2 = new Description("   ");
            Assert.Equal(string.Empty, desc2.Value);

            var desc3 = new Description("  some text  ");
            Assert.Equal("some text", desc3.Value);
        }

        [Fact]
        public void Constructor_Throws_When_Length_Exceeds_500()
        {
            var longText = new string('a', 501);
            var ex = Assert.Throws<ArgumentException>(() => new Description(longText));
            Assert.Equal("Description must be 500 characters or less.", ex.Message);
        }

        [Fact]
        public void Equals_Returns_True_For_Same_Value()
        {
            var d1 = new Description("test");
            var d2 = new Description("test");
            Assert.True(d1.Equals(d2));
            Assert.True(d1.Equals(d2));
        }

        [Fact]
        public void Equals_Returns_False_For_Different_Value()
        {
            var d1 = new Description("test1");
            var d2 = new Description("test2");
            Assert.False(d1.Equals(d2));
        }

        [Fact]
        public void GetHashCode_Is_Consistent_With_Value()
        {
            var d = new Description("hashcode");
            Assert.Equal("hashcode".GetHashCode(), d.GetHashCode());
        }

        [Fact]
        public void Implicit_Conversion_To_String_Returns_Value()
        {
            var d = new Description("implicit");
            string s = d;
            Assert.Equal("implicit", s);
        }

        [Fact]
        public void Implicit_Conversion_From_String_Creates_Description()
        {
            Description d = "implicit from string";
            Assert.Equal("implicit from string", d.Value);
        }
    }
}
