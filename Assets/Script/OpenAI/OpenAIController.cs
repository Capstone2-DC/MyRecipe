using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using OpenAI_API.Models;
using System.Diagnostics;
using System.IO;

public class OpenAIController : MonoBehaviour
{
    //public GetImageURL GetImage;

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
        //OkBtn.onClick.AddListener(() => GetResponse(OkBtn, UIManager.Ingredients + "���� ���� �� �ִ� ���̿��� �� �� �ǰ��� ������ ��õ����"));
        OkBtn.onClick.AddListener(() =>
        {
            string questionIngredients = null;
            foreach (var ingredi in UIManager.ingredientsList)
            {
                questionIngredients += ingredi;
            }
            if (string.IsNullOrEmpty(questionIngredients))
            {
                // �� ��° �Ű������� ����ִ� ��� ����� ���
                UnityEngine.Debug.LogWarning("���� ��Ḧ ������ ��Ű���? �˷��ֽø� �׿� �´� �ǰ��� �����Ǹ� ��õ�ص帱�Կ�.");
            }
            else
            {
                GetResponse(OkBtn, questionIngredients + "���� ���� �� �ִ� ���̿��� �� �� " + UIManager.Categories + "�����Ǹ� ��õ����");
            }
        });
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
            new ChatMessage(ChatMessageRole.System, "�ʴ� ������ �ִ� ��Ḧ �Է��ϸ� ���̿��� �� �� ������� ��õ���ִ� �ý����̾�. ������ �̸�: ���� ����: �ʿ��� ���: ���̰� ���� �� ������ ��: �ҿ�ð�: �� ������ �� ���缭 �����.")//You are an artificial intelligence that recommends nutritious meals for your child when you input the ingredients you have. Divide the recipe name, recipe, and required ingredients into descriptions.
        };

        inputField.text = "";
        string startString = "�ȳ��ϼ���, �����Ǹ� ��õ�ص帮�ڽ��ϴ�";
        textField.text = startString;
        UnityEngine.Debug.Log(startString);
    }
    public async void GetResponse(Button button, string question)
    {
        button.enabled = false;

        //���� �޼����� inputField��
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User; //�� userMessage�� role�� User.
        //userMessage.Content = inputField.text;

        //�Է¹��� ��������� ���Բ� ��ȯ
        //userMessage.Content = UIManager.ingredients + "���� ���� �� �ִ� ���̿��� �� �� �ǰ��� ������ ��õ����";
        userMessage.Content = question; //2023_05_08: �Ű������� �޴� ������� ����

        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        UnityEngine.Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

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

        UpdateText(responseMessage);
        //GetImageURL.Insatnce.CrawlingImage(recipeName);

        Recipe.SetActive(true);


        UnityEngine.Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //������ message����Ʈ�� �߰�
        messages.Add(responseMessage);

        button.enabled = true;
        CrawlingImage(recipeName);
        UnityEngine.Debug.Log(recipeName);
        UnityEngine.Debug.Log(recipeName.GetType());

    }

    public void UpdateText(ChatMessage responseMessage)
    {
        //string�������� ����Ϸ��� responMessage.ToString()�� �ƴ϶�, responMessage.Content�� ����ϸ� �ȴ�
        string[] splitText = responseMessage.Content.Split(new string[] { "������ �̸�:", "���� ����:", "�ʿ��� ���:", "���̰� ���� �� ������ ��:" }, StringSplitOptions.None);
        recipeName = splitText[1]; //static
        string recipe = splitText[2];
        string ingredients = splitText[3];
        string caution = splitText[4];
        UnityEngine.Debug.Log("recipeName:" + recipeName);
        UnityEngine.Debug.Log("recipe:" + recipe);
        UnityEngine.Debug.Log("ingredients:" + ingredients);
        UnityEngine.Debug.Log("caution:" + caution);

        recipeNameField.text = recipeName;
        recipeField.text = string.Format("���� ����: \n{0}", recipe);
        ingredientsNeededField.text = string.Format("�ʿ��� ���: \n\n{0}", ingredients);
        cautionField.text = string.Format("���̰� ���� �� ������ ��: \n\n{0}", caution);

    }


    public RawImage targetRawImage;
    public void CrawlingImage(string recipe)
    {

        string pythonScriptPath = Application.dataPath + "/Script/URLwithPython/Python/RecipeSearch.py";
        string recipeName = recipe;

        // Python ��ũ��Ʈ ����
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        //UnityEngine.Debug.Log("���" + Path.Combine(Application.persistentDataPath, "recipe_image.jpg"));
        UnityEngine.Debug.Log("���" + Application.persistentDataPath + "/" + "recipe_image.jpg");
        startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\" \"{Path.Combine(Application.persistentDataPath, "recipe_image.jpg")}\"";
        //startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\" \"{Application.persistentDataPath + "/" + "recipe_image.jpg"}\"";
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        // Python ��ũ��Ʈ�� ��� �б�
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // �̹��� ���� �ε�
        string imagePath = Path.Combine(Application.persistentDataPath, "recipe_image.jpg");
        //string imagePath = Application.persistentDataPath +"/"+ "recipe_image.jpg";
        UnityEngine.Debug.Log(imagePath);
        UnityEngine.Debug.Log(File.Exists(imagePath));
        if (File.Exists(imagePath))
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Unity���� �̹��� ǥ��
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);

            // Assign the texture to the targetRawImage
            targetRawImage.texture = texture;

            // �̹��� ���� ����
            // File.Delete(imagePath);
        }
        else
        {
            UnityEngine.Debug.LogError("�̹��� ������ ã�� �� �����ϴ�.");
        }
    }
    public static string recipeName { get; set; }
}