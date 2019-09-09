using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.VaultTypes.ItemTypes
{
    public class ItemIdentity
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("address1")]
        public string Address_1 { get; set; }

        [JsonProperty("address2")]
        public string Address_2 { get; set; }

        [JsonProperty("address3")]
        public string Address_3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("ssn")]
        public string SocialSecurityNumber { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("passportNumber")]
        public string PassportNumber { get; set; }

        [JsonProperty("licenseNumber")]
        public string LicenseNumber { get; set; }
    }
}
