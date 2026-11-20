using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int points = 0;
    [SerializeField] private TextMeshProUGUI PointsLabel;

    private void Update()
    {
        PointsLabel.text = "Points: " + points;
    }
}
