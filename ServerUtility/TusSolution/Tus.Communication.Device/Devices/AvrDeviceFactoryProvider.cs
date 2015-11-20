using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using Tus.Communication;

namespace Tus.Communication.Device.Composition
{
    class AvrDeviceFactoryProvider
    {
        private static Lazy<IDeviceFactory, IDeviceFactoryMetadataAttribute>[] container;
        public static Lazy<IEnumerable<Lazy<IDeviceFactory, IDeviceFactoryMetadataAttribute>>> Factories
            = new Lazy<IEnumerable<Lazy<IDeviceFactory, IDeviceFactoryMetadataAttribute>>>(() =>
                {
                    if (AvrDeviceFactoryProvider.container == null)
                    {
                        var catalog = new AggregateCatalog();
                        catalog.Catalogs.Add(new DirectoryCatalog(System.IO.Directory.GetCurrentDirectory()));

                        container = new CompositionContainer(catalog).GetExports<IDeviceFactory, IDeviceFactoryMetadataAttribute>().ToArray() ;

                    }
                    return container;
                });
    }
}
