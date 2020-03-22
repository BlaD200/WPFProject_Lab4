﻿using System.Collections.Generic;

namespace WPFProject_Lab4.Tools.Navigation
{
    internal abstract class BaseNavigationModel :  INavigationModel
    {
        private readonly IContentOwner _contentOwner;
        private readonly Dictionary<ViewType, INavigatable> _viewsDictionary;

        protected BaseNavigationModel(IContentOwner contentOwner)
        {
            _contentOwner = contentOwner;
            _viewsDictionary = new Dictionary<ViewType, INavigatable>();
        }

        protected void AddView(ViewType viewType, INavigatable iNavigatable)
        {
            if (_viewsDictionary.ContainsKey(viewType))
                _viewsDictionary.Remove(viewType);

            _viewsDictionary.Add(viewType, iNavigatable);
        }

        public void Navigate(ViewType viewType)
        {
            if (!_viewsDictionary.ContainsKey(viewType) || viewType == ViewType.Main || viewType == ViewType.EditUser)
            {
                InitializeView(viewType);
            }


            _contentOwner.Content = _viewsDictionary[viewType];
        }

        protected abstract void InitializeView(ViewType viewType);
    }
}
