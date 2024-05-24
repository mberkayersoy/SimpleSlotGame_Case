using System;

public interface ISpinState
{
    public event Action SpinStarted;
    public event Action SpinFinished;
}
