using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Theme
{
    public class Theme
    {
        public string Name { get; set; }
        public BackgroundSettings Background { get; set; }
        public EditorSettings Editor { get; set; }
        public ColorSettings Colors { get; set; }
        public BorderSettings Border { get; set; }
        public ButtonSettings Button { get; set; }
        public InputSettings Input { get; set; }
        public SidebarSettings Sidebar { get; set; }
        public StatusBarSettings StatusBar { get; set; }
        public TabSettings Tabs { get; set; }
        public ScrollbarSettings Scrollbar { get; set; }
        public NotificationSettings Notifications { get; set; }

        public Theme()
        {
            Background = new BackgroundSettings();
            Editor = new EditorSettings();
            Colors = new ColorSettings();
            Border = new BorderSettings();
            Button = new ButtonSettings();
            Input = new InputSettings();
            Sidebar = new SidebarSettings();
            StatusBar = new StatusBarSettings();
            Tabs = new TabSettings();
            Scrollbar = new ScrollbarSettings();
            Notifications = new NotificationSettings();
        }
    }

    public class BackgroundSettings
    {
        public string Color { get; set; }
        public string Image { get; set; }
        public float ImageOpacity { get; set; }
        public string ImagePosition { get; set; }
        public string ImageRepeat { get; set; }
    }

    public class EditorSettings
    {
        public string FontFamily { get; set; }
        public float FontSize { get; set; }
        public float LineHeight { get; set; }
        public float LetterSpacing { get; set; }
        public string CursorColor { get; set; }
        public string CursorStyle { get; set; }
        public bool WordWrap { get; set; }
        public int TabSize { get; set; }
        public bool AutoIndent { get; set; }
        public bool HighlightActiveLine { get; set; }
        public bool HighlightCurrentLine { get; set; }
        public bool LineNumbers { get; set; }
    }

    public class ColorSettings
    {
        public string Foreground { get; set; }
        public string Background { get; set; }
        public string SelectionBackground { get; set; }
        public string LineHighlight { get; set; }
        public string Caret { get; set; }
        public string Comment { get; set; }
        public string Keyword { get; set; }
        public string String { get; set; }
        public string Number { get; set; }
        public string Function { get; set; }
        public string Variable { get; set; }
        public string Type { get; set; }
        public string Error { get; set; }
        public string Warning { get; set; }
    }

    public class BorderSettings
    {
        public string Color { get; set; }
        public string Width { get; set; }
        public string Style { get; set; }
    }

    public class ButtonSettings
    {
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public string BorderRadius { get; set; }
        public string HoverBackgroundColor { get; set; }
        public string HoverColor { get; set; }
    }

    public class InputSettings
    {
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
        public string BorderRadius { get; set; }
        public string FocusBorderColor { get; set; }
    }

    public class SidebarSettings
    {
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public string Width { get; set; }
        public BorderSettings Border { get; set; }

        public SidebarSettings()
        {
            Border = new BorderSettings();
        }
    }

    public class StatusBarSettings
    {
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
    }

    public class TabSettings
    {
        public string BackgroundColor { get; set; }
        public string ActiveColor { get; set; }
        public string InactiveColor { get; set; }
        public string BorderColor { get; set; }
    }

    public class ScrollbarSettings
    {
        public string Width { get; set; }
        public string Color { get; set; }
        public string HoverColor { get; set; }
    }

    public class NotificationSettings
    {
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public string ErrorBackgroundColor { get; set; }
        public string ErrorColor { get; set; }
        public string WarningBackgroundColor { get; set; }
        public string WarningColor { get; set; }
    }
}
