namespace WebApisforstorebySuchi.Common.Objects
{
    public class ProductDTO
    {
        
    public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    
}
}
