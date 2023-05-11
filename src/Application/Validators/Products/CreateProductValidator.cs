using FluentValidation;
using TechnestHackhaton.Application.DTOs.Test;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateTestValidator : AbstractValidator<CreateTestDto>
    {
        public CreateTestValidator()
        {

        }
    }
}
