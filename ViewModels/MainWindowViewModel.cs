using AvaloniaSelectionProblemExample.Models;

using DynamicData;
using DynamicData.Binding;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;

namespace AvaloniaSelectionProblemExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly DataStorage _dataStorage;
        public Array Directions { get; set; }
        [Reactive]
        public SortDirection SortingDirection { get; set; }
        private readonly ReadOnlyObservableCollection<string> _itemsList;
        public ReadOnlyObservableCollection<string> ItemsList => _itemsList;
        [Reactive]
        public string? SelectedItem { get; set; }

        public MainWindowViewModel()
        {
            Directions = Enum.GetValues(typeof(SortDirection));

            _dataStorage = new DataStorage();

            var sort = this.WhenAnyValue(x => x.SortingDirection)
               .Select(BuildThemeSort!);

            _dataStorage.Characters
                .RefCount()
                .Sort(sort)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _itemsList)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(SelectedItem)));
        }

        private SortExpressionComparer<string> BuildThemeSort(SortDirection direction)
        {
            switch (direction)
            {
                case SortDirection.Ascending:
                    return SortExpressionComparer<string>.Ascending(v => v.Length);
                case SortDirection.Descending:
                    return SortExpressionComparer<string>.Descending(v => v.Length);
                default:
                    return SortExpressionComparer<string>.Descending(v => v.Length);
            }
            
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
