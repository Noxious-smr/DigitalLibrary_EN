   M S B A M L   `   `   `  ????C  ?UI_Layer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:UI_Layer.ViewsUI_Layer.Views  5      9!clr-namespace:UI_Layer.ViewModelsUI_Layer.ViewModels  5      F BDomainLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullL5clr-namespace:DomainLayer.Models;assembly=DomainLayerDomainLayer.Models 5      9!clr-namespace:UI_Layer.ConvertersUI_Layer.Converters  5
          UI_Layer.Views.AddView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:UI_Layer.Views     5      8
viewModels!clr-namespace:UI_Layer.ViewModels     5      Hmodels5clr-namespace:DomainLayer.Models;assembly=DomainLayer     5      W SFontAwesome5.Net, Version=2.1.6.0, Culture=neutral, PublicKeyToken=9cfaf01297a008f8.fa%http://schemas.fontawesome.com/icons/  5	      2
converters!clr-namespace:UI_Layer.Converters  5
      ??5      ?? 5   
     ?? ImageSource$.  (/Images/AddView/AddViewBackground_02.jpg??6    ? 	Resources 5      .   (UI_Layer.Converters.StringToIntConverter   stringToIntConvert'   !UI_Layer.Converters.ISBNConverter  ISBN_Converter ?? 
TargetType ?? Property ?? Value ?? 
TargetType ?? CornerRadius ?? Padding ??Name	 =? Errors
 ??Name ?? Padding J? RenderTransformOrigin ?? ScaleX ?? ScaleY ? Triggers :? RoutedEvent ?? TargetProperty W? From	 W? To g? Duration g? AutoReverse ?? 
TargetName ?? Triggers P? Property P? Value ?? 
TargetName??%?  &	        &	      '?? &       5   
   6*     5   
   6#   ?? 5   
   " ??6   .?5      ??  ?@J?SnapsToDevicePixels$
 True??64   ?? 5       ? ??$ 0 0 0 5q?6'   ?? 5       ?@??
CaretBrush
 ?????6+   ?? 5       ? ??
 ?????6+   ?? 5       ? ??$ Center=?69   ?? 5       ? ??$ Center=?62   ?? 5       ?@=?ErrorTemplate 5      ?? 5      .R?5       ?? .+?5!      ?? $??Center=?68   5"      5#      5%      ?? 5&       ? ?? 5'      ?? 5(      " ??6&   .R?5)      ?? .+?5*      ?? $??Border??5+   #   $ 2??5,   #   $ 2q?5-   #   
?????? ???#888888$??1q?50   #   ._?51   "   ?? $ PART_ContentHost??6:   $??0q?6/   6:   52      ?? 53      #??z	 6+   ??54   "   ?? 55   &   .R?56   *   ?? $
 errorTxtBlk??64   
???????$??10(?6z   $??0 5 0 5q?6?   ???	#a1fc0000$ 2q?57   K   $ 0.5 0.5&?6W   ??56   \   ?? *ErrorContent+z?58   .   ?? 59   2   txtBlkScale??$ 1Q?6V   $ 1Q?6a    5;   .   :? 5<   2   $ Loaded??6?   .[?5=   6   ?? .??5>   :   ?? .??5?   >   W? $ Opacity?6N    05@   N    15A   N   $ 0:0:2D?5B   N   $ False??5C   N   W? 5D   >   $ txtBlkScale??6N   $ (ScaleTransform.ScaleX)?5E   N    15F   N    1.055G   N   $ 0:0:0.3D?5H   N   $
 True??5I   N   W? 5J   >   $ txtBlkScale??6N   $ (ScaleTransform.ScaleY)?5K   N    25L   N   $ 0:0:0.3D?5M   N   $
 True??5N   N   5O   ;   5P   7   5Q   3   5S   +   5T   '   5V      5W       5X      P? 5Y       ?@J?IsMouseOver$
 True??6=   . ?5Z   "   ?? $ Border??6N    ? ??
 ?????5[      5]      5_      5`      5a      .??5b      ?  ? ColumnDefinitions 5c   
   ?? 5d      ?? 5e      ?? 5f       ? RowDefinitions 5h   
   ?? 5i      ?? 5j      ?? 5k      .+?5m   
   ?? $??Cabinet??6   $??0??6$   $??0??61     groupBoxStyle#??[ 6A   .??5n      ?? $??10q?6   .+?5o      ?? $??Cabinet name:??6   $??0 0 0 5q?61   ?? 5p      -   5        $??CabinetTxtBox??5p      ??6/   ?? *AddCabinet.Name+ ?? Mode$ TwoWay=?" ?? ValidatesOnNotifyDataErrors$
 True?? ?? UpdateSourceTrigger$ PropertyChanged=??? 5q      $??10(?6e   $??0 5 0 5q?6s   ???	#A3013d0a$ 2q?5r   3   $ 0.5 0.5&?6?     errorTextBlock#??[ 6_   ??5q      ?? *AddCabinet.Message+$ PropertyChanged=?y?5s      ?? *AddCabinet.ShowMessage+  VisibilityConverter  ?? 	Converter#  [ ! ?? FallbackValue$! 	Collapsed???? 5t      $	??Add??6     AddEditButtons#??[ 6'   ??6O   ?? *AddCabinet.AddCabinetCommand+??6?   ?? " ?? ElementName$" CabinetTxtBox??# ?? Path$
# Text?5u      5v      ?? 5w   
   $??Shelf??6   $??1??6"   $??0??6/   #??[ 6?   .??5x      ?? $??10q?6   .+?5y      ?? $??Cabinet:??6   $??0 0 0 5q?6,   ?? 5z        ComboBoxItemContainerStyle#??[ 6     
MyComboBox#??[ 6]   $??0 0 0 5q?6?   ??5{      ?? *AddCabinet.Cabinets+$ ?? SelectedItem$ 5|      ?? *AddShelf.cabinetViewModel+$ PropertyChanged=???5}      ?? 5~      .R?5      ?? % ?? Orientation$% 
Horizontal=?6)   .+?5?   "   ?? $??Cabinet name: ??6,   ?? 5?   "   ??6,   ?? *CabinetName+& ?? StringFormat$& [{0}]??5?      5?      5?      ?? 5?      $??Shelf number:??6   $??0 0 0 5q?61   ?? 5?      -   5        $??ShelfTextBox??5?      ??6.   ?? *AddShelf.Shelf_Nr+$ TwoWay=?$
 True??$ PropertyChanged=??? 5?      $??10(?6c   $??0 5 0 5q?6q   ???	#A3013d0a$ 2q?5?   3   $ 0.5 0.5&?6?   #??[ 6_   ??5?      ?? *AddShelf.Message+$ PropertyChanged=?y?5?      ?? *AddShelf.ShowMessage+#  [ $! 	Collapsed???? 5?      $	??Add??6   #??[ 6'   ??6O   ?? *AddShelf.AddShelfCommand+??6|   ?? $" ShelfTextBox??$
# Text?5?      5?      ?? 5?   
   $??Category??6   $??2??6%   $??0??62   #??[ 6B   .??5?      ?? $??10q?6   .+?5?      ?? $??Category name:??6   $??0 0 0 2q?62   ?? 5?      -   5        ' ??Name$' CategoryTextBox??5?      ??63   ?? *AddCategory.Category_Name+$ TwoWay=?$
 True??$ PropertyChanged=??? 5?      $??Image:??6   $??0 0 0 2q?6*   ]? 5?      $??0 0 0 5q?6   $
