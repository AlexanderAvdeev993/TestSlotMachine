using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State("IdleState")]
public class IdleState : FSMState
{
    [Enter]
    private void EnterThis()
    {
        Log.Debug("Enter IdleState");
        Settings.Invoke("OnStopFieldChanged");
    }
    
    [Bind("StartButton")]
    private void OnStartButtonClick()
    {   
        Debug.Log("ClickButtonStart  in IdleState");
        Settings.Invoke("OnStartFieldChanged");
        Parent.Change("StartState");
    }
}
