namespace SearchPlaceApp.Models
{
    public class PlaceDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FormattedAddress { get; set; }
        public string Vicinity { get; set; }
        public string Url { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string UtcOffset { get; set; }
    }
}
