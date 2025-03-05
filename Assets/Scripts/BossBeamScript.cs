using UnityEngine;

public class BossBeamScript : MonoBehaviour
{
    private float Timer;
    Material material;

    private void OnCollisionStay2D(Collision2D collision)
    {
        Timer += Time.deltaTime;
        if (Timer > 2f)
        {
            material.color = Color.blue;
        }
    }
}
