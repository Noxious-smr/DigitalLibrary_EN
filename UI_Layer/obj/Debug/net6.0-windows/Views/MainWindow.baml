   M S B A M L   `   `   `  ????C  ?UI_Layer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:UI_Layer.ViewsUI_Layer.Views  5   	       UI_Layer.Views.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:UI_Layer.Views     5   	   W SFontAwesome5.Net, Version=2.1.6.0, Culture=neutral, PublicKeyToken=9cfaf01297a008f8.fa%http://schemas.fontawesome.com/icons/  5   	     ? Title$  
MainWindow??5	   	   $	??920??6   $
??1280??6)   $	??920??66   $
??1320??6F    ? WindowStyle$
 None=?6V    ? AllowsTransparency$
 True??5
   	   
??????  ? 
ResizeMode$ NoResize=?6<    ? WindowStartupLocation$ CenterScreen=?6R    ? 	Resources 5       ?? 
TargetType ?? Triggers :? RoutedEvent	 ?? TargetProperty
 g? Duration	 W? To g? 	BeginTime??%?  '??       ?? 5   
   " ??6    5      :? 5      $ ContentControl.MouseEnter??6   [?5      ?? 5      .??5      ?? .??5   "   W? $	 Opacity?62   $
 0:0:0.5D?6V    16i   5      5      5      :? 5      $ ContentControl.MouseLeave??6   [?5      ?? 5      .??5      ?? .??5   "   W? 	 0:1:262   $	 Opacity?6D   $
 0:0:1D?6h    0.36y   5      5      5      53      54      .??55      ? ??56   
   ?? 57       ?? ImageSource$. (/Images/MainWindow/HomeBackground_02.jpg??6    ? RowDefinitions 59   
   ?? 5:      $??50 ?6   ?? 5;      $??* ?6    ? ColumnDefinitions 5=   
   ?? 5>      $
??auto ?6   ?? 5?      -   5         ??Name$ ContentColumn??5?      $??* ?66   .+?5A   
   ?? -   5        $??0??5A      $??2??6"   $??0??66   $??0 5 5 0q?6C   $??Stretch=?5B      
???????$??0 0 0 2q?6^   
?????? ._?5C      ?  5D      ?? 5E      $??* ?6'   ?? 5F      $
??auto ?6'   ?? 5G      $
??auto ?6'   .+?5I      ?? $??Library Management System??6   
???????$??28(?6P   $??Center=?6^   $??Center=?6{   $
??Bold"?6?   ?? 5J      -   5        $??MinimizeButton??5J      $??1??6/   $??0q?6?   $??40??6J   $??35??6U   $??Center=?5K      ???	#00401d13   HomeCloseMinimizeButtons#??[  5L      ??5M        FontAwesome5.FontAwesome  5N         Icon$ Regular_WindowMinimize=?6)   $??20(?6G   
???????6U   5P      ?? 5Q      -   5        $??CloseButton??5Q      $??2??6,   $??0q?6<   $??40??6G   $??35??6R   $??Center=?5R      ???	#00401d13#??[  5S      ??5T        5U      $ Regular_WindowClose=?6)   $??30(?6D   
???????6R   5W      5X      5Y      ?? 5Z   
   -   5         ??Name$ border??5Z      $??1??6!   
?????? $
??Left=?6G   $??Stretch=?6b   $??5 -6 5 0q?6~   $	??150??6?   ._?5[      A?  A? Rows$ 5??6   .+?5\      ?? $??0q?6   
???????$??2q?68   $	??130??6L     SidebarButtonsStyle#??[ 6X   ??5]      ??  ?? RelativeSource ??  ?? Mode$
 Self=? ?? Path$ Width???6b   ?? *NavigateHomeCommand+??5^      ?? 5_      $$ /Images/MainWindow/Home_03.png??6%   5a      ?? 5b      $	??130??6   #??[ 6&   ??5c      ??  ?? $
 Self=?$ Width???6b   ?? *NavigateCategoriesCommand+??5d      ?? 5e      $+ %/Images/MainWindow/Categories_008.png??6%   5g      ?? 5h      #??[ 6@   $	??130??5i      ??5h      ?? *NavigateAddCommand+??5i   &   ??  ?? $
 Self=?$ Width???5j      ?? 5k      $&  /Images/MainWindow/AddButton.png??6%    j? Viewport$ 0.08,0.1 0.85,0.85?6T   5m      ?? 5n      #??[ 6C   $	??130??5o      ??5n      ?? *NavigateUpdateCommand+??5o   %   ??  ?? $
 Self=?$ Width???5p      ?? 5q      $' !/Images/MainWindow/EditButton.png??6%   $ 0.2,0.2 0.65,0.65?6U   5s      ?? 5t      #??[ 6F   $	??130??5u      ??5t      ?? *NavigateDashboardCommand+??5u   %   ??  ?? $
 Self=?$ Width???5v      ?? 5w      $) #/Images/MainWindow/ReportButton.png??6%   $ -0.05,0.1 1,0.8?6W   5y      5z      5{      ?? 5|   
   -   5        $??	myContent??5|   j   $??1??6   $??1??6&   $??
5 15 10 15q?6V    J? Opacity$ 1Q?6{   ??66   ?? *CurrentView+6{   5~      5      