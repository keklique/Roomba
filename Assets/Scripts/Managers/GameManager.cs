using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField]private GameObject pathofRobotPrefab;

    [SerializeField]private GameObject robot;
    [SerializeField]private GameObject workingParticlesofRobot;
    [SerializeField]private ParticleSystem workingParticleEffectofRobot;
    [SerializeField]private float speedofRobot = 1f;
    private float initialSpeedofRobot;
    private int pathPointIndex = 0;
    [SerializeField]private GameObject currentLine;
    [SerializeField]private LineRenderer lineRenderer;
    [SerializeField]private List<Vector3> mousePositions;
    private Plane basePlane;
    private Vector3 hit;
    private Ray ray;
    [SerializeField]private int batteryCapacity;
    bool isDrawable = true;
    bool isDrawing = false;
    bool isCleaningFinished = false;
    private Vector3 target;
    private GameObject UIManager;
    private GameObject soundManager;
    private GameObject levelManager;
    [SerializeField]private float progressionPercent;

    private Controller controller;
    public bool isTouchStarted = false;
    public bool isTouchEnded = true;


    protected override void Awake(){
        base.Awake();
        controller = new Controller();
        controller.Mouse.TouchPressed.started += ctx => TouchStarted(ctx);
        controller.Mouse.TouchPressed.canceled += ctx => TouchEnded(ctx);
    }
    void Start(){
        GetManagers();
        //workingParticleEffectofRobot = workingParticlesofRobot.GetComponent<ParticleSystem>();
        initialSpeedofRobot = speedofRobot;
    }
    void Update(){
        if(levelManager.GetComponent<LevelManager>().currentLevelIndex!=0){
        DrawPath();
        //DrawingFinishCheck();
        CleaningCheck();
        }
    }
    void GetManagers(){
        UIManager= GameObject.FindWithTag("UIManager");
        UIManager.SendMessage("SetInıtialBatteryCapacity", batteryCapacity);
        soundManager= GameObject.FindWithTag("SoundManager");
        levelManager= GameObject.FindWithTag("LevelManager");
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }

    void TouchStarted(InputAction.CallbackContext context)
    {
        isTouchStarted = true;
        isTouchEnded = false;
    }
    void TouchEnded(InputAction.CallbackContext context)
    {
        isTouchStarted = false;
        isTouchEnded = true;
        if(isDrawing)
        {
            isDrawable = false;
            isDrawing = false;
        }
    }
    void CreatePath()
    {
        isDrawing= true;
        currentLine = Instantiate(pathofRobotPrefab,Vector3.zero,Quaternion.identity);
        lineRenderer= currentLine.GetComponent<LineRenderer>();
        mousePositions.Clear();
        mousePositions.Add(MousetoWorldPosition(controller.Mouse.TouchPosition.ReadValue<Vector2>()));
        mousePositions.Add(MousetoWorldPosition(controller.Mouse.TouchPosition.ReadValue<Vector2>()));
        lineRenderer.SetPosition(0,mousePositions[0]);
        lineRenderer.SetPosition(1,mousePositions[1]);
    }

    void UpdatePath(Vector3 newMousePosition)
    {
        mousePositions.Add(newMousePosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newMousePosition);
    }

    //
    Vector3 MousetoWorldPosition(Vector3 mousePosition)
    {
        Vector3 tempPlane = Vector3.zero;
        tempPlane.y = .204f;
        basePlane = new Plane(Vector3.up, tempPlane);
        Vector3 screenCoordinates = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        ray = Camera.main.ScreenPointToRay(screenCoordinates);
        float ent = 100.0f;
        basePlane.Raycast(ray, out ent);
        hit = ray.GetPoint(ent);
        return hit;
    }

    void DrawPath()
    {
        if(isDrawable)
        {
            if(isTouchStarted)
            {
            CreatePath();
            isTouchStarted = false;
            }
            if(!isTouchEnded && isDrawing)
            {
                Vector3 tempMousePosition = MousetoWorldPosition(controller.Mouse.TouchPosition.ReadValue<Vector2>());
                if(Vector3.Distance(tempMousePosition,mousePositions[mousePositions.Count - 1 ])> .1f)
                {
                    if(mousePositions.Count<batteryCapacity){
                    UpdatePath(tempMousePosition);
                    }else{
                        isDrawable = false;
                    }
                }
            }
        }
    }
    // void DrawingFinishCheck()
    // {
    //     if(isDrawing && Input.GetMouseButtonUp(0))
    //     {
    //         isDrawable = false;
    //         isDrawing = false;
    //     }
    // }

    void CleaningCheck()
    {

        if(!isDrawing && !isDrawable && !isCleaningFinished)
        {
            Clean();
        }
    }

    void Clean(){
        currentLine.SetActive(false);
        soundManager.SendMessage("PlayVacuumSound");
        if(target==Vector3.zero)
        {
            workingParticleEffectofRobot.Play();
            target = mousePositions[0];
        }

        Vector3 dirofRobot = target - robot.transform.position;
        robot.transform.Translate(dirofRobot.normalized * speedofRobot * Time.deltaTime, Space.World);

        if(Vector3.Distance(robot.transform.position,target) <=0.2f)
        {
            if(pathPointIndex != mousePositions.Count -1 )
            {
                GetNextPathPoint();
            }
            else{
                LevelFinished();
                soundManager.SendMessage("StopVacuumSound");
                workingParticleEffectofRobot.Stop();
                isCleaningFinished = true;
            }
        }
    }

    void GetNextPathPoint()
    {
        pathPointIndex++;
        target = mousePositions[pathPointIndex];
        UIManager.SendMessage("ReduceRemainBattery");
    }

    void LevelFinished()
    {
        
        progressionPercent= UIManager.GetComponent<UIManager>().progressionPercent;
        if(progressionPercent>=.5f){
            UIManager.SendMessage("LevelCompleted");
        }else{
            UIManager.SendMessage("LevelFailed", .1f);
        }
    }

    void ReloadLevel()
    {
        levelManager.SendMessage("ReloadLevel");
    }

    void LoadNextLevel()
    {
        levelManager.SendMessage("LoadNextLevel");
    }

    void MainMenu()
    {
        Debug.Log("Open main menu");
    }

    void RobotCrashed()
    {
        workingParticleEffectofRobot.Stop();
        speedofRobot = 0f;
        UIManager.SendMessage("LevelFailed",2f);
        soundManager.SendMessage("StopVacuumSound");
    }

    void ResetVaraibles(){
        isDrawable = true;
        isCleaningFinished = false;
        pathPointIndex = 0;
        robot = GameObject.FindWithTag("Player");
        workingParticlesofRobot = GameObject.FindWithTag("RobotParticle");
        workingParticleEffectofRobot = workingParticlesofRobot.GetComponent<ParticleSystem>();
        target = Vector3.zero;
        UIManager.SendMessage("SetInıtialBatteryCapacity", batteryCapacity);
        speedofRobot = initialSpeedofRobot;
    }

    // void GetRobot(){
    //     robot = GameObject.FindWithTag("Player");
    //     workingParticlesofRobot = GameObject.FindWithTag("VacuumParticle");
    //     UIManager.SendMessage("ResetUI");
    //     workingParticleEffectofRobot = workingParticlesofRobot.GetComponent<ParticleSystem>();
    // }

}

