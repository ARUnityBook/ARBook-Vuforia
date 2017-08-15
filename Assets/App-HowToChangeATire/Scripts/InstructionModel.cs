using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionModel {
    private List<InstructionStep> steps = new List<InstructionStep>();

    public void LoadData() {
        steps.Clear();
        TextAsset text_asset = (TextAsset)Resources.Load("instructionsCSV");
        fgCSVReader.LoadFromString(text_asset.text, new fgCSVReader.ReadLineDelegate(csvReader));
    }


    public InstructionStep GetInstructionStep(int index) {
        if (index < 0 || index >= steps.Count)
            return null;
        return steps[index];
    }

    public int GetCount() {
        return steps.Count;
    }

    private void csvReader(int line_index, List<string> line) {
        if (line_index == 0)
            return;
        steps.Add(new InstructionStep(line));
    }

}

