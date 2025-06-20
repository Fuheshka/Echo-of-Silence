using UnityEngine;
using UnityEngine.UI;

public class StressUIUpdater : MonoBehaviour
{
    public StressSystem stressSystem;
    public Slider stressSlider;

    private void OnEnable()
    {
        if (stressSystem != null)
        {
            stressSystem.OnStressChanged += UpdateStressUI;
        }
    }

    private void OnDisable()
    {
        if (stressSystem != null)
        {
            stressSystem.OnStressChanged -= UpdateStressUI;
        }
    }

    private void UpdateStressUI(float normalizedStress)
    {
        if (stressSlider != null)
        {
            stressSlider.value = normalizedStress;
        }
    }
}
