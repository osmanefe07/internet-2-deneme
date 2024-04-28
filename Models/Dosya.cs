using System.ComponentModel.DataAnnotations;

namespace vize.Models
{
    public class Dosya
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int Size { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }


    }
}
