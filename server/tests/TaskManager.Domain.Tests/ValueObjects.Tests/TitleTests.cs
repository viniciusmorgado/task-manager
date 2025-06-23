using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Tests.ValueObjects.Tests
{
    public class TitleTests
    {
        [Fact]
        public void Constructor_Throws_When_Null_Or_Whitespace()
        {
            Assert.Throws<ArgumentException>(() => new Title(null));
            Assert.Throws<ArgumentException>(() => new Title(""));
            Assert.Throws<ArgumentException>(() => new Title("   "));
        }

        [Fact]
        public void Constructor_Throws_When_Length_Exceeds_100()
        {
            var longText = new string('a', 101);
            var ex = Assert.Throws<ArgumentException>(() => new Title(longText));
            Assert.Equal("Title must be 100 characters or less.", ex.Message);
        }

        [Fact]
        public void Constructor_Trims_Value()
        {
            var title = new Title("  trimmed title  ");
            Assert.Equal("trimmed title", title.Value);
        }

        [Fact]
        public void Equals_Returns_True_For_Same_Value()
        {
            var t1 = new Title("test");
            var t2 = new Title("test");
            Assert.True(t1.Equals(t2));
            Assert.True(t1.Equals(t2));
        }

        [Fact]
        public void Equals_Returns_False_For_Different_Value()
        {
            var t1 = new Title("test1");
            var t2 = new Title("test2");
            Assert.False(t1.Equals(t2));
        }

        [Fact]
        public void GetHashCode_Is_Consistent_With_Value()
        {
            var t = new Title("hashcode");
            Assert.Equal("hashcode".GetHashCode(), t.GetHashCode());
        }

        [Fact]
        public void Implicit_Conversion_To_String_Returns_Value()
        {
            var t = new Title("implicit");
            string s = t;
            Assert.Equal("implicit", s);
        }

        [Fact]
        public void Implicit_Conversion_From_String_Creates_Title()
        {
            Title t = "implicit from string";
            Assert.Equal("implicit from string", t.Value);
        }
    }
}