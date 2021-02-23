using System.Threading.Tasks;

namespace Model
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
