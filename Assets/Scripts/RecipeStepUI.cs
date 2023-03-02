using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipeStepUI : MonoBehaviour
{
    public Text stepText;
    public GameObject completedLine;
    public GameObject redCurrentStep;
    public GameObject whiteCompletedStep;
    // Start is called before the first frame update
    public void SetCurrentStep()
    {
        stepText.color = Color.red;
        redCurrentStep.SetActive(true);
        completedLine.SetActive(true);

    }
    public void SetStepCompleted()
    {
        whiteCompletedStep.SetActive(true);
        stepText.color = Color.white;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
