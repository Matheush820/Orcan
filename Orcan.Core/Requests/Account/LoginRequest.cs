﻿using System.ComponentModel.DataAnnotations;

namespace Orcan.Core.Requests.Account;
public class LoginRequest : Request
{
    [Required(ErrorMessage = "Email")]
    [EmailAddress(ErrorMessage = "Email Inválido")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Senha Inválida")]
    public string Password { get; set; } = string.Empty;
}
