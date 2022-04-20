using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LayerController : MonoBehaviour
{
    //Database
    //ImageID, K, 

    static string image_path = "/Resources/Textures/PreprocessImageData/";///../Assets/

    public static string[] imageList;
    public int image_nums = 6;
    //Painting folder

    public GameObject Painting_Folder;//
    static Vector3 Painting_Folder_position = new Vector3(27.4f, 1f, 1.7f);//new Vector3(-1, 5, 10);
    public GameObject Cur_Painting;

    public static List<GameObject> cur_painting_lists;
    public bool viz_start = false;
    //Paintings
    public GameObject each_painting;
    public GameObject each_layer;
    //private float resize_args = 10000;
    //Distribution: Rotation. spread, zoom
    public Vector3 distribution = new Vector3(0, 0, 0.5f);
    private void Awake()
    {
        imageList = new string[6];
        imageList[0] = "Galaxy";
        imageList[1] = "Starrynight";
        imageList[2] = "Sunflower";
        imageList[3] = "Circle_1";
        imageList[4] = "Circle_2";
        imageList[5] = "Circle_3";
    }
    //at the start
    private void Start()
    {
        cur_painting_lists = new List<GameObject>();
        //Generate_paintings(5, 1);
        //Debug.Log(Cur_Painting.transform.position);
    }

    public void Generate_Image_Default(Vector2 res)
    {
        Generate_paintings((int)res.x, (int)res.y);
    }
    public void Receive_Distribution_Vec3(Vector3 distribution_UI)
    {
        distribution = distribution_UI;
        viz_start = true;
    }
    private void Update()
    {
        if (!viz_start) return;
        Rotation(distribution.x);
        Spread(distribution.y);
        Zoom(distribution.z);
        //Debug.Log(distribution);
        //Debug.Log(Cur_Painting.transform.position);
    }
    // K value, image_id
    public void Generate_paintings(int k, int image_id)
    {
        //clear old
        if(Cur_Painting) Destroy(Cur_Painting);
        //if (cur_painting_lists == null)
        //    cur_painting_lists = new List<GameObject>();
        cur_painting_lists.Clear();

        //t
        Cur_Painting = Instantiate(each_painting, Painting_Folder_position, Quaternion.identity, Painting_Folder.transform);
        
        Cur_Painting.transform.position = Painting_Folder_position;
        string path_after_folder = imageList[image_id] + "/K" + k.ToString() + "/";
        string path = image_path + path_after_folder;
        //Debug.Log(path);
        DirectoryInfo direction = new DirectoryInfo(Application.dataPath + path);
        FileInfo[] images = direction.GetFiles("*.png", SearchOption.TopDirectoryOnly);
        //Debug.Log(images.Length);
        foreach (var img in images)
        {
            //Debug.Log(img);
            //Debug.Log(".." + path + img.Name);
            Texture2D t = AssetDatabase.LoadAssetAtPath("Assets" + path + img.Name, typeof(Texture2D)) as Texture2D;//
            GameObject i = Instantiate(each_layer, Cur_Painting.transform.position, Quaternion.identity, Cur_Painting.transform);//new GameObject(img.Name)
            SpriteRenderer spriterenderer = i.AddComponent<SpriteRenderer>();//"SpriteRenderer"
            //Debug.Log("Textures/PreprocessImageData/" + path_after_folder + img.Name);
            spriterenderer.sprite = //Resources.Load<Sprite>("Textures/PreprocessImageData/" + path_after_folder + img.Name);
                Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0), 100.0f);//.01f pixel per unit 
            cur_painting_lists.Add(i);
        }
        
    }


    //public void Change_K(int k)//image stay the same
    //{
        
    //}

    //public void Change_Image(int image_id)
    //{
        
    //}
    public void Rotation(float rotationSpeed)
    {
        if (Cur_Painting)
        {//rotationSpeed,
            //Painting_Folder.transform.Rotate(Vector3.up, rotationSpeed*20*Time.deltaTime, Space.Self);

            //Cur_Painting.transform.RotateAround(Painting_Folder_position, Vector3.up, rotationSpeed * 20 * Time.deltaTime); 

            //Cur_Painting.transform.Rotate(new Vector3(0, rotationSpeed, 0),  Space.Self); //new Vector3(0, rotationSpeed, 0), Space.Self (27.4f, 1.4f, 1.7f)
            //Cur_Painting.transform.rotation = Quaternion.AngleAxis(30, Vector3.up) * Cur_Painting.transform.rotation;

            Painting_Folder.transform.rotation = Quaternion.AngleAxis(10* rotationSpeed , Vector3.up) * Painting_Folder.transform.rotation;
        }
    }
    public void Spread(float interval)
    {
        if (!Cur_Painting) return;
        int i = 0;
        foreach(var painting in cur_painting_lists){
            i++;
            painting.transform.localPosition = new Vector3(painting.transform.localPosition.x, painting.transform.localPosition.y, i * interval /5);//* 100.0f
            //Debug.Log(painting);
        }
    }
    public void Zoom(float zoom){
        if (Cur_Painting){
            var zoomVal = zoom * zoom;
            Painting_Folder.transform.localScale = new Vector3(zoomVal, zoomVal, zoomVal);

            //foreach (var painting in cur_painting_lists)
            //{
            //    painting.transform.localScale = new Vector3(zoomVal, zoomVal, zoomVal);
            //}
        }
    }
}


public class ImportAsset
{
    [MenuItem("AssetDatabase/ImportExample")]
    static void ImportExample()
    {
        AssetDatabase.ImportAsset("Assets/Textures/texture.jpg", ImportAssetOptions.Default);
    }
}