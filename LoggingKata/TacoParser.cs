using System.ComponentModel.DataAnnotations;

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
               
                logger.LogWarning("Less than 3 . Incomplate data");
               
                return null; 
            }

            var latitude = double.Parse(cells[0]); 
           
            var longitude = double.Parse(cells[1]);
           
            var name = cells[2];
           
            var point = new Point();
            point.Latitude = latitude;
            point.Longitude = longitude;

            var tacobell = new TacoBell();
            tacobell.Name = name;
            tacobell.Location = point;


           
            return tacobell;
        }
    }
}