namespace MHQuestGenerator
{
    public class Assets
    {
        public string imageMale { get; set; }
        public string imageFemale { get; set; }
    }

    public class Attributes
    {
    }

    public class Bonus
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Rank> ranks { get; set; }
    }

    public class Crafting
    {
        public List<Material> materials { get; set; }
    }

    public class Defense
    {
        public int @base { get; set; }
        public int max { get; set; }
        public int augmented { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int rarity { get; set; }
        public int carryLimit { get; set; }
        public int value { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Material
    {
        public int quantity { get; set; }
        public Item item { get; set; }
    }

    public class Modifiers
    {
    }

    public class Piece
    {
        public int id { get; set; }
        public string type { get; set; }
        public string rank { get; set; }
        public int rarity { get; set; }
        public Attributes attributes { get; set; }
        public Defense defense { get; set; }
        public Resistances resistances { get; set; }
        public string name { get; set; }
        public List<Slot> slots { get; set; }
        public List<Skill> skills { get; set; }
        public int armorSet { get; set; }
        public Assets assets { get; set; }
        public Crafting crafting { get; set; }
    }

    public class Rank
    {
        public int pieces { get; set; }
        public Skill skill { get; set; }
        public string description { get; set; }
    }

    public class Resistances
    {
        public int fire { get; set; }
        public int water { get; set; }
        public int ice { get; set; }
        public int thunder { get; set; }
        public int dragon { get; set; }
    }

    public class ArmorSet
    {
        public int id { get; set; }
        public string rank { get; set; }
        public string name { get; set; }
        public List<Piece> pieces { get; set; }
        public Bonus bonus { get; set; }
    }

    public class Skill
    {
        public int id { get; set; }
        public int level { get; set; }
        public Modifiers modifiers { get; set; }
        public int skill { get; set; }
        public string description { get; set; }
        public string skillName { get; set; }
    }

    public class Skill2
    {
        public int id { get; set; }
        public int level { get; set; }
        public Modifiers modifiers { get; set; }
        public int skill { get; set; }
        public string skillName { get; set; }
    }

    public class Slot
    {
        public int rank { get; set; }
    }
}
