﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace XLib.AvaloniaControl;

public partial class TreeItemView : UserControl
{
    public TreeItemView()
    {
        InitializeComponent();
        Toggle.Checked += (_, _) =>
        {
            if (Instance != null) Instance.IsExpanded = true;
            OnItemExpand?.Invoke();
        };
        Toggle.Unchecked += (_, _) =>
        {
            if (Instance != null) Instance.IsExpanded = false;
            OnItemFurl?.Invoke();
        };
    }

    #region 属性

    /// <summary>树项实例</summary>
    public TreeItem? Instance { get; set; } = null;

    #endregion

    #region 事件

    /// <summary>当项展开时</summary>
    public Action? OnItemExpand { get; set; }

    /// <summary>当项折叠时</summary>
    public Action? OnItemFurl { get; set; }

    /// <summary>当命中项时</summary>
    public Action<TreeItem, KeyModifiers>? OnItemHited { get; set; }

    /// <summary>双击项时</summary>
    public Action<TreeItem>? OnDoubleClick { get; set; }

    #endregion

    #region 公开方法

    public void Update()
    {
        // 实例不为空
        if (Instance != null)
        {
            // 显示控件
            IsVisible = true;
            // 背景色
            UpdateBackground();
            // 左边距
            MainGrid.Margin = new Thickness(Instance.Deep * 23, 0, 0, 0);
            // 折叠按钮
            Toggle.IsVisible = Instance.ItemList.Count > 0;
            Toggle.IsChecked = Instance.IsExpanded;
            // 图标
            Image_Icon.Source = Instance.Icon;
            // 标题
            TB_Title.Text = Instance.Content.ToString();
        }
        // 实例为空，则隐藏控件
        else IsVisible = false;
    }

    #endregion

    #region 控件事件

    private void Back_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        _isMouseHover = true;
        UpdateBackground();
    }

    private void Back_OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        _isMouseHover = false;
        UpdateBackground();
    }

    private void Back_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (Instance != null)
        {
            OnItemHited?.Invoke(Instance, e.KeyModifiers);
            // 双击时切换折叠按钮
            if (e.ClickCount == 2 && Instance.ItemList.Count > 0)
            {
                if (!Instance.IsFolder) OnDoubleClick?.Invoke(Instance);
                else Toggle.IsChecked = !Toggle.IsChecked;
            }
        }

        e.Handled = true;
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 更新背景
    /// </summary>
    private void UpdateBackground()
    {
        Back.Background = _default;
        if (_isMouseHover) Back.Background = _hover;
        if (Instance != null && Instance.Selected) Back.Background = _selected;
    }

    #endregion

    #region 字段

    /// <summary>鼠标悬停</summary>
    private bool _isMouseHover = false;

    /// <summary>默认背景</summary>
    private readonly IBrush _default = Brushes.Transparent;

    /// <summary>悬停时背景</summary>
    private readonly Brush _hover = new SolidColorBrush(Color.FromArgb(25, 255, 255, 255));

    /// <summary>选中时背景</summary>
    private readonly Brush _selected = new SolidColorBrush(Color.FromArgb(51, 255, 255, 255));

    #endregion
}