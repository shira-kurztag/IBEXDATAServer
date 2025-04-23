using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ApartmentDTO(
    int ApartmentId,
    int BuildingId,
    int ApartmentNumberByContract,
    int ApartmentStatus,
    int? Floor,
    double? ApartmentSurfaceByContract,
    int? ApartmentNumberByAddress,
    bool IsCompanyHasCompletedCommitments,
    bool IsGivenPossessionOfTheApartment,
    bool IsProducedLease,
    bool? IsDetachedApartment,
    List<LinkagesapartmentDTO> LinkagesApartments

         );

}
