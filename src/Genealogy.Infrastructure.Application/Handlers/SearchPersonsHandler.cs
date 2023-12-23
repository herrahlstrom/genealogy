using System.Data;
using Genealogy.Application.Request;
using Genealogy.Domain.Data.Entities;
using Genealogy.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Application.Handlers;

public class SearchPersonsHandler(GenealogyDbContext dbContext) : IRequestHandler<SearchPersonsRequest, SearchPersonsResponse>
{
    public async Task<SearchPersonsResponse> Handle(SearchPersonsRequest request, CancellationToken cancellationToken)
    {
        IQueryable<PersonEntity> query = dbContext.Persons;
        foreach (var textPart in request.QueryString.ToLower().Split())
        {
            query = query.Where(x => x.Name.ToLower().Contains(textPart));
        }

        var items = await query.Select(x => new SearchPersonsResponseItem(x.Id, x.Name))
                                  .ToListAsync(cancellationToken);

        return new SearchPersonsResponse(items);
    }
}
