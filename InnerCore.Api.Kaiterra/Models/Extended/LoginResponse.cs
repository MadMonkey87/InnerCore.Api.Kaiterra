using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember(Name = "token")]
        public String AccessToken { get; set; }
    }
}
