namespace KonSolutionsApi.Common;

public class FlagDownloadProgress
{
    public int  Total     { get; set; }
    public int  Processed { get; set; }
    public bool IsRunning { get; set; }
    public int  Percentage => Total == 0 ? 0 : (int)(Processed * 100.0 / Total);
}
