using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using OpenAI_API.Models;

public class OpenAIController : MonoBehaviour
{
    public GetImageURL GetImage;

    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button OkBtn;
    //public Button KoreanBtn;
    public GameObject Recipe;

    public TMP_Text recipeNameField;
    public TMP_Text recipeField;
    public TMP_Text ingredientsNeededField;
    public TMP_Text cautionField;


    private OpenAIAPI api;
    private List<ChatMessage> messages;
    void Start()
    {
        api = new OpenAIAPI("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");
        StartConversation();
        //OkBtn.onClick.AddListener(() => GetResponse(OkBtn, UIManager.Ingredients + "으로 만들 수 있는 아이에게 해 줄 건강식 레시피 추천해줘"));
        OkBtn.onClick.AddListener(() =>
        {
            string ingredients = UIManager.Ingredients;
            if (string.IsNullOrEmpty(ingredients))
            {
                // 두 번째 매개변수가 비어있는 경우 경고문구 출력
                Debug.LogWarning("무슨 재료를 가지고 계신가요? 알려주시면 그에 맞는 건강한 레시피를 추천해드릴게요.");
            }
            else
            {
                GetResponse(OkBtn, ingredients + "으로 만들 수 있는 아이에게 해 줄 " + UIManager.Categories + "레시피를 추천해줘");
            }
        });
    }

    private void StartConversation()
    {
        //ChatGpt에는 크게 role과 content가 있다
        //role은 세가지가 있는데, 1. System, 2. User, 3. Assistants
        //1.System은 유저에게 메세지를 받기 전에 모델을 초기화하거나 구성하려는 경우에 사용.
        //2. Assistants는 이전에 ChatGPT가 보낸 메시지가 무엇인지 알려줄 수 있다
        //유저와 어시스턴트 사이의 대화를 저장하고, 어시스턴트에게 이전 대화를 전달하여 응답값 조절할 수 있다.
        messages = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "너는 가지고 있는 재료를 입력하면 아이에게 해 줄 영양식을 추천해주는 시스템이야. 레시피 이름: 조리 순서: 필요한 재료: 아이가 먹을 때 주의할 점: 소요시간: 의 형식을 꼭 맞춰서 대답해.")//You are an artificial intelligence that recommends nutritious meals for your child when you input the ingredients you have. Divide the recipe name, recipe, and required ingredients into descriptions.
        };
    
        inputField.text = "";
        string startString = "안녕하세요, 레시피를 추천해드리겠습니다";
        textField.text = startString;
        Debug.Log(startString);
    }
    public async void GetResponse(Button button, string question)
    {
        if(question == null)
        {
            //Log가 아닌 아래문구를 포함한 경고창 띄워주는 작업 필요
            Debug.Log("무슨 재료를 가지고 계신가요? 알려주시면 그에 맞는 건강한 레시피를 추천해드릴게요.");
            return;
        }
        /* if (inputField.text.Length < 1)
         {
             return;
         }*/
        button.enabled = false;

        //유저 메세지에 inputField를
        ChatMessage userMessage = new ChatMessage(); 
        userMessage.Role = ChatMessageRole.User; //이 userMessage는 role이 User.
        //userMessage.Content = inputField.text;

        //입력받은 재료기반으로 묻게끔 변환
        //userMessage.Content = UIManager.ingredients + "으로 만들 수 있는 아이에게 해 줄 건강식 레시피 추천해줘";
        userMessage.Content = question; //2023_05_08: 매개변수로 받는 방식으로 변경

        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

        //list에 메세지 추가
        messages.Add(userMessage);

        //textField에 userMessage표시 -> 필요없음
        textField.text = string.Format("You: {0}", userMessage.Content);

        //inputField 초기화
        inputField.text = "";

        // 전체 채팅을 openAI 서버에전송하여 다음 메시지(응답)를 가져오도록
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.9,
            MaxTokens = 400,
            Messages = messages
        });

        //응답 가져오기
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        UpdateText(responseMessage);
        //GetImageURL.Insatnce.CrawlingImage(recipeName);

        Recipe.SetActive(true);
        
         
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //응답을 message리스트에 추가
        messages.Add(responseMessage);

        button.enabled = true;

        GetImage.CrawlingImage(recipeName);
    }

    public void UpdateText(ChatMessage responseMessage)
    {
        //string형식으로 사용하려면 responMessage.ToString()이 아니라, responMessage.Content를 사용하면 된다
        string[] splitText = responseMessage.Content.Split(new string[] { "레시피 이름:", "조리 순서:", "필요한 재료:", "아이가 먹을 때 주의할 점:" }, StringSplitOptions.None);
        recipeName = splitText[1]; //static
        string recipe = splitText[2];
        string ingredients = splitText[3];
        string caution = splitText[4];
        Debug.Log("recipeName:" +recipeName);
        Debug.Log("recipe:" +recipe);
        Debug.Log("ingredients:"+ingredients);
        Debug.Log("caution:" +caution);

        recipeNameField.text = recipeName;
        recipeField.text = string.Format("조리 순서: \n{0}", recipe);
        ingredientsNeededField.text = string.Format("필요한 재료: \n\n{0}", ingredients);
        cautionField.text = string.Format("아이가 먹을 때 주의할 점: \n\n{0}", caution);

       
    }

   public static string recipeName { get; set; }
}