namespace FundRaiser.Common.Mappers.ConfigMappers
{
    public class StorageSettings
    {
        public const string StorageSection = "Storage";
        
        public string BasePath { get; set; }
        public string ImagesPath { get; set; }
        public string VideoPath { get; set; }
    }
}