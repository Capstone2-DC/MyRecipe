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
        okBtn.onClick.AddListener(() => GetResponse());
    }

    private void StartConversation()
    {
        //ChatGpt���� ũ�� role�� content�� �ִ�
        //role�� �������� �ִµ�, 1. System, 2. User, 3. Assistants
        //1.System�� �������� �޼����� �ޱ� ���� ���� �ʱ�ȭ�ϰų� �����Ϸ��� ��쿡 ���.
        //2. Assistants�� ������ ChatGPT�� ���� �޽����� �������� �˷��� �� �ִ�
        //������ ��ý���Ʈ ������ ��ȭ�� �����ϰ�, ��ý���Ʈ���� ���� ��ȭ�� �����Ͽ� ���䰪 ������ �� �ִ�.
        messages = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "�ʴ� ������ �ִ� ��Ḧ �Է��ϸ� ���̿��� �� �� ������� ��õ���ִ� �ý����̾�. ������ �̸�: ������: �ʿ��� ���: ���̰� ���� �� ������ ��: �� ������ �� ���缭 �����.")//You are an artificial intelligence that recommends nutritious meals for your child when you input the ingredients you have. Divide the recipe name, recipe, and required ingredients into descriptions.
        };
    
        inputField.text = "";
        string startString = "�ȳ��ϼ���, �����Ǹ� ��õ�ص帮�ڽ��ϴ�";
        textField.text = startString;
        Debug.Log(startString);
    }
    private async void GetResponse()
    {
        if(UIManager.ingredients == null)
        {
            //Log�� �ƴ� �Ʒ������� ������ ���â ����ִ� �۾� �ʿ�
            Debug.Log("���� ��Ḧ ������ ��Ű���? �˷��ֽø� �׿� �´� �ǰ��� �����Ǹ� ��õ�ص帱�Կ�.");
            return;
        }
       /* if (inputField.text.Length < 1)
        {
            return;
        }*/
        okBtn.enabled = false;

        //���� �޼����� inputField��
        ChatMessage userMessage = new ChatMessage(); 
        userMessage.Role = ChatMessageRole.User; //�� userMessage�� role�� User.
        //userMessage.Content = inputField.text;

        //�Է¹��� ��������� ���Բ� ��ȯ
        userMessage.Content = UIManager.ingredients + "���� ���� �� �ִ� ���̿��� �� �� �ǰ��� ������ ��õ����";
        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

        //list�� �޼��� �߰�
        messages.Add(userMessage);

        //textField�� userMessageǥ�� -> �ʿ����
        textField.text = string.Format("You: {0}", userMessage.Content);

        //inputField �ʱ�ȭ
        inputField.text = "";

        // ��ü ä���� openAI �����������Ͽ� ���� �޽���(����)�� ����������
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.9,
            MaxTokens = 400,
            Messages = messages
        });

        //���� ��������
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        Recipe.SetActive(true);
        UpdateText(responseMessage);

        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //������ message����Ʈ�� �߰�
        messages.Add(responseMessage);

        okBtn.enabled = true; 
    }

    public void UpdateText(ChatMessage responseMessage)
    {
        //string�������� ����Ϸ��� responMessage.ToString()�� �ƴ϶�, responMessage.Content�� ����ϸ� �ȴ�
        string[] splitText = responseMessage.Content.Split(new string[] { "������ �̸�:", "������:", "�ʿ��� ���:", "���̰� ���� �� ������ ��:" }, StringSplitOptions.None);
        string recipeName = splitText[1];
        string recipe = splitText[2];
        string ingredients = splitText[3];
        string caution = splitText[4];
        Debug.Log("recipeName:" +recipeName);
        Debug.Log("recipe:" +recipe);
        Debug.Log("ingredients:"+ingredients);
        Debug.Log("caution:" +caution);

        recipeNameField.text = recipeName;
        recipeField.text = string.Format("������: \n\n{0}", recipe);
        ingredientsNeededField.text = string.Format("�ʿ��� ���: \n\n{0}", ingredients);
        cautionField.text = string.Format("���̰� ���� �� ������ ��: \n\n{0}", caution);
    }

}