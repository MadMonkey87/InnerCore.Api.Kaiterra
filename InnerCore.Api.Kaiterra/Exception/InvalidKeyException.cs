namespace InnerCore.Api.Kaiterra.Exception
{
    public class InvalidKeyException : System.Exception
    {
        public InvalidKeyException() : base("the provided access key is invalid")
        {

        }
    }
}
