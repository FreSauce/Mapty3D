using System;
using System.Collections.Generic;

namespace Mapzen.VectorData.Formats
{
    using PbfFeature = Mvtcs.Tile.Types.Feature;
    using PbfLayer = Mvtcs.Tile.Types.Layer;

    public class MvtFeatureCollection : FeatureCollection
    {
        private PbfLayer layer;

        public MvtFeatureCollection(PbfLayer layer)
        {
            this.layer = layer;
        }

        public override string Name { get { return layer.Name; } }

        public override IEnumerable<Feature> Features
        {
            get
            {
                foreach (var pbfFeature in layer.Features)
                {
                    yield return new MvtFeature(layer, pbfFeature);
                }
            }
        }
    }
}
