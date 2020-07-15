namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Error, less than 3 items entered.");

            }

            var lat = double.Parse(cells[0]);

            var longitude = double.Parse(cells[1]);

            var name = cells[2];

            // Your going to need to parse your string as a `double`
            // which is similar to parsing a string as an `int`

            // You'll need to create a TacoBell class
            // that conforms to ITrackable

            // Then, you'll need an instance of the TacoBell class
            // With the name and point set correctly
            var locationPoint = new Point();
            locationPoint.Latitude = lat;
            locationPoint.Longitude = longitude;

            var tacoBell = new TacoBell()
            {
                Name = name,
                Location = locationPoint
            };

            return tacoBell;
        }
    }
}