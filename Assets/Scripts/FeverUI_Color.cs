using UnityEngine;
using UnityEngine.UI;

public class FeverGaugeUI_Color : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private FeverManager feverManager;
    [SerializeField] private Image fillImage;

    [Header("Colors (Normal Mode)")]
    public Color emptyColor = Color.red;
    public Color midColor = Color.yellow;
    public Color fullColor = Color.green;

    private float rainbowHue = 0f;

    private void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = feverManager.maxGauge;
    }

    private void Update()
    {
        slider.value = feverManager.currentGauge;

        if (feverManager.isFever)
        {
            //  무지개 색 순환
            rainbowHue += Time.deltaTime * 2f; // 속도 조절
            if (rainbowHue > 1f) rainbowHue -= 1f;

            Color rainbowColor = Color.HSVToRGB(rainbowHue, 1f, 1f);
            fillImage.color = rainbowColor;
        }
        else
        {
            // 일반 색상 전환
            float t = feverManager.currentGauge / feverManager.maxGauge;
            if (t < 0.5f)
            {
                float subT = t / 0.5f;
                fillImage.color = Color.Lerp(emptyColor, midColor, subT);
            }
            else
            {
                float subT = (t - 0.5f) / 0.5f;
                fillImage.color = Color.Lerp(midColor, fullColor, subT);
            }
        }
    }
}