using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class UserWithPositionsDto : UserDto
    {
        public List<PositionWithCompletionInformation>? positions { get; set; }
    }
}
