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
        string? BuildingNumber,
        double? FloorsNumber,
        bool IsElevator,
        string? AddressAndNumberOfMunicipal,
        bool IsBuildingPermit,
        bool Is4Form,
        string? FullAssetIdentificationBeforePerselasia,
        string? AnotherIdentification,
        bool IsPerselasia,
        string? FullAssetIdentificationAfterPerselasia,
        int? Bloc,
        int? Smooth,
        int? SmothArea,
        bool? IsStartingRishumBaitMeshutaf,
        bool? IsPrepareWarningComment,
        bool? IsRishumBaitMeshutaf,
        bool IsTookJointListingExpenses,
        double? PrincipalAmount,
        DateOnly? CollectionExpensesFrom1,
        double? CollectionAmount1,
        DateOnly? CollectionExpensesFrom2,
        double? CollectionAmount2,
        DateOnly? CollectionExpensesFrom3,
        double? CollectionAmount3,
        string? BuildingDrawingFile,
        string? Note
    );
}