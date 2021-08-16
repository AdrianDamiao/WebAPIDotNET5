using FluentValidation;
using WebAPIDotNET5.DTOs;
public class DiretorInputPostDTOValidador : AbstractValidator<DiretorInputPostDTO>
{
    public DiretorInputPostDTOValidador()
    {
        RuleFor(diretor => diretor.Nome)
            .NotEmpty().WithMessage("O {PropertyName} do diretor é obrigatório.")
            .Length(3, 40).WithMessage("O {PropertyName} deve conter entre {MinLength} e {MaxLength} caracteres.");

        RuleFor(diretor => diretor.Email)

            .NotEmpty().WithMessage("O {PropertyName} do diretor é obrigatório.")
            .EmailAddress().WithMessage("O {PropertyName} é invalido")
            .Length(3, 60).WithMessage("O {PropertyName} deve conter entre {MinLength} e {MaxLength} caracteres.");
    }
}