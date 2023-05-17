using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetImageURL : MonoBehaviour
{
  /*  public static GetImageURL Insatnce { get; private set; }
    private void Awake()
    {
        Insatnce = this;
    }*/

    public void CrawlingImage(string recipe)
    {
        UnityEngine.Debug.Log(recipe);
        string pythonScriptPath = Application.dataPath + "/Script/URLwithPython/Python/RecipeSearch.py";
        // string recipeName = "��������";
        string recipeName = recipe;

        // Python ��ũ��Ʈ ����
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\"";
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
        UnityEngine.Debug.Log(imagePath);
        UnityEngine.Debug.Log(File.Exists(imagePath));
        if (File.Exists(imagePath))
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Unity���� �̹��� ǥ��
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);

            // �̹����� ���ϴ� ���� ǥ���ϴ� �ڵ� �ۼ�
            // ����: RawImage ������Ʈ�� ���� GameObject�� �ؽ�ó�� �Ҵ��Ͽ� �̹����� ǥ��
            RawImage rawImage = GetComponent<RawImage>();
            rawImage.texture = texture;

            // �̹��� ���� ����
           File.Delete(imagePath);
        }
        else
        {
            UnityEngine.Debug.LogError("�̹��� ������ ã�� �� �����ϴ�.");
        }
    }
}
