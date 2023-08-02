namespace gemini_server.Responses;

class SlowDown : TemporaryFailureResponse
{
    private int _retryTimeout;

    public SlowDown(int retryTimeout)
    {
        _retryTimeout = retryTimeout;
    }

    public override int GetStatusCode() => 44;

    public override string GetMeta() => _retryTimeout.ToString();
}