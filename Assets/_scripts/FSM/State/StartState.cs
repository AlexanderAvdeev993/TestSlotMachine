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
    private float _targetSpeed;
    private bool _isClickActive = false;
    
    [Enter]
    private void EnterThis()
    { 
        Debug.Log("StartState Enter");
        GetDataInModel();
         foreach (var item in _rowList)
         {
             item.StartScrollAcceleration(_targetSpeed, _duration);
         }
    }

    private void GetDataInModel()
    {
        _rowList = Settings.Model.Get<List<SlotItemScroll>>("rowSlots");
        _targetSpeed = Settings.Model.Get<float>("slotSpeed");
        _duration = Settings.Model.Get<float>("duration");
    }
    
    [One (3f)]
    private void ChangeButtonActive()
    {
        _isClickActive = true;
        Debug.Log("StopButtonActive");
    }
    
    [Bind("StopButton")]
    private void OnStopButtonClick()
    {
        if (_isClickActive)
        {   
            Parent.Change("StopState");
        }
    }
}
