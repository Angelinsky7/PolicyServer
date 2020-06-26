using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPolicyServer.Quickstart {
    public class ListViewModel {

        public String SearchString { get; set; }

        public String SortOrder { get; set; }
        public Int32 Count { get; set; }
        public Int32 PageSize { get; set; } = 10;

        public Int32 TotalPages => (Int32)Math.Ceiling(Decimal.Divide(Count, PageSize));
        public Int32 CurrentPage { get; set; }
        public Boolean ShowPrevious => CurrentPage > 1;
        public Boolean ShowNext => CurrentPage < TotalPages;
        public Boolean ShowFirst => CurrentPage != 1;
        public Boolean ShowLast => CurrentPage != TotalPages;

        public ListViewModel() { }
        public ListViewModel(ListViewModel src) {
            SearchString = src.SearchString;
            SortOrder = src.SortOrder;
            Count = src.Count;
            PageSize = src.PageSize;
            CurrentPage = src.CurrentPage;
        }

    }
}
