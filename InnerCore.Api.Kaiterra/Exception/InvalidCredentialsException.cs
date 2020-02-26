namespace InnerCore.Api.Kaiterra.Exception
{
    public class InvalidCredentialsException : System.Exception
    {
        public InvalidCredentialsException() : base("the provided credentials are not valid")
        {

        }
    }
}
