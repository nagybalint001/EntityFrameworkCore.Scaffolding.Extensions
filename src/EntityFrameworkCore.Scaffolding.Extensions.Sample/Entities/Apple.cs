using System;
using System.Collections.Generic;
using EntityFrameworkCore.Scaffolding.Extensions.CommonInterfaces;
using EntityFrameworkCore.Scaffolding.Extensions.Sample.Enums;

namespace EntityFrameworkCore.Scaffolding.Extensions.Sample.Entities;

/// <summary>
/// Apple entity
/// </summary>
public partial class Apple : IAuditableEntity<DateTimeOffset, Guid>, ISoftDeletableEntity
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Status of the apple.
    /// </summary>
    public AppleStatus? Status { get; set; }

    /// <summary>
    /// The tree it is belonging to.
    /// </summary>
    public int TreeId { get; set; }

    /// <summary>
    /// Creation time.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Created by.
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// Last modification time.
    /// </summary>
    public DateTimeOffset ModifiedAt { get; set; }

    /// <summary>
    /// Last modified by.
    /// </summary>
    public Guid ModifiedBy { get; set; }

    /// <summary>
    /// Is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    public virtual Tree Tree { get; set; } = null!;
}
