using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.DataLayer.Enums;

namespace Contacts.DataLayer.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Family { get; set; }


        [MaxLength(11)]
        public string Mobile { get; set; }

        [MaxLength(11)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
        public int Age { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        [Required]
        public ContactType Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
