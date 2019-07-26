using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]           //Для работы скрипта необходим компонент RigidBody
[RequireComponent(typeof(AudioSource))]         //Для работы скрипта необходим компонент AudioSource

public class MovePlayer : MonoBehaviour {
    public GameObject countOfSheetsText;        //Игровой объект - полоса прогресса
    public GameObject[] sheets;                 //Массив, который содержит информацию о записках
    public GameObject ui_Pause;
    public GameObject countOfTimeText;
    public GameObject endGameText;

    public float walkSpeed = 7.0f;             //Скорость ходьбы

    [SerializeField]
    public float sensitivity_x;                 //Чувствительность мыши по оси OX

    public AudioClip[] Sounds;                  //Звуки ходьбы

    private Rigidbody rb;                       //Компонент RigidBody
                                                          
    private AudioSource audio;                  //Звук для ходьбы
    public AudioSource takingSheetSound;        //Звук подбора запискиы
    public AudioSource gameOverSound;           //Звук конца игры
    public AudioSource winSound;           //Звук конца игры

    private bool walk = false;                  //true - если обычный шаг, false - если бег
    private float savedWalkSpeed;               //Сохраненная скорость ходьбы
    private bool paused = false;                //true - меню паузы, false - игра
    private Coroutine coroutine;                //Куротина звуков ходьбы
    private bool isGameOver = false;
    

    void Start () {
        rb = GetComponent <Rigidbody> ();
        audio = GetComponent<AudioSource>();
        savedWalkSpeed = walkSpeed;
        sensitivity_x = StaticValues.sens_x;
        RandomSpawnSheets();
    }
	
	void FixedUpdate () {
        sensitivity_x = StaticValues.sens_x;
        Walking();
        MouseChecking();
        Running();
        CountingDown();
    }

    void Update()
    {
        Jump();
        Win();
        TakingSheet();
        Exit();
    }

