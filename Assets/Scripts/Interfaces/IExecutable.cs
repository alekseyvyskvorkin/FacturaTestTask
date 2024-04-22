using TestTask.Handlers;

namespace TestTask.Interfaces
{
    public interface IExecutable
    {
        public ExecuteHandler ExecuteHandler { get; set; }
        public void Execute();
    }
}


