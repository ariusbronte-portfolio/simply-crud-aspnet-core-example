using SimplyArchitecture.WebApi.Validators;
using Xunit;

namespace SimplyArchitecture.Tests.Unit.Validators
{
    /// <inheritdoc cref="SimplyArchitecture.WebApi.Validators.GreaterThanAttribute"/>
    public class GreaterThanTest
    {
        [Fact]
        public void GreaterThan_GreaterThanZero_SuccessValidation()
        {
            // Arrange
            const int arg = 0;
            const int arg1 = 1;
            
            // Act
            var greaterThan = new GreaterThanAttribute(value: arg);
            var validation = greaterThan.IsValid(value: arg1);
            
            // Arrange
            Assert.True(condition: validation);
        }
        
        [Fact]
        public void GreaterThan_EqualZero_FailedValidation()
        {
            // Arrange
            const int arg = 0;
            
            // Act
            var greaterThan = new GreaterThanAttribute(value: arg);
            var validation = greaterThan.IsValid(value: arg);
            
            // Arrange
            Assert.False(condition: validation);
        }

        [Fact]
        public void GreaterThan_GreaterThanZero_FailedValidation()
        {
            // Arrange
            const int arg = 0;
            const int arg1 = -1;
            
            // Act
            var greaterThan = new GreaterThanAttribute(value: arg);
            var validation = greaterThan.IsValid(value: arg1);
            
            // Arrange
            Assert.False(condition: validation);
        }
    }
}