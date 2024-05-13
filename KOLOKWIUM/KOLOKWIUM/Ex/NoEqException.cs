namespace KOLOKWIUM.Ex;

[Serializable]
public class NoEqException : Exception
{
    public NoEqException ()
    {}

    public NoEqException (string message) 
        : base(message)
    {}

    public NoEqException (string message, Exception innerException)
        : base (message, innerException)
    {}  
}