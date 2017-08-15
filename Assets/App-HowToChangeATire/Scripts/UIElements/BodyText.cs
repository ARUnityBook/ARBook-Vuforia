using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BodyText : InstructionElement {
    protected override void InstructionUpdate(InstructionStep step) {
        GetComponent<Text>().text = step.BodyText;
    }
}