??True??6-   $??Stretch=?6B   .+?5?      ?? $??Right=?6     BrowseButtonStyle#??[ 64   $??Browse??6_   $??5,0,0,0q?6p   $??25??6?   $??12(?6?   $??50??6?   $??Right=?5?   U   $??0q?6q   ??6!   ?? *!AddCategory.GetImagePathCommand+6q   ?? 5?      -   5        $' ImageTextBox??5?      $	??Top=?64   $??0q?6I   ??6T   ?? *AddCategory.ImagePath+$ TwoWay=?$
 True??$ PropertyChanged=?5?      ?? 5?      $??10(?6f   $??0 5 0 5q?6t   ???	#A3013d0a$ 2q?5?   3   $ 0.5 0.5&?6?   #??[ 6_   ??5?      ?? *AddCategory.Message+$ PropertyChanged=?y?5?      ?? *AddCategory.ShowMessage+#  [ $! 	Collapsed???? 5?      #??[ 6   $	??Add??6A   ??6O   ?? * AddCategory.AddCategoryCommand+??6?   ?? $" CategoryTextBox??$
# Text?5?      5?      ?? 5?   
   $
??Book??6   $??0??6!   $??1??6.   $??3??6>   #??[ 6O   .??5?      ?? $??10q?6   .+?5?      ?? $??Title:??6   $??0 0 0 2q?6*   ?? 5?      ??6   ?? *AddBook.Title+$ TwoWay=?$ PropertyChanged=?$
 True???? 5?      $??ISBN_10:??6   $??0 0 0 2q?6,   ?? 5?      ??6   ?? *AddBook.ISBN_10+$ TwoWay=?$ PropertyChanged=?$
 True??#  [ ?? 5?      $??ISBN_13:??6   $??0 0 0 2q?6,   ?? 5?      ??6   ?? *AddBook.ISBN_13+$ TwoWay=?$ PropertyChanged=?$
 True??#  [ ?? 5?      $??Image:??6   $??0 0 0 2q?6*   ]? 5?      $??0 0 0 5q?6   $
??True??6-   $??Stretch=?6B   .+?5?      ?? $??Right=?6   #??[ 64   $??Browse??6_   $??5,0,0,0q?6p   $??25??6?   $??12(?6?   $??50??6?   $??Right=?5?   "   $??0q?6>   ??6R   ?? *AddBook.GetImagePathCommand+?? 5?      -   5        $' BookImageTextBox??5?      $	??Top=?68   $??0q?6M   ??6X   ?? *AddBook.ImagePath+$ PropertyChanged=?5?      ?? 5?      $??Page count:??6   $??0 0 0 2q?6/   ?? 5?      ??6   ?? *AddBook.PageCount+$ TwoWay=?$ PropertyChanged=?$
 True??#  [  ?? 5?      $??
Publisher:??6   $??0 0 0 2q?6.   ?? 5?      ??6   ?? *AddBook.Publisher+$ TwoWay=?$ PropertyChanged=?$
 True???? 5?      $??
Published:??6   $??0 0 0 2q?6.   (  "System.Windows.Controls.DatePicker  5?       	 CalendarStyle1(   CalendarStyle#( [	 6    
 DatePickerStyle1#??[
 6M   )   SelectedDate) 5?      ?? *AddBook.Published+$ TwoWay=?
