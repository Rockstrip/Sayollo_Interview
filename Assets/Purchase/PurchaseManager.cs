using System.Net.Http;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private RawImage rawImage;
    [Header("InputFields")]
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField cardNumber;
    [SerializeField] private TMP_InputField expirationDate;
    
    private const string PostPurchaseURL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/ad";
    private const string PostUserInfoURL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/action";

    public async void ShowInfo()
    {
        var stringContent = new StringContent(JsonUtility.ToJson(new object()), Encoding.UTF8, "application/json");
        
        var response = await RequestManager.Post(PostPurchaseURL, stringContent);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            data = data.Replace('\'', '\"');
            var purchaseItemResponse = JsonUtility.FromJson<PurchaseItemResponse>(data);
            GenerateDescription(new PurchaseItem(purchaseItemResponse));

            content.SetActive(true);
            Debug.Log("ShowInfo: " + response.StatusCode);
        }
        else
        {
            Debug.LogError(response.StatusCode);
        }
    }
    
    private async void GenerateDescription(PurchaseItem purchaseItem)
    {
        textMeshProUGUI.text = $"{purchaseItem.title}\nPrice: {purchaseItem.price} {purchaseItem.currency}";
        var context = await RequestManager.GetByteArray(purchaseItem.item_image);
        var texture2D = new Texture2D(1,1, TextureFormat.RGB24, false);
        texture2D.LoadImage(context);
        rawImage.texture = texture2D;
    }

    public async void Buy()
    {
        var userInfo = new UserInfo(email.text, cardNumber.text, expirationDate.text);
        var content = new StringContent(JsonUtility.ToJson(userInfo), Encoding.UTF8, "application/json");
        var response = await RequestManager.Post(PostUserInfoURL, content);
        if (response.IsSuccessStatusCode)
            Debug.Log("Buy Status: " + response.StatusCode);
        else
            Debug.LogError(response.StatusCode);
    }
}