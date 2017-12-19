using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using DataGrid = Microsoft.Windows.Controls.DataGrid;
using DataGridColumnHeader = Microsoft.Windows.Controls.Primitives.DataGridColumnHeader;

// Other

namespace WpfApp.FilterDataGrid
{
    /// <summary>
    ///     A grid that makes inline filtering possible.
    /// </summary>
    public class FilteringDataGrid : DataGrid
    {
        /// <summary>
        ///     Case sensitive filtering
        /// </summary>
        public static DependencyProperty IsFilteringCaseSensitiveProperty =
            DependencyProperty.Register("IsFilteringCaseSensitive", typeof(bool), typeof(FilteringDataGrid),
                new PropertyMetadata(true));

        /// <summary>
        ///     This dictionary will have a list of all applied filters
        /// </summary>
        private readonly Dictionary<string, string> columnFilters;

        /// <summary>
        ///     Cache with properties for better performance
        /// </summary>
        private readonly Dictionary<string, PropertyInfo> propertyCache;

        /// <summary>
        ///     Register for all text changed events
        /// </summary>
        public FilteringDataGrid()
        {
            // Initialize lists
            columnFilters = new Dictionary<string, string>();
            propertyCache = new Dictionary<string, PropertyInfo>();

            // Add a handler for all text changes
            AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(OnTextChanged), true);

            // Datacontext changed, so clear the cache
            DataContextChanged += FilteringDataGrid_DataContextChanged;
        }

        /// <summary>
        ///     Case sensitive filtering
        /// </summary>
        public bool IsFilteringCaseSensitive
        {
            get => (bool) GetValue(IsFilteringCaseSensitiveProperty);
            set => SetValue(IsFilteringCaseSensitiveProperty, value);
        }

        /// <summary>
        ///     Clear the property cache if the datacontext changes.
        ///     This could indicate that an other type of object is bound.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilteringDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            propertyCache.Clear();
        }

        /// <summary>
        ///     When a text changes, it might be required to filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the textbox
            var filterTextBox = e.OriginalSource as TextBox;

            // Get the header of the textbox
            var header = TryFindParent<DataGridColumnHeader>(filterTextBox);
            if (header != null)
            {
                UpdateFilter(filterTextBox, header);
                ApplyFilters();
            }
        }

        /// <summary>
        ///     Update the internal filter
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="header"></param>
        private void UpdateFilter(TextBox textBox, DataGridColumnHeader header)
        {
            // Try to get the property bound to the column.
            // This should be stored as datacontext.
            var columnBinding = header.DataContext?.ToString() ?? "";

            // Set the filter 
            if (!string.IsNullOrEmpty(columnBinding))
                columnFilters[columnBinding] = textBox.Text;
        }

        /// <summary>
        ///     Apply the filters
        /// </summary>
        /// <param name="border"></param>
        private void ApplyFilters()
        {
            // Get the view
            var view = CollectionViewSource.GetDefaultView(ItemsSource);
            if (view != null)
                view.Filter = delegate(object item)
                {
                    // Show the current object
                    var show = true;

                    // Loop filters
                    foreach (var filter in columnFilters)
                    {
                        var property = GetPropertyValue(item, filter.Key);
                        if (property != null)
                        {
                            // Check if the current column contains a filter
                            var containsFilter = false;
                            if (IsFilteringCaseSensitive)
                                containsFilter = property.ToString().Contains(filter.Value);
                            else
                                containsFilter = property.ToString().ToLower().Contains(filter.Value.ToLower());

                            // Do the necessary things if the filter is not correct
                            if (!containsFilter)
                            {
                                show = false;
                                break;
                            }
                        }
                    }

                    // Return if it's visible or not
                    return show;
                };
        }

        /// <summary>
        ///     Get the value of a property
        /// </summary>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private object GetPropertyValue(object item, string property)
        {
            // No value
            object value = null;

            // Get property  from cache
            PropertyInfo pi = null;
            if (propertyCache.ContainsKey(property))
            {
                pi = propertyCache[property];
            }
            else
            {
                pi = item.GetType().GetProperty(property);
                propertyCache.Add(property, pi);
            }

            // If we have a valid property, get the value
            if (pi != null)
                value = pi.GetValue(item, null);

            // Done
            return value;
        }

        /// <summary>
        ///     Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the queried item.</param>
        /// <returns>
        ///     The first parent item that matches the submitted
        ///     type parameter. If not matching item can be found, a null reference is being returned.
        /// </returns>
        public static T TryFindParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            //get parent item
            var parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            if (parentObject is T parent)
                return parent;
            return TryFindParent<T>(parentObject);
        }

        /// <summary>
        ///     This method is an alternative to WPF's
        ///     <see cref="VisualTreeHelper.GetParent" /> method, which also
        ///     supports content elements. Do note, that for content element,
        ///     this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise null.</returns>
        public static DependencyObject GetParentObject(DependencyObject child)
        {
            if (child == null) return null;

            if (child is ContentElement contentElement)
            {
                var parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                return contentElement is FrameworkContentElement fce ? fce.Parent : null;
            }

            // If it's not a ContentElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }
    }
}