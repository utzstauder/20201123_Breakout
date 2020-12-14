using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataHandler : MonoBehaviour
{
    AbstractSaveData saveData;

    private void Awake()
    {

#if UNITY_SWITCH && !UNITY_EDITOR
        saveData = new SwitchSaveData();
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        saveData = new WindowsSaveData();
#endif

        Debug.Log(saveData.Load());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            saveData.Save("test");
        }
    }
}
