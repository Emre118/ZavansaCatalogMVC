using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.ViewModels;

public class DashboardViewModel
{
    public int TotalProductCount { get; set; }
    public int ActiveProductCount { get; set; }
    public int CollectionCount { get; set; }
    public int UnreadMessageCount { get; set; }
    public IEnumerable<ContactMessage> LatestMessages { get; set; } = new List<ContactMessage>();
}
