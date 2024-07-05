namespace PRN231.Domain.Interfaces.Cache;

public interface IRedisService
{
    public Task<bool> SetAsync<T>(string key, T value, int expirySeconds = 900);
    public Task<T> GetAsync<T>(string key);
    public Task<bool> RemoveAsync(string key);
}
