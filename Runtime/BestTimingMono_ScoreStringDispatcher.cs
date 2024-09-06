using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BestTimingMono_ScoreStringDispatcher : MonoBehaviour
{

    public string m_currentScore;
    public string m_lastScore;
    public string m_bestScore;
    public string m_bestScoreEver;

    public UnityEvent<string> m_onCurrentScore;
    public UnityEvent<string> m_onLastScore;
    public UnityEvent<string> m_onBestScore;
    public UnityEvent<string> m_onBestScoreEver;

    public void SetCurrentScore(string score)
    {
        m_currentScore = score;
        m_onCurrentScore.Invoke(m_currentScore);
    }
    public void SetLastScore(string score) {
        m_lastScore = score;
        m_onLastScore.Invoke(m_lastScore);
    }
    public void SetBestScore(string score) {
        m_bestScore = score;
        m_onBestScore.Invoke(m_bestScore);
    }
    public void SetBestScoreEver(string score) {
        m_bestScoreEver = score;
        m_onBestScoreEver.Invoke(m_bestScoreEver);
    }


}
