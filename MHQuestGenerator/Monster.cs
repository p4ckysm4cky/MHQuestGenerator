namespace MHQuestGenerator
{
    public class Location
    {
        public int id { get; set; }
        public int zoneCount { get; set; }
        public string name { get; set; }
    }

    public class Monster
    {
        public int id { get; set; }
        public string type { get; set; }
        public string species { get; set; }
        public List<object> elements { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<object> ailments { get; set; }
        public List<Location> locations { get; set; }
        public List<object> resistances { get; set; }
        public List<Weakness> weaknesses { get; set; }
        public List<object> rewards { get; set; }
    }

    public class Weakness
    {
        public string element { get; set; }
        public int stars { get; set; }
        public object condition { get; set; }
    }
}
