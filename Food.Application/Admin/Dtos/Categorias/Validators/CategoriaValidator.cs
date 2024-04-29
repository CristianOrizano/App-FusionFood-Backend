using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Categorias.Validators
{
    public class CategoriaValidator : AbstractValidator<CategoriaSaveDto>
    {
        //se puede validar para vairos campos
        public CategoriaValidator()
        {
            /*  RuleFor(x => x.descripcion)
              .NotNull().WithMessage("Name required")
             .NotEmpty().WithMessage("El campo Nombre es obligatorio.")
             .MinimumLength(3).WithMessage("El campo Nombre debe tener al menos 3 caracteres.")
             .MaximumLength(10).WithMessage("El campo Nombre no debe exceder los 10 caracteres."); */
            RuleFor(x => x.Nombre)
                     .NotNull().WithMessage("Nombre requerido")
                   .Length(3, 15)
                    .WithMessage("el nombre min 3 y max 15");
        }

    }
}
