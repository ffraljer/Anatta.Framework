namespace Anatta.Framework.Sound;

public class Sample : IDisposable
{
    private readonly byte[] _data;
    private int _lastHandle;

    public Sample(byte[] data)
    {
        this._data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public static Sample Load(string resource)
    {
        return new Sample(Audio.LoadAudio(resource));
    }

    public int Play()
    {
        _lastHandle = Audio.Play(_data);
        return _lastHandle;
    }

    public void Stop()
    {
        if (_lastHandle != 0)
            Audio.Stop(_lastHandle);
    }

    public void Dispose()
    {
        Stop();
    }
}