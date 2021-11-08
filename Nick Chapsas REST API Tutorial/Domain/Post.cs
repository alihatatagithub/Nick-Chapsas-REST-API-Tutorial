using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Domain
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
