using Nature.Common;

namespace Nature.Chemkin
{
    public interface IFormatInfo
    {
        bool IsValidElementId(string value);

        bool IsRealNumber(string value);

        double ToDouble(string value);        

        bool IsElectronId(string id);

        bool IsValidPhaseIdentifier(string phaseId);
        ThermodynamicPhase ParseThermodynamicPhase(string value);
    }
}
