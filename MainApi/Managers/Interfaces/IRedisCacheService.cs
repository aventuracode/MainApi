namespace MainApi.Managers.Interfaces
{
    public interface IRedisCacheService
    {
        Task Add(string key, string value);
        Task<string> Get(string key);
    }
}
