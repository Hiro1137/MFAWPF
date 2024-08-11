using HandyControl.Controls;
using HandyControl.Data;

namespace MFAWPF.Utils;

public static class TaskManager
{
    /// <summary>
    /// 执行任务, 并带有更好的日志显示
    /// </summary>
    /// <param name="action">要执行的动作</param>
    /// <param name="name">日志显示名称</param>
    /// <param name="prompt">日志提示</param>
    public static void RunTask(
        Action action,
        string name = nameof(Action),
        string prompt = ">>> ",
        bool catchException = true)
    {
        Growl.Info($"{prompt}任务 {name} 开始.");

        if (catchException)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Growls.Error($"{prompt}任务 {name} 失败.");
            }
        }
        else action();

        Growl.Info($"{prompt}任务 {name} 完成.");
    }

    /// <summary>
    /// 异步执行任务, 并带有更好的日志显示
    /// </summary>
    /// <param name="action">要执行的动作</param>
    /// <param name="name">任务名称</param>
    /// <param name="prompt">日志提示</param>
    public static async Task RunTaskAsync(
        Action action, Action handleError = null,
        string name = nameof(Action),
        string prompt = ">>> ",
        bool catchException = true)
    {
        Console.WriteLine($"异步任务 {name} 开始.");
        if (catchException)
        {
            var task = Task.Run(action);
            task.ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    if (handleError != null)
                        handleError.Invoke();
                    
                    Console.WriteLine($"{prompt}异步任务 {name} 失败: {t.Exception.GetBaseException().Message}");
                    LoggerService.LogError(t.Exception.GetBaseException());
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
        else await Task.Run(action);

        Console.WriteLine($"{prompt}异步任务 {name} 已完成.");
    }
}