﻿#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ReferenceDto : IReference
{
    [JsonIgnore] public int JobId { get; set; }

    [JsonIgnore] public int Id { get; set; }

    public string Text { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}