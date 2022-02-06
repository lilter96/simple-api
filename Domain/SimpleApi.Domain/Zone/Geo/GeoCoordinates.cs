namespace SimpleApi.Domain.Zone.Geo;

public class GeoCoordinates
{
    public GeoCoordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; }

    public double Longitude { get; }
}