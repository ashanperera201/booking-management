using System;
using System.Collections.Generic;

namespace BookingManagementDomain.EntityModels;

public partial class Permission
{
    public int Id { get; set; }

    public Guid UniqueId { get; set; }

    public int? RoleId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string PermissionDescription { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual Role? Role { get; set; }
}
