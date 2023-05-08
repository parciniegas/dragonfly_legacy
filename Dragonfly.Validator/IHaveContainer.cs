
namespace Dragonfly.Validator
{
    public interface IHaveContainer
    {
        string ContainerName { get;}
        void UpdateContainerName(string containerName);
    }
}
