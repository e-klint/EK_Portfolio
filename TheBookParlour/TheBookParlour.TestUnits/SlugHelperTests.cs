using TheBookParlour.Core.Helpers;

namespace TheBookParlour.TestUnits
{
    public class SlugHelperTests
    {
        [Fact]
        public void GenerateSlug_ShouldReturnLowercaseWithDashes()
        {
            // Arrange
            string input = "Harry Potter";

            // Act
            string result = SlugHelper.GenerateSlug(input);

            // Assert
            Assert.Equal("harry-potter", result);
        }

        [Fact]
        public void GenerateSlug_WithDoubleSpaces_ShouldReturnSingleDash()
        {
            string input = "Harry  Potter";
            string result = SlugHelper.GenerateSlug(input);
            Assert.Equal("harry-potter", result);
        }

        [Fact]
        public void GenerateSlug_WithSpecialCharacters_ShouldRemoveCharacters()
        {
            string input = "Harry Potter!";
            string result = SlugHelper.GenerateSlug(input);
            Assert.Equal("harry-potter", result);
        }
    }
}
