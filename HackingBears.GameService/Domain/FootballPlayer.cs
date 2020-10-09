namespace HackingBears.GameService.Domain
{
    public sealed class FootballPlayer
    {
        public int Id { get; set; }
        public int JerseyNumber { get; set; }
        public string JerseyColor { get; set; }
        public string ShortColor { get; set; }
        public Position Position { get; set; }
    }
}