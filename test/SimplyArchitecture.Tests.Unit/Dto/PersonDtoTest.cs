using System;
using System.ComponentModel.DataAnnotations;
using SimplyArchitecture.WebApi.Dto;
using Xunit;

namespace SimplyArchitecture.Tests.Unit.Dto
{
    public class PersonDtoTest
    {
        [Fact]
        public void PersonDto_CorrectForm_SuccessValidation()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "John",
                LastName = "Karlson",
                Id = 18
            };

            // Act
            var context = new ValidationContext(dto);
            var success = Validator.TryValidateObject(dto, context, null);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public void PersonDto_FirstNameRequired_FailedValidation()
        {
            // Arrange
            var dto = new PersonDto
            {
                LastName = "lastName",
                Age = 18
            };

            // Act
            var action = new Action(() =>
            {
                var context = new ValidationContext(dto);
                Validator.ValidateObject(dto, context, true);
            });

            // Assert
            var exception = Assert.Throws<ValidationException>(action);
            Assert.Equal("The FirstName field is required.", exception.Message);
        }
        
        [Fact]
        public void PersonDto_FirstNameStringLength128_FailedValidation()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = new string('z', 256), 
                LastName = "lastName",
                Age = 18
            };

            // Act
            var action = new Action(() =>
            {
                var context = new ValidationContext(dto);
                Validator.ValidateObject(dto, context, true);
            });

            // Assert
            var exception = Assert.Throws<ValidationException>(action);
            Assert.Equal("The field FirstName must be a string with a maximum length of 128.", exception.Message);
        }
        
        [Fact]
        public void PersonDto_LastNameRequired_FailedValidation()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "firstName",
                Age = 18
            };

            // Act
            var action = new Action(() =>
            {
                var context = new ValidationContext(dto);
                Validator.ValidateObject(dto, context, true);
            });

            // Assert
            var exception = Assert.Throws<ValidationException>(action);
            Assert.Equal("The LastName field is required.", exception.Message);
        }
        
        [Fact]
        public void PersonDto_LastNameStringLength128_FailedValidation()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "firstName",
                LastName = new string('z', 256), 
                Age = 18
            };

            // Act
            var action = new Action(() =>
            {
                var context = new ValidationContext(dto);
                Validator.ValidateObject(dto, context, true);
            });

            // Assert
            var exception = Assert.Throws<ValidationException>(action);
            Assert.Equal("The field LastName must be a string with a maximum length of 128.", exception.Message);
        }
        
        [Fact]
        public void PersonDto_AgeNegative_FailedValidation()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "firstName",
                LastName = "lastName",
                Age = -1
            };

            // Act
            var action = new Action(() =>
            {
                var context = new ValidationContext(dto);
                Validator.ValidateObject(dto, context, true);
            });

            // Assert
            var exception = Assert.Throws<ValidationException>(action);
            Assert.Equal("The field Age must be greater than 0.", exception.Message);
        }
        
        [Fact]
        public void PersonDto_AgeEqualZero_FailedValidation()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "firstName",
                LastName = "lastName",
                Age = 0
            };

            // Act
            var action = new Action(() =>
            {
                var context = new ValidationContext(dto);
                Validator.ValidateObject(dto, context, true);
            });

            // Assert
            var exception = Assert.Throws<ValidationException>(action);
            Assert.Equal("The field Age must be greater than 0.", exception.Message);
        }
    }
}