/**Creator Sarith Nugawila**/
/**All Rights Reserved, Solid Roots Studios**/
/**Not for Commercial Use**/

using UnityEngine;
using System.Collections;

public class Timer 
{
    long m_Milliseconds = 0;
    public int m_Seconds = 0;
    public int m_Minutes = 0;
    int m_Hours = 0;
    float m_Interval = 1000; //1 second  
    bool m_IsPaused = true;
    
    public Timer()
    {
    }
    
    public void Update()
    {
        if (!m_IsPaused)
            m_Milliseconds += (long)((Time.deltaTime)*1000);

                
        if (m_Milliseconds >= 1000)
        {
            m_Milliseconds = m_Milliseconds - (long)m_Interval; //collect the left over milliseconds for accuracy

            m_Seconds++;
            
            if (m_Seconds >= 60)
            {
                m_Seconds = 0;
                m_Minutes++;
            }
            
            if (m_Minutes >= 60)
            {
                m_Minutes = 0;
                m_Hours++;
            }
        }
        
    }
    
    public void PauseTimer()
    {
        m_IsPaused = true;
    }
    
    public void StopTimer()
    {
        m_IsPaused = true;
        m_Milliseconds = m_Seconds = m_Minutes = m_Hours = 0;        
    }
    
    public void StartTimer()
    {
        m_IsPaused = false;
    }
    
    public void RestartTimer()
    {
        m_IsPaused = false;
        m_Milliseconds = m_Seconds = m_Minutes = m_Hours = 0;
    }
    
    public bool getIsPaused()
    {
        return m_IsPaused;
    }
    
    //used to set custom interval
    public void SetInterval(float interval)
    {
        m_Interval = interval;
    }
    
    public long GetTotalMilliSeconds()
    {
        long milli = 0;
        milli += m_Hours * 60 * 60 * 1000;
        milli += m_Minutes * 60 * 1000;
        milli += m_Seconds * 1000;
        milli += m_Milliseconds;
        
        return milli;
    }
    
    public void SetTotalMilliSeconds(long milli)
    {
        m_Hours = (int)(milli / (3600 * 1000));
        milli = (m_Hours>0) ? (milli-(m_Hours*(3600*1000))) : milli;
        
        m_Minutes = (int)(milli / (60 * 1000));
        milli = (m_Minutes > 0) ? (milli - (m_Minutes * (60 * 1000))) : milli;
        
        m_Seconds = (int)(milli / 1000);
        milli = (m_Seconds > 0) ? (milli - (m_Seconds * 1000)) : milli;
        
        m_Milliseconds = milli;
        
        //Debug.Log("INputed time becomes: " + m_Hours + " : " + m_Minutes + " : " + m_Seconds + " : " + m_Milliseconds);
    }
    
    public bool IsTimePassed(long milliseconds)
    {
        if (milliseconds <= GetTotalMilliSeconds())
            return true;
        else
            return false;
    }
    
    public bool IsTimePassed(int hours, int minutes, int seconds, long milliseconds)
    {
        if (hours <= m_Hours)
        {
            if(minutes <= m_Minutes) 
            {                
                if (seconds <= m_Seconds)
                {                    
                    if (milliseconds <= m_Milliseconds )
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }               
        else
            return false;
    }
    
    
    
    
    public string getTotalTimeString()
    {
        string hours = (m_Hours < 10) ? 0 + m_Hours.ToString() : m_Hours.ToString();
        string minuts = (m_Minutes < 10) ? 0 + m_Minutes.ToString() : m_Minutes.ToString();
        string seconds = (m_Seconds < 10) ? 0 + m_Seconds.ToString() : m_Seconds.ToString();
        return hours + " : " + minuts + " : " + seconds;
    }
}
