using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    private static readonly HttpClient _client = new();

    public static async Task<string> GetString(string url)
    {
        return await _client.GetStringAsync(url);
    }   
    public static async Task<byte[]> GetByteArray(string url)
    {
        return await _client.GetByteArrayAsync(url);
    }    
    public static async Task<HttpResponseMessage> Post(string url, StringContent stringContent)
    {
        return await _client.PostAsync(url, stringContent);
    }
}
