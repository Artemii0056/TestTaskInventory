namespace Services.RandomServices
{
    public interface IRandomService
    {
        int GenerateValue(int min, int max);
        int  GenerateValue(int max);
    }
}