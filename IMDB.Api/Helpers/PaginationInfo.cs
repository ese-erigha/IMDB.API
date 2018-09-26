using System;
namespace IMDB.Api.Helpers
{
    public class PaginationInfo
    {
        const int maxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
