namespace Structures.CombinedStatuses;

public abstract class CombinedStatus
{
    public string ModuleState { get; set; }
    public bool IsBusy { get; set; }
    public bool IsReady { get; set; }
    public bool IsError { get; set; }
    public bool KeyLock { get; set; }
}