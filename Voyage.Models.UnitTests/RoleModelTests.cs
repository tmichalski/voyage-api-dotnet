﻿using FluentAssertions;
using FluentValidation.Attributes;
using Voyage.Models.Validators;
using Xunit;

namespace Voyage.Models.UnitTests
{
    [Trait("Category", "Model.Validation")]
    public class RoleModelTests
    {
        [Fact]
        public void Class_Should_Have_Validator_Class()
        {
            typeof(RoleModel)
                .Should()
                .BeDecoratedWith<ValidatorAttribute>(_ => _.ValidatorType == typeof(RoleModelValidator));
        }
    }
}
