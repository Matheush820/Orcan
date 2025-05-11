using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcan.Core.Requests.Categories;
public class UpdateCategoryRequest : Request
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Titulo invalido")]
    [MaxLength(80, ErrorMessage = "Titulo deve conter até 80 caracteres")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Descrição invalida")]
    public string? Description { get; set; }
}
