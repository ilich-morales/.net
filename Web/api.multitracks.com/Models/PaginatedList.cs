using System.Runtime.CompilerServices;

namespace api.multitracks.com.Models
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int PageSize { get; }
        public int TotalCount { get; }        
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasNext => PageNumber < TotalPages;
        public bool HasPrevious => PageNumber > 1;

        public PaginatedList(IReadOnlyCollection<T> items, int totalCount, int pageSize, int pageNumber)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            Items = items;
        }

        public static PaginatedList<T> ToPagedList(IEnumerable<T> data, int pageSize, int pageNumber)
        {
            var totalCount = data.Count();
            var items = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, totalCount, pageSize, pageNumber);
        }
    }
}
