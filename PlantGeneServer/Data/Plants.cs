namespace PlantGeneServer.Data
{
    public class Plants
    {
        public string gene { get; set; } = null!;
        public string size { get; set; } = null!;
        public string charecteristic { get; set; } = null!;
        public decimal? cost { get; set; }
        public string family { get; set; } = null!;
        public decimal familyId { get; set; }
        public decimal geneId { get; set; }
    }
}