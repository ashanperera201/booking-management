using System;
using System.Collections.Generic;

namespace BookingManagementDomain.EntityModels;

public partial class Booking
{
    public int Id { get; set; }

    public Guid UniqueId { get; set; }

    public string FromAddress { get; set; } = null!;

    public string ToAddress { get; set; } = null!;

    public string GoodType { get; set; } = null!;

    public DateTime BookedTime { get; set; }

    public decimal Weight { get; set; }

    public int Pricing { get; set; }

    public string AssignedDriver { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
}
