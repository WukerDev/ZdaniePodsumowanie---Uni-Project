using System.ComponentModel.DataAnnotations;

namespace ZdaniePodsumowanie
{
    public class sensor
    {
        [Key]
        public int IDSensor { get; set; }
        public string NazwaSensora { get; set; }
    }
}