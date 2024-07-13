#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;

namespace ResumePro.Core.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IMapper mapper)
    {
        return mapper.ProjectTo<TDestination>(source);
    }
}