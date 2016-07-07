﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.Controls
{
    [ContentProperty("ItemsSource")]
    [DefaultEvent("SelectionChanged"),
     TemplatePart(Name = "PART_Container", Type = typeof(Grid)),
     TemplatePart(Name = "PART_Button", Type = typeof(Button)),
     TemplatePart(Name = "PART_ButtonContent", Type = typeof(ContentControl)),
     TemplatePart(Name = "PART_Popup", Type = typeof(Popup)),
     TemplatePart(Name = "PART_Expander", Type = typeof(Button)),
     TemplatePart(Name = "PART_ListBox", Type = typeof(ListBox))]
    public class SplitButton : ItemsControl
    {
        public static readonly RoutedEvent ClickEvent
            = EventManager.RegisterRoutedEvent("Click",
                                               RoutingStrategy.Bubble,
                                               typeof(RoutedEventHandler),
                                               typeof(SplitButton));

        public static readonly RoutedEvent SelectionChangedEvent
            = EventManager.RegisterRoutedEvent("SelectionChanged",
                                               RoutingStrategy.Bubble,
                                               typeof(SelectionChangedEventHandler),
                                               typeof(SplitButton));

        public event SelectionChangedEventHandler SelectionChanged
        {
            add { this.AddHandler(SelectionChangedEvent, value); }
            remove { this.RemoveHandler(SelectionChangedEvent, value); }
        }

        public event RoutedEventHandler Click
        {
            add { this.AddHandler(ClickEvent, value); }
            remove { this.RemoveHandler(ClickEvent, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(SplitButton));

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(SplitButton), new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(SplitButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ExtraTagProperty = DependencyProperty.Register("ExtraTag", typeof(object), typeof(SplitButton));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(SplitButton), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(SplitButton));
        public static readonly DependencyProperty IconTemplateProperty = DependencyProperty.Register("IconTemplate", typeof(DataTemplate), typeof(SplitButton));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SplitButton));
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(SplitButton));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(SplitButton));

        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(SplitButton), new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ButtonArrowStyleProperty = DependencyProperty.Register("ButtonArrowStyle", typeof(Style), typeof(SplitButton), new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ListBoxStyleProperty = DependencyProperty.Register("ListBoxStyle", typeof(Style), typeof(SplitButton), new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ArrowBrushProperty = DependencyProperty.Register("ArrowBrush", typeof(Brush), typeof(SplitButton), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Reflects the parameter to pass to the CommandProperty upon execution. 
        /// </summary>
        public object CommandParameter
        {
            get { return (object)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the target element on which to fire the command.
        /// </summary>
        public IInputElement CommandTarget
        {
            get { return (IInputElement)this.GetValue(CommandTargetProperty); }
            set { this.SetValue(CommandTargetProperty, value); }
        }

        /// <summary>
        /// Get or sets the Command property. 
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary> 
        ///  The index of the first item in the current selection or -1 if the selection is empty. 
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { this.SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        ///  The first item in the current selection, or null if the selection is empty. 
        /// </summary>
        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        /// <summary> 
        /// Indicates whether the Popup is visible. 
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)this.GetValue(IsExpandedProperty); }
            set { this.SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// Gets or sets an extra tag.
        /// </summary>
        public object ExtraTag
        {
            get { return this.GetValue(ExtraTagProperty); }
            set { this.SetValue(ExtraTagProperty, value); }
        }

        /// <summary>
        /// Gets or sets the dimension of children stacking.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)this.GetValue(OrientationProperty); }
            set { this.SetValue(OrientationProperty, value); }
        }

        /// <summary>
        ///  Gets or sets the Content used to generate the icon part.
        /// </summary>
        [Bindable(true)]
        public object Icon
        {
            get { return this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }

        /// <summary> 
        /// Gets or sets the ContentTemplate used to display the content of the icon part. 
        /// </summary>
        [Bindable(true)]
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)this.GetValue(IconTemplateProperty); }
            set { this.SetValue(IconTemplateProperty, value); }
        }

        /// <summary>
        /// Gets/sets the button style.
        /// </summary>
        public Style ButtonStyle
        {
            get { return (Style)this.GetValue(ButtonStyleProperty); }
            set { this.SetValue(ButtonStyleProperty, value); }
        }

        /// <summary>
        /// Gets/sets the button arrow style.
        /// </summary>
        public Style ButtonArrowStyle
        {
            get { return (Style)this.GetValue(ButtonArrowStyleProperty); }
            set { this.SetValue(ButtonArrowStyleProperty, value); }
        }

        /// <summary>
        /// Gets/sets the popup listbox style.
        /// </summary>
        public Style ListBoxStyle
        {
            get { return (Style)this.GetValue(ListBoxStyleProperty); }
            set { this.SetValue(ListBoxStyleProperty, value); }
        }

        /// <summary>
        /// Gets/sets the brush of the button arrow icon.
        /// </summary>
        public Brush ArrowBrush
        {
            get { return (Brush)this.GetValue(ArrowBrushProperty); }
            set { this.SetValue(ArrowBrushProperty, value); }
        }

        private Button _clickButton;
        private Button _expander;
        private ListBox _listBox;
        private Popup _popup;

        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            e.RoutedEvent = ClickEvent;
            this.RaiseEvent(e);
        }

        private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.RoutedEvent = SelectionChangedEvent;
            this.RaiseEvent(e);

            this.IsExpanded = false;
        }

        private void ExpanderClick(object sender, RoutedEventArgs e)
        {
            this.IsExpanded = !this.IsExpanded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._clickButton = this.EnforceInstance<Button>("PART_Button");
            this._expander = this.EnforceInstance<Button>("PART_Expander");
            this._listBox = this.EnforceInstance<ListBox>("PART_ListBox");
            this._popup = this.EnforceInstance<Popup>("PART_Popup");
            this.InitializeVisualElementsContainer();
        }

        //Get element from name. If it exist then element instance return, if not, new will be created
        private T EnforceInstance<T>(string partName) where T : FrameworkElement, new()
        {
            T element = this.GetTemplateChild(partName) as T ?? new T();
            return element;
        }

        private void InitializeVisualElementsContainer()
        {
            this._expander.Click -= this.ExpanderClick;
            this._clickButton.Click -= this.ButtonClick;
            this._listBox.SelectionChanged -= this.ListBoxSelectionChanged;
            this._listBox.PreviewMouseLeftButtonDown -= this.ListBoxPreviewMouseLeftButtonDown;
            this._popup.Opened -= this.PopupOpened;
            this._popup.Closed -= this.PopupClosed;

            this._expander.Click += this.ExpanderClick;
            this._clickButton.Click += this.ButtonClick;
            this._listBox.SelectionChanged += this.ListBoxSelectionChanged;
            this._listBox.PreviewMouseLeftButtonDown += this.ListBoxPreviewMouseLeftButtonDown;
            this._popup.Opened += this.PopupOpened;
            this._popup.Closed += this.PopupClosed;
        }

        //Make popup close even if no selectionchanged event fired (case when user select the save item as before)
        private void ListBoxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as DependencyObject;
            if (source != null)
            {
                var item = ContainerFromElement(this._listBox, source) as ListBoxItem;
                if (item != null)
                {
                    this.IsExpanded = false;
                }
            }
        }

        private void PopupClosed(object sender, EventArgs e)
        {
            this.ReleaseMouseCapture();
            this._expander?.Focus();
        }

        private void PopupOpened(object sender, EventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, this.OutsideCapturedElementHandler);
        }

        private void OutsideCapturedElementHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            this.IsExpanded = false;
            Mouse.RemovePreviewMouseDownOutsideCapturedElementHandler(this, this.OutsideCapturedElementHandler);
        }
    }
}