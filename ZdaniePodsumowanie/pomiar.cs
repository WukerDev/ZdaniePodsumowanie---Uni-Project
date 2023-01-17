using System.ComponentModel.DataAnnotations;

namespace ZdaniePodsumowanie
{
    public class pomiar
    {
        [Key]
        public int IDPomiar { get; set; }
        public int VarWysokosc { get; set; }
        public int VarSzerokosc { get; set; }
        public int FK_Sensor { get; set; }
    }
}