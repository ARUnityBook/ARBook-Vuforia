using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TitleText : InstructionElement {
    protected override void InstructionUpdate(InstructionStep step) {
        GetComponent<Text>().text = step.Title;
    }
}

