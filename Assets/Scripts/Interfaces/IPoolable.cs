using TestTask.Spawn;

namespace TestTask.Interfaces
{
    public interface IPoolable
    {
        public PoolContainer Pool { get; set; }
        public void ReturnToPool();
    }
}


