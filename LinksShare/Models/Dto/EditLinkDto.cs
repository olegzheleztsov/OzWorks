using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinksShare.Models.Dto
{
    public class EditLinkDto : CreateLinkDto
    {
        public int LinkId { get; set; }
    }
}