    IEnumerator WaitCor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        audio.PlayOneShot(Sounds[Random.Range(0, 3)]);
        coroutine = null;
    }

    private void RandomSpawnSheets()        //Генерация на игровом поле записок в случайных местах
    {
        int[] ran;

        switch (StaticValues.difficulty) {
            case 0:
                ran = Algorithms.UnicRandom(0, sheets.Length, 10);

                for (int i = 0; i < 10; i++)
                {
                    sheets[ran[i]].SetActive(true);
                }

                countOfSheetsText.GetComponent<Text>().text = "0/2";

                break;

            case 1:
                ran = Algorithms.UnicRandom(0, sheets.Length, 8);

                for (int i = 0; i < 8; i++)
                {
                    sheets[ran[i]].SetActive(true);
                }

                countOfSheetsText.GetComponent<Text>().text = "0/4";

                break;

            case 2:
                ran = Algorithms.UnicRandom(0, sheets.Length, 8);

                for (int i = 0; i < 8; i++)
                {
                    sheets[ran[i]].SetActive(true);
                }

                countOfSheetsText.GetComponent<Text>().text = "0/6";

                break;

            case 3:
                ran = Algorithms.UnicRandom(0, sheets.Length, 8);

                for (int i = 0; i < 8; i++)
                {
                    sheets[ran[i]].SetActive(true);
                }

                countOfSheetsText.GetComponent<Text>().text = "0/8";

                break;
        }
    }


    private void MouseChecking()        //Поворот камеры по Y
    {
        float yRot = Input.GetAxis("Mouse X");
        Vector3 Rotation = new Vector3(0f, yRot, 0f) * sensitivity_x;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(Rotation));
    }

    private void Walking()              //Метод ходьбы
    {
        float xPos = Input.GetAxisRaw("Horizontal");
        float yPos = Input.GetAxisRaw("Vertical");

        Vector3 movHor = transform.right * xPos;
        Vector3 movVer = transform.forward * yPos;

        Vector3 velocity = (movVer + movHor).normalized * walkSpeed;

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            walk = true;
        }
        else walk = false;

        if (CollisionEnter.col && walk && coroutine == null){
            if (walkSpeed == 7.0f)
                coroutine = StartCoroutine(WaitCor(.5f));
            else coroutine = StartCoroutine(WaitCor(.3f));
        }else if (coroutine == null)
        {
            audio.Stop();
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void CountingDown()         //Обратный отсчет времени
    {
        if (isGameOver) return;

        if (StaticValues.time <= 0)
        {
            endGameText.SetActive(true);
            gameOverSound.Play();
            isGameOver = true;
            HideAllSheets();

            return;
        }
        string t = Algorithms.FormatTime((int)StaticValues.time);

        countOfTimeText.GetComponent<Text>().text = t;
        StaticValues.time -= Time.fixedDeltaTime;
    }

    private void TakingSheet()          //Метод подбора записки
    {
        float distance;

        var centerPoint = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(centerPoint);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        string t = "";

        if (Input.GetButtonDown("Sheet take"))
        {
            for (int i = 0; i < 18; i++)
            {
                distance = Vector3.Distance(rb.transform.position, sheets[i].transform.position);

                if (sheets[i].active == true && hit.collider.tag == "Sheet" && distance < 6f)
                {
                    sheets[i].SetActive(false);
                    StaticValues.countOfSheets++;
                    
                    switch (StaticValues.difficulty)
                    {
                        case 0:
                            t = StaticValues.countOfSheets.ToString() + "/2";
                            break;

                        case 1:
                            t = StaticValues.countOfSheets.ToString() + "/4";
                            break;

                        case 2:
                            t = StaticValues.countOfSheets.ToString() + "/6";
                            break;

                        case 3:
                            t = StaticValues.countOfSheets.ToString() + "/8";
                            break;
                    }

                    takingSheetSound.Play();
                    countOfSheetsText.GetComponent<Text>().text = t;
                }
            }
        }
    }

    private void Running()          //Метод бега
    {
        if (Input.GetKey(KeyCode.LeftShift)) walkSpeed = StaticValues.runningSpeed;
        else walkSpeed = savedWalkSpeed;
    }

    private void Jump()             //Метод прыжка
    {
        bool checkJump = false;
        checkJump = Input.GetButtonDown("Jump");

        Vector3 vectorJump = new Vector3(0f, StaticValues.jumpHeight, 0f);

        if (checkJump && CollisionEnter.col)
        {
            rb.AddForce(vectorJump, ForceMode.Impulse);
        }
    }

    private void Win()          //Победа
    {
        if (!isGameOver)
        {
            switch (StaticValues.difficulty)
            {
                case 0:
                    if (StaticValues.countOfSheets == 2)
                    {
                        isGameOver = true;
                        endGameText.GetComponent<Text>().text = "ПОБЕДА!!!";
                        endGameText.SetActive(true);
                        HideAllSheets();
                        winSound.Play();
                    }

                    break;

                case 1:
                    if (StaticValues.countOfSheets == 4)
                    {
                        isGameOver = true;
                        endGameText.GetComponent<Text>().text = "ПОБЕДА!!!";
                        endGameText.SetActive(true);
                        HideAllSheets();
                        winSound.Play();
                    }

                    break;

                case 2:
                    if (StaticValues.countOfSheets == 6)
                    {
                        isGameOver = true;
                        endGameText.GetComponent<Text>().text = "ПОБЕДА!!!";
                        endGameText.SetActive(true);
                        HideAllSheets();
                        winSound.Play();
                    }

                    break;

                case 3:
                    if (StaticValues.countOfSheets == 8)
                    {
                        isGameOver = true;
                        endGameText.GetComponent<Text>().text = "ПОБЕДА!!!";
                        endGameText.SetActive(true);
                        HideAllSheets();
                        winSound.Play();
                    }

                    break;
            }
        }
    }

    private void HideAllSheets()            //Спрятать все записки
    {
        for (int i = 0; i < sheets.Length; i++)
        {
            sheets[i].SetActive(false);
        }
    }

    private void Exit()             //Выход из игры
    {
        if (Input.GetButtonDown("Cancel"))
        {
            StaticValues.notPaused = 0;
            Time.timeScale = StaticValues.notPaused;
            ui_Pause.SetActive(true);
            Cursor.visible = true;
        }
    }
}
