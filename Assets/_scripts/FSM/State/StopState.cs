﻿using System.Collections.Generic;
using AxGrid;
using AxGrid.FSM;
using UnityEngine;

[State("StopState")]
public class StopState : FSMState
{   
    private List<SlotItemScroll> _rowList = new List<SlotItemScroll>();
    private float _duration;
    private int _slotIndex;
    private ParticleSystem _particleSystem;
    
    [Enter]
    private void EnterThis()
    {
        Log.Debug(" Enter StopState ");
        _slotIndex = 0;
        GetDataInModel();
    }
    
    private void GetDataInModel()
    {
        _rowList = Settings.Model.Get<List<SlotItemScroll>>("rowSlots");
        _duration = Settings.Model.Get<float>("duration");
        _particleSystem = Settings.Model.Get<ParticleSystem>("particleSystem");
    }
    
    [Loop (3f)]
    private void LoopStopSlot()
    {   
        if(_rowList.Count <= _slotIndex)
            return;
        
        _rowList[_slotIndex].StopScrollSmoothly(Random.Range(_duration, _duration * 2));
        _slotIndex++;
    }
    
    [One (15f)]
    private void TransitionIdleState()
    {   
        _particleSystem.Play();
        Parent.Change("IdleState");
    }
}
