using UnityEngine;

public class ParametricChair : MonoBehaviour
{
    [Header("Primary Parameters")]

    [SerializeField]
    [Range(400f, 450f)]
    private float seatHeight = 430f;

    [SerializeField]
    private float seatWidth = 450f;

    [SerializeField]
    private float seatDepth = 420f;

    [SerializeField]
    private float seatThickness = 30f;

    [Header("Backrest")]

    [SerializeField]
    private float backrestHeight = 400f;

    [SerializeField]
    [Range(90f, 110f)]
    private float backrestAngle = 100f;

    [Header("Leg Settings")]

    [SerializeField]
    private bool autoLegWidth = true;

    [SerializeField]
    [Range(30f, 80f)]
    private float legWidth = 40f;

    [SerializeField]
    private bool autoLegLength = true;

    [SerializeField]
    private float legLengthOffset = 400f;

    [SerializeField]
    private float legInset = 40f;

    private float legHeight;

    private void OnValidate()
    {
        UpdateChair();
    }

    private void UpdateChair()
    {
        seatHeight = Mathf.Clamp(seatHeight, 400f, 450f);

        legHeight = seatHeight - seatThickness;

        if (autoLegWidth)
        {
            legWidth = Mathf.Clamp(seatWidth * 0.08f, 30f, 60f);
        }

        Transform seat = transform.Find("Seat");
        if (seat != null)
        {
            seat.localScale = new Vector3(seatWidth, seatThickness, seatDepth);
            seat.localPosition = new Vector3(
                0f,
                seatHeight - seatThickness / 2f,
                0f
            );
        }

        Transform backrest = transform.Find("Backrest");
        if (backrest != null)
        {
            backrest.localScale = new Vector3(
                seatWidth,
                backrestHeight,
                legWidth
            );

            float backY = seatHeight + backrestHeight / 2f;
            float backZ = -seatDepth / 2f + legWidth / 2f;

            backrest.localPosition = new Vector3(0f, backY, backZ);
            backrest.localRotation = Quaternion.Euler(backrestAngle - 90f, 0f, 0f);
        }

        float xPos = seatWidth / 2f - legInset - legWidth / 2f;
        float zPos = seatDepth / 2f - legInset - legWidth / 2f;
        float yPos = legHeight / 2f;

        float rearLegHeight;
        if (autoLegLength)
        {
            rearLegHeight = legHeight + legLengthOffset;
        }
        else
        {
            rearLegHeight = legHeight;
        }

        UpdateLeg("Leg_FL", -xPos, yPos, zPos, legHeight);
        UpdateLeg("Leg_FR", xPos, yPos, zPos, legHeight);

        UpdateLeg("Leg_BL", -xPos, rearLegHeight / 2f, -zPos, rearLegHeight);
        UpdateLeg("Leg_BR", xPos, rearLegHeight / 2f, -zPos, rearLegHeight);
    }

    private void UpdateLeg(
        string legName,
        float x,
        float y,
        float z,
        float height)
    {
        Transform leg = transform.Find(legName);

        if (leg == null)
        {
            return;
        }

        leg.localScale = new Vector3(legWidth, height, legWidth);
        leg.localPosition = new Vector3(x, y, z);
    }
}