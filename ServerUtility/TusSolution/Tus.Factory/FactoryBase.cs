using System.ComponentModel.Composition;
using Tus.Communication;

namespace Tus.Factory
{
    [InheritedExport]
    public abstract class FactoryBase <T>
    {
        [Import]
        public IConsoleApplicationSettings ApplicationSettings{get;set;}

        public abstract T Create();
    }
}