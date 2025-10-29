namespace TrainingManage.Api.Models.Stats
{
    public class ParticipationDto
    {
        public string[] Labels { get; set; } = Array.Empty<string>();
        public int[] Values { get; set; } = Array.Empty<int>();
    }
}
