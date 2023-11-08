using System.ComponentModel.DataAnnotations;

public class UpdateProfileDto
{
    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Location { get; set; }

    public string PhoneNumber { get; set; }
    public string ProfileImage { get; set; }

}
