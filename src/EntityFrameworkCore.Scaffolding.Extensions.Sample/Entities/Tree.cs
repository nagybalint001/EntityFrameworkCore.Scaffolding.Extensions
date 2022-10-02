﻿using System;
using System.Collections.Generic;
using EntityFrameworkCore.Scaffolding.Extensions.CommonInterfaces;
using EntityFrameworkCore.Scaffolding.Extensions.Sample.Enums;

namespace EntityFrameworkCore.Scaffolding.Extensions.Sample.Entities;

/// <summary>
/// Tree entity
/// </summary>
public partial class Tree : IAuditableEntity<DateTimeOffset, Guid>
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the tree.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of the tree.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Type of the tree.
    /// </summary>
    public TreeType Type { get; set; }

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

    public virtual ICollection<Apple> Apples { get; } = new List<Apple>();
}
