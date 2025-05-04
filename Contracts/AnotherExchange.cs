namespace Contracts;

public class AnotherExchange 
{
    public string Msg { get; set; }

    public AnotherExchange(string msg)
    {
        Msg = msg;
    }

    // Required for deserialization
    public AnotherExchange() { }
}
