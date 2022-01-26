using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button m_PlayButton;

    public delegate void PlayButtonDelegate();
    public event PlayButtonDelegate OnClickPlay;

    void Start()
    {
        m_PlayButton.onClick.AddListener(() =>
        {
            OnClickPlay.Invoke();
        });
    }
}
