using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PanelControl : MonoBehaviour
{
    [Tooltip("The Ui Panel to display VideoPlayer")] [SerializeField]
    private GameObject videoPanel; 
    [Tooltip("Preloader VideoPlayer")] [SerializeField]
    private VideoPlayer videoPlayer;
    [Tooltip("Preloader VideoPlayer")] [SerializeField]
    private Scrollbar progressBar;

    public float Progress
    {
        get => progressBar.size;
        set => progressBar.size = value;
    }
    
    public void StopPreloaderVideo()
    {
        videoPanel.SetActive(false);
        videoPlayer.Stop();
    }
    
    
    
    
    

}
