namespace Contracts;

public class ArticleViewed 
{
    public string Msg { get; set; }

    public ArticleViewed(string msg)
    {
        Msg = msg;
    }

    // Required for deserialization
    public ArticleViewed() { }
}
