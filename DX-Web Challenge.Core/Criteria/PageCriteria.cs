namespace DX_Web_Challenge.Core.Criteria
{
    public class PageCriteria
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string SortField { get; set; }
        public string SortOrder { get; set; }

        public bool HasPagination => PageNumber.HasValue == true && PageSize.HasValue == true;
        public int SkipValue => (PageNumber.Value - 1) * PageSize.Value;
        public bool IsAscending => string.IsNullOrWhiteSpace(SortOrder) == true || SortOrder.ToLower() == "asc";
    }
}