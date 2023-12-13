using FluentValidation;
using TestLatviaProject.Dtos;

namespace TestLatviaProject.Validations
{
    public class TasksValidation : AbstractValidator<TasksDto>
    {
        public TasksValidation()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
