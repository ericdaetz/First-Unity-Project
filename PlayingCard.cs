using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCard : MonoBehaviour
{

    public SpriteRenderer sprite_renderer;
    public Sprite card_front;
    public Sprite card_back;
    private bool clickable;
    private bool need_to_flip;
    public GameObject game_manager_obj;
    public GameManager game_manager;

    private float hold_time = 2.0f;
    private float wait_time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        clickable = true;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        game_manager_obj = GameObject.Find("Game Manager");
        game_manager = game_manager_obj.GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(need_to_flip){
            wait_time += Time.deltaTime;
            if(wait_time > hold_time){
                wait_time -= 2.0f;
                sprite_renderer.sprite = card_back;
                UnlockClickable();
                need_to_flip = false;
            }
        } 
    }

    void OnMouseDown(){
        if(clickable){
            sprite_renderer.sprite = card_front;
            print(sprite_renderer.sprite.name);
            game_manager.ReceiveCard(gameObject);
            //LockClickable();
        }
    }
    
    public void LockClickable(){
        clickable = false;
    }

    public void UnlockClickable(){
        clickable = true;
    }

    public void FlipDown(){
        need_to_flip = true;
    }
}
