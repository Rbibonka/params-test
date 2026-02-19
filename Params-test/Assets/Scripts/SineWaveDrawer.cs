using UnityEngine;

[ExecuteAlways]
public class SineWaveDrawer : MonoBehaviour
{
    [Header("Sine Wave Settings")]
    [SerializeField]
    private float amplitude = 1f;

    [SerializeField]
    private float wavelength = 2f;

    [SerializeField]
    private int points = 100;

    [SerializeField]
    private float length = 10f;

    [Header("Moving Point")]
    [SerializeField]
    [Range(0f, 1f)]
    private float normalizedPosition = 0f;

    [SerializeField]
    private Transform movingPoint;

    private LineRenderer lineRenderer;

    public float Amplitude => amplitude;
    public float Wavelength => wavelength;
    public float Length => length;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = points;
        lineRenderer.useWorldSpace = true;
        lineRenderer.widthMultiplier = 0.1f;
    }

    public void SetPointPosition(float normalizedPosition)
    {
        this.normalizedPosition = normalizedPosition;
    }

    public void SetLength(float length)
    {
        this.length = length;
    }

    public void SetAmplitude(float amplitude)
    {
        this.amplitude = amplitude;
    }

    public void SetWavelength(float wavelength)
    {
        this.wavelength = wavelength;
    }

    private void Update()
    {
        if (lineRenderer.positionCount != points)
        {
            lineRenderer.positionCount = points;
        }

        lineRenderer.positionCount = points;

        DrawSineWave();
        MovePointAlongWave();
    }

    private void DrawSineWave()
    {
        for (int i = 0; i < points; i++)
        {
            float t = (float)i / (points - 1);
            float x = t * length;
            float y = Mathf.Sin(x * Mathf.PI * 2 / wavelength) * amplitude;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }

    private void MovePointAlongWave()
    {
        if (movingPoint == null)
        {
            return;
        }

        float xPos = normalizedPosition * length;
        float yPos = Mathf.Sin(xPos * Mathf.PI * 2 / wavelength) * amplitude;
        movingPoint.position = new Vector3(xPos, yPos, 0f);
    }
}