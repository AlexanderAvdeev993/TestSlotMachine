using AxGrid;
using AxGrid.FSM;

[State("InitState")]
public class InitState : FSMState
{
    [Enter]
    private void EnterThis()
    {
        Log.Debug($"{Parent.CurrentStateName} ENTER");
        Parent.Change("IdleState");
    }
}
