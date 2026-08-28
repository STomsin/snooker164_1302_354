using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    [SerializeField]
    private GameObject[] ballPositions;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject cubeBall;

    [SerializeField]
    private float xInput = 0f;

    [SerializeField]
    private GameObject ballline;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private TMP_Text notiText;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBall(BallColor.Red, 1);
        SetBall(BallColor.Yellow, 2);
        SetBall(BallColor.Green, 3);
        SetBall(BallColor.Brown, 4);
        SetBall(BallColor.Blue, 5);
        SetBall(BallColor.Pink, 6);
        SetBall(BallColor.Black, 7);
    }

    // Update is called once per frame
    void Update()
    {


        RotateBall();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShootBall();
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            xInput = 0.1f;
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            xInput = -0.1f;
        }
        else
        {
            xInput = 0f;
        }

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            StopBall();
        }
        if (Keyboard.current.leftShiftKey.isPressed & Keyboard.current.sKey.wasPressedThisFrame)
            SaveGame();

    }

    private void SetBall(BallColor col, int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                    ballPositions[i].transform.position,
                    Quaternion.identity);
        Balls b = obj.GetComponent<Balls>();
        b.SetColorAndPoint(col);
    }

    private void ShootBall()
    {
        Rigidbody rb = cubeBall.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
        ballline.SetActive(false);
        cam.transform.parent = null;
        cam.transform.position = new Vector3(0, 50, 0);
        cam.transform.eulerAngles = new Vector3(90, 90, 0);
    }

    private void RotateBall()
    {
        if (cubeBall != null)
        {
            if (cubeBall != null)
                cubeBall.transform.Rotate(0f, xInput * 5, 0f);
        }
    }

    private void StopBall()
    {
        Rigidbody rb = cubeBall.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        cubeBall.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        ballline.SetActive(true);
        CameraBehindCubeBall();
    }

    private void CameraBehindCubeBall()
    {
        cam.transform.parent = cubeBall.transform;
        cam.transform.position = cubeBall.transform.position + new Vector3(0f, 7f, -15f);
        cam.transform.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    public void ShowNotiText(int n)
    {
        playerScore += n;
        notiText.text = $"This ball:{n}\nTotal Score is {playerScore}";
    }

    public void ShowString()
    {
        notiText.text = "You lose";
    }

    public void SaveGame()
    {
        StopBall();

        if (cubeBall != null)
        {
            PlayerPrefs.SetFloat("cueBallPosX", cubeBall.transform.position.x);
            PlayerPrefs.SetFloat("cueBallPosY", cubeBall.transform.position.x);
            PlayerPrefs.SetFloat("cueBallPosZ", cubeBall.transform.position.x);
        }
    }

    public void LoadGame()
    {
        StopBall();

        if (cubeBall != null)
        {
            float x = PlayerPrefs.GetFloat("cueBallPosX");
            float y = PlayerPrefs.GetFloat("cueBallPosY");
            float z = PlayerPrefs.GetFloat("cueBallPosZ");

            cubeBall.transform.position = new Vector3(x, y, z);
            
            Debug.Log("Loaded");

        }
    }
}