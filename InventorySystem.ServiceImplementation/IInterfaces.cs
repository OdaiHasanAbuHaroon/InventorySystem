using System.Reflection;

namespace InventorySystem.ServiceImplementation
{
    public interface IIScoped { }
    public interface IISingleton { }
    public interface IITransient { }

    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
