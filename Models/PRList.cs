namespace PRPicks.Models
{
    public struct PRList
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }
        public List<PRList> DropdownItems { get; set; }
    }
}