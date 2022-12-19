﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Core.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }

        [StringLength(13)]
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }

    }
}