* ? Tag* 6U   ?? *AddBook.Today+$ OneWay=?$ PropertyChanged=??? 5?      $??Author:??6   $??0 0 0 2q?6+   ?? 5?      ??6   ?? *AddBook.Author+$ TwoWay=?$ PropertyChanged=?$
 True???? 5?      $??	Language:??6   $??0 0 0 2q?6-   ?? 5?      $??0 0 0 5q?6   #??[ 6,   #??[ 6n   ??5?      ?? *AddBook.Languages++ ?? SelectedIndex+ 6E   ?? *AddBook.ComboBoxIndex+$ OneTime=?$ 5?      ?? *AddBook.Language+$ OneWayToSource=?$! English???? 5?      $??Description:??6   $??0 0 0 2q?60   ]? 5?      $??0 0 0 5q?6   $
??True??6-   $??Stretch=?6B   .+?5?      ?? $??Right=?6   #??[ 64   $??Browse??6_   $??5,0,0,0q?6p   $??25??5?      $??12(?6)   $??50??67   $??Right=?6B   $??0q?6^   ??5?      ?? *#!AddBook.GetDescriptionPathCommand+?? 5?      -   5        $' DescriptionTextBox??5?      $	??Top=?6:   $??0q?6O   ??6Z   ?? *AddBook.Description+$ TwoWay=?$ PropertyChanged=?$
 True??5?      ?? 5?      $??	Category:??6   $??0 0 0 5q?6-   ?? 5?      $??0 0 0 5q?6   #??[ 6,   #??[ 6n   , ?? DisplayMemberPath$, CategoryName??5?   W   ??6   ?? *AddCategory.Categories+$ OneWay=?$ 6x   ?? *AddBook.Category++ 5?      ?? *AddBook.ComboBoxIndex+$ OneTime=??? 5?      $??Cabinet:??6   $??0 0 0 5q?6,   ?? 5?      $??0 0 0 5q?6   #??[ 6,   #??[ 6n   $, CabinetName??5?   G   $- ?? IsSynchronizedWithCurrentItem$
