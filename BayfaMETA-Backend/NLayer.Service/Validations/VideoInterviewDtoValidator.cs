using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validations
{
    /// <summary>
    /// Class for validating VideoInterviewDto attributes.
    /// </summary>
    public class VideoInterviewDtoValidator : AbstractValidator<VideoInterviewDto>
    {
        public VideoInterviewDtoValidator() {
            RuleFor(x => x.format).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.size).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
        }

     
    }
}
