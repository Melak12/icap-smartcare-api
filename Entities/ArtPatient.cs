namespace SmartcareAPI.Entities
{
    public class ArtPatient
    {
        public long mrn { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string ARVDoseDays { get; set; }
        public string Drug { get; set; }
        public string Followupdate { get; set; }
        public string nextvisitdate { get; set; }
    }
}