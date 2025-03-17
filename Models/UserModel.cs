﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TrackMyStaffWebApplication.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string? ImgUrl { get; set; }

        public string Role { get; set; }

        [ForeignKey("CompanyModel")]
        public int? CompanyId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
