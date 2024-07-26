using UnityEngine;

public class DummyFortuneWheelRotationHandler : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private RectTransform spinWheel;

    private void FixedUpdate()
    {
        spinWheel.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
    }
}
