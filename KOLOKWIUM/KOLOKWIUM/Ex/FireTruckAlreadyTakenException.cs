namespace KOLOKWIUM.Ex;

[Serializable]
public class FireTruckAlreadyTakenException : Exception
{
    public FireTruckAlreadyTakenException ()
    {}

    public FireTruckAlreadyTakenException (string message) 
        : base(message)
    {}

    public FireTruckAlreadyTakenException (string message, Exception innerException)
        : base (message, innerException)
    {}  
}