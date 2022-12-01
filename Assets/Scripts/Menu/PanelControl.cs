using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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

    [SerializeField] private Button enterButton;
    
    public float Progress
    {
        get => progressBar.size;
        set => progressBar.size = value;
    }

    private void Start()
    {
        enterButton.onClick.AddListener(() =>
        {
            EventSystem.current.enabled = false;
            Debug.LogError("EventSystemIsOff-HardOff");
        });
    }

    public void StopPreloaderVideo()
    {
        videoPanel.SetActive(false);
        videoPlayer.Stop();
    }
    
    
    
    
    

}
