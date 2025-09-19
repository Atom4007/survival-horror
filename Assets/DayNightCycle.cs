using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // ������
    public Gradient lightColor; // ���� ����� � ����������� �� ������� �����
    public Gradient ambientColor; // ���� ����������� �����
    public float fullDayDuration = 120f; // ����������������� ����� � ��������
    [Range(0, 1)] public float currentTime = 0f; // ������� ����� ����� (0-1)

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
