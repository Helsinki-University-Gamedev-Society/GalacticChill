using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraPan : MonoBehaviour
{
    public GameObject player0;
    public float panSpeed = 90f;
    public float rotateSpeed = 1.5f;
    public float minRotation = -45f;
    public float maxRotation = 0f;
    private float xRotation = 0f;
    public float zoomSpeed = 5f;
    public float maxZoom = -5f;
    public float minZoom = -20f;
    
    public Image fadeImage;
    public float fadeSpeed = 0.25f;

    private void Awake()
    {
        fadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void Start()
    {
        player0 = GameObject.FindWithTag("Player0");
        transform.position = new Vector3(player0.transform.position.x,player0.transform.position.y,-10f);
    }
    
    void Update()
    {
        if (Input.GetMouseButton(2)) 
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
                        
            Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x - x * panSpeed * Time.deltaTime,0f,ResourceManager.Instance.tileArray.GetLength(1)+1),Mathf.Clamp(transform.position.y - y * panSpeed * Time.deltaTime,0f,ResourceManager.Instance.tileArray.GetLength(0)+1),transform.position.z);
            transform.position = newPos;
        }
        
        try{
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 newPos2 = new Vector3(Mathf.Clamp(transform.position.x + horizontal * 0.2f * panSpeed * Time.deltaTime,0f,ResourceManager.Instance.tileArray.GetLength(1)+1),Mathf.Clamp(transform.position.y + vertical * 0.2f * panSpeed * Time.deltaTime,0f,ResourceManager.Instance.tileArray.GetLength(0)+1),transform.position.z);
            transform.position = newPos2;
        } catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Camera transform not yet loaded.");
        }
        
        
        if (Input.GetMouseButton(1)) 
        {
            xRotation -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime * 360f;
            xRotation = Mathf.Clamp(xRotation, minRotation, maxRotation);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            
            if (xRotation > -45f && xRotation < 0f)
            {
                float y = Input.GetAxis("Mouse Y");
                transform.position += new Vector3(0f, -y * panSpeed * Time.deltaTime, 0f);
            }
        }

        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float newZoom = transform.position.z + zoomDelta;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZoom);
        
        /* float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float curRotation = Mathf.Abs(transform.eulerAngles.x);
        float newZ = transform.position.z + Mathf.Cos(curRotation)*zoomDelta;
        newZ = Mathf.Clamp(newZ, minZoom, maxZoom);
        Vector3 newZoom = new Vector3(transform.position.x, transform.position.y + Mathf.Cos(90 - curRotation)*zoomDelta, newZ);
        transform.position = newZoom; */
    }
    
    public void Shake()
    {
        StartCoroutine(CameraShake(1f));
    }
    
    public IEnumerator CameraShake(float duration)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        
        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f,1f) * 0.2f;
            float y = UnityEngine.Random.Range(-1f,1f) * 0.2f;
            
            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = originalPos;
    }
    
    public IEnumerator FadeToBlack(int i)
    {
        float alpha = 0.0f;

        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        
        SceneManager.LoadScene(i);
    }

    public IEnumerator FadeToClear()
    {
        float alpha = 1.0f;

        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
