using UnityEngine;
using UnityEngine.UI;

public class FeverGaugeUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private FeverManager feverManager;

    private void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = feverManager.maxGauge;
    }

    private void Update()
    {
        slider.value = feverManager.currentGauge;
    }
}