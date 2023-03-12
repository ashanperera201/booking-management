using System;
using System.Collections.Generic;

namespace BookingManagementDomain.EntityModels;

public partial class Role
{
    public int Id { get; set; }

    public Guid UniqueId { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleDescription { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
