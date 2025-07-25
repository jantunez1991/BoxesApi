namespace BoxesApi.Models;

public class Lead
{
    public int PlaceId { get; set; }
    public DateTime AppointmentAt { get; set; }
    public string ServiceType { get; set; } = null!;
    public Contact Contact { get; set; } = null!;
    public Vehicle? Vehicle { get; set; }
}

public class Contact
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}

public class Vehicle
{
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = null!;
}