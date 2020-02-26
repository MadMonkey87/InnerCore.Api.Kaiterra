namespace InnerCore.Api.Kaiterra.Exception
{
    public class InvalidResponseException : System.Exception
    {
        public InvalidResponseException(string response) : base($"the server answered with an invalid response: {response}")
        {

        }
    }
}
