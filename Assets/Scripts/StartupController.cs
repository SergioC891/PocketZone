using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class StartupController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    void Awake()
    {
        Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS,
            OnManagersProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED,
            OnManagersStarted);
    }

    void OnDestroy()
    {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS,
            OnManagersProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED,
            OnManagersStarted);
    }

    private void OnManagersProgress(int numReady, int numModules)
    {
        if (progressBar != null)
        {
            float progress = (float)numReady / numModules;
            progressBar.value = progress;
        }
    }

    private void OnManagersStarted()
    {
//        Managers.Mission.GoToNext();
    }
}
