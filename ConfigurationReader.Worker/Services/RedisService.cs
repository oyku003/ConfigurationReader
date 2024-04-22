using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace ConfigurationReader.Worker.Services
{
    public class RedisService
    {
        private readonly string _host;

        private readonly int _port;

        private ConnectionMultiplexer _ConnectionMultiplexer;
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);

        public async Task<bool> TakeLockAsync(string key, RedisValue redisValue, TimeSpan duration)
            => await GetDb(1).LockTakeAsync(key, redisValue, duration);
       
    }
}
