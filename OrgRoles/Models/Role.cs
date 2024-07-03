﻿using System.ComponentModel.DataAnnotations;

namespace OrgRoles.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public Role? Parent { get; set; }

    }
}
