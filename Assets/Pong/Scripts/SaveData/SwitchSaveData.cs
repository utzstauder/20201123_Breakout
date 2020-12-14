using UnityEngine;

public class SwitchSaveData : AbstractSaveData
{
    public SwitchSaveData()
    {
        Debug.Log("SwitchSaveData initialized!");
    }

    public override string Load()
    {
        // platform dependent implementation
        return "SwitchSaveData";
    }

    public override void Save(string payload)
    {

        Debug.Log("Switch save data saved!");
    }
}
