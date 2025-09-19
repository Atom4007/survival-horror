using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // Солнце
    public Gradient lightColor; // Цвет света в зависимости от времени суток
    public Gradient ambientColor; // Цвет амбиентного света
    public float fullDayDuration = 120f; // Продолжительность цикла в секундах
    [Range(0, 1)] public float currentTime = 0f; // Текущее время суток (0-1)

    void Update()
    {
        currentTime += Time.deltaTime / fullDayDuration;
        if (currentTime > 1f) currentTime = 0f;

        float sunAngle = (currentTime * 360f) - 90f;
        directionalLight.transform.localRotation = Quaternion.Euler(sunAngle, 170f, 0f);
        directionalLight.color = lightColor.Evaluate(currentTime);
        RenderSettings.ambientLight = ambientColor.Evaluate(currentTime);
    }
}
