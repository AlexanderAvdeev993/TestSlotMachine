using AxGrid;
using AxGrid.FSM;

[State("StopState")]
public class StopState : FSMState
{
    [Enter]
    private void EnterThis()
    {
        Log.Debug(" Enter StopState ");
    }
    
    [One (3f)]
    private void TransitionIdleState()
    {   
        Parent.Change("IdleState");
    }
}
