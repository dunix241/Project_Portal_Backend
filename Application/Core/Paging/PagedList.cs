using Microsoft.EntityFrameworkCore;

namespace Application.Core;

public class PagedList<T>
{
    public ICollection<T> Items { get; set; }
    public Pagination Pagination { get; set; }

    public async Task GetItemsAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        Items = new List<T>(await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
        Pagination = new Pagination(pageNumber, (int)Math.Ceiling(count / (double)pageSize), pageSize, count);
    }

}