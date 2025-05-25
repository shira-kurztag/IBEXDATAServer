using System;
using System.Collections.Generic;

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
        List<LinkagesapartmentDTO> LinkagesApartments,
        DateOnly? PurchasDate // <-- הוספה כאן
    );
}