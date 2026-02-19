using UnityEngine;

public class ParametricTable : MonoBehaviour
{
    [Header("Primary Parameters")]

    [SerializeField]
    [Range(740f, 760f)]
    private float height = 750f;

    [SerializeField]
    private float width = 1200f;

    [SerializeField]
    private float depth = 600f;

    [Header("Secondary")]

    [SerializeField]
    private float tabletopThickness = 30f;

    [SerializeField]
    private float legInset = 50f;

    [SerializeField]
    private bool ergonomicMode = true;

    [Header("Leg Settings")]

    [SerializeField]
    private bool autoLegWidth = true;

    [SerializeField]
    [Range(30f, 120f)]
    private float legWidth = 60f;

    private float legHeight;

    private void OnValidate()
    {
        UpdateTable();
    }

    private void UpdateTable()
    {
        if (ergonomicMode)
        {
            height = Mathf.Clamp(height, 740f, 760f);
        }

        legHeight = height - tabletopThickness;

        if (autoLegWidth)
        {
            legWidth = Mathf.Clamp(width * 0.04f, 40f, 80f);
        }

        Transform top = transform.Find("Top");
        if (top != null)
        {
            top.localScale = new Vector3(width, tabletopThickness, depth);
            top.localPosition = new Vector3(
                0f,
                height - tabletopThickness / 2f,
                0f
            );
        }

        float xPos = width / 2f - legInset - legWidth / 2f;
        float zPos = depth / 2f - legInset - legWidth / 2f;
        float yPos = legHeight / 2f;

        UpdateLeg("Leg_FL", -xPos, yPos, zPos);
        UpdateLeg("Leg_FR", xPos, yPos, zPos);
        UpdateLeg("Leg_BL", -xPos, yPos, -zPos);
        UpdateLeg("Leg_BR", xPos, yPos, -zPos);
    }

    private void UpdateLeg(
        string legName,
        float x,
        float y,
        float z)
    {
        Transform leg = transform.Find(legName);

        if (leg == null)
        {
            return;
        }

        leg.localScale = new Vector3(legWidth, legHeight, legWidth);
        leg.localPosition = new Vector3(x, y, z);
    }
}