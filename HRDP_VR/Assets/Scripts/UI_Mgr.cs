using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Mgr : MonoBehaviour
{
    public GameObject LeftHand;
    public GameObject Img_Showcase;
    public GameObject Painting_Folder;

    public Slider k_slider;
    public GameObject Choose_Img_UI;
    public GameObject Change_Distribution_UI;
    public List<Sprite> textures;
    public int image_index = 0;
    //Image change to last one
    public int K;
    //3 index
    private float rotation_speed = 0f;
    private float spread = 0f;
    private float zoom = 1f;

    public bool viz_start = false;
    private void Start()
    {
        Choose_Img_UI.SetActive(true);
        Change_Distribution_UI.SetActive(false);
    }
    private void Update()
    {
        //Let it follow the left controller :(
        transform.position = Camera.main.transform.position + new Vector3(0, -0.6f, -0.5f);//LeftHand.transform.position + new Vector3(0,-0.3f,0);
        //transform.forward = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        transform.LookAt(Camera.main.transform);


        if (!viz_start) return;
        //Am I going to send msgs every frame? hmm
        Painting_Folder.SendMessage("Receive_Distribution_Vec3", new Vector3(rotation_speed, spread, zoom));
        //Debug.Log(new Vector3(rotation_speed, spread, zoom));
    }


    public void Hit_Left()
    {
        if (image_index <= 0) image_index = textures.Count - 1;
        else
        {
            image_index = (image_index - 1) % textures.Count;
        }
        //Show image
        ShowImg();   
    }
    public void Hit_Right()
    {
        image_index = (image_index + 1) % textures.Count;
        //Show image
        ShowImg();
    }
    public void ShowImg()
    {
        Img_Showcase.GetComponent<Image>().sprite = textures[image_index];
    }

    public void Hit_Visualize()
    {
        viz_start = true;
        //LayerController.
        Vector2 res = new Vector2(K, image_index);
        Painting_Folder.SendMessage("Generate_Image_Default", res);
        //Hide before, show next
        Choose_Img_UI.SetActive(false);
        Change_Distribution_UI.SetActive(true);
    }
    public void Hit_Back_Btn()
    {
        viz_start = false;
        Choose_Img_UI.SetActive(true);
        Change_Distribution_UI.SetActive(false);
    }
    public void SetRotationSpeed(float rotateSpeedUpdate)
    {
        rotation_speed = rotateSpeedUpdate;
        //Debug.Log(rotation_speed);
    }
    public void SetSpread(float spread_val)
    {
        spread = spread_val;
        //Debug.Log(spread_val);
    }
    public void SetZoom(float zoom_val)
    {
        zoom = zoom_val;
        //Debug.Log(zoom_val);

    }

    public void SetK()
    {
        K = (int) k_slider.value; 
    }

}
