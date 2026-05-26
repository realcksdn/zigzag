using System;
using UnityEngine;

public class FeverManager : MonoBehaviour
{
    [Header("Fever Settings")]
    public float maxGauge = 100f;
    public float feverDrainRate = 20f; // 초당 줄어드는 양
    public float currentGauge = 0f;
    public bool isFever = false;

    public float scoreMultiplier => isFever ? 2f : 1f;

    public event Action OnFeverStart;
    public event Action OnFeverEnd;

    void Update()
    {
        if (isFever)
        {
            currentGauge -= feverDrainRate * Time.deltaTime;

            if (currentGauge <= 0f)
            {
                EndFever();
            }
        }
    }

    public void AddGauge(float amount)
    {
        if (isFever) return;

        currentGauge += amount;
        currentGauge = Mathf.Clamp(currentGauge, 0f, maxGauge);

        if (currentGauge >= maxGauge)
        {
            StartFever();
        }
    }

    private void StartFever()
    {
        isFever = true;
        OnFeverStart?.Invoke();
    }

    private void EndFever()
    {
        isFever = false;
        currentGauge = 0f;
        OnFeverEnd?.Invoke();
    }
}