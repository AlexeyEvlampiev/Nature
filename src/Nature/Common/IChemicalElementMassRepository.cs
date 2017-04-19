namespace Nature.Common
{
    public interface IChemicalElementMassRepository
    {
        bool HasMass(string elementCode);

        double GetMolarMass(string elementCode);
    }
}
