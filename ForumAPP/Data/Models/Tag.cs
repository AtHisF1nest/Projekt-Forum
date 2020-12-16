using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPP.Data.Models
{
    [Serializable]
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Color { get; set; }
        public List<Thread> Threads { get; set; }
    }
}