using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Image.DTOs
{
    public class CreateImgRequestDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        public Guid? EntityId { get; set; }
        public string EntityType { get; set; }
    }
}
