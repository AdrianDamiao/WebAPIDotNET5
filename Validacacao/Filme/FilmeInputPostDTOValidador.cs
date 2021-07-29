using FluentValidation;
using WebAPIDotNET5.DTOs;

public class FilmeInputPostDTOValidador : AbstractValidator<FilmeInputPostDTO>
{
    public FilmeInputPostDTOValidador()
    {
        RuleFor(filme => filme.Titulo)
            .NotEmpty().WithMessage("O {PropertyName} do filme é obrigatório.")
            .Length(3, 100).WithMessage("O {PropertyName} deve conter entre {MinLength} e {MaxLength} caracteres.");

        RuleFor(filme => filme.DiretorId)
            .NotEmpty().WithMessage("O {PropertyName} do diretor é obrigatório.")
            .NotEqual(0).WithMessage("O {PropertyName} do diretor não pode ser {ComparisonValue}.");
    }
}