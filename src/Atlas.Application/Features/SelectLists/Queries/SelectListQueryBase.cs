using Atlas.Shared.Common;

namespace Atlas.Application.Features.SelectLists.Queries;

public abstract class SelectListQueryBase : IQuery<Result<List<SelectListItemDto>>>
{
    public string? SearchTerm { get; set; }
    public int? MaxResults { get; set; }
}

