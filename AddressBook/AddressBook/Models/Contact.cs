using SQLite;

namespace AddressBook.Models;

public class Contact
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }

    [NotNull] public string Name { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    
    public string? Twitter { get; set; }
    
    public string? Instagram { get; set; }
    
    public byte[]? Photo { get; set; }
}