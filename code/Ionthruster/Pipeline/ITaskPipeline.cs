using Ionthruster.Tasks;
using System.Threading.Tasks;
using System;

namespace Ionthruster.Pipeline
{
    //public interface ITaskPipeline
    //{
    //    //ITaskPipeline<TNextOutput> Branch<TTask, TNextOutput>() where TTask : class, ITask<TNextOutput>;

    //    Task Flush();

    //    //ITaskPipeline Join<TTask>() where TTask : class, ITask;
    //}

    //public interface ITaskPipeline<TOutput>
    //{
    //    //ITaskPipeline<TNextOutput> Branch<TNextOutput, TTask>() where TTask : class, ITask<TNextOutput>;

    //    Task<TOutput> Flush();

    //    //ITaskPipeline<TNextOutput> Join<TTask, TNextOutput>() where TTask : class, ITask<TOutput, TNextOutput>;
    //}

    public interface ITaskPipeline<in TTask>
        where TTask : ITask
    {
        Task Flush();
    }

    public static class TaskPipelineExtension
    {
        public static ITaskPipeline<ITask> Join<TNextTask>(this ITaskPipeline<ITask> pipeline)
            where TNextTask : class, ITask
        {
            return default(ITaskPipeline<ITask>);
        }

        //public static ITaskPipeline<IVoidTask> Join<TTask>(this ITaskPipeline<IVoidTask> pipeline)
        //    where TTask : IVoidTask
        //{
        //    return default(ITaskPipeline<IVoidTask>);
        //}

        //public static ITaskPipeline<IOutputTask<TOutput>> Join<TTask, TOutput>(this ITaskPipeline<IVoidTask> pipeline)
        //{
        //    return default(ITaskPipeline<IOutputTask<TOutput>>);
        //}
    }

    public class OutputTask : IOutputTask<bool>
    {
        public Task<bool> Run()
        {
            throw new NotImplementedException();
        }
    }

    public class Sandbox
    {
        public async Task Play()
        {
            var pipelineOne = (ITaskPipeline<ITask>)default(ITaskPipeline<CleanProjectTask>);

            await pipelineOne.Join<CleanProjectTask>()
                .Join<OutputTask>()
                .Join<GitVersionTask>()
                .Flush();
        }
    }
}