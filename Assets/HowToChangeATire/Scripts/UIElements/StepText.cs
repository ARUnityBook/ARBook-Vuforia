using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StepText : InstructionElement {
    protected override void InstructionUpdate(InstructionStep step) {
        GetComponent<Text>().text = "Step: " + step.Name;
    }
}


