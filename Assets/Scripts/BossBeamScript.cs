using System.Security.Cryptography;
using UnityEditor.Build;
using UnityEngine;

public class BossBeamScript : MonoBehaviour
{
    public float Timer;
   public Material material;
    public bool playerPresent = false;
    private void Awake()
    {
        material = GetComponent<Material>();
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 6f)
        {
            Destroy(gameObject);
        }
        if (Timer > 4f && playerPresent) {
            material.color = Color.blue;
            Debug.Log("GO BLUE YOU FUCKER");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Player")
        {
            playerPresent = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPresent = false;
        }
    }
    

}
