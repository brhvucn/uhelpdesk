using System.ComponentModel.DataAnnotations;

namespace uHelpDesk.Admin.ViewModels.Account;

public class UserVM
{
    public string Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Role { get; set; }
}
