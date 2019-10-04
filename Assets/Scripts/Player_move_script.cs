using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Player_move_script : MonoBehaviour
{
    public CharacterController2D controller;
    public GlitchEffect Camera;
    public PlatformController platController;
    public Transform RightBound;
    public Transform LeftBound;
    public Transform GroundCheck;
    public float move_speed = 40.0f;

    WebCamTexture webCamTexture;
    Sprite WebCamSprite;

    private string savePath = "E:/WebCamSnaps/";

    //[HideInInspector]
    public int repeat = 0;


    float HorMove = 0.0f;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();
        
    }

    void WebcamActive()
    {
        webCamTexture.Play();
        TakeSnapshot();
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = LoadNewSprite(savePath + "playerPhoto.png");
        
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 10.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
        Texture2D SpriteTexture = LoadTexture(FilePath);
        WebCamSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),
            new Vector2(0, 0), PixelsPerUnit);
 
        return WebCamSprite;
    }

    void TakeSnapshot()
    {
        Texture2D snap = new Texture2D(webCamTexture.width, webCamTexture.height);
        snap.SetPixels(webCamTexture.GetPixels());
        snap.Apply();

        File.WriteAllBytes(savePath + "playerPhoto.png", snap.EncodeToPNG());
    }

    // Update is called once per frame
    void Update()
    {
        HorMove = Input.GetAxis("Horizontal") * move_speed;
        if (Input.GetButton("Jump"))
        {
            jump = true;
        }

        if (repeat <= 2)
        {
            //do nothing
        }
        else if (repeat == 3)
            Camera.intensity = 0.2f;
        else if (repeat == 4)
            Camera.intensity = 0.3f;
        else if (repeat == 5)
            Camera.intensity = 0.4f;
        else if (repeat == 6)
        {
            Camera.flipIntensity = 0.3f;
        }
        else if (repeat == 10)
        {    
            Camera.flipIntensity = 0.7f;
        }
        else if (repeat == 11)
        {
            Camera.colorIntensity = 0.5f;
        }
        else if(repeat == 13)
        {
            Camera.intensity = 1.0f;
            Camera.flipIntensity = 1.0f;
            Camera.colorIntensity = 1.0f;
        }
        else if (repeat == 14)
        {
            WebcamActive();
        }
        if(repeat == 16)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        
        
    }

    void FixedUpdate()
    {
        
        controller.Move(HorMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.gameObject.tag == "LeftBound")
        {
            platController.UpdateReset();          
            transform.position = new Vector2(RightBound.position.x - RightBound.GetComponent<BoxCollider2D>().size.x - 0.2f,
                transform.position.y);
            repeat++;
        }
        if(other.gameObject.tag == "RightBound")
        {
            platController.UpdateReset();           
            transform.position = new Vector2(LeftBound.position.x + LeftBound.GetComponent<BoxCollider2D>().size.x + 0.2f,
                transform.position.y);
            repeat++;
        }

    }

    void LevelOne()
    {
        
    }
}
