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
        string startString = "�ȳ��ϼ���, �����Ǹ� ��õ�ص帮�ڽ��ϴ�";
        textField.text = startString;
        Debug.Log(startString);
    }
    private async void GetResponse()
    {
       /* if (inputField.text.Length < 1)
        {
            return;
        }*/
        //��ư Disable
        okBtn.enabled = false;

        //���� �޼����� inputField��
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        //userMessage.Content = inputField.text;

        //�Է¹��� ��������� ���Բ� ��ȯ
        userMessage.Content = UIManager.ingredients + "���� ���� �� �ִ� ������ ��õ����";
        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

        //list�� �޼��� �߰�
        messages.Add(userMessage);

        //textField�� userMessageǥ�� 
        textField.text = string.Format("You: {0}", userMessage.Content);

        //inputField �ʱ�ȭ
        inputField.text = "";

        // ��ü ä���� openAI �����������Ͽ� ���� �޽���(����)�� ����������
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 400,
            Messages = messages
        });

        //���� ��������
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //������ message����Ʈ�� �߰�
        messages.Add(responseMessage);

        //textField�� ���信 ���� Update
        textField.text = string.Format("You: {0}\n\nChatGPT: {1}", userMessage.Content, responseMessage.Content); //������� ����� �亯userMessage.Content�� ǥ��


        //Okbtn�ٽ� Ȱ��ȭ
        okBtn.enabled = true;
    }

}