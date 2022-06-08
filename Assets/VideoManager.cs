using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    private const string GetVastURL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad/vast";
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    public async void PlayVideo()
    {
        var vast = await RequestManager.GetString(GetVastURL);
        _videoPlayer.url = VideoFromVast(vast);
        _videoPlayer.Play();
        Debug.Log("Video is Playing");
    }

    public async void DownloadVideo()
    {
        var vast = await RequestManager.GetString(GetVastURL);
        var videoUrl = VideoFromVast(vast);
        
        var videoName = videoUrl.Split('/').Last();
        var content = await RequestManager.GetByteArray(videoUrl);
        await File.WriteAllBytesAsync(Application.persistentDataPath + "/" + videoName, content);
        
        Debug.Log("Video is downloaded");
    }
    
    private string VideoFromVast(string vast)
    {
        const string pattern = "^.*<MediaFile><!\\[CDATA\\[(.*)]]><\\/MediaFile>.*$";
        var match = Regex.Matches(vast, pattern, RegexOptions.IgnoreCase);
        if (match.Count > 0 && match[0].Groups.Count > 1)
        {
            return match[0].Groups[1].Value;
        }
        else
        {
            Debug.LogError("Invalid VAST");
            return null;
        }
    }
}