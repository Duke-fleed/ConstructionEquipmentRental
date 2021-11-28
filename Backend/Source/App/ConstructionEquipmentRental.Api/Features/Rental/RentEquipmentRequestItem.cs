using System.ComponentModel.DataAnnotations;

namespace ConstructionEquipmentRental.Api.Features.Rental
{
    /// <summary>
    /// List item for rental requests
    /// </summary>
    public record struct RentEquipmentRequestItem
    {
        /// <summary>
        /// Id of the rental item
        /// </summary>
        [Required]
        public int? ItemId { get; set; }
        /// <summary>
        /// Number of rental days for the item
        /// </summary>
        [Required]
        [Range(1,int.MaxValue)]
        public int? NumberOfDays { get; set; }
    }
}
