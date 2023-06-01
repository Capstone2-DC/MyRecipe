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
        //startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\" \"{Path.Combine(Application.persistentDataPath, "recipe_image.jpg")}\"";
        startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\" \"{Application.persistentDataPath + "/" + "recipe_image.jpg"}\"";
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
            File.Delete(imagePath);
        }
        else
        {
            UnityEngine.Debug.LogError("�̹��� ������ ã�� �� �����ϴ�.");
        }
    }
}
