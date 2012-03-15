namespace MicropostMVC.BusinessServices
{
    public interface IHashSalt
    {
        string Hash { get; }
        string Salt { get; }
    }
}