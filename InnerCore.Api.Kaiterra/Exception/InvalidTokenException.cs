namespace InnerCore.Api.Kaiterra.Exception
{
    public class InvalidKTokenException : System.Exception
    {
        public InvalidKTokenException() : base("the provided access token is invalid")
        {

        }
    }
}
