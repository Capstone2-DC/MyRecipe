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
        // string recipeName = "제육볶음";
        string recipeName = recipe;

        // Python 스크립트 실행
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        startInfo.Arguments = $"{pythonScriptPath} \"{recipeName}\"";
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
        UnityEngine.Debug.Log(imagePath);
        UnityEngine.Debug.Log(File.Exists(imagePath));
        if (File.Exists(imagePath))
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Unity에서 이미지 표시
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);

            // 이미지를 원하는 곳에 표시하는 코드 작성
            // 예시: RawImage 컴포넌트를 가진 GameObject에 텍스처를 할당하여 이미지를 표시
            RawImage rawImage = GetComponent<RawImage>();
            rawImage.texture = texture;

            // 이미지 파일 삭제
           File.Delete(imagePath);
        }
        else
        {
            UnityEngine.Debug.LogError("이미지 파일을 찾을 수 없습니다.");
        }
    }
}
