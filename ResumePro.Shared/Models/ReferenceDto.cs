#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ReferenceDto : IReference
{
    [JsonIgnore] public int PersonId { get; set; }

    public int Id { get; set; }

    public string Text { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public int Order { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }
}