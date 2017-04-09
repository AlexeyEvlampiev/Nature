namespace Nature.Chemkin
{
    public interface IDeserializationContext
    {
        IDeserializationSession GetSession();

        IDeserializationDiagnosticsCallack GetDiagnosticsCallback();

        IFormatInfo GetFormatInfo();
    }
}
