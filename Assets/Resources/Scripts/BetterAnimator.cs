using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterAnimator : MonoBehaviour
{
    public List<string> frameNames;
    public Sprite[] frames;
    public float frameDuration = 0.1f;
    public bool playOnStart = true;
    public bool dead = false;

    private int currentFrameIndex = 0;
    private float timer = 0f;
    private SpriteRenderer spriteRenderer;
    private bool isPlaying = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        FillFrames();

        if (playOnStart)
        {
            Play();
        }
    }

    private void Update()
    {
        if (isPlaying && !dead)
        {
            timer += Time.deltaTime;

            if (timer >= frameDuration)
            {
                timer = 0f;
                currentFrameIndex = (currentFrameIndex + 1) % frames.Length;
                spriteRenderer.sprite = frames[currentFrameIndex];
            }
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
        timer = 0f;
        currentFrameIndex = 0;
        spriteRenderer.sprite = frames[currentFrameIndex];
    }
    
    // this method should be updated if we also have different animations 
    // for now we can just flip the sprite for moving left, and make no changes for up/down.
    public void FillFrames()
    {
        if (gameObject.tag=="Player0")
        {
            frames = Resources.LoadAll<Sprite>("Tile/Sprites/player0sprites");
        } else if (gameObject.tag=="Player1")
        {
            gameObject.transform.localScale = new Vector3(0.5f,0.5f,1f);
            frames = Resources.LoadAll<Sprite>("Tile/Sprites/player1sprites");
        } else if (gameObject.tag=="Player2")
        {
            gameObject.transform.localScale = new Vector3(0.4f,0.4f,1f);
            frames = Resources.LoadAll<Sprite>("Tile/Sprites/player2sprites");
        }
    }
}