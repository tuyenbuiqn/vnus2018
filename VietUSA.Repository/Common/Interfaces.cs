namespace VietUSA.Repository.Common
{
    public interface IHasIdentityAndName
    {
        long Id { get; set; }
        string Name { get; set; }
        string Code { get; set; }
    }
}
