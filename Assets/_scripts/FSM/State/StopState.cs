using System.Collections;
using System.Collections.Generic;
using AxGrid;
using AxGrid.FSM;
using UnityEngine;

[State("StopState")]
public class StopState : FSMState
{   
    private List<SlotItemScroll> _rowList = new List<SlotItemScroll>();
    private float _duration;
    private int slotIndex;
    private Coroutine stopCoroutine;
    
    private float delayTime = 1f;
    
    [Enter]
    private void EnterThis()
    {
        Log.Debug(" Enter StopState ");
        GetDataInModel();
    }
    
    private void GetDataInModel()
    {
        _rowList = Settings.Model.Get<List<SlotItemScroll>>("rowSlots");
        _duration = Settings.Model.Get<float>("duration");
    }

    private IEnumerator stopCorutine()
    {
        yield return null;
    }
    
    [Loop (3f)]
    private void LoopStopSlot()
    {   
        Debug.Log("loop");
        if(_rowList.Count <= slotIndex)
            return;
        
        _rowList[slotIndex].StopScrollSmoothly(Random.Range(_duration, _duration + 5));
        slotIndex++;
    }
    
    [One (10f)]
    private void TransitionIdleState()
    {   
        Parent.Change("IdleState");
    }
}
