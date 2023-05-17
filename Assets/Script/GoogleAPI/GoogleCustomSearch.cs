using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using OpenAI_API.Moderation;

public class GoogleCustomSearch : MonoBehaviour
{
    public string apiKey; // Google Custom Search JSON API Ű
    public string searchEngineId; // �˻� ���� ID
    public string searchQuery; // �˻���

    public RawImage imageDisplay; // �̹����� ǥ���� RawImage ������Ʈ

    private void Start()
    {
        // �˻� ��û
        SearchImages();
    }

    private void SearchImages()
    {
        // API ��û URL ����
        string url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={searchEngineId}&q={searchQuery}&searchType=image";

        // UnityWebRequest ����
        UnityWebRequest request = UnityWebRequest.Get(url);

        // ��û �� ��� ó��
        StartCoroutine(SendRequest(request));
    }

    private IEnumerator SendRequest(UnityWebRequest request)
    {
        // ��û ������
        yield return request.SendWebRequest();

        // ��û�� �Ϸ�Ǿ��� ������ ���� ���
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)

        //!request.isNetworkError && !request.isHttpError�� ������ ����.
        //request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError �� ��� ���
        {
            // API ���� ������ �Ľ�
            string responseText = request.downloadHandler.text;
            SearchResponse response = JsonUtility.FromJson<SearchResponse>(responseText);

            // �̹��� URL ��������
            string imageUrl = response.items[0].link;

            // �̹��� �ٿ�ε�
            UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return imageRequest.SendWebRequest();

            // �ٿ�ε� �Ϸ� �� ǥ��
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

    // �˻� ����� ������ ������ �� Ŭ����
    [System.Serializable]
    private class SearchResponse
    {
        public ImageResult[] items;
    }

    // �̹��� ����� ������ ������ �� Ŭ����
    [System.Serializable]
    private class ImageResult
    {
        public string link;
    }
}
