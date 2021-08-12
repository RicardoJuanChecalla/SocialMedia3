using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using SocialMedia3.Core.DTOs;
using FluentValidation;

namespace SocialMedia3.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            // RuleFor(post => post.Description)
            // .NotNull()
            // .Length(10,500);

            RuleFor(post => post.Description)
            .NotNull()
            .WithMessage("La descripcion no puede ser nula");

            RuleFor(post => post.Description)
            .Length(10,500);

            RuleFor(post => post.Date)
            .NotNull()
            .LessThan(DateTime.Now);
        }
    }
}