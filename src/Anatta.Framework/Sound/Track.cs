using Anatta.Framework.Graphics.Managers;

namespace Anatta.Framework.Sound;

public class Track : IDisposable
{
    private readonly byte[] _data;
    private int _handle;

    public bool Loop { get; set; }

    public Track(byte[] data)
    {
        this._data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public static Track Load(string resource)
    {
        return new Track(Audio.LoadAudio(resource));
    }

    public void Play() {
        Console.WriteLine("[Track] Track Played");
        _handle = Audio.Play(_data, Loop);
    }

    public void Stop()
    {
        if (_handle != 0)
        {
            Audio.Stop(_handle);
            _handle = 0;
        }
    }

    public void Restart()
    {
        Stop();
        Play();
    }

    public bool IsPlaying => _handle != 0;

    public void Dispose()
    {
        Stop();
    }
}