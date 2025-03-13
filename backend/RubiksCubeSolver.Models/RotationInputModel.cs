using Newtonsoft.Json;

namespace RubiksCubeSolver.Models
{
    public class RotationInputModel
    {
        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("clockwise")]
        public bool Clockwise { get; set; }
    }
}
