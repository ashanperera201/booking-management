using System;
using System.Collections.Generic;

namespace BookingManagementDomain.EntityModels;

public partial class User
{
    public int Id { get; set; }

    public Guid UniqueId { get; set; }

    public int? RoleId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserContact { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual Role? Role { get; set; }
}