- TrueY?6g   ??6   ?? *AddCabinet.Cabinets+6g   ?? 5?      $??Shelf:??6   $??0 0 0 5q?6*   ?? 5?      $??0 0 0 5q?6   #??[ 6,   #??[ 6n   $, ShelfNr??5?   O   $
- TrueY?6k   ??6   ?? *AddCabinet.Cabinets/Shelves+$ 5?      ?? *AddBook.Shelf++ 6B   ?? *AddBook.ComboBoxIndex+$ OneTime=??? 5?      $??10(?6b   $??0 5 0 5q?6p   ???	#A3013d0a$ 2q?5?   3   $ 0.5 0.5&?6?   #??[ 6_   ??5?      ?? *AddBook.Message+$ PropertyChanged=?y?5?      ?? *AddBook.ShowMessage+#  [ $! 	Collapsed???? 5?      $	??Add??6   #??[ 6'   $??Center=?6O   $	??100??6l   $??value??5?   D   ??6   ?? *AddBook.AddBookCommand+6D   5?      5?      ?? 5?   
   $
??Copy??6   $??0??6!   $??2??6.   #??[ 6>   .??5?      ?? $??10q?6   .+?5?      ?? $
??Book??6   $??0 0 0 5q?6(   ?? 5?      $??0 0 0 5q?6   $, Title??6T   #??[ 5?      #??[ 6^   ??5?   .   ?? *AddBook.Books+$ 5?      ?? *AddCopy.Book+$ PropertyChanged=?+ 5?      ?? *AddCopy.ComboBoxIndex+$ OneTime=?5?   ^   ?? 5?      $??State??6   $??0 0 0 5q?6)   ?? 5?      $??0 0 0 5q?6   #??[ 5?      #??[ 6]   ??5?   ,   ?? *AddCopy.States+$
! Good??$ 5?      ?? *AddCopy.State+$ PropertyChanged=?+ 5?      ?? *AddCopy.ComboBoxIndex+$ OneTime=?5?   ]   ?? 5?      $	??Add??6   #??[ 6'   $??value??5?      ??5?   O   ?? *AddCopy.AddCopyCommand+5?      5?      5?      ?? 5?   
   $??Employee??6   $??1??6%   $??2??62   $??2??6B   $	??340??6S   $	??Top=?6`   #??[ 5?      .??5?      ?? $??10q?6   .+?5?      ?? $??First name:??6   $??0 0 0 5q?6/   ?? 5?      -   5        $' FNtxtBox??5?      ??6,   ?? *AddEmployee.FirstName+$ TwoWay=?$ PropertyChanged=?$
 True???? 5?      $??
Last name:??6   $??0 0 0 5q?6.   ?? 5?      ??6   ?? *AddEmployee.LastName+$ TwoWay=?$ PropertyChanged=?$
 True???? 5?      $??
Birthdate:??6   $??0 0 0 5q?6.     5?      #( [	 6   #??[
 6M   $??0 0 0 10q?5     ) 5      ?? *AddEmployee.Birthdate+$ TwoWay=?$
 True??$ PropertyChanged=?* 5     ?? *AddEmployee.Today+$ OneWay=?$ PropertyChanged=??? 5     $??10(?6f   $??0 5 0 5q?6t   ???	#A3013d0a$ 2q?5  3   $ 0.5 0.5&?6?   #??[ 6_   ??5     ?? *AddEmployee.Message+$ PropertyChanged=?y?5     ?? *AddEmployee.ShowMessage+#  [ $! 	Collapsed???? 5     $	??Add??6   #??[ 6'   ??6O   ?? * AddEmployee.AddEmployeeCommand+??5     ?? $" FNtxtBox??$
# Text?5     5	     5
     5     