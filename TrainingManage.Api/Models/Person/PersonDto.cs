using System.Text.Json.Serialization;

namespace TrainingManage.Api.Models.Person
{
    /// <summary>
    /// Datový přenosový objekt reprezentující osobu.
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identifikační číslo osoby.
        /// </summary>
        [JsonPropertyName("identificationNumber")]
        public int IdentificationNumber { get; set; }

        /// <summary>
        /// Jméno osoby.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Věk osoby.
        /// </summary>
        [JsonPropertyName("age")]
        public int Age { get; set; }

        /// <summary>
        /// Telefonní číslo osoby.
        /// </summary>
        [JsonPropertyName("telephone")]
        public string Telephone { get; set; } = "";

        /// <summary>
        /// E-mailová adresa osoby.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
    }
}
