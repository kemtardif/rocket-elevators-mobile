using System;

namespace Rem.Models
{
    public class Elevator
    {
        public int id { get; set; }
        public string status { get; set; }
        public string serial_number { get; set; }
        public string model { get; set; }
        public string type_building { get; set; }
        public string TextColor { get; set; }
        public DateTime date_last_inspection { get; set; }

    }
}