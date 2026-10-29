using UnityEngine;
public class Billboard : MonoBehaviour
{
// keep this object (a text label) always facing the camera
void LateUpdate()
{
if (Camera.main == null) return;
transform.rotation = Quaternion.LookRotation(transform.position -
Camera.main.transform.position);
}
}