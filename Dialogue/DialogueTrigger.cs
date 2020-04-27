using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueTrigger : MonoBehaviour
{
    public Flowchart triggerChart;
    void OnTriggerEnter(Collider other)
    {
        triggerChart.ExecuteBlock("Trigger");
        triggerChart.SetBooleanVariable("ConvowBird", true);
        Destroy(gameObject);
    }
}
