using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

[State("IdleState")]
public class IdleState : FSMState
{
    [Enter]
    private void EnterThis()
    {
        Log.Debug("Enter IdleState");
    }
    
    [Bind("StartButton")]
    private void OnStartButtonClick()
    {   
        Parent.Change("StartState");
    }
}
