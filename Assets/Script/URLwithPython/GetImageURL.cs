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

        // Python 스크립트 실행
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        //UnityEngine.Debug.Log("경로" + Path.Combine(Application.persistentDataPath, "recipe_image.jpg"));
        UnityEngine.Debug.Log("경로" + Application.persistentDataPath + "/" + "recipe_image.jpg");
        //startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\" \"{Path.Combine(Application.persistentDataPath, "recipe_image.jpg")}\"";
        startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\" \"{Application.persistentDataPath + "/" + "recipe_image.jpg"}\"";
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        // Python 스크립트의 출력 읽기
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // 이미지 파일 로드
        string imagePath = Path.Combine(Application.persistentDataPath, "recipe_image.jpg");
        //string imagePath = Application.persistentDataPath +"/"+ "recipe_image.jpg";
        UnityEngine.Debug.Log(imagePath);
        UnityEngine.Debug.Log(File.Exists(imagePath));
        if (File.Exists(imagePath))
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Unity에서 이미지 표시
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);

            // Assign the texture to the targetRawImage
            targetRawImage.texture = texture;

            // 이미지 파일 삭제
            File.Delete(imagePath);
        }
        else
        {
            UnityEngine.Debug.LogError("이미지 파일을 찾을 수 없습니다.");
        }
    }
}
