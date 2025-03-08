#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class TemplateDto : ITemplate
{
    public bool IsGlobal { get; set; }
    public string Name { get; set; }
    public string Source { get; set; }
    public string Format { get; set; }
    public string Engine { get; set; }
    public int Id { get; set; }
}