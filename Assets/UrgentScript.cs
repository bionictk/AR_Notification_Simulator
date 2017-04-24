using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UrgentScript : MonoBehaviour
{
    public Color color = new Color(0.6000000238418579f, 0.0f, 0.0f);
    public Vector3 size = new Vector3(1.5f, 0.5f, 0.05f);
    public float duration = 8.0f;

    bool isLocationPassed = false, isTimeOfDayPassed = false, isUserActivityPassed = false, isCognitiveLoadPassed = false, isEmotionPassed = false;
    int isVisible = 0;
    bool isAnimating = false;
    ConditionManager conditionManager;
    float timer, animationTimer, animationSpeed = 0.9f;
    Vector3 startPos, endPos, startScale, endScale;
    
    void Start()
    {
        TraverseHide(gameObject);
        conditionManager = GameObject.Find("ConditionManager").GetComponent<ConditionManager>();
    }

    void Update()
    {
        if (isVisible == 0 && conditionManager.event_state == ConditionManager._event.urgent)
        {
            isLocationPassed = isTimeOfDayPassed = isUserActivityPassed = isCognitiveLoadPassed = isEmotionPassed = false;
            if (conditionManager.location_state == ConditionManager._location.classroom || conditionManager.location_state == ConditionManager._location.car || conditionManager.location_state == ConditionManager._location.home)
            {
                isLocationPassed = true;
            }

            if (conditionManager.timeofday_state == ConditionManager._timeofday.morning || conditionManager.timeofday_state == ConditionManager._timeofday.lunch || conditionManager.timeofday_state == ConditionManager._timeofday.afternoon || conditionManager.timeofday_state == ConditionManager._timeofday.evening)
            {
                isTimeOfDayPassed = true;
            }

            if (conditionManager.useractivity_state == ConditionManager._useractivity.sitting || conditionManager.useractivity_state == ConditionManager._useractivity.standing || conditionManager.useractivity_state == ConditionManager._useractivity.walking)
            {
                isUserActivityPassed = true;
            }

            if (conditionManager.cognitiveload_state == ConditionManager._cognitiveload.low || conditionManager.cognitiveload_state == ConditionManager._cognitiveload.normal || conditionManager.cognitiveload_state == ConditionManager._cognitiveload.high)
            {
                isCognitiveLoadPassed = true;
            }

            if (conditionManager.emotion_state == ConditionManager._emotion.angry || conditionManager.emotion_state == ConditionManager._emotion.neutral || conditionManager.emotion_state == ConditionManager._emotion.happy)
            {
                isEmotionPassed = true;
            }

            if (isLocationPassed && isTimeOfDayPassed && isUserActivityPassed && isCognitiveLoadPassed && isEmotionPassed)
            {
                ShowNotification();
            }
        }
        else if (isVisible == 2)
        {
            if (timer < 0)
            {
                HideNotification();
            }
            else timer -= Time.deltaTime;
        }
        else if (isAnimating)
        {
            if (animationTimer < 0)
            {
                if (isVisible == 1) isVisible = 2;
                else
                {
                    isVisible = 0;
                    TraverseHide(gameObject);
                }
                isAnimating = false;
                transform.localPosition = endPos;
                
                //transform.localScale = endScale; //for wall&boundary only
            } else
            {
                //for hud
                transform.localPosition = Vector3.Lerp(endPos, startPos, animationTimer / animationSpeed);
                
                //for wall&boundary
                //transform.localScale = Vector3.Lerp(endScale, startScale, animationTimer / animationSpeed);
                animationTimer -= Time.deltaTime;
                
            }
        }
    }
    
    void ShowNotification()
    {

#if !UNITY_EDITOR
        conditionManager.event_state = ConditionManager._event.idle;
#endif
        //isVisible = true;
        isVisible = 1;
        isAnimating = true;
        animationTimer = animationSpeed;
        timer = duration;
        
        //for hud
        transform.localScale = size;
        TraverseShow(gameObject);
        transform.parent = GameObject.Find("AnchorHUD").transform;
        endPos = new Vector3(0, 0, 0);
        startPos = endPos + new Vector3(0.5f, 0.5f, 0.5f);
        transform.localRotation = Quaternion.FromToRotation(Vector3.zero, -Camera.main.transform.forward);


        //for wall&boundary
        /*
        transform.localScale = new Vector3(0, 0, 0);
        TraverseShow(gameObject);
        transform.parent = GameObject.Find("AnchorWall").transform;
        endPos = new Vector3(0, 0, 0);
        transform.localPosition = endPos;
        startScale = new Vector3(0, 0, 0);
        
        //for wall
        //endScale = size * 15;

        //for boundary
        endScale = new Vector3(size.x, size.z, size.y) * 50;*/
    }

    void HideNotification()
    {
        isVisible = 3;
        isAnimating = true;
        animationTimer = 1.0f;
        //for hud
        startPos = new Vector3(0, 0, 0);
        endPos = startPos + new Vector3(0.5f, 0.5f, 0.5f);

        //for wall&boundary
        /*
        endPos = new Vector3(0, 0, 0);
        startScale = transform.localScale;
        endScale = new Vector3(0, 0, 0);*/
    }

    void TraverseHide(GameObject obj)
    {
        if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = false;
        if (GetComponent<Text>() != null) GetComponent<Text>().enabled = false;
        foreach (Transform child in obj.transform)
        {
            if (child.GetComponent<Renderer>() != null) child.GetComponent<Renderer>().enabled = false;
            if (child.GetComponent<Text>() != null) child.GetComponent<Text>().enabled = false;
            TraverseHide(child.gameObject);
        }

    }

    void TraverseShow(GameObject obj)
    {
        if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = true;
        if (GetComponent<Text>() != null) GetComponent<Text>().enabled = true;
        foreach (Transform child in obj.transform)
        {
            if (child.GetComponent<Renderer>() != null) child.GetComponent<Renderer>().enabled = true;
            if (child.GetComponent<Text>() != null) child.GetComponent<Text>().enabled = true;
            TraverseHide(child.gameObject);
        }
    }
}