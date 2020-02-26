namespace InnerCore.Api.Kaiterra.Exception
{
    public class DeviceNotFoundException : System.Exception
    {
        public DeviceNotFoundException() : base($"the device could not be found")
        {

        }
    }
}
