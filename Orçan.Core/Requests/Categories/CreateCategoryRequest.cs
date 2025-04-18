using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orçan.Core.Requests.Categories;
public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Titulo invalido")]
    [MaxLength(80, ErrorMessage = "O titulo deve conter ate 80 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descricao invalido")]
    public string Description { get; set; } = string.Empty;
}
