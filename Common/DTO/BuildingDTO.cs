using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record BuildingDTO(
    int BuildingId,
    int? ProjectId, 
    int? BuildingStatus,
    string? BuildingNumber );
}
