﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleetKassa
{
    public class ObservableViewModelCollection<TViewModel, TModel> : ObservableCollection<TViewModel> where TViewModel : ViewModels.ViewModelBase<TModel> where TModel : class
    {
        public bool DisableSync { get; set; } = false;

        public Action<TViewModel> ItemAdded { get; set; }
        public Action<TViewModel> ItemRemoved { get; set; }

        protected IList<TModel> ModelCollection { get; set; }

        public ObservableViewModelCollection() : base()
        {
            CollectionChanged += Sync_Collection;
        }

        public ObservableViewModelCollection(IList<TModel> modelCollection = null, Func<TModel, TViewModel> vmConstructor = null) : this()
        {
            ModelCollection = modelCollection;
            if (ModelCollection != null && vmConstructor != null)
            {
                ExecuteWithSyncDisabled(() =>
                {
                    foreach (var item in ModelCollection)
                    {
                        Add(vmConstructor(item));
                    }
                });
            }
        }

        public ObservableViewModelCollection(Action<TViewModel> added = null,
                                             Action<TViewModel> removed = null, IList<TModel> modelCollection = null,
                                             Func<TModel, TViewModel> vmConstructor = null) : this(modelCollection, vmConstructor)
        {
            ItemAdded = added;
            ItemRemoved = removed;
        }

        public void ExecuteWithSyncDisabled(Action action)
        {
            DisableSync = true;
            action();
            DisableSync = false;
        }

        private void Sync_Collection(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (DisableSync) return;
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.Cast<TViewModel>())
                {
                    ItemAdded?.Invoke(item);
                    ModelCollection?.Add(item.Model);
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.Cast<TViewModel>())
                {
                    ItemRemoved?.Invoke(item);
                    ModelCollection?.Remove(item.Model);
                }
            }
        }
    }
}
