using UnityEngine;

public class FrameMenu : PictureMenu {

    public override void InitMenu() {
    }
    public override void BeginEdit() {
    }

    public override void ObjectClicked(GameObject clickedGameObject) {
        GameObject frame = clickedGameObject.transform.GetChild(0).gameObject;
        picture.SetFrame(frame);
        DoneEdit(); // close menu when one pic is picked
    }
}