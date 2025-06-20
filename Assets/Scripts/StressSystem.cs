using System;
using UnityEngine;

public class StressSystem : MonoBehaviour
{
    [Header("Stress Settings")]
    public float maxStress = 100f;
    public float stressDecreaseRate = 1f; // Reduced stress decrease rate for slower decrease
    public float stressIncreaseRate = 10f; // Base stress increase per noise unit
    public float breathHoldStressMultiplier = 0.3f; // Multiplier to slow stress increase when holding breath

    [Header("Stress Feedback")]
    public Action<float> OnStressChanged; // Pass current stress normalized 0-1
    public Action OnStressMaxed;

    private float currentStress = 0f;
    private bool isHoldingBreath = false;
    private bool stressMaxedTriggered = false;

    void Update()
    {
        // Decrease stress over time if not maxed and not increasing
        if (currentStress > 0f && !stressMaxedTriggered)
        {
            currentStress -= stressDecreaseRate * Time.deltaTime;
            currentStress = Mathf.Clamp(currentStress, 0f, maxStress);
            OnStressChanged?.Invoke(currentStress / maxStress);
        }
        else if (stressMaxedTriggered)
        {
            // Allow stress to decrease again after maxed
            if (currentStress > 0f)
            {
                currentStress -= stressDecreaseRate * Time.deltaTime;
                currentStress = Mathf.Clamp(currentStress, 0f, maxStress);
                OnStressChanged?.Invoke(currentStress / maxStress);

                if (currentStress < maxStress)
                {
                    stressMaxedTriggered = false;
                }
            }
        }
    }

    public void IncreaseStress(float noiseLevel)
    {
        if (stressMaxedTriggered)
            return;

        float multiplier = isHoldingBreath ? breathHoldStressMultiplier : 1f;
        currentStress += noiseLevel * stressIncreaseRate * multiplier;
        currentStress = Mathf.Clamp(currentStress, 0f, maxStress);
        OnStressChanged?.Invoke(currentStress / maxStress);

        if (currentStress >= maxStress && !stressMaxedTriggered)
        {
            stressMaxedTriggered = true;
            OnStressMaxed?.Invoke();
        }
    }

    public void SetHoldingBreath(bool holding)
    {
        isHoldingBreath = holding;
    }

    public float GetCurrentStress()
    {
        return currentStress;
    }

    public bool IsStressMaxed()
    {
        return stressMaxedTriggered;
    }
}
