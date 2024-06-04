using System.Collections.Generic;
using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State("StartState")]
public class StartState : FSMState
{
    private List<SlotItemScroll> _rowList = new List<SlotItemScroll>();
    private float _duration;
    private float _elapsedTime;
    private float _targetSpeed;
    
    [Enter]
    private void EnterThis()
    { 
        Debug.Log("StartState Enter");
        GetDataInModel();
        //_elapsedTime = 0;
        
         foreach (var item in _rowList)
         {
             item.StartScrollAcceleration(_targetSpeed, _duration);
             //item.ScrollSpeed = 1000;
         }
    }

    private void GetDataInModel()
    {
        _rowList = Settings.Model.Get<List<SlotItemScroll>>("rowSlots");
        _targetSpeed = Settings.Model.Get<float>("slotSpeed");
        _duration = Settings.Model.Get<float>("duration");
    }
    
    [Bind("StopButton")]
    private void OnStopButtonClick()
    {
        Debug.Log("ClickButtonStop");
        Parent.Change("StopState");
    }
}
