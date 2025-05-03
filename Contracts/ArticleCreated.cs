namespace Contracts;

public class ArticleCreated 
{
    public string Msg { get; set; }

    public ArticleCreated(string msg)
    {
        Msg = msg;
    }

    // Required for deserialization
    public ArticleCreated() { }
}
