﻿// Copyright 2008 - Paul den Dulk (Geodan)
// 
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.ComponentModel;
using BruTile;
using BruTile.Cache;
using BruTile.UI.Fetcher;
using System.Collections.Generic;

namespace BruTile.UI
{
    public class TileLayer<T> 
    {
        #region Fields

        TileSchema schema;
        TileFetcher<T> tileFetcher;
        MemoryCache<T> memoryCache = new MemoryCache<T>(7,64);
        List<BruTile.UI.Marker> markerCache = new List<BruTile.UI.Marker>();
        List<BruTile.UI.Marker> eventCache = new List<BruTile.UI.Marker>();
        List<System.Windows.Shapes.Line> lineCache = new List<System.Windows.Shapes.Line>();
        List<BruTile.UI.Ellipse> ellipseCache = new List<BruTile.UI.Ellipse>();

        List<BruTile.UI.Marker> killCache = new List<Marker>();

        const int maxRetries = 3;

        #endregion

        #region EventHandlers

        public event DataChangedEventHandler DataChanged;

        #endregion

        #region Properties

        public TileSchema Schema
        {
            //TODO: check if we need realy need this property
            get { return schema; }
        }

        public MemoryCache<T> MemoryCache
        {
            get { return memoryCache; }
        }

        public List<BruTile.UI.Marker> MarkerCache
        {
            get { return markerCache; }
        }

        public List<BruTile.UI.Marker> EventCache
        {
            get { return eventCache; }
        }

        public List<System.Windows.Shapes.Line> LineCache
        {
            get { return lineCache; }
        }

        public List<BruTile.UI.Ellipse> ellipsesCache
        {
            get { return ellipseCache;  }
        }

        public List<Marker> KillCache
        {
            get { return killCache; }
        }


        #endregion

        #region Constructors

        public TileLayer(TileSource source, ITileFactory<T> tileFactory)
        {
            this.schema = source.Schema;
            tileFetcher = new TileFetcher<T>(source, memoryCache, tileFactory);
            tileFetcher.DataChanged += new DataChangedEventHandler(tileFetcher_DataChanged);

        }

        ~TileLayer()
        {
        }

        #endregion

        #region Public Methods

        public void ViewChanged(Extent extent, double resolution)
        {
            tileFetcher.ViewChanged(extent, resolution);
        }

        /// <summary>
        /// Aborts the fetch of data that is currently in progress.
        /// With new ViewChanged calls the fetch will start again. 
        /// Call this method to speed up garbage collection
        /// </summary>
        public void AbortFetch()
        {
            if (tileFetcher != null)
            {
                tileFetcher.AbortFetch();
            }
        }

        #endregion

        #region Private Methods



        private void tileFetcher_DataChanged(object sender, DataChangedEventArgs e)
        {
            OnDataChanged(e);
        }

        private void OnDataChanged(DataChangedEventArgs e)
        {
            if (DataChanged != null)
                DataChanged(this, e);
        }
        #endregion
    }
}
