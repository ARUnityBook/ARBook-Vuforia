using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionStep {
    public string Name;
    public string Title;
    public string BodyText;
    public string ImageName;
    public string VideoName;
    public string ARPrefabName;

    private const int NameColumn = 0;
    private const int TitleColumn = 1;
    private const int BodyColumn = 2;
    private const int ImageColumn = 3;
    private const int VideoColumn = 4;
    private const int ARColumn = 5;

    public InstructionStep(List<string> values) {
        foreach (string item in values) {
            if (values.IndexOf(item) == NameColumn) {
                Name = item;
            }
            if (values.IndexOf(item) == TitleColumn) {
                Title = item;
            }
            if (values.IndexOf(item) == BodyColumn) {
                BodyText = item;
            }
            if (values.IndexOf(item) == ImageColumn) {
                ImageName = item;
            }
            if (values.IndexOf(item) == VideoColumn) {
                VideoName = item;
            }
            if (values.IndexOf(item) == ARColumn) {
                ARPrefabName = item;
            }

        }
    }

}

