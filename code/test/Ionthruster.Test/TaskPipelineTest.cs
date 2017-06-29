using System;
using System.Threading.Tasks;
using Xunit;

namespace Ionthruster.Test
{
    public class TaskPipelineTest
    {
        [Fact]
        public void TestPipeline()
        {
            var firstPipe = new TaskPipeline<CleanProjectTask>();
            var convertedPipe = (ITaskPipeline<ITask>)firstPipe;
            var lastPipe = convertedPipe.Join<CleanProjectTask>()
                .Join<BuildProjectTask>()
                .Join<GitVersionTask>();
        }
    }

    public interface ITaskPipeline<out TTask>
        where TTask : ITask
    {
        ITaskPipeline<TNextTask> Join<TNextTask>()
            where TNextTask : class, ITask;
    }

    public class TaskPipeline<TTask> : ITaskPipeline<TTask>
        where TTask : ITask
    {
        ITaskPipeline<TNextTask> ITaskPipeline<TTask>.Join<TNextTask>()
        {
            return new TaskPipeline<TNextTask>();
        }
    }

    public interface ITask
    {

    }

    public interface IVoidTask : ITask
    {
        Task Run();
    }

    public interface IActionTask<TInput> : ITask
    {
        Task Run(TInput input);
    }

    public interface IFuncTask<TInput, TOutput> : ITask
    {
        Task<TOutput> Run(TInput input);
    }

    public class CleanProjectTask : IVoidTask
    {
        public Task Run()
        {
            throw new NotImplementedException();
        }
    }

    public class BuildProjectTask : IActionTask<string>
    {
        public Task Run(string input)
        {
            throw new NotImplementedException();
        }
    }

    public class GitVersionTask : IFuncTask<string, object>
    {
        public Task<object> Run(string input)
        {
            throw new NotImplementedException();
        }
    }
}
