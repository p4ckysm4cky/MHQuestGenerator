namespace MHQuestGenerator.Models
{
    public class Quest
    {
        public long Id { get; set; }
        public string Monster { get; set; }
        public string ArmorSet { get; set; }
        public bool isComplete { get; set; }
    }
}
