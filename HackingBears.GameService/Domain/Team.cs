namespace HackingBears.GameService.Domain
{
    public sealed class Team
    {
        #region Properties
        
        public int Id { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }

        public string FirstColor { get; set; }
        
        public string SecondColor { get; set; }

        #endregion
    }
}