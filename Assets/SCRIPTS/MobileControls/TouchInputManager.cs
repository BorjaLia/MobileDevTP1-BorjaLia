using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    [Serializable]
    public class TouchInputs {

        [SerializeField] public CustomHorizontalController driveControl;
        [SerializeField] public CustomSwipeController handleControl;
    }

    [SerializeField] public List<TouchInputs> inputList = new List<TouchInputs>();

    public enum TouchControlType { DriveControl,HandleControl }

    private TouchControlType m_ControlType = TouchControlType.HandleControl;
    private TouchControlType currentControlType { get { return m_ControlType; } set { SetControlType(value); } }

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
        if (inputList.Count < 2) { Debug.LogWarning("Less than 2 Touch inputs selected!"); }
    }

    public void SetControlType(TouchControlType type)
    {
        m_ControlType = type;

        foreach (var input in inputList)
        {
                input.driveControl.gameObject.SetActive(type == TouchControlType.DriveControl);
                input.handleControl.gameObject.SetActive(type == TouchControlType.HandleControl);
        }
    }

    public void RegisterInput(TouchInputs input)
    {
        inputList.Add(input);
        Debug.Log("Registered new input");
    }
}
