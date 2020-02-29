using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
    [DataContract]
    public class Device
    {
        /// <summary>
        /// This id could be used to query the sensor data using the BasicKaiterraClient
        /// </summary>
        [DataMember(Name = "timeID")]
        public String Id { get; set; }

        [DataMember(Name = "name")]
        public String Name { get; set; }

        [DataMember(Name = "firmware_version")]
        public String Firmware { get; set; }

        [DataMember(Name = "charging")]
        public bool? Charging { get; set; }

        [DataMember(Name = "level")]
        public int? BatteryLevel { get; set; }

        [DataMember(Name = "mac_eth")]
        public String MacAddressEthernet { get; set; }

        [DataMember(Name = "mac_wifi")]
        public String MacAddressWifi { get; set; }

        [DataMember(Name = "serial")]
        public String SerialNumber { get; set; }

        [DataMember(Name = "lastHandshakeTime")]
        public DateTimeOffset? LastHandshake { get; set; }
    }
}
