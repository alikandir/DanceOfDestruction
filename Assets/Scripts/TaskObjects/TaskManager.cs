using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class TaskManager : MonoBehaviour
{
    [SerializeField] GameObject[] taskObjectList;
    [SerializeField] TextMeshProUGUI taskTextUI;
    [SerializeField] Camera mainCamera;
    [SerializeField] int secondsToTask;
    [SerializeField] int taskTime;
    Transform player;
    Vector3 leftBottomCorner;
    Vector3 rightTopCorner;
    float distance;
    Timer timer;
    Timer taskTimer;
    bool isTaskGiven = false;
    SunOrbiter[] stars;
    GameObject spawned;
    public event Action<GameObject> SpawnedTask;
    public event Action OnTaskFinished;
    public Animator QuestPopAnimator;
    public AudioClip NewTask;
    public AudioClip TaskSuccesful;
    public TextMeshProUGUI activeTaskText;
    float plSize;
    float size;
    public TextMeshProUGUI timeLeftText;

    float timeLeftCount;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer(secondsToTask);
        timer.StartTimer();
        taskTimer = new Timer(taskTime);
        player = GameObject.Find("Player").GetComponent<Transform>();
        distance = player.position.z - mainCamera.transform.position.z;
        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));

        stars = FindObjectsOfType<SunOrbiter>();
        taskTextUI.gameObject.SetActive(false);
        activeTaskText.text = "No Task";
        timeLeftCount=secondsToTask;
        

    }
    
    
    private void Update()
    {
        distance = player.position.z - mainCamera.transform.position.z;
        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));

        if (timer.TimeOut && !isTaskGiven)
        {
            GiveTask();
            
            isTaskGiven = true; // Ensure this is set immediately after giving a task
            QuestPopAnimator.Play("NewTask2");
            QuestPopAnimator.gameObject.GetComponent<AudioSource>().clip = NewTask;
            QuestPopAnimator.gameObject.GetComponent<AudioSource>().Play();



        }
        else if (isTaskGiven && taskTimer.TimeOut)
        {
            TaskFailed();
        }
        ShowCountDown();
    }

    public void ShowCountDown()
    {
        timeLeftCount-=Time.deltaTime;
        if ( isTaskGiven)
        {
            timeLeftText.text = "Time Left\n" + timeLeftCount.ToString("0.0");
        }
        else
            timeLeftText.text = "Time to Next Task\n" + timeLeftCount.ToString("0.0");

    }

        int SupremeTaskSelector()
    {
        if (taskObjectList.Length > 0)
        {
            return UnityEngine.Random.Range(0, taskObjectList.Length);
        }
        else
            return -9;
    }

    /*void Spawn(int Task, float size, Vector3 Location)
    {
        spawned = Instantiate(taskObjectList[Task], Location, Quaternion.identity);
        spawned.GetComponent<Edibles>().size = size;
    }
    */ 
    Vector3 LocationGenerator()
    {
        Vector3 location = Vector3.zero;
        int side = UnityEngine.Random.Range(0, 4); //0 is left 1 is top 2 is right 3 is bottom
        if (side == 0)
        {
            location = new Vector3(leftBottomCorner.x, UnityEngine.Random.Range(leftBottomCorner.y, rightTopCorner.y), player.position.z);
        }
        else if (side == 1)
        {
            location = new Vector3(UnityEngine.Random.Range(leftBottomCorner.x, rightTopCorner.x), rightTopCorner.y, player.position.z);
        }
        else if (side == 2)
        {
            location = new Vector3(rightTopCorner.x, UnityEngine.Random.Range(leftBottomCorner.y, rightTopCorner.y), player.position.z);
        }
        else if (side == 3)
        {
            location = new Vector3(UnityEngine.Random.Range(leftBottomCorner.x, rightTopCorner.x), leftBottomCorner.y, player.position.z);
        }

        return 2 * location - player.transform.position;
    }

    float SizeGenerator()
    {
        plSize = player.transform.gameObject.GetComponent<PlayerSizeControl>().size;
        return plSize * UnityEngine.Random.Range(1.1f, 1.3f);
    }

    void GiveTask()
    {
        timeLeftCount = taskTime;
        size = SizeGenerator();
        //Spawn(SupremeTaskSelector(), size, LocationGenerator());
        spawned = stars[UnityEngine.Random.Range(0, stars.Length)].GetComponent<SunOrbiter>().AddPlanet(taskObjectList[SupremeTaskSelector()]);//added
        spawned.GetComponent<TaskObjectsBase>().targetSize = size;
        spawned.GetComponent<Edibles>().size = size;
        if (taskTextUI != null)
        {
            taskTextUI.text = spawned.GetComponent<TaskObjectsBase>().taskText;
        }

        SpawnedTask?.Invoke(spawned);
        spawned.GetComponent<TaskObjectsBase>().OnTaskComplete += TaskComplete;
        isTaskGiven = true;
        StartCoroutine(RevealText()) ;
        taskTimer.StartTimer();
        spawned.transform.GetChild(0).gameObject.SetActive(true);
        activeTaskText.text = "Active Task";


    }

    public void TaskComplete()
    {
        timeLeftCount = secondsToTask;
        isTaskGiven = false;
        timer.StartTimer();
        OnTaskFinished.Invoke();
        QuestPopAnimator.Play("TaskClosed");
        taskTextUI.gameObject.SetActive(false);
        QuestPopAnimator.gameObject.GetComponent<AudioSource>().clip = TaskSuccesful;
        QuestPopAnimator.gameObject.GetComponent<AudioSource>().Play();
        activeTaskText.text = "No Task";


    }

    public void TaskFailed()
    {
        player.GetComponent<PlayerSizeControl>().GameOverEmit();
    }
     IEnumerator ShowTaskText()
    {
        yield return new WaitForSeconds(1);
        taskTextUI.gameObject.SetActive(true);
        
    }
    IEnumerator RevealText()
    {
        taskTextUI.gameObject.SetActive(true);
        taskTextUI.text = "";
        string fullText = spawned.GetComponent<TaskObjectsBase>().taskText + "\nTarget Size:"+size.ToString("0.0");
        foreach (char c in fullText)
        {
            taskTextUI.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
