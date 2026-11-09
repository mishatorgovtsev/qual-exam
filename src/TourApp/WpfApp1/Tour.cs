namespace WpfApp1
{
    public class Tour
    {
        public string Destination { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

        public Tour(string destination, int duration, decimal price)
        {
            Destination = destination;
            Duration = duration;
            Price = price;
        }

        public override string ToString()
        {
            return Destination + " | " + Duration + " дн. | " + Price + " руб.";
        }
    }
}