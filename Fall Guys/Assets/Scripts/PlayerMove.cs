using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    Vector3 direction;

    float timer;
    [SerializeField] Text timeText;
    bool isFinish;

    [SerializeField] float jumpSpeed;
    bool isGrounded;
    Animator anim;

    Vector3 startPosition;
    bool slide;
    int record;

    [SerializeField] Text recordText;
    [SerializeField] List<Text> scoreUI;
    [SerializeField] List<int> scorePoints;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        for (var i = 0; i < scoreUI.Count; i++)
        {
            scoreUI[i].text = scorePoints[i].ToString() + " очков " + scorePoints[i + 3].ToString() + " сек";
        }
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        record = PlayerPrefs.GetInt("record", 0);
        recordText.text = "Record: " + record.ToString();
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = startPosition;
        }

        if (!isFinish)
        {
            timer += Time.deltaTime;
            timeText.text = Mathf.Round(timer).ToString();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = transform.TransformDirection(x, 0, z);

        if (direction.magnitude > 0)
        {
            anim.SetBool("Run", true);
        }

        else anim.SetBool("Run", false);

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
                //GetComponent<AudioSource>().Play();
            }
        }

        if (slide)
        {
            rb.AddForce(direction * 1, ForceMode.VelocityChange);
        }

    }

    private void Record(int count)
    {
        record += count;
        PlayerPrefs.SetInt("record", record);
        recordText.text = record.ToString();
    }

    private void Win()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel + 1);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other != null)
        {
            isGrounded = true;
            anim.SetBool("Jump", false);
        }
        if (other.gameObject.CompareTag("slide"))
        {
            slide = true;
        }
        else slide = false;
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
        anim.SetBool("Jump", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("plate"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            isFinish = true;

            if (timer > scorePoints[4] && timer <= scorePoints[5])
            {
                Record(scorePoints[2]);
            }
            else if (timer > scorePoints[3] && timer < scorePoints[4])
            {
                Record(scorePoints[1]);
            }
            else if (timer <= scorePoints[3])
            {
                Record(scorePoints[0]);
            }
            Destroy(other.gameObject);
            Invoke("Win", 5);
        }

        if (other.CompareTag("CheckPoint"))
        {
            startPosition = transform.position;
        }
    }
}