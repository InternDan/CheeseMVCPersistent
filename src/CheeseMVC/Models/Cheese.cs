namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public CheeseType Type { get; set; }
        public CheeseCategory Category { get; set; }
        public int CatgoryID { get; set; }
        public int ID { get; set; }
    }
}
