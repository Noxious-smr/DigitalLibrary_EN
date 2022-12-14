#pragma checksum "..\..\..\..\Views\CategoriesView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48C20058716D2A5EDB4978E9212BB4F24D3C77F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using UI_Layer.Views;


namespace UI_Layer.Views {
    
    
    /// <summary>
    /// CategoriesView
    /// </summary>
    public partial class CategoriesView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 341 "..\..\..\..\Views\CategoriesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border mainBorder;
        
        #line default
        #line hidden
        
        
        #line 342 "..\..\..\..\Views\CategoriesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView CategoriesList;
        
        #line default
        #line hidden
        
        
        #line 513 "..\..\..\..\Views\CategoriesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox BooksList;
        
        #line default
        #line hidden
        
        
        #line 602 "..\..\..\..\Views\CategoriesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SideBarInfo;
        
        #line default
        #line hidden
        
        
        #line 627 "..\..\..\..\Views\CategoriesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid InfoGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UI_Layer;component/views/categoriesview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\CategoriesView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mainBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.CategoriesList = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.BooksList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.SideBarInfo = ((System.Windows.Controls.Grid)(target));
            
            #line 602 "..\..\..\..\Views\CategoriesView.xaml"
            this.SideBarInfo.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.SideBarInfo_DataContextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 626 "..\..\..\..\Views\CategoriesView.xaml"
            ((System.Windows.Controls.TextBlock)(target)).DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.TextBlock_DataContextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.InfoGrid = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 3:
            
            #line 358 "..\..\..\..\Views\CategoriesView.xaml"
            ((System.Windows.Controls.Primitives.ToggleButton)(target)).Click += new System.Windows.RoutedEventHandler(this.CategorisButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

