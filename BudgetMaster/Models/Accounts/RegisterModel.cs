using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetMaster.Models.Accounts;

public class RegisterModel
{
    [DisplayName("Screen name")]
    public string Username { get; set; } = string.Empty;
    [DataType(DataType.EmailAddress), Required]
    public string Email { get; set; } = string.Empty;
    [DataType(DataType.Password), Required]
    public string Password { get; set; } = string.Empty;
    [DataType(DataType.Password), Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
}
