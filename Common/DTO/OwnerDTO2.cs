using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class OwnerDTO2
    {

        public int OwnerId { get; set; } //id 

        public int OwnerStatus { get; set; } // סטטוס

        public int? ApartmentId { get; set; } // id דירה

        public int? OwnerApartmentStatus { get; set; }  //סטטוס דירה
        public string? purchasedFrom { get; set; } //ממי הדירה נרכשת 
        public DateOnly? PurchaseDate { get; set; }//   תאריך רכישה

        //`
        public string? AddressAccordingToContract { get; set; } //כתובת לפי חוזה
        public string? AddressAndNumberOfMunicipal { get; set; } //   כתובת הדירה הנרכשת 

        //
        public string? MailingAddress { get; set; } //כתובת למשלוח דואר
        //```
        public string? SecondAddress { get; set; }// כתובת שניה
        //

        public string? DescriptionPhone1 { get; set; }//תיאור טלפון 1

        public string? DescriptionPhone2 { get; set; }//תיאור טלפון 2

        public string? DescriptionPhone3 { get; set; }//תיאור טלפון 3

        public string? NumberStringPhone1 { get; set; }// טלפון 1

        public string? NumberStringPhone2 { get; set; }// טלפון 2

        public string? NumberStringPhone3 { get; set; }  // טלפון 3

        public int? Fax { get; set; }// פקס

        public string? Email { get; set; }//    דוא"ל

        public string? LawyerName { get; set; }//   שם עו"ד

        public DateOnly? DeadlineForReporting { get; set; }//   תאריך אחרון לדיווח
        public bool IsReported { get; set; }//   האם דווח
            //``
        public bool IsConfirmationReporting { get; set; }//   האם דווח אישור

        public string? ReporteFile { get; set; }//   קובץ דיווח

  
        public bool IsCorrectLackPurchaseTaxBalance { get; set; }//אישור היעדר יתרה במס רכישה במסגרת הרכישה: 

        public string? ReportedApproved { get; set; }
        public string? IncumbentNumber { get; set; }//   מספר מחזיק
        //

        public bool HavePowerOfAttorney { get; set; }//   האם יש ייפוי כוח
        //```
        public bool IsCorrectPowerOfAttorney { get; set; }//   האם ייפוי הכוח תקין
        public string? PowerOfAttorneyFile { get; set; }//   קובץ ייפוי כוח
        //

        public bool IsGivenVouchers { get; set; }//   האם ניתנו שוברי תשלום
        public bool IsLegalExpensesPaid { get; set; }//   האם שולמו הוצאות משפטיות
        //```
        public string? PaidNote { get; set; }//   הערה על תשלום
        //

        //``
        public bool? IsSignedTofesHearot { get; set; } /// האם נחתם טופס הוראות בלתי חוזרות במעמד החתימה? 
                                                       

      
        public bool? IsProducedHachira { get; set; }  ///האם הופקה חחירה במעמד החתימה?
        //
        public bool IsFormSignedIrrevocableInstructions { get; set; }//האם  היעדר יתרה במס רכישה במסגרת הרכישה: 
        
        //```
        public string? SignedIrrevocableInstructionsFile { get; set; }//   אישור היעדר יתרה במס רכישה במסגרת הרכישה: 
                                                                      //

        public bool IsFurthermoreLackOfApproval { get; set; }//האם התקבל אישור על הצהרת הדיווח

        public string? LeaseNumberString { get; set; } //מספר חוזה חכירה


    }
}
