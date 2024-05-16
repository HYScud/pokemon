using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;
    List<Sprite> spriteList;
    float frameRate;

    int currentFrame;
    float timer;

    public List<Sprite> SpriteList { get => spriteList; set => spriteList = value; }

    public SpriteAnimator(SpriteRenderer spriteRenderer, List<Sprite> spriteList, float frameRate = 0.16f)
    {
        this.spriteRenderer = spriteRenderer;
        this.SpriteList = spriteList;
        this.frameRate = frameRate;
    }

    // ��ʼ�������Լ������һ��ͼƬ
    public void Start()
    {
        this.currentFrame = 0;
        this.timer = 0;
        if (this.SpriteList != null && this.SpriteList.Count > 0)
            spriteRenderer.sprite = SpriteList[0];
    }

    // ����ͼƬ�仯
    public void HandleUpdate()
    {
        timer += Time.deltaTime;
        if (timer > frameRate)
        {
            currentFrame = (currentFrame + 1) % SpriteList.Count;
            spriteRenderer.sprite = SpriteList[currentFrame];
            timer -= frameRate;
        }
    }

    public void SetDefaultSprite()
    {
        if (spriteRenderer != null && spriteList != null && spriteList.Count > 0)
        {
            spriteRenderer.sprite = spriteList[0];
        }
    }
}
