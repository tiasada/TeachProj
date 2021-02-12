namespace Domain.Common
{
    public interface ICrypt
    {
        string CreateMD5(string input);
    }
}