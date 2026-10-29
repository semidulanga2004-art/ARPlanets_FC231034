using UnityEngine;
public class TouchGestures : MonoBehaviour
{
public float minScale = 0.3f; // smallest allowed size
public float maxScale = 3f; // biggest allowed size
void Update()
{
// gestures need exactly two fingers
if (Input.touchCount != 2) return;
Touch t0 = Input.GetTouch(0);
Touch t1 = Input.GetTouch(1);
// where were the fingers in the previous frame?
Vector2 prev0 = t0.position - t0.deltaPosition;
Vector2 prev1 = t1.position - t1.deltaPosition;
// 1. PINCH = change of distance between the fingers -&gt; scale
float prevDistance = Vector2.Distance(prev0, prev1);
float currentDistance = Vector2.Distance(t0.position, t1.position);
if (prevDistance > 1f)
{
float factor = currentDistance / prevDistance;
float newScale = Mathf.Clamp(transform.localScale.x * factor, minScale, maxScale);
transform.localScale = Vector3.one * newScale;
}
// 2. TWIST = change of angle between the fingers -&gt; rotate
float prevAngle = Mathf.Atan2(prev1.y - prev0.y, prev1.x - prev0.x) * Mathf.Rad2Deg;
float currentAngle = Mathf.Atan2(t1.position.y - t0.position.y, t1.position.x -
t0.position.x) * Mathf.Rad2Deg;
float deltaAngle = Mathf.DeltaAngle(prevAngle, currentAngle);
transform.Rotate(0f, -deltaAngle, 0f, Space.World);
}
}