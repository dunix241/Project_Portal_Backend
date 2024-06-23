using Microsoft.EntityFrameworkCore;


namespace Application.Core
{
    public class PagedList<T>
    {
        public ICollection<T> Items { get; set; }
        public Pagination Pagination { get; set; }

        public async Task GetItemsAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count =  source.Count();
            Items =  source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            Pagination = new Pagination(pageNumber, (int)Math.Ceiling(count / (double)pageSize), pageSize, count);
        }
    }
}
