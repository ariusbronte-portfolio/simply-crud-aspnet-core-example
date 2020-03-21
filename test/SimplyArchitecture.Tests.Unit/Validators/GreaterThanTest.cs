using SimplyArchitecture.WebApi.Validators;
using Xunit;

namespace SimplyArchitecture.Tests.Unit.Validators
{
    /// <inheritdoc cref="SimplyArchitecture.WebApi.Validators.GreaterThan"/>
    public class GreaterThanTest
    {
        [Fact]
        public void GreaterThan_GreaterThanZero_SuccessValidation()
        {
            // Arrange
            const int arg = 0;
            const int arg1 = 1;
            
            // Act
            var greaterThan = new GreaterThan(arg);
            var validation = greaterThan.IsValid(arg1);
            
            // Arrange
            Assert.True(validation);
        }
        
        [Fact]
        public void GreaterThan_EqualZero_FailedValidation()
        {
            // Arrange
            const int arg = 0;
            
            // Act
            var greaterThan = new GreaterThan(arg);
            var validation = greaterThan.IsValid(arg);
            
            // Arrange
            Assert.False(validation);
        }

        [Fact]
        public void GreaterThan_GreaterThanZero_FailedValidation()
        {
            // Arrange
            const int arg = 0;
            const int arg1 = -1;
            
            // Act
            var greaterThan = new GreaterThan(arg);
            var validation = greaterThan.IsValid(arg1);
            
            // Arrange
            Assert.False(validation);
        }
    }
}