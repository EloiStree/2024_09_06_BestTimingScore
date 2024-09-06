using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TimeBestScoreType
{
    Survival,
    BestTime
}

public class BestTimingMono_SessionBestScore : MonoBehaviour
{

    public TimeBestScoreType m_timeBestScoreType;
    public float m_bestMeasureTime= 600;
    public float m_lastMeasureTime = 0;
    public string m_scoreFormat = "{0:000.000}";

    public UnityEvent<float> m_lastTimingChanged;
    public UnityEvent<float> m_bestTimingChanged;
    public UnityEvent<string> m_lastTimingChangedAsString;
    public UnityEvent<string> m_bestTimingChangedAsString;

    private void Awake()
    {
        if (m_timeBestScoreType == TimeBestScoreType.BestTime)
            m_bestMeasureTime = 600;
        else if (m_timeBestScoreType == TimeBestScoreType.Survival)
            m_bestMeasureTime = 0;
    }

    [ContextMenu("Reset current best score")]
    public void ResetCurrentBestScore() { 
    
        if(m_timeBestScoreType== TimeBestScoreType.BestTime)
        m_bestMeasureTime = 600;
        else if(m_timeBestScoreType== TimeBestScoreType.Survival)
            m_bestMeasureTime = 0;
        m_bestTimingChanged.Invoke(m_bestMeasureTime);
        m_bestTimingChangedAsString.Invoke(string.Format(m_scoreFormat, m_bestMeasureTime));
    }

    [ContextMenu("Set current as best score")]
    public void SetCurrentBestScoreToLastRace() { 
    
        SetLastTimingInSeconds(m_lastMeasureTime);
    }

    public void SetLastTimingInSeconds(float timeInSeconds)
    {
        m_lastMeasureTime = timeInSeconds;
        bool bestTimeChanged = false;
        
        if(m_timeBestScoreType== TimeBestScoreType.BestTime)
            bestTimeChanged = m_lastMeasureTime < m_bestMeasureTime;
        else if(m_timeBestScoreType== TimeBestScoreType.Survival)
            bestTimeChanged = m_lastMeasureTime > m_bestMeasureTime;

        if(bestTimeChanged)
        {
            m_bestMeasureTime = m_lastMeasureTime;
        }

        m_lastTimingChanged.Invoke(m_lastMeasureTime);
        m_lastTimingChangedAsString.Invoke(string.Format(m_scoreFormat, m_lastMeasureTime));
        if(bestTimeChanged)
        {
            m_bestTimingChanged.Invoke(m_bestMeasureTime);
            m_bestTimingChangedAsString.Invoke(string.Format(m_scoreFormat, m_bestMeasureTime));
        }
    }
}
