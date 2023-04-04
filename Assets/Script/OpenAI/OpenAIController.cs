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

    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button okBtn;


    private OpenAIAPI api;
    private List<ChatMessage> messages;
    void Start()
    {
        //api = new OpenAIAPI(Environment.GetEnvironmentVariable("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", EnvironmentVariableTarget.User));
        api = new OpenAIAPI("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");
        StartConversation();
        okBtn.onClick.AddListener(() => GetResponse());
    }

    private void StartConversation()
    {
        messages = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "You are an artificial intelligence that recommends nutritious meals for your child when you input the ingredients you have. Divide the recipe name, recipe, and required ingredients into descriptions.")//You are an artificial intelligence that recommends nutritious meals for your child when you input the ingredients you have. Divide the recipe name, recipe, and required ingredients into descriptions.
        };

        inputField.text = "";
        string startString = "안녕하세요, 레시피를 추천해드리겠습니다";
        textField.text = startString;
        Debug.Log(startString);
    }
    private async void GetResponse()
    {
       /* if (inputField.text.Length < 1)
        {
            return;
        }*/
        //버튼 Disable
        okBtn.enabled = false;

        //유저 메세지에 inputField를
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        //userMessage.Content = inputField.text;

        //입력받은 재료기반으로 묻게끔 변환
        userMessage.Content = UIManager.ingredients + "으로 만들 수 있는 레시피 추천해줘";
        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

        //list에 메세지 추가
        messages.Add(userMessage);

        //textField에 userMessage표시 
        textField.text = string.Format("You: {0}", userMessage.Content);

        //inputField 초기화
        inputField.text = "";

        // 전체 채팅을 openAI 서버에전송하여 다음 메시지(응답)를 가져오도록
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 400,
            Messages = messages
        });

        //응답 가져오기
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //응답을 message리스트에 추가
        messages.Add(responseMessage);

        //textField를 응답에 따라 Update
        textField.text = string.Format("You: {0}\n\nChatGPT: {1}", userMessage.Content, responseMessage.Content); //응답받은 사용자 답변userMessage.Content도 표시


        //Okbtn다시 활성화
        okBtn.enabled = true;
    }

}