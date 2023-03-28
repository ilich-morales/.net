using api.multitracks.com.Models;

namespace api.multitracks.com.Extensions
{
    public static class MappingExtensions
    {
        public static PaginatedList<TDestination> ToPaginatedList<TDestination>(this IEnumerable<TDestination> queryable, int pageSize, int pageNumber) where TDestination : class
        => PaginatedList<TDestination>.ToPagedList(queryable, pageSize, pageNumber);
    }
}
