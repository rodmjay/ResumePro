#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Generation;

public class PdfSettings
{
    public bool CreateUpdatePdf { get; set; } = false;
    public bool DisplayInExplorer { get; set; } = true;
    public string FontFamily { get; set; } = "Verdana";
}