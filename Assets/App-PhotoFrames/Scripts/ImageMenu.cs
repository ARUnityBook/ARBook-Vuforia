using UnityEngine;

public class ImageMenu : PictureMenu {

    public Texture[] ImageTextures;
    [SerializeField]
    private ClickableObject nextButton;
    [SerializeField]
    private ClickableObject previousButton;

    //The items per page is calculated by the number of images shown at start.
    private int indexOffset;

    public override void InitMenu() {
        base.SubscribeClickableObjects();
        previousButton.OnClickableObjectClicked.AddListener(ScrollPrevious);
        nextButton.OnClickableObjectClicked.AddListener(ScrollNext);
    }

    public override void BeginEdit() {
        UpdateImages();
    }

    public override void ObjectClicked(GameObject clickedGameObject) {
        Texture texture = clickedGameObject.GetComponent<Renderer>().material.mainTexture;
        picture.SetTexture(texture);
        DoneEdit(); // close ImageMenu when one pic is picked
    }

    private void UpdateImages() {
        for (int i = 0; i < clickableObjects.Length; i++) {
            //Sets the texture for the images based on index
            clickableObjects[i].GetComponent<Renderer>().material.mainTexture = ImageTextures[i + indexOffset];
        }
    }

    public void ScrollNext(Object eventData) {
        if ((indexOffset + clickableObjects.Length) < ImageTextures.Length) {
            indexOffset++;
        }
        UpdateImages();
    }

    public void ScrollPrevious(Object eventData) {
        if ((indexOffset - 1) >= 0) {
            indexOffset--;
        }
        UpdateImages();
    }
}
