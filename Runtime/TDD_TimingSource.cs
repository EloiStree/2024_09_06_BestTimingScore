using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TDD_TimingSource : MonoBehaviour
{

    public float m_currentTiming;
    public InputActionReference m_confirmedTiming;

    public DateTime m_whenStarted;
    public UnityEvent<float> m_onConfirmTiming;
    public UnityEvent<float> m_onCurrentTiming;
    public float m_timingInSeconds;

    private void Awake()
    {
        m_whenStarted = DateTime.Now;
        m_confirmedTiming.action.Enable();
        m_confirmedTiming.action.performed += ctx => ConfirmedTiming();
        m_currentTiming = 0;
    }


    [ContextMenu("Confirm timing")]
    public void ConfirmedTiming()
    {

        m_currentTiming = GetSecondPasted();
        m_whenStarted = DateTime.Now;
        m_onConfirmTiming.Invoke(m_currentTiming);




    }

    public float GetSecondPasted()
    {
        return GetSecondPasted(DateTime.Now);
    }

    public float GetSecondPasted(DateTime now)
    {
        return (float)(now - m_whenStarted).TotalSeconds;
    }

    public void Update()
    {
       m_currentTiming = GetSecondPasted();
        m_onCurrentTiming.Invoke(m_currentTiming);
    }

}
