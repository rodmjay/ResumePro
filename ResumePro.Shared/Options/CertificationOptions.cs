#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class CertificationOptions : IName, IBody
{
    [Required][MaxLength(255)] public string Name { get; set; }

    [Required] public DateTime? Date { get; set; }

    [MaxLength(255)]
    [Required] public string Body { get; set; }
}