namespace PCShop.Models.Request
{
    public class EditRequest
    {
        public int id { get; set; }
        public string memory { get; set; }
        public string storage { get; set; }
        public string port { get; set; }
        public string render { get; set; }
        public string weight { get; set; }
        public string psu { get; set; }
        public string processor { get; set; }
    }
}
