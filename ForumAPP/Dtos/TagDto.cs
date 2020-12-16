using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumAPP.Dtos
{
    public class TagDto
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Color { get; set; }
    }
}