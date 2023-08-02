namespace gemini_server.Responses;

class SlowDownResponse : TemporaryFailureResponse
{
    private int _retryTimeout;

    public SlowDownResponse(int retryTimeout)
    {
        _retryTimeout = retryTimeout;
    }

    public override int GetStatusCode() => 44;

    public override string GetMeta() => _retryTimeout.ToString();
}