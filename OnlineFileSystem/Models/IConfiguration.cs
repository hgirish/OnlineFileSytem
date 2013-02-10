namespace OnlineFileSystem.Models
{
    public interface IConfiguration<TSettings> where TSettings : new()
    {
        TSettings Settings { get; }
    }

    public class AppSettings
    {
        public string RootPath { get; set; }
    }
}