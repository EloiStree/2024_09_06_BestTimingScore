using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkyRidingMono_SecondsToFormatString : MonoBehaviour
{
    public string m_formatString = "{0:000.000}";

    public UnityEvent<string> m_onPushedToString;
    public string m_lastPushed;

    public void PushInSeconds(float seconds) { 
    
        string formatted = string.Format(m_formatString, seconds);
        m_lastPushed = formatted;
        m_onPushedToString.Invoke(formatted);

    }
    public void PushInMilliseconds(int milliseconds) { 
    
        float seconds = milliseconds / 1000.0f;
        PushInSeconds(seconds);

    }
}
