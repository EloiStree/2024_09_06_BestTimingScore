using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class BestTimingMono_BestScoreStorage : MonoBehaviour
{
    public const float m_defaultBestScore = 600;
    public float m_bestTimingEver = m_defaultBestScore;
    public string m_storageGuid;
    public string m_bestScoreFormat= "{0:000.000}";

    public UnityEvent<float> m_newBestScoreEver;
    public UnityEvent<string> m_newBestScoreAsString;

    private void Reset()
    {
        ChangeGuid();
    }

    public void ResetBestScore() { 
    
        m_bestTimingEver = m_defaultBestScore;
    }

    [ContextMenu("Change Guid")]
    private void ChangeGuid()
    {
        m_storageGuid = System.Guid.NewGuid().ToString();
    }
    private bool m_wasLoadedOnce;

    public void SetBestTiming(float timing)
    {

        if(!m_wasLoadedOnce)
            LoadBestScore();
       
        m_bestTimingEver = timing;
        m_newBestScoreEver.Invoke(timing);
        m_newBestScoreAsString.Invoke(string.Format(m_bestScoreFormat, timing));
        
    }

    private void Start()
    {
        InvokeRepeating("SaveBestScore", 0, 60);
    }
    public string StorageBestScore()
    {
        return Application.persistentDataPath + "/" + m_storageGuid + ".txt";
    }

    public void OnEnable()
    {
        LoadBestScore();

    }
    public void OnDisable()
    {
        SaveBestScore();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveBestScore();
        }
    }
    public void OnApplicationQuit()
    {
        SaveBestScore();
    
    }
    public void OnDestroy()
    {
        SaveBestScore();
    }

    int m_lastSavedBestScoreInMs = 600;

    [ContextMenu("Save")]
    public void SaveBestScore()
    {
        string path = StorageBestScore();
        int milliseconds = (int)(m_bestTimingEver * 1000);
        if (milliseconds != m_lastSavedBestScoreInMs) { 
            m_lastSavedBestScoreInMs = milliseconds;
            File.WriteAllText(path,milliseconds.ToString());
        }
        NotifyBestScoreChanged();
    }

    [ContextMenu("Load")]
    public void LoadBestScore() {

        string path = StorageBestScore();
        if (File.Exists(path))
        {
            if(int.TryParse(File.ReadAllText(path), out int milliseconds))
            {
                m_bestTimingEver = milliseconds / 1000f;
                m_lastSavedBestScoreInMs = milliseconds;
            }
            else
            {
                m_bestTimingEver = m_defaultBestScore;
            }
        }
        else
        {
            m_bestTimingEver = m_defaultBestScore;
        }
        m_wasLoadedOnce = true;
        NotifyBestScoreChanged();
    }

    private void NotifyBestScoreChanged()
    {
        m_newBestScoreEver.Invoke(m_bestTimingEver);
        m_newBestScoreAsString.Invoke(string.Format(m_bestScoreFormat, m_bestTimingEver));
    }
}
