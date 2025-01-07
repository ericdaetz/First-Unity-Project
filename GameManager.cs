using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    //Necessary Variables for Operation
    public GameObject[] card_array;
    public GameObject playing_card;
    public PlayingCard card_script;
    public PlayingCard card_script1;
    public PlayingCard card_script2;
    public Button button;
    public int matches;
    public int num_cards = 12;
    public int active_cards = 0;

    public float x_start;
    public float x_start2;
    public float x_offset = 300f;
    public float y_start;
    public float y_start2;

    //Sprite Assets
    public Sprite card_back;
    public Sprite card_triangle;
    public Sprite card_square;
    public Sprite card_gums;
    public Sprite card_bag;
    public Sprite card_raindrop;
    public Sprite card_mirror;
    public Sprite[] card_sprites;

    //Sprite Renderer
    public SpriteRenderer sprite_renderer;

    //Sprites to compare
    public GameObject card1;
    public GameObject card2;

    //Timer and Lock
    private float hold_time = 2.0f;
    private float wait_time = 0.0f;

    private bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        //Set up variables
        matches = 0;
        x_start = -795f;
        y_start = 340f;
        x_start2 = 195f;
        y_start2 = 0f;
        active_cards = 0;

        button = GameObject.Find("Return Button").GetComponent<Button>();
        button.interactable = false;

        playing_card = GameObject.Find("Playing Card");
        sprite_renderer = playing_card.GetComponent<SpriteRenderer>();

        card_array = new GameObject[num_cards];
        CreateCards();
        SetCardFronts();
        
    }

    void CreateCards(){
        float offset_counter = 0f;
        for(int i = 0; i < num_cards; i++){

            GameObject card_created;
            if(i < 3){
                card_created = Instantiate(playing_card, new Vector2(x_start + (x_offset * offset_counter), y_start), Quaternion.identity);
            }
            else if(i < 6){
                card_created = Instantiate(playing_card, new Vector2(x_start2 + (x_offset * offset_counter), y_start), Quaternion.identity);
            }
            else if(i < 9){
                card_created = Instantiate(playing_card, new Vector2(x_start + (x_offset * offset_counter), y_start2), Quaternion.identity);
            }
            else{
                card_created = Instantiate(playing_card, new Vector2(x_start2 + (x_offset * offset_counter), y_start2), Quaternion.identity);
            }
            //print("Object should be created");
            card_array[i] = card_created;
            offset_counter++;
            if(offset_counter >= 3f){
                offset_counter = 0f;
            }
        }
    }


    void SetCardFronts(){

        //Sets up card sprites to be used
        card_sprites = new Sprite[] {card_back, card_triangle, card_square, card_gums, card_bag, card_raindrop, card_mirror};
        
        //Prepares the array to shuffle numbers associated with the card fronts
        int[] shuffle_array = new int[num_cards];
        bool first_entry = true;
        int counter = 1;

        
        for(int i = 0; i < shuffle_array.Length; i++){
            if(first_entry){
                shuffle_array[i] = counter;
                first_entry = false;
            }
            else{
                shuffle_array[i] = counter++;
                first_entry = true;
            }
        }

        //Now simply shuffle the indices, a bit long-winded but linear time, so whatever
        for(int i = 0; i < shuffle_array.Length; i++){
            int swap_index = Random.Range(0,shuffle_array.Length);
            int temporary_value = shuffle_array[i];
            shuffle_array[i] = shuffle_array[swap_index];
            shuffle_array[swap_index] = temporary_value;
        }

        //card_array needs to have sprites set
        for(int i = 0; i < shuffle_array.Length; i++){
            int selected_sprite = shuffle_array[i];
            card_script = card_array[i].GetComponent<PlayingCard>();
            card_script.sprite_renderer.size = sprite_renderer.size;
            card_script.card_front = card_sprites[selected_sprite];
            card_script.card_back = card_back;
            //print(card_script.card_front.name);
        }

        /*
        foreach(int i in shuffle_array){
            print(i);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        if(locked){
            wait_time += Time.deltaTime;
            if(wait_time > hold_time){
                ReleaseLocks();
                wait_time -= 2.0f;
            }
        } 
    }

    void CheckMatch(){
        card_script1 = card1.GetComponent<PlayingCard>();
        card_script2 = card2.GetComponent<PlayingCard>();
        if(card_script1.card_front.name == card_script2.card_front.name){
            //print("Match");
            matches++;
            if(matches >= (num_cards)/2){
                button.interactable = true;
            }
        }
        else{
            card_script1.FlipDown();
            card_script2.FlipDown();
            EngageLocks();
            locked = true;
        }
        card1 = null;
        card2 = null;
    }

    public void ReceiveCard(GameObject received_card){
        if(card1 == null){
            card1 = received_card;
            //print("Card1 set");
        }
        else if(card2 == null){
            card2 = received_card;
            //print("Card2 set");
            CheckMatch();
        }
    }

    void ReleaseLocks(){
        for(int i = 0; i < card_array.Length; i++){
            card_script = card_array[i].GetComponent<PlayingCard>();
            if(card_script.sprite_renderer.sprite.name == card_script.card_back.name){
                card_script.UnlockClickable();
            }
        }
        locked = false;
    }

    void EngageLocks(){
        for(int i = 0; i < card_array.Length; i++){
            card_script = card_array[i].GetComponent<PlayingCard>();
            card_script.LockClickable();
        }
        locked = true;
    }

}
