using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    public float rot_speed_x = 0f;
    public float rot_speed_y = 0f;
    public float rot_speed_z = 0f;
    public bool local = false;

    // Update is called once per frame
    void Update()
    {
        if (local)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rot_speed_x);
        }
        else
        {
            transform.Rotate(new Vector3(rot_speed_x, rot_speed_y, rot_speed_z) * Time.deltaTime, Space.World);
        }
    }
}
