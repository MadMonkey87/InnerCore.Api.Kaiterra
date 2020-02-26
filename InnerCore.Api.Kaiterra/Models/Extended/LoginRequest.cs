using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember(Name = "email")]
        public String Email { get; set; }

        [DataMember(Name = "password")]
        public String Password { get; set; }
    }
}
