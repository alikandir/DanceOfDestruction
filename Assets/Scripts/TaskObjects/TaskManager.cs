using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
            Debug.Log("Task Given: " + isTaskGiven);
        }
        else if (isTaskGiven && taskTimer.TimeOut)
        {
            TaskFailed();
        }
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
        float plSize = player.transform.gameObject.GetComponent<PlayerSizeControl>().size;
        return plSize * UnityEngine.Random.Range(1.5f, 2f);
    }

    void GiveTask()
    {
        float size = SizeGenerator();
        //Spawn(SupremeTaskSelector(), size, LocationGenerator());
        spawned = stars[UnityEngine.Random.Range(0, stars.Length)].GetComponent<SunOrbiter>().AddPlanet(taskObjectList[SupremeTaskSelector()]);//added
        spawned.GetComponent<TaskObjectsBase>().targetSize = size;
        spawned.GetComponent<Edibles>().size = size;
        //taskTextUI.text = spawned.GetComponent<TaskObjectsBase>().taskText;
        SpawnedTask?.Invoke(spawned);
        isTaskGiven = true;
        taskTimer.StartTimer();
        
    }

    public void TaskComplete()
    {
        isTaskGiven = false;
        timer.StartTimer();
        OnTaskFinished.Invoke();
        Debug.Log("Task Completed");
    }

    public void TaskFailed()
    {
        Debug.Log("GAME OVER");
        timer.StartTimer();
    }
}
