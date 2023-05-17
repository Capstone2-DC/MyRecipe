using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using OpenAI_API.Moderation;

public class GoogleCustomSearch : MonoBehaviour
{
    public string apiKey; // Google Custom Search JSON API 키
    public string searchEngineId; // 검색 엔진 ID
    public string searchQuery; // 검색어

    public RawImage imageDisplay; // 이미지를 표시할 RawImage 컴포넌트

    private void Start()
    {
        // 검색 요청
        SearchImages();
    }

    private void SearchImages()
    {
        // API 요청 URL 생성
        string url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={searchEngineId}&q={searchQuery}&searchType=image";

        // UnityWebRequest 생성
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 요청 및 결과 처리
        StartCoroutine(SendRequest(request));
    }

    private IEnumerator SendRequest(UnityWebRequest request)
    {
        // 요청 보내기
        yield return request.SendWebRequest();

        // 요청이 완료되었고 에러가 없는 경우
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)

        //!request.isNetworkError && !request.isHttpError는 사용되지 않음.
        //request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError 를 대신 사용
        {
            // API 응답 데이터 파싱
            string responseText = request.downloadHandler.text;
            SearchResponse response = JsonUtility.FromJson<SearchResponse>(responseText);

            // 이미지 URL 가져오기
            string imageUrl = response.items[0].link;

            // 이미지 다운로드
            UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return imageRequest.SendWebRequest();

            // 다운로드 완료 및 표시
            if (imageRequest.result != UnityWebRequest.Result.ConnectionError && imageRequest.result != UnityWebRequest.Result.ProtocolError)
            //!imageRequest.isNetworkError && !imageRequest.isHttpError
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
                imageDisplay.texture = texture;
            }
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    // 검색 결과를 저장할 데이터 모델 클래스
    [System.Serializable]
    private class SearchResponse
    {
        public ImageResult[] items;
    }

    // 이미지 결과를 저장할 데이터 모델 클래스
    [System.Serializable]
    private class ImageResult
    {
        public string link;
    }
}
