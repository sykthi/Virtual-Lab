using UnityEngine;

public class Jockey : MonoBehaviour
{
    [SerializeField] private GameObject _gmNeedle; // Galvanometer needle object
    [SerializeField] private ResistorController _resistorController;

    private float minJockeyX = -0.00619f; // Jockey's leftmost position
    private float maxJockeyX = 0.0060f;   // Jockey's rightmost position

    private Quaternion minRotation = new Quaternion(-0.887016952f, 0.122483291f, 0.241484642f, -0.374010682f);
    private Quaternion maxRotation = new Quaternion(-0.881631017f, -0.0922427922f, -0.260462224f, -0.382592976f);

    [SerializeField] private float needleSmoothSpeed = 5f; // Adjust for smoothness

    void Update()
    {
        float jockeyX = transform.position.x;

        // Normalize the jockey's position between 0 and 1
        float t = Mathf.InverseLerp(minJockeyX, maxJockeyX, jockeyX);

        // Compute the target rotation for the needle
        Quaternion targetRotation = Quaternion.Lerp(minRotation, maxRotation, t);

        // Smoothly interpolate towards the target rotation using Slerp
        _gmNeedle.transform.localRotation = Quaternion.Slerp(
            _gmNeedle.transform.localRotation,
            targetRotation,
            needleSmoothSpeed * Time.deltaTime
        );
    }
}
