   M S B A M L   `   `   `  ����Y  Unmct.ba.cashlessproject.tempui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:nmct.ba.cashlessproject.tempui.Viewmodel(nmct.ba.cashlessproject.tempui.Viewmodel  5      ?    9nmct.ba.cashlessproject.tempui.View.Management.Statistics   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Evm6clr-namespace:nmct.ba.cashlessproject.tempui.Viewmodel     5        � DataContext  5	      ;  5nmct.ba.cashlessproject.tempui.Viewmodel.StatisticsVM  5
   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��150 �6   6-   �� 5      $	��150 �6   5      �� 5      $	��450 �6   6-   �� 5      $	��450 �6   6-    � RowDefinitions 5   
   �� 5      $	��150 �6   6+   �� 5      $��50 �6   6*   �� 5      $��50 �6   6*   �� 5      $	��200 �6   6+   �� 5      $��64 �6   6*   .+�5   
   �� $
��From��6   $��1��6   $��0��6,   $��Center=�6<   $��Center=�6Y   �� 5    
   $
��Till��6   $��2��6   $��0��6,   $��Center=�6<   $��Center=�6Y   (  "System.Windows.Controls.DatePicker  5!   
   $��1��6   $��1��6%   $��Center=�62   $��Center=�6O      SelectedDate 6j   �� *From+  5"   
   $��1��6   $��2��6%   $��Center=�62   $��Center=�6O    6j   �� *Until+�� 5#   
   $��Total income��6   $��0��6'   $��4��67   $��Center=�6D   $��Center=�6a   �� 5$   
   $��1��6+   $��4��6;   $��Center=�6H   $��Center=�6e   ��6   �� *Total+6e   �� 5%   
   $$��total income per cash register��6   $��2��6:   $��4��6J   $��Center=�6W   $��Center=�6t   �� 5&   
   $��total income per product��6   $��3��64   $��4��6D   $��Center=�6Q   $��Center=�6n   �� 5(   
   $��2��6   $��0��6#   $��4��60   $��8q�6A   ��6L   �� *	Registers+ �� SelectedItem 6n   �� *SelectedRegister+ �� View 5)      �� 5*      .P�5+      �� $��Cashregister��6%    �� Width$	 430��6;    �� DisplayMemberBinding 6G   �� *RegisterName+6w   5,      5.      �� 50   
   $��3��6   $��0��6#   $��4��60   $��8q�6A   ��6M   �� *
Products+ 6n   �� *SelectedProduct+ 51      �� 52      .P�53      �� $��Product��6%   $	 430��66    6B   �� *ProductName+6q   54      56      �� 57   
   $��	Calculate��6   $
��Left=�6%   $��	40,10,0,0q�6@   $��3��6S   $	��Top=�6`   $��75��6x   ��6�   �� *SearchStatsCommand+�� 58   
   $��Export��6   $��1��6"   $
��Left=�62   $��	42,10,0,0q�6M   $��3��6`   $	��Top=�6�   $��75��6�    J� RenderTransformOrigin$ -0.133,0.85&�6�   ��6m   �� *ExportToExcelCommand+6�   5>      5?      