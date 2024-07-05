using PRN231.Domain.Interfaces.Cache;
using StackExchange.Redis;
using System.Text.Json;

namespace PRN231.Infrastructure.Cache;

public class RedisService(IDatabase redis) : IRedisService
{
    private readonly IDatabase _redis = redis;

    public async Task<bool> SetAsync<T>(string key, T value, int expirySeconds = 900)
    {
        string jsonStr = JsonSerializer.Serialize(value);
        var setTask = _redis.StringSetAsync(key, jsonStr);
        var expireTask = _redis.KeyExpireAsync(key, TimeSpan.FromSeconds(expirySeconds));
        var res = await Task.WhenAll(setTask, expireTask);
        return res[0] && res[1];
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var jsonStr = await _redis.StringGetAsync(key);
        if (jsonStr == RedisValue.Null) return default;
        var res = JsonSerializer.Deserialize<T>(jsonStr);
        return res;
    }

    public async Task<bool> RemoveAsync(string key)
    {
        var res = await _redis.KeyDeleteAsync(key);
        return res;
    }
}
