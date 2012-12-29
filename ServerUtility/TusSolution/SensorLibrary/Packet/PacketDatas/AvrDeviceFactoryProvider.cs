using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using SensorLibrary.Devices.BasicDevices;
using SensorLibrary.Devices.Factories;

namespace SensorLibrary.Devices
{
    class AvrDeviceFactoryProvider
    {
        public static Lazy<IEnumerable<Lazy<IDeviceFactory, IDeviceFactoryMetadataAttribute>>> Factories
            = new Lazy<IEnumerable<Lazy<IDeviceFactory,IDeviceFactoryMetadataAttribute>>>(() =>
                {
                    var catalog = new AggregateCatalog();
                    catalog.Catalogs.Add(new DirectoryCatalog(System.IO.Directory.GetCurrentDirectory()));

                    var container = new CompositionContainer(catalog);
                    return container.GetExports<IDeviceFactory, IDeviceFactoryMetadataAttribute>();
                });
    }
}
