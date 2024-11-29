namespace HiddenVilla_Client.Model.ViewModel
{
    public class HomeVM
    {
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public int Nights { get; set; }
    }
}
