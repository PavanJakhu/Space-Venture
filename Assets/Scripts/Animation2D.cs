/**Creator Sarith Nugawila**/
/**All Rights Reserved, Solid Roots Studios**/
/**Not for Commercial Use**/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animation2D
{
    private GameObject m_GameObject;    
    public float m_GameObjectInitScale;   
    protected List<Sprite[]> m_SpriteList;
    SpriteRenderer m_Renderer;

    public int m_CurrentFrame;
    public int m_NumFrames;
    public int m_AnimState;                     //Which Animation State we are in (which set of sprites to animate)
    public int m_StartFrame;
    public int m_EndFrame;
    public bool m_IsPaused = false;
    public long m_CurrentAnimInterval;
    public long m_AnimInterval;
   
    private bool m_PlayBackWards = false;
    public bool m_Switched = false;
    Timer m_AnimTimer = new Timer(); 


    public Animation2D(Vector2 position, float scale, float rotation, List<Sprite[]> spriteList, long interval, int startframe=0, int endframe=-1)                       
    {      
        m_GameObject = new GameObject("Animation");
        m_Renderer = m_GameObject.AddComponent<SpriteRenderer>();

        //translate touch position to world points
        //Vector3 wp = position;       
      
        //Create an instance of the Prefab Object 
       // m_GameObject = (GameObject)MonoBehaviour.Instantiate(prefab, wp /*- m_vTouchOffset*/, Quaternion.Euler(0, 0, Mathf.Rad2Deg*rotation));      
        m_Renderer = m_GameObject.GetComponent<SpriteRenderer>();

        m_GameObjectInitScale = m_GameObject.transform.localScale.x;

        m_GameObject.transform.localScale = new Vector3(scale, scale, 0f);
              

        m_SpriteList = spriteList;
        m_AnimState = 0;

        this.m_CurrentFrame = this.m_StartFrame = startframe;
        this.m_NumFrames = spriteList[m_AnimState].Length;

        if(endframe==-1)
            m_EndFrame = m_NumFrames - 1;
        else
            this.m_EndFrame = endframe;

        this.m_AnimInterval = m_CurrentAnimInterval = interval;
        m_AnimTimer.StartTimer();       //Star the animation timer

        m_Renderer.sprite = m_SpriteList [m_AnimState] [m_CurrentFrame];
    }

    public void SetAnimInterval(long interval)
    {
        m_CurrentAnimInterval = m_AnimInterval = interval;
    }

  

    //A single animation only update (used to update the frame)
    public void UpdateFrame()
    {
        m_Renderer.sprite = m_SpriteList[m_AnimState][m_CurrentFrame];
    }

    public virtual void Update()
    {
        if(!m_IsPaused)
            m_AnimTimer.Update();

        m_Switched = false;

        if (m_AnimTimer.GetTotalMilliSeconds() >= m_CurrentAnimInterval)
        {
            if (m_PlayBackWards)
            {
                m_Switched = true;
                m_CurrentFrame--;               
                
                if (m_CurrentFrame < 0)
                    m_CurrentFrame = m_EndFrame;

                m_Renderer.sprite = m_SpriteList[m_AnimState][m_CurrentFrame];
            }
            else
            {
                m_Switched = true;
                m_CurrentFrame++;
               
                if (m_CurrentFrame > m_EndFrame)
                    m_CurrentFrame = m_StartFrame; 

                m_Renderer.sprite = m_SpriteList[m_AnimState][m_CurrentFrame];
            }           
            
            m_AnimTimer.RestartTimer();
            
            //We use this way due to problems that occur, when the animation interval is modified externally
            //m_AnimTimer = m_AnimTimer - m_AnimInterval; //add the bleed over time for accuracy
        }  

    }

    /************4 DIFFERENT WAY OF SWITCHING SPRITES***************/
    //Switch a specific set from within the already loaded List
    public void SwitchSpriteSetIndex(int set, bool restart = false, bool holdanim = false)
    {
        //Debug.Log("Animation we are dealing with is: " + m_ZOrder);
        m_AnimState = set;
        this.m_NumFrames = m_SpriteList[m_AnimState].Length;

        if(!holdanim)
            m_EndFrame = m_NumFrames - 1;

        //Switch the Frame immediately
        if (restart)
            m_StartFrame = m_CurrentFrame = 0;
        else
            m_CurrentFrame = (m_CurrentFrame > m_EndFrame) ? 0 : m_CurrentFrame;


        m_Renderer.sprite = m_SpriteList [m_AnimState] [m_CurrentFrame];
       
    }

    //Switch the entire List to a new List of Sprite Sets
    public void SwitchSpriteSet(List<Sprite[]> spriteSetList, int set, bool restart = false, bool holdanim = false)
    {
        m_AnimState = set;
        m_SpriteList = spriteSetList;
        
        this.m_NumFrames = m_SpriteList[m_AnimState].Length;
        
        if(!holdanim)
            m_EndFrame = m_NumFrames - 1;
        
        //Switch the Frame immediately
        if (restart)
            m_StartFrame = m_CurrentFrame = 0;
        else
            m_CurrentFrame = (m_CurrentFrame > m_EndFrame) ? 0 : m_CurrentFrame;

        m_Renderer.sprite = m_SpriteList [m_AnimState] [m_CurrentFrame];
        
    }

    //Switch to a specific set of sprites
    public void SwitchSpriteSet(Sprite[] spriteSet, bool restart = false, bool holdanim = false)
    {
        m_AnimState = 0;
        m_SpriteList.Clear();
        m_SpriteList.Add(spriteSet);

        this.m_NumFrames = m_SpriteList[m_AnimState].Length;
        
        if(!holdanim)
            m_EndFrame = m_NumFrames - 1;
        
        //Switch the Frame immediately
        if (restart)
            m_StartFrame = m_CurrentFrame = 0;
        else
            m_CurrentFrame = (m_CurrentFrame > m_EndFrame) ? 0 : m_CurrentFrame;

        m_Renderer.sprite = m_SpriteList [m_AnimState] [m_CurrentFrame];        
    }

    //Switch to a sprite set using the TowerUpgrade enumeration
    //public void SwitchSpriteSet(TowerReader.TowerUpgrade upgradetype, bool restart = false, bool holdanim = false)
    //{
    //    m_AnimState = 0;
    //    m_SpriteList.Clear();
    //    m_SpriteList.Add(ContentLoader.Init.GetTowerUpgradeSprites(upgradetype));

    //    this.m_NumFrames = m_SpriteList[m_AnimState].Length;
        
    //    if(!holdanim)
    //        m_EndFrame = m_NumFrames - 1;
        
    //    //Switch the Frame immediately
    //    if (restart)
    //        m_StartFrame = m_CurrentFrame = 0;
    //    else
    //        m_CurrentFrame = (m_CurrentFrame > m_EndFrame) ? 0 : m_CurrentFrame;

    //    m_Renderer.sprite = m_SpriteList [m_AnimState] [m_CurrentFrame];
        
    //}
       
    public void PlayBackwards()
    {
        m_PlayBackWards = true;
    }
    
    public void PlayForwards()
    {
        m_PlayBackWards = false;
    }

    public void PauseAnimation()
    {
        m_IsPaused = true;
    }
    
    public void ResumeAnimation()
    {
        m_IsPaused = false;
    }
    
    public void RestartAnimation()
    {
        m_IsPaused = false;
        m_CurrentFrame = 0;
        m_AnimTimer.RestartTimer();
    }

    public void StopAnimation()
    {
        m_IsPaused = true;
        m_CurrentFrame = 0;
        m_AnimTimer.RestartTimer();
    }
    
  

	
}
