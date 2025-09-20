using TMPro;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class DevCanvas : MonoBehaviour
{
    public static DevCanvas Instance { get; private set; }

    [SerializeField] private TMP_Text userInputText1;
    [SerializeField] private TMP_Text userInputText2;

    private void Awake()
    {
        Instance = this;
        userInputText1.text = "No input";
        userInputText2.text = "No input";
    }

    public void UpdateIT1(string info)
    {
        userInputText1.text = info;
    }

    public void UpdateIT2(string info)
    {
        userInputText2.text = info;
    }
}
