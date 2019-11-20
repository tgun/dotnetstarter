using System.ComponentModel.DataAnnotations;

namespace dotnetstarter.Resources {
    /// <summary>
    /// Resource, a response model, sans any data coming in from the DB that you may not want to include.
    /// </summary>
    public class ExampleResource {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
