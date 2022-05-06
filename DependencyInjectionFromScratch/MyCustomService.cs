public class MyCustomService : IMyCustomService
{
    private readonly AppSettings _appSettings;

    public MyCustomService(AppSettings appSettings, IMyCustomService customService)
    {
        _appSettings = appSettings;
    }
    public MyCustomService(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }
    public void Run()
    {
        Console.WriteLine("MIAU!");
    }
}