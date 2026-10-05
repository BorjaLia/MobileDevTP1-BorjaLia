using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    [Serializable]
    public class TouchInputs
    {
        [SerializeField] public CustomHorizontalController driveControl;
        [SerializeField] public CustomSwipeController handleControl;
    }

    //[SerializeField] public List<TouchInputs> inputList = new List<TouchInputs>();
    [SerializeField] public Dictionary<string, TouchInputs> inputDictionary = new Dictionary<string, TouchInputs>();

    public enum TouchControlType { DriveControl, HandleControl }

    //private TouchControlType m_ControlType = TouchControlType.HandleControl;
    //private TouchControlType currentControlType { get { return m_ControlType; } set { SetControlType(value); } }

    private static TouchInputManager _Instance;
    public static TouchInputManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<TouchInputManager>();
                _Instance.name = _Instance.GetType().ToString();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }
    public static TouchInputManager Get()
    {
        return Instance;
    }

    private void Start()
    {
        if (SettingsManager.singleplayer)
        {
            Vector2 anchorMaxSize = inputDictionary["Player1"].driveControl.transform.parent.GetComponent<RectTransform>().anchorMax;
            anchorMaxSize.x = anchorMaxSize.y;

            inputDictionary["Player1"].driveControl.transform.parent.GetComponent<RectTransform>().anchorMax = anchorMaxSize;

            Destroy(inputDictionary["Player2"].driveControl.transform.parent.gameObject);
            inputDictionary.Remove("Player2");
        }
        else
        {
            if (inputDictionary.Count < 2) { Debug.LogWarning("Less than 2 Touch inputs selected!"); }
        }
    }

    public void SetControlType(TouchControlType type,string key)
    {
        inputDictionary[key].driveControl.gameObject.SetActive(type == TouchControlType.DriveControl);
        inputDictionary[key].handleControl.gameObject.SetActive(type == TouchControlType.HandleControl);
        PLayerInputHandler.ResetFade();
    }

    public void RegisterInput(TouchInputs input, string key)
    {
        inputDictionary[key] = input;
        //inputList.Add(input);
        Debug.Log("Registered new input");
    }
}
