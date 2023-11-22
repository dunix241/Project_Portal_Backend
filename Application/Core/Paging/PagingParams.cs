namespace Application.Core;

public class PagingParams
{
    private const int MaxPageSize = 25;
    private int _pageSize = 10;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Min(MaxPageSize, value);
    }

    public string? SearchString { get; set; }
    public string? Order { get; set; }
    public string? OrderBy { get; set; }
    public string? Filter { get; set; }
    public string? FilterBy { get; set; }
}