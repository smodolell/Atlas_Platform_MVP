namespace Atlas.Shared.Common;

public class AppSettingDto
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; } = "";

    public AppSettingDto(ApplicationOptions options)
    {
        ApplicationId = options.ApplicationId;
        ApplicationName = options.ApplicationName;
    }
}


public class ApplicationOptions
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
}