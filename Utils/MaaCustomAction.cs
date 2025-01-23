using MaaFramework.Binding;
using MaaFramework.Binding.Custom;
using MFAWPF.Utils.Exceptions;

namespace MFAWPF.Utils;

public abstract class MaaCustomAction : IMaaCustomAction
{
    public abstract string Name { get; set; }
    
    public bool Run(in IMaaContext inContext, in RunArgs inArgs)
    {
        var context = inContext;
        var args = inArgs;
        try
        {
            return Execute(context, args);
        }
        catch (MaaStopException)
        {
            LoggerService.LogInfo("Stopping MaaCustomAction");
            return false;
        }
        catch (MaaErrorHandleException)
        {
            LoggerService.LogInfo("ErrorHandle MaaCustomAction");
            ErrorHandle(context, args);
            return true;
        }
    }

    protected abstract bool Execute(IMaaContext context, RunArgs args);

    protected virtual void ErrorHandle(IMaaContext context, RunArgs args) {}
}
