namespace HelperLibrary.Models;

public class FullPersonModel : PersonModel
{
    public int Id { get; set; }
    public required PhoneModel CellPhone { get; set; }
}