using UnityEngine;

public enum PictureCommand { ADD, EDIT, DONE, CANCEL, MOVE, SCALE, DELETE, IMAGE, FRAME }

public class PictureController : MonoBehaviour {
    public GameObject toolbar;
    public Transform framedImage;
    public GameObject frameMenu;
    public GameObject imageMenu;

    private Vector3 startPosition;
    private Vector3 startScale;
    private Quaternion startRotation;
    private GameObject startFrame;
    private Texture startTexture;

    private Transform frameSpawn;
    private Renderer imageRenderer;

    void Start() {
        frameSpawn = framedImage.Find("FrameSpawn");

        Transform image = framedImage.Find("Image");
        imageRenderer = image.gameObject.GetComponent<Renderer>();

        BeginEdit();
    }

    public void Execute(PictureCommand command) {
        switch (command) {
            case PictureCommand.EDIT:
                BeginEdit();
                break;

            case PictureCommand.DONE:
                DoneEdit();
                break;

            case PictureCommand.CANCEL:
                CancelEdit();
                break;

            case PictureCommand.FRAME:
                OpenFrameMenu();
                break;

            case PictureCommand.IMAGE:
                OpenImageMenu();
                break;

            case PictureCommand.ADD:
                AddPicture();
                break;

            case PictureCommand.DELETE:
                DeletePicture();
                break;
        }
    }

    private void BeginEdit() {
        SavePictureProperties();
        toolbar.SetActive(true);
    }

    private void DoneEdit() {
        toolbar.SetActive(false);
        Destroy(startFrame);
    }

    private void CancelEdit() {
        RestorePictureProperties();
        toolbar.SetActive(false);
    }

    private void OpenFrameMenu() {
        frameMenu.SetActive(true);
    }

    private void OpenImageMenu() {
        imageMenu.SetActive(true);
    }

    private void AddPicture() {
        toolbar.SetActive(false);
        GameController.instance.CreateNewPicture();
    }

    private void DeletePicture() {
        Destroy(gameObject);
    }


    private void SavePictureProperties() {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        startScale = framedImage.localScale;
        startFrame = Instantiate(GetCurrentFrame());
        startFrame.SetActive(false);
        startTexture = imageRenderer.material.mainTexture;
    }

    private void RestorePictureProperties() {
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
        framedImage.localScale = startScale;
        startFrame.SetActive(true);
        SetFrame(startFrame);
        imageRenderer.material.mainTexture = startTexture;

    }

    public void SetFrame(GameObject frameGameObject) {
        GameObject currentFrame = GetCurrentFrame();
        if (currentFrame != null)
            Destroy(currentFrame);

        GameObject newFrame = Instantiate(frameGameObject, frameSpawn);
        newFrame.transform.localPosition = Vector3.zero;
        newFrame.transform.localEulerAngles = Vector3.zero;
        newFrame.transform.localScale = Vector3.one;
    }

    private GameObject GetCurrentFrame() {
        Transform currentFrame = frameSpawn.GetChild(0);
        if (currentFrame != null) {
            return currentFrame.gameObject;
        }
        return null;
    }

    public void SetTexture(Texture texture) {
        imageRenderer.material.mainTexture = texture;
        framedImage.transform.localScale = TextureToScale(framedImage.transform.localScale, texture);
    }

    private Vector3 TextureToScale(Vector3 startScale, Texture texture) {
        Vector3 scale = Vector3.one * startScale.z;
        if (texture.width > texture.height) {
            scale.y *= (texture.height * 1.0f) / texture.width;
        } else {
            scale.x *= (texture.width * 1.0f) / texture.height;
        }
        return scale;
    }
}

