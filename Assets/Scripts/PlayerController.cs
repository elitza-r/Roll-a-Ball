using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text scoreText;
    public string MiniGame1;
    public GameObject Player;

    private Rigidbody rb;
    private int count;
    private int score;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winText.text = "";
        score = 0;
        SetScoreText ();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float colorR = Mathf.Abs(Player.transform.position.x / 10);
        float colorG = Mathf.Abs(Player.transform.position.y / 10);
        float colorB = Mathf.Abs(Player.transform.position.z / 10);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Color colornow = new Vector4(colorR, colorG, colorB, 0.0f);

        Player.GetComponent<Renderer>().material.color = colornow;
        print(Player.transform.position);

        rb.AddForce(movement * speed);

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            SetCountText();
            SetScoreText();
            if (count == 12)
            {
                Player.transform.position = new Vector3(22, 0);
            }
                
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            score = score - 1;
            SetScoreText();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWall"))
        {
            score = score - 1;
            SetScoreText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (count >= 24)
        {
            winText.text = "You finished with a score of: " + score.ToString();
        }
    }
}