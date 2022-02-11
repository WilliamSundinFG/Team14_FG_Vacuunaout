namespace Team14
{
    public delegate void TaskTrigger();
    public interface ITaskInvoker
    {
        public event TaskTrigger Trigger;
    }
}