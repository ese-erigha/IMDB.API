using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ResponseDto = IMDB.Api.Models.ResponseDto;

namespace IMDB.Api.Helpers
{
    public class PagedList<TDest> where TDest : ResponseDto.BaseModel
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<TDest> Items { get; set; } = new List<TDest>();


        public bool HasPrevious
        {
            get
            {
                return (CurrentPage > 1);
            }
        }


        public bool HasNext
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }

        public PagedList(IEnumerable<TDest> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage =  pageNumber;
            TotalPages = (count > 0) ? (int)Math.Ceiling(count / (double)pageSize) : 0;
            Items = items;
        }
    }
}
