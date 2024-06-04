using System.Collections.Generic;
using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;
using UnityEngine.Serialization;

public class FSMMain : MonoBehaviourExtBind
{ 
    [SerializeField] private List<SlotItemScroll> _rowsSlots;
    //[SerializeField] private float _slotSpeed;
    //[SerializeField] private float _duration;
    
    
    [OnStart]
    private void StartThis()
    {
        Log.Debug("FSMMain Start");
            
        Settings.Fsm = new FSM(); 
        Settings.Fsm.Add(new InitState());
        Settings.Fsm.Add(new IdleState());
        Settings.Fsm.Add(new StartState());
        Settings.Fsm.Add(new StopState());
        
        Settings.Model.Add("rowSlots" ,_rowsSlots);
        Settings.Fsm.Start("InitState");
    }

    [OnUpdate]
    private void UpdateThis()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }

    /*private void InitModel()
    {
        Settings.Model.Add("rowSlots" ,_rowsSlots);
        Settings.Model.Add("slotSpeed",_slotSpeed);
        Settings.Model.Add("duration",_slotSpeed);
    }*/
}

