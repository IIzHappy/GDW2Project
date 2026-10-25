using UnityEngine;

public class BossBeamScript : MonoBehaviour
{
    public float Timer;
    public Material material;
    public bool playerPresent = false;
    GameObject player;
    public int damage;
    public bool hasAttacked = false;
    public Sprite beamSprite;
    public AudioSource AudioSource;

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        if (enabled == false)
        {
            enabled = true;
        }
    }

  
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 1.5f)
        {
            GetComponentInParent<BossOneController>().moveSpeed = 23f;
            AudioSource.Stop();
            Destroy(gameObject);

        }
        if (Timer > 1f && playerPresent && hasAttacked == false) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
           // gameObject.GetComponent<SpriteRenderer>().sprite = beamSprite;
            transform.GetChild(0).gameObject.SetActive(true);
            AudioSource.Play();
           // gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            if (player.gameObject.tag == "Player")
            {
                player.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                Debug.Log("beam damage");
                hasAttacked = true;
            }
            Debug.Log("GO BLUE YOU FUCKER");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Player")
        {
            playerPresent = true;
            player = collision.gameObject;
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
