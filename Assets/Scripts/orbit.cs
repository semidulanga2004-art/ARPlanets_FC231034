using UnityEngine;
public class Orbit : MonoBehaviour
{
public Transform center; // what we go around (the Sun)
public float orbitSpeed = 20f; // degrees per second around the center
public float spinSpeed = 50f; // degrees per second around itself
void Update()
{
// 1. travel around the center (like Earth around the Sun)
if (center != null)
{
transform.RotateAround(center.position, center.up, orbitSpeed * Time.deltaTime);
}
// 2. spin around our own axis (like day and night)
transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
}
}