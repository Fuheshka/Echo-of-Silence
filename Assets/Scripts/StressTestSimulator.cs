using UnityEngine;

public class StressTestSimulator : MonoBehaviour
{
    public StressSystem stressSystem;
    public float noiseIncreaseAmount = 0.1f;
    public float noiseIncreaseInterval = 1f;

    private float timer = 0f;

    void Update()
    {
        if (stressSystem == null)
            return;

        timer += Time.deltaTime;

        // Simulate noise increase every interval when pressing N key
        if (Input.GetKey(KeyCode.N) && timer >= noiseIncreaseInterval)
        {
            stressSystem.IncreaseStress(noiseIncreaseAmount);
            timer = 0f;
        }

        // Holding breath is handled in FirstPersonController
    }
}